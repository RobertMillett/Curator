using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDFParser.Models;
using System.IO;
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

                var romArgs = rom.OverrideArgs ? rom.CustomArgs : console.RomArgs += $" {rom.CustomArgs}";

                var exepath = $"\"{console.EmulatorPath}\" {console.EmulatorArgs} \"{rom.FileName}";

                if (!string.IsNullOrWhiteSpace(console.RomArgs))
                    exepath += $" {console.RomArgs}";
                exepath += "\"";

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
            File.WriteAllBytes(SteamShortcutsFile, serialisedShortcuts);
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
