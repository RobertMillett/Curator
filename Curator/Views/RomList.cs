using System.Windows.Forms;
using Curator.Data;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Curator
{
    public partial class Form1
    {
        public List<CuratorDataSet.ROMRow> romListRoms;

        #region Event Handlers
        private void romListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var rom = romListRoms[e.Item.Index];

            if (rom.Enabled == e.Item.Checked)
                return;

            _romController.SetRomEnabledState(rom, e.Item.Checked);

            if (romDetailsName.Text == rom.Name)
                UpdateSelectedRomDetails(rom);
        }

        private void romListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];
            UpdateSelectedRomDetails(rom);
        }
        #endregion

        public void RomListViewUpdateCheckedState(CuratorDataSet.ROMRow rom)
        {
            var romIndex = romListRoms.IndexOf(rom);

            romListView.Items[romIndex].Checked = rom.Enabled;
        }

        public void UpdateRomListViewItems()
        {
            romListRoms = new List<CuratorDataSet.ROMRow>();

            var selectedRomFolders = new List<CuratorDataSet.RomFolderRow>();

            foreach (var romFolderIndex in romFolderListBox.SelectedIndices)
            {
                var path = romFolderListBox.Items[(int)romFolderIndex].ToString();
                var romFolder = _romFolderController.GetRomFolderByPath(path);
                selectedRomFolders.Add(romFolder);
            }

            romListView.Items.Clear();

            selectedRomFolders = selectedRomFolders.Any() ? selectedRomFolders : _romFolderController.GetRomFoldersForActiveConsole();

            romListView.ItemChecked -= romListView_ItemChecked;
            foreach (var RomFolder in selectedRomFolders)
            {
                foreach (var romItem in _romController.GetRomsByRomFolderId(RomFolder.Id))
                {
                    var name = _romController.RomNameConstructor(romItem);
                    var romListViewItem = new ListViewItem
                    {
                        Text = name,
                        Name = name,
                        Checked = romItem.Enabled
                    };

                    romListView.Items.Add(romListViewItem);
                    romListRoms.Add(romItem);
                }
            }
            romListView.ItemChecked += romListView_ItemChecked;
        }
    }
}
