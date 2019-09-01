using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDFParser.Models;
using System.IO;
using Microsoft.Win32;
using Gameloop.Vdf;
using Gameloop.Vdf.JsonConverter;
using SteamIDs_Engine;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Curator.Data.SteamDb;
using System;

namespace Curator.Data
{
    public class SteamController
    {
        public CuratorDataSet CuratorData;
        public string SteamShortcutsFile;
        private const string BackupsFolder = "Curator Backups";

        public SteamController(CuratorDataSet curatorData)
        {
            CuratorData = curatorData;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ShortcutsPath))
            {
                SteamShortcutsFile = Properties.Settings.Default.ShortcutsPath;
            }
        }

        public void ExportToSteam()
        {
            // Step 1 - Read current Shortcuts file
            var currentShortcuts = ParseShortCuts();

            //Step 2 - Add the ROMS to the list
            foreach (var rom in CuratorData.ROM.ToList())
            {
                var existingRomEntry = currentShortcuts.Where(x => x.AppName == rom.Name && x.Exe.Contains(rom.Extension)).FirstOrDefault();

                if (rom.Enabled == false)
                {
                    if (existingRomEntry != null)
                    {
                        currentShortcuts.Remove(existingRomEntry);
                    }
                    continue;
                }

                var RomFolder = CuratorData.RomFolder.Where(x => x.Id == rom.RomFolder_Id).First();
                var console = CuratorData.Console.Where(y => y.Id == RomFolder.Console_Id).First();

                var exepath = GetExePath(rom, console);

                var newRomEntry = new VDFEntry
                {
                    AllowDesktopConfig = 1,
                    AppName = rom.Name,
                    Exe = exepath,
                    StartDir = Path.GetDirectoryName(console.EmulatorPath),
                    Index = 0,
                    Icon = "",
                    IsHidden = 0,
                    OpenVR = 0,
                    ShortcutPath = "",
                    Tags = new string[] { console.Name }
                };

                if (!string.IsNullOrWhiteSpace(rom.GridPicture))
                    UpdateRomGridImage(newRomEntry, rom.GridPicture);

                if (existingRomEntry != null)
                {
                    currentShortcuts[currentShortcuts.IndexOf(existingRomEntry)] = newRomEntry;
                }
                else
                {
                    currentShortcuts.Add(newRomEntry);
                }
            }

            WriteOut(currentShortcuts);
        }

        public string GetExePath(CuratorDataSet.ROMRow rom, CuratorDataSet.ConsoleRow console)
        {
            var romArgs = rom.OverrideArgs ? rom.CustomArgs : $"{console.RomArgs} {rom.CustomArgs}";

            var exepath = $"\"{console.EmulatorPath}\" {console.EmulatorArgs} \"{rom.FileName}\"";

            if (!string.IsNullOrWhiteSpace(romArgs))
                exepath += $" {romArgs}";

            return exepath;
        }

        internal string TryFindAndSetShortcutsAutomatically()
        {
            var accountName = string.Empty;

            var steamInstallPath = Registry.LocalMachine.OpenSubKey(@"Software\Valve\Steam").GetValue("InstallPath").ToString();

            if (File.Exists(Path.Combine(steamInstallPath, "Steam.exe")))
            {
                var loginUsersPath = Path.Combine(steamInstallPath, "config", "loginusers.vdf");

                var loggedInUsers = VdfConvert.Deserialize(File.ReadAllText(loginUsersPath))
                    .Value
                    .ToJson()
                    .ToObject<Dictionary<string, SteamUser>>();

                var mostRecentUserId = loggedInUsers.Keys.First(x => loggedInUsers[x].MostRecent == 1);

                var steam32Id = SteamIDConvert.Steam64ToSteam32(long.Parse(mostRecentUserId));

                var steamId = steam32Id.Substring(steam32Id.Length - 8);

                var userFolders = Directory.GetDirectories(Path.Combine(steamInstallPath, "userdata"));

                foreach (var folder in userFolders)
                {
                    var directory = Path.Combine(folder, "config");
                    CreateRequiredFilesFoldersIfNotExist(directory);
                }

                if (userFolders.Any(x => x.Contains(steamId)))
                {
                    var shortcutFilePath = Path.Combine(steamInstallPath, "userdata", steamId, "config", "shortcuts.vdf");
                    if (File.Exists(shortcutFilePath))
                    {
                        SetSteamShortcutFile(shortcutFilePath);
                        accountName = loggedInUsers[mostRecentUserId].AccountName;                        
                    }
                }
            }

            return accountName;
        }

        private void CreateRequiredFilesFoldersIfNotExist(string steamUserFolder)
        {
            var shortcutsFile = Path.Combine(steamUserFolder, "shortcuts.vdf");
            var gridsFolder = Path.Combine(steamUserFolder, "grid");

            if (!File.Exists(Path.Combine(steamUserFolder, "shortcuts.vdf")))
                File.Create(shortcutsFile);

            if (!Directory.Exists(gridsFolder))
                Directory.CreateDirectory(gridsFolder);
        }

        private List<VDFEntry> ParseShortCuts()
        {
            var currentShortcuts = new List<VDFEntry>();
            try
            {
                currentShortcuts = VDFParser.VDFParser.Parse(SteamShortcutsFile).ToList();
            }
            catch (VDFParser.VDFTooShortException)
            {

            }

            return currentShortcuts;
        }

        public bool ShortcutsContainConsole(string consoleName)
        {
            var shortcuts = ParseShortCuts();
            return shortcuts.Any(x => x.Tags.Contains(consoleName));
        }

        private void WriteOut(List<VDFEntry> currentShortcuts)
        {
            //Step 2.5 fix indexes
            for (int i = 0; i < currentShortcuts.Count; i++)
            {
                currentShortcuts[i].Index = i;
            }

            //Step 3 - Serialise
            var serialisedShortcuts = VDFParser.VDFSerializer.Serialize(currentShortcuts.ToArray());

            //Step 4 - Write out
            SaveBackup();
            File.WriteAllBytes(SteamShortcutsFile, serialisedShortcuts);
        }

        private void SaveBackup()
        {
            var backupsDirectory = Path.Combine(Path.GetDirectoryName(SteamShortcutsFile), BackupsFolder);
            Directory.CreateDirectory(backupsDirectory);
            var backupFileName = DateTime.UtcNow.ToString("yyyy-MM-dd_HHmmss") + "__shortcuts.vdf";
            File.Move(SteamShortcutsFile, Path.Combine(backupsDirectory, backupFileName));
        }

        public void DeleteShortcutsByTag(string consoleName)
        {
            var shortcuts = VDFParser.VDFParser.Parse(SteamShortcutsFile).ToList();
            var shortcutsToRemove = new List<VDFEntry>();


            foreach (var shortcut in shortcuts)
            {
                if (shortcut.Tags.Contains(consoleName))
                    shortcutsToRemove.Add(shortcut);
            }

            foreach(var shortcutToRemove in shortcutsToRemove)
            {
                shortcuts.Remove(shortcutToRemove);
            }

            WriteOut(shortcuts);
        }

        private void UpdateRomGridImage(VDFEntry rom, string gridPicturePath)
        {  
            // ############# GENERATE APP ID ##################

            var stringValue = $"{rom.Exe + rom.AppName}";
            var byteArray = Encoding.ASCII.GetBytes(stringValue);

            var thing = Crc32.Crc32Algorithm.Compute(byteArray);
            var longThing = (ulong)thing;
            longThing = (longThing | 0x80000000);
            longThing = longThing << 32;
            longThing = (longThing | 0x02000000);
            var finalConversion = longThing.ToString();

            // ############# GENERATE APP ID ##################

            var extension = Path.GetExtension(gridPicturePath);
            var imagesFolder = Path.Combine(Path.GetDirectoryName(SteamShortcutsFile), "grid");
            var steamGridImageFilePath = Path.Combine(imagesFolder, finalConversion + extension);

            if (File.Exists(steamGridImageFilePath))
                File.Delete(steamGridImageFilePath);

            File.Copy(gridPicturePath, steamGridImageFilePath);
        }

        public void SetSteamShortcutFile(string fileName)
        {
            Properties.Settings.Default["ShortcutsPath"] = fileName;
            Properties.Settings.Default.Save();
            SteamShortcutsFile = fileName;
        }
    }
}
