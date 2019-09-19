using System;
using System.Windows.Forms;
using MetroFramework;


namespace Curator
{
    public partial class Form1
    {
        private void UpdateRomFolderView()
        {
            UpdateConsoleDetailsWithRomFolders();
            _romController.LoadRoms();
            UpdateRomListViewItems();
        }

        #region On Form Load
        
        #endregion

        #region Event Handlers
        private void AddRomFolder_Button_Click(object sender, EventArgs e)
        {
            if (romFolderDialog.ShowDialog() == DialogResult.OK)
            {
                _romFolderController.AddToActiveConsole(romFolderDialog.SelectedPath);
                UpdateConsoleDetailsWithRomFolders();
                _romController.LoadRoms();
                UpdateRomListViewItems();
            }
        }

        private void romFolderListBox_KeyDown(object sender, KeyEventArgs e)
        {            
            if (e.KeyCode == Keys.Delete)
            {
                var romFolder = romFolderListBox.SelectedItem?.ToString();

                if (ShowDeleteRomFolderConfirmationMessage(romFolder) == DialogResult.OK)
                {
                    var romFolderId = _romFolderController.GetRomFolderByPath(romFolder).Id;
                    _romController.DeleteAllRomsForRomFolder(romFolderId);
                    _romFolderController.Remove(romFolder);
                }

                UpdateConsoleDetailsWithRomFolders();
                UpdateRomListViewItems();
            }               
        }

        private void romFolderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (romFolderListBox.SelectedIndex == -1)
            {
                UpdateRomListViewItems();
                return;
            }            

            UpdateRomListViewItems();
        }

        private void fetchRomsButton_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Fetch ROMs from ROM Folders?", "Curator", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _romController.LoadRoms();
                UpdateRomListViewItems();
            }
        }
        #endregion

        private void UpdateConsoleDetailsWithRomFolders()
        {
            romFolderListBox.Items.Clear();

            foreach (var RomFolder in _romFolderController.GetRomFoldersForActiveConsole())
            {
                var RomFolderPath = RomFolder.Path;
                if (!romFolderListBox.Items.Contains(RomFolderPath))
                    romFolderListBox.Items.Add(RomFolderPath);
            }
        }
    }
}
