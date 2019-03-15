using System;
using System.Windows.Forms;
using MetroFramework.Forms;
using MetroFramework;
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
                try
                {
                    _steamController.ExportToSteam();
                    ShowSteamModifiedMessage();
                }
                catch (Exception ex)
                {
                    ShowSteamExportFailedMessage(ex.Message);
                }
            }
        }

        private void ShowSteamExportFailedMessage(string message)
        {
            MetroMessageBox.Show(this, $"Overwriting Steam Shortcuts.vdf has failed! Exception:\n{message}", "Curator", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        public void ShowSteamModifiedMessage()
        {
            MetroMessageBox.Show(this, "Your Steam Shortcuts have been successfully updated. Please re-launch Steam to see the changes.", "Shortcuts Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public void ShowSaveSuccessMessage()
        {
            MetroMessageBox.Show(this, "Save Successful", "Curator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowSaveFailureMessage(string message)
        {
            MetroMessageBox.Show(this, $"Save failed! Exception: \n{message}", "Curator", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
