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
using Crc32;

namespace Curator
{
    public partial class Form1 : MetroForm
    {
        public static string ShortcutsPath;
        public static Console ActiveConsoleTest;
        public static SampleDataSet.consoleRow ActiveConsole;
        
        public Form1()
        {
            InitializeComponent();
            ShortcutsPath = Properties.Settings.Default.ShortcutsPath ?? "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sampleDataSet.rom' table. You can move, or remove it, as needed.
            this.romTableAdapter.Fill(this.sampleDataSet.rom);
            // TODO: This line of code loads data into the 'sampleDataSet.rom' table. You can move, or remove it, as needed.
            this.romTableAdapter.Fill(this.sampleDataSet.rom);
            this.romfolderTableAdapter.Fill(this.sampleDataSet.romfolder);          
            this.consoleTableAdapter.Fill(this.sampleDataSet.console);

            //this.StyleManager = metroStyleManager1;
            //metroStyleManager1.Theme = MetroThemeStyle.Dark;
            Shown += GetSteamShortcutsFile;
            FormClosing += OnFormClosing;
            Resize += OnFormResized;
            
            comboBox1.SelectedIndexChanged += ConsoleHasChanged;

            ConsoleHasChanged(sender, e);

            romListView.Columns[0].Width = romListView.Width - 24;

            romListView.ItemCheck += RomEnabled;
        }

        private void RomEnabled(object sender, ItemCheckEventArgs e)
        {
            var rom = sampleDataSet.rom.Where(x => RomNameConstructor(x) == romListView.Items[e.Index].Text).First();
            rom.enabled = e.NewValue == CheckState.Checked;
        }

        private string RomNameConstructor(SampleDataSet.romRow romItem)
        {
            return $"{romItem.name} ({romItem.extension.Trim('.').ToUpper()})";
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

            var saveChanges = MetroMessageBox.Show(this, "Save changes?", "", MessageBoxButtons.OKCancel);

            if (saveChanges == DialogResult.OK)
            {
                consoleTableAdapter.Update(this.sampleDataSet.console);
                romfolderTableAdapter.Update(this.sampleDataSet.romfolder);
                romTableAdapter.Update(this.sampleDataSet.rom);
            }

            ExportToSteam();
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
            foreach (var rom in romTableAdapter.GetData().ToList())
            {
                var existingRomEntry = currentShortcuts.Where(x => x.AppName == rom.name && x.Exe.Contains(rom.extension)).FirstOrDefault();

                if (rom.enabled == false)
                {
                    if (existingRomEntry != null)
                    {
                        currentShortcuts.Remove(existingRomEntry);
                    }                    
                    continue;
                }

                var romFolder = romfolderTableAdapter.GetData().Where(x => x.romfolder_id == rom.romfolder_id).First();
                var console = consoleTableAdapter.GetData().Where(y => y.console_Id == romFolder.console_id).First();

                var exepath = $"\"{console.emulator_path}\" {console.emulator_args} \"{romFolder.path}\\{rom.name + rom.extension}\"";

                if (!string.IsNullOrEmpty(console.rom_args))
                    exepath = exepath + " " + console.rom_args;

                var newRomEntry = new VDFEntry
                {
                    AllowDesktopConfig = 1,
                    AppName = rom.name,
                    Exe = exepath,
                    StartDir = Path.GetDirectoryName(console.emulator_path),
                    Index = 0,
                    Icon = "",
                    IsHidden = 0,
                    OpenVR = 0,
                    ShortcutPath = "",
                    Tags = new string[] { console.name }
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
                systemDetailsName.Text = ActiveConsole.name;
                emulatorPathTextBox.Text = ActiveConsole.emulator_path;
                emulatorArgsTextBox.Text = ActiveConsole.emulator_args;
                romArgsTextBox.Text = ActiveConsole.rom_args;

                UpdateConsoleDetailsWithRomFolders();
            }
        }

        private void SetActiveConsole(object sender, EventArgs e)
        {
            ActiveConsole = comboBox1.Text == string.Empty ? null : sampleDataSet.console.Where(x => x.name == comboBox1.Text).First();
        }

        private void GetSteamShortcutsFile(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ShortcutsPath))
            {
                var welcomeBox = MetroMessageBox.Show(this, "Please open your Steam Shortcuts .vdf file", "Welcome to Curator", MessageBoxButtons.OKCancel);

                if (welcomeBox == DialogResult.Cancel)
                {
                    Application.Exit();
                }

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default["ShortcutsPath"] = openFileDialog1.FileName;
                    Properties.Settings.Default.Save();

                    ShortcutsPath = openFileDialog1.FileName;
                }
                else
                {
                    GetSteamShortcutsFile(this, e);
                }
            }

