using System;
using System.Windows.Forms;
using Curator.Data;
using MetroFramework;
using System.Collections.Generic;

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

        private void romFolderListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (romFolderListBox.SelectedIndex == -1)
            {
                UpdateRomListViewItems();
                return;
            }

            var romFolders = new List<CuratorDataSet.RomFolderRow>();

            foreach (var romFolderIndex in romFolderListBox.SelectedIndices)
            {
                var path = romFolderListBox.Items[(int)romFolderIndex].ToString();
                var romFolder = _romFolderController.GetRomFolderByPath(path);
                romFolders.Add(romFolder);
            }

            UpdateRomListViewItems(romFolders);
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
