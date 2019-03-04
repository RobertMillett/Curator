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
using Curator.Data.SteamDb;
using System.Reflection;
using Crc32;
using System.ComponentModel;

namespace Curator
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class Form1 : MetroForm
    {   
        public static CuratorDataSet.ConsoleRow ActiveConsole;
        private SteamController _steamController;
        private ConsoleController _consoleController;
        private RomController _romController;
        public static RomFolderController _romFolderController;
        private SaveLoadController _saveLoadController;

        public Form1()
        {
            InitializeComponent();

            RegisterControllers();

            _saveLoadController.Load();
        }

        private void RegisterControllers()
        {
            _steamController = new SteamController(CuratorDataSet);
            _consoleController = new ConsoleController(CuratorDataSet.Console);
            _romController = new RomController(CuratorDataSet.ROM);
            _romFolderController = new RomFolderController(CuratorDataSet.RomFolder);
            _saveLoadController = new SaveLoadController(CuratorDataSet);
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
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

            if (e.CloseReason != CloseReason.UserClosing || !CuratorDataSet.HasChanges())
                return;

            if (MetroMessageBox.Show(this, "Save changes?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _saveLoadController.Save();
                return;
            }

            _saveLoadController.Exit();
        }
        #endregion

        private async void getGridPicturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var rom in _romController.GetAllRomsWhere(x => x.Enabled == true))
            {
                await SteamGridDbClient.FetchGamePictures(rom);
            }
        }

        public void ShowSteamModifiedMessage()
        {
            MetroMessageBox.Show(this, "Your Steam Shortcuts have been successfully modified. Please re-launch Steam to see the changes.", "Shortcuts Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        
    }
}
