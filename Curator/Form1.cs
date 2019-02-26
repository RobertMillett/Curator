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
using Curator.Data;
using Curator.Data.Controllers;
using System.Reflection;
using Crc32;
using System.ComponentModel;

namespace Curator
{
    public partial class Form1 : MetroForm
    {        
        public static string DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Curator", "XmlDoc.xml");
        public static CuratorDataSet.ConsoleRow ActiveConsole;
        public static bool PromptSave;
        private SteamController _steamController;
        private ConsoleController _consoleController;

        public Form1()
        {
            InitializeComponent();           

            if (!File.Exists(DataPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(DataPath));
                CuratorDataSet.WriteXml(DataPath);
            }

            CuratorDataSet.ReadXml(DataPath);

            _steamController = new SteamController(CuratorDataSet);
            _consoleController = new ConsoleController(CuratorDataSet.Console);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.StyleManager = metroStyleManager1;
            //metroStyleManager1.Theme = MetroThemeStyle.Dark;
            RegisterEventHandlers();

            ConsoleHasChanged(sender, e);

            romListView.Columns[0].Width = romListView.Width - 24;
        }

        private void RegisterEventHandlers()
        {
            //Form Event Handlers
            Shown += EnforceShortcutsFile;
            FormClosing += OnFormClosing;
            Resize += OnFormResized;

            //Data Event Handlers
            CuratorDataSet.Console.RowChanged += PromptSaveTrue;
            CuratorDataSet.RomFolder.RowChanged += PromptSaveTrue;
            CuratorDataSet.ROM.RowChanged += PromptSaveTrue;

            //UI Event Handlers
            comboBox1.SelectedIndexChanged += ConsoleHasChanged;
            romListView.ItemCheck += RomEnabled;
        }

        private void PromptSaveTrue(object sender, DataRowChangeEventArgs e)
        {
            PromptSave = true;            
            this.consoleBindingSource2.ResetBindings(false);
        }

        private void EnforceShortcutsFile(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_steamController.SteamShortcutsFile))
            {
                var welcomeBox = MetroMessageBox.Show(this, "Please open your Steam Shortcuts .vdf file", "Welcome to Curator", MessageBoxButtons.OKCancel);

                if (welcomeBox == DialogResult.Cancel)
                    Application.Exit();

                if (steamShortcutsFileDialog.ShowDialog() != DialogResult.OK)
                    EnforceShortcutsFile(this, e);

                _steamController.SetSteamShortcutFile(steamShortcutsFileDialog.FileName);
            }

            this.Text = $"Curator - {_steamController.SteamShortcutsFile}";
        }

        #region Event Handlers
        private void OnFormResized(object sender, EventArgs e)
        {
            romListView.Columns[0].Width = romListView.Width - 24;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {            
            comboBox1.SelectedIndexChanged -= ConsoleHasChanged;

            if (e.CloseReason != CloseReason.UserClosing || !PromptSave)
                return;

            var saveChanges = MetroMessageBox.Show(this, "Save changes?", "", MessageBoxButtons.OKCancel);

            if (saveChanges == DialogResult.OK)
                CuratorDataSet.WriteXml(DataPath);
        }
        #endregion
    }
}