            this.Text = $"Curator - {ShortcutsPath}";
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
            this.tableAdapterManager.UpdateAll(this.sampleDataSet);

        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            if (!sampleDataSet.Tables["console"].Select().Where(x => x["name"].ToString() == comboBox1.Text).Any())
            {
                sampleDataSet.console.Rows.Add(null, comboBox1.Text);
                consoleTableAdapter.Update(this.sampleDataSet.console);
            }

            //This will always set to the most recently added item as it is added to the bottom of the list.
            comboBox1.SelectedIndex = comboBox1.Items.Count -1;
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {   
            if (MetroMessageBox.Show(this, "This will delete the console and all of it's associated data!", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var romFolderRows = sampleDataSet.romfolder.Where(x => x.console_id == ActiveConsole.console_Id);
                foreach (var row in romFolderRows)
                {
                    sampleDataSet.romfolder.Rows.Remove(row);
                }

                consoleTableAdapter.Delete(ActiveConsole.console_Id);

                sampleDataSet.console.Rows.Remove(ActiveConsole);
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (emulatorPathFileDialog.ShowDialog() == DialogResult.OK)
            {
                ActiveConsole.emulator_path = emulatorPathFileDialog.FileName;

                sampleDataSet.console.Where(x => x.name == ActiveConsole.name).First().emulator_path = emulatorPathFileDialog.FileName;

                emulatorPathTextBox.Text = emulatorPathFileDialog.FileName;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void emulatorArgsTextBox_Leave(object sender, EventArgs e)
        {
            ActiveConsole.emulator_args = emulatorArgsTextBox.Text;
        }

        private void romArgsTextBox_Leave(object sender, EventArgs e)
        {
            ActiveConsole.rom_args = romArgsTextBox.Text;
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            if (romFolderDialog.ShowDialog() == DialogResult.OK)
            {               
                sampleDataSet.romfolder.Rows.Add(null, romFolderDialog.SelectedPath, ActiveConsole.console_Id);
                romfolderTableAdapter.Update(this.sampleDataSet.romfolder);

                UpdateConsoleDetailsWithRomFolders();
            }
        }

        private void UpdateConsoleDetailsWithRomFolders()
        {
            romFolderListBox.Items.Clear();

            foreach (var romFolder in sampleDataSet.romfolder.Where(x => x.console_id == ActiveConsole.console_Id))
            {
                var romFolderPath = romFolder.path;
                if (!romFolderListBox.Items.Contains(romFolderPath))
                    romFolderListBox.Items.Add(romFolderPath);
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

            var romFolders = sampleDataSet.romfolder.Where(x => x.console_id == ActiveConsole.console_Id);

            foreach (var romFolder in romFolders)
            {
                var romList = Directory.GetFiles(romFolder.path);

                foreach (var rom in romList)
                {
                    var romName = Path.GetFileNameWithoutExtension(rom);
                    if (!sampleDataSet.rom.Where(x => x.name == romName).Any())
                    {
                        sampleDataSet.rom.Rows.Add(
                        null,
                        romName,
                        Path.GetExtension(rom),
                        romFolder.romfolder_id,
                        true);
                    }                    
                }
            }

            UpdateRomListViewItems();
        }

        private void UpdateRomListViewItems()
        {
            romListView.Items.Clear();

            var romFolders = sampleDataSet.romfolder.Where(x => x.console_id == ActiveConsole.console_Id);
            
            foreach (var romFolder in romFolders)
            {
                foreach (var romItem in sampleDataSet.rom.ToList())
                {
                    if (romItem.romfolder_id == romFolder.romfolder_id)
                    {
                        var romListViewItem = new ListViewItem
                        {
                            Text = RomNameConstructor(romItem),
                            Checked = romItem.enabled
                        };

                        romListView.Items.Add(romListViewItem);
                    }
                }
            }         
        }


    }
}
