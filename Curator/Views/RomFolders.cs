using System;
using System.Linq;
using System.Windows.Forms;
using Curator.Data;
using MetroFramework;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void AddRomFolder_Button_Click(object sender, EventArgs e)
        {
            if (romFolderDialog.ShowDialog() == DialogResult.OK)
            {
                _romFolderController.Add(romFolderDialog.SelectedPath);
                UpdateConsoleDetailsWithRomFolders();
                _romController.LoadRoms();
                UpdateRomListViewItems();
            }
        }

        private void romFolderListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                _romFolderController.Remove(romFolderListBox.SelectedItem.ToString());

            UpdateConsoleDetailsWithRomFolders();
        }

        private void fetchRomsButton_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Fetch ROMs from ROM Folders?", "Curator", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _romController.LoadRoms();
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
