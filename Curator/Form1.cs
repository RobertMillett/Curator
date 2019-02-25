using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using VDFParser.Models;
using System.IO;
using System.Text;
using System.Reflection;
using Crc32;
using System.ComponentModel;

namespace Curator
{
    public partial class Form1 : MetroForm
    {
        public static string ShortcutsPath;
        public static string DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "XmlDoc.xml");
        public static CuratorDataSet.ConsoleRow ActiveConsole;
        public static bool PromptSave;

        public Form1()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Properties.Settings.Default.ShortcutsPath))
            {
                ShortcutsPath = Properties.Settings.Default.ShortcutsPath;
                this.Text = $"Curator - {ShortcutsPath}";
            }

            if (!File.Exists(DataPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath));
                CuratorDataSet.WriteXml(DataPath);
            }

            CuratorDataSet.ReadXml(DataPath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.StyleManager = metroStyleManager1;
            //metroStyleManager1.Theme = MetroThemeStyle.Dark;
            Shown += FirstTimeGetSteamShortcutsFile;
            FormClosing += OnFormClosing;
            Resize += OnFormResized;

            CuratorDataSet.Console.RowChanged += PromptSaveTrue;
            CuratorDataSet.RomFolder.RowChanged += PromptSaveTrue;
            CuratorDataSet.ROM.RowChanged += PromptSaveTrue;            

            comboBox1.SelectedIndexChanged += ConsoleHasChanged;

            ConsoleHasChanged(sender, e);

            romListView.Columns[0].Width = romListView.Width - 24;

            romListView.ItemCheck += RomEnabled;
        }

        private void PromptSaveTrue(object sender, DataRowChangeEventArgs e)
        {
            PromptSave = true;            
            this.consoleBindingSource2.ResetBindings(false);
        }

        private void RomEnabled(object sender, ItemCheckEventArgs e)
        {            
            var rom = CuratorDataSet.ROM.Where(x => RomNameConstructor(x) == romListView.Items[e.Index].Text).First();
            rom.Enabled = e.NewValue == CheckState.Checked;
        }

        private string RomNameConstructor(CuratorDataSet.ROMRow romItem)
        {
            return $"{romItem.Name} ({romItem.Extension.Trim('.').ToUpper()})";
        }

        private void OnFormResized(object sender, EventArgs e)
        {
            romListView.Columns[0].Width = romListView.Width - 24;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {            
            comboBox1.SelectedIndexChanged -= ConsoleHasChanged;

            if (e.CloseReason != CloseReason.UserClosing)
                return;

            if (!PromptSave)
                return;

            var saveChanges = MetroMessageBox.Show(this, "Save changes?", "", MessageBoxButtons.OKCancel);

            if (saveChanges == DialogResult.OK)
            {
                //New DB code
                CuratorDataSet.WriteXml(DataPath);
            }
        }

        private void ExportToSteam()
        {
            // Step 1 - Read current Shortcuts file
            var currentShortcuts = new List<VDFParser.Models.VDFEntry>();
            try
            {
                currentShortcuts = VDFParser.VDFParser.Parse(ShortcutsPath).ToList();
            }
            catch (VDFParser.VDFTooShortException)
            {

            }

            //Step 2 - Add the ROMS to the list
            foreach (var rom in CuratorDataSet.ROM.ToList())
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

                var RomFolder = CuratorDataSet.RomFolder.Where(x => x.Id == rom.RomFolder_Id).First();
                var console = CuratorDataSet.Console.Where(y => y.Id == RomFolder.Console_Id).First();

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
            File.WriteAllBytes(ShortcutsPath, serialisedShortcuts);
        }

        private void ConsoleHasChanged(object sender, EventArgs e)
        {   
            SetActiveConsole(sender, e);
            UpdateConsoleDetailsView(sender, e);
            //GetRoms();
        }

        private void UpdateConsoleDetailsView(object sender, EventArgs e)
        {
            if (ActiveConsole != null)
            {
                systemDetailsName.Text = ActiveConsole.Name;
                emulatorPathTextBox.Text = ActiveConsole.EmulatorPath;
                emulatorArgsTextBox.Text = ActiveConsole.EmulatorArgs;
                romArgsTextBox.Text = ActiveConsole.RomArgs;

                UpdateConsoleDetailsWithRomFolders();
            }
        }

        private void SetActiveConsole(object sender, EventArgs e)
        {
            ActiveConsole = comboBox1.Text == string.Empty ? null : CuratorDataSet.Console.Where(x => x.Name == comboBox1.Text).First();
        }

        private void FirstTimeGetSteamShortcutsFile(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ShortcutsPath))
                return;

            var welcomeBox = MetroMessageBox.Show(this, "Please open your Steam Shortcuts .vdf file", "Welcome to Curator", MessageBoxButtons.OKCancel);

            if (welcomeBox == DialogResult.Cancel)
            {
                Application.Exit();
            }

            var shortcutIsSet = SetSteamShortcutFile();

            if (!shortcutIsSet)
                FirstTimeGetSteamShortcutsFile(this, e);
            
        }

        private bool SetSteamShortcutFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default["ShortcutsPath"] = openFileDialog1.FileName;
                Properties.Settings.Default.Save();

                ShortcutsPath = openFileDialog1.FileName;

                this.Text = $"Curator - {ShortcutsPath}";
                this.Refresh();
                return true;
            }

            return false;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void consoleListButtonContainer_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void consoleBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.consoleBindingSource.EndEdit();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            if (!CuratorDataSet.Tables["console"].Select().Where(x => x["name"].ToString() == comboBox1.Text).Any())
            {
                CuratorDataSet.Console.Rows.Add(null, comboBox1.Text);                
            }

            if (!CuratorDataSet.Console.Where(x => x.Name == comboBox1.Text).Any())
            {
                CuratorDataSet.Console.Rows.Add(null, comboBox1.Text);
            }

            //This will always set to the most recently added item as it is added to the bottom of the list.
            comboBox1.SelectedIndex = comboBox1.Items.Count -1;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {   
            if (MetroMessageBox.Show(this, "This will delete the console and all of it's associated data!", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var RomFolderRows = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);
                foreach (var row in RomFolderRows)
                {
                    CuratorDataSet.RomFolder.Rows.Remove(row);
                }                

                CuratorDataSet.Console.Rows.Remove(ActiveConsole);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (emulatorPathFileDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveConsole.EmulatorPath = emulatorPathFileDialog.FileName;

                //CuratorDataSet.Console.Where(x => x.Name == ActiveConsole.Name).First().EmulatorPath = emulatorPathFileDialog.FileName;
                this.Refresh();

                emulatorPathTextBox.Text = emulatorPathFileDialog.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emulatorArgsTextBox_Leave(object sender, EventArgs e)
        {
            ActiveConsole.EmulatorArgs = emulatorArgsTextBox.Text;
        }

        private void romArgsTextBox_Leave(object sender, EventArgs e)
        {
            ActiveConsole.RomArgs = romArgsTextBox.Text;
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (romFolderDialog.ShowDialog() == DialogResult.OK)
            {
                CuratorDataSet.RomFolder.Rows.Add(null, romFolderDialog.SelectedPath, ActiveConsole.Id);
                UpdateConsoleDetailsWithRomFolders();
            }
        }

        private void UpdateConsoleDetailsWithRomFolders()
        {
            romFolderListBox.Items.Clear();

            foreach (var RomFolder in CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id))
            {
                var RomFolderPath = RomFolder.Path;
                if (!romFolderListBox.Items.Contains(RomFolderPath))
                    romFolderListBox.Items.Add(RomFolderPath);
            }

            GetRoms();
        }

        private void metroListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void GetRoms()
        {
            if (ActiveConsole == null)
                return;

            var RomFolders = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);

            foreach (var RomFolder in RomFolders)
            {
                var romList = Directory.GetFiles(RomFolder.Path);

                foreach (var rom in romList)
                {
                    var romName = Path.GetFileNameWithoutExtension(rom);
                    if (!CuratorDataSet.ROM.Where(x => x.Name == romName).Any())
                    {
                        var romRow = CuratorDataSet.ROM.NewROMRow();
                        romRow.Name = romName;
                        romRow.Extension = Path.GetExtension(rom);
                        romRow.RomFolder_Id = RomFolder.Id;
                        romRow.Enabled = true;

                        CuratorDataSet.ROM.Rows.Add(romRow);
                    }                    
                }
            }

            UpdateRomListViewItems();
        }

        private void UpdateRomListViewItems()
        {
            romListView.Items.Clear();

            var RomFolders = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);
            
            foreach (var RomFolder in RomFolders)
            {
                foreach (var romItem in CuratorDataSet.ROM.ToList())
                {
                    if (romItem.RomFolder_Id == RomFolder.Id)
                    {
                        var romListViewItem = new ListViewItem
                        {
                            Text = RomNameConstructor(romItem),
                            Checked = romItem.Enabled
                        };

                        romListView.Items.Add(romListViewItem);
                    }
                }
            }         
        }

        private void SetShortcutsvdfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSteamShortcutFile();
        }

        private void exportToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Override current Shortcuts file?", "Export to Steam", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ExportToSteam();
            }
        }
    }
}
