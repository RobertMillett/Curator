using System;
using System.IO;
using Gameloop.Vdf;
using SteamIDs_Engine;
using Gameloop.Vdf.Linq;
using Gameloop.Vdf.JsonConverter;
using System.Threading;
using System.Diagnostics;
using Microsoft.Win32;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
using System.Collections.Generic;
using Curator.Data;
using Curator.Data.Controllers;

namespace Curator
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class Form1 : MetroForm
    {   
        public static CuratorDataSet.ConsoleRow ActiveConsole;
        private SteamController _steamController;
        public static ConsoleController _consoleController;
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

            SetToolTips();            
        }

        private void RegisterEventHandlers()
        {
            //Form Event Handlers
            Shown += EnforceShortcutsFile;
            FormClosing += OnFormClosing;
            Resize += OnFormResized;
            Shown += ValidateRomFolders;
        }

        private void ValidateRomFolders(object sender, EventArgs e)
        {
            var invalidFolders = _romFolderController.ValidateFolders();

            if (invalidFolders.Any())
            {
                if (ShowInvalidROMFolderMessage(invalidFolders) == DialogResult.OK)
                {
                    foreach (var folder in invalidFolders)
                    {
                        _romController.DeleteAllRomsForRomFolder(folder.Id);
                        folder.Delete();
                    }
                }
            }

            ConsoleHasChanged(sender, e);
        }

        private void EnforceShortcutsFile(object sender, EventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(_steamController.SteamShortcutsFile))
            {
                string accountName = _steamController.TryFindAndSetShortcutsAutomatically();
                
                if (accountName != null) //Automatically found shortcuts.vdf
                {
                    var welcomeMessage = MetroMessageBox.Show(this, $"Found shortcuts.vdf file for Steam Account: '{accountName}'.\nIf this is not correct you can change it using the Steam Header Menu -> Set Shortcuts File button.", "Welcome to Curator", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var welcomeBox = MetroMessageBox.Show(this, "Please open your Steam 'shortcuts.vdf' file.\nThis is usually found in C:\\Program Files (x86)\\Steam\\userdata\\{your_user_id}\\config", "Welcome to Curator", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                    if (welcomeBox == DialogResult.Cancel)
                        Application.Exit();

                    if (steamShortcutsFileDialog.ShowDialog() != DialogResult.OK)
                        EnforceShortcutsFile(this, e);

                    _steamController.SetSteamShortcutFile(steamShortcutsFileDialog.FileName);
                }
            }

            ValidateSettings();

            this.Text = $"Curator - {_steamController.SteamShortcutsFile}";
            this.Refresh();
        }

        private void ValidateSettings()
        {
            if (!File.Exists(_steamController.SteamShortcutsFile))
            {
                MetroMessageBox.Show(this, $"There was an issue loading your shortcuts.vdf file.\nPlease re-add it manually.\nAttemted to load: {_steamController.SteamShortcutsFile}", "Curator", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (steamShortcutsFileDialog.ShowDialog() != DialogResult.OK)
                    EnforceShortcutsFile(this, null);

                _steamController.SetSteamShortcutFile(steamShortcutsFileDialog.FileName);
            }

            var gridDirectory = Path.Combine(Path.GetDirectoryName(_steamController.SteamShortcutsFile), "grid");

            if (!Directory.Exists(gridDirectory))
                Directory.CreateDirectory(gridDirectory);
        }

        #region Event Handlers
        private void OnFormResized(object sender, EventArgs e)
        {
            romListView.Columns[0].Width = romListView.Width - 24;

            loadingPicturesSpinner.Location = new System.Drawing.Point
            {
                Y = this.Size.Height - 25,
                X = 30
            };

            taskLabel.Location = new System.Drawing.Point
            {
                Y = this.Size.Height - 25,
                X = 50
            };
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {            
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;

            if (e.CloseReason != CloseReason.UserClosing)
                return;

            if (CuratorDataSet.HasChanges())
            {
                if (MetroMessageBox.Show(this, "Save changes?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        _saveLoadController.Save();
                    }
                    catch (Exception ex)
                    {
                        ShowSaveFailureMessage(ex.Message);
                    }
                }
            }

            _saveLoadController.Exit();

            if (MetroMessageBox.Show(this, "Export to Steam?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                AttemptSteamExport();
            }
        }

        #endregion               
    }
}
