﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using VDFParser.Models;
using System.IO;
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
            var currentShortcuts = new List<VDFParser.Models.VDFEntry>();
            try
            {
                currentShortcuts = VDFParser.VDFParser.Parse(SteamShortcutsFile).ToList();
            }
            catch (VDFParser.VDFTooShortException)
            {

            }

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

                var exepath = $"\"{console.EmulatorPath}\" {console.EmulatorArgs} \"{RomFolder.Path}\\{rom.Name + rom.Extension}\"";

                if (!string.IsNullOrEmpty(console.RomArgs))
                    exepath = exepath + " " + console.RomArgs;

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

                // ############# GENERATE APP ID ##################

                var stringValue = $"{newRomEntry.Exe + newRomEntry.AppName}";
                var byteArray = Encoding.ASCII.GetBytes(stringValue);

                var thing = Crc32.Crc32Algorithm.Compute(byteArray);
                var longThing = (ulong)thing;
                longThing = (longThing | 0x80000000);
                longThing = longThing << 32;
                longThing = (longThing | 0x02000000);
                var finalConversion = longThing.ToString();

                // ############# GENERATE APP ID ##################

                if (existingRomEntry != null)
                {
                    currentShortcuts[currentShortcuts.IndexOf(existingRomEntry)] = newRomEntry;
                }
                else
                {
                    currentShortcuts.Add(newRomEntry);
                }
            }

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

        public void SetSteamShortcutFile(string fileName)
        {
            Properties.Settings.Default["ShortcutsPath"] = fileName;
            Properties.Settings.Default.Save();
        }
    }
}
