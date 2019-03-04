using System.Windows.Forms;
using Curator.Data;
using System;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void romListView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var rom = _romController.GetRom(e.Item.Text);
            _romController.SetRomEnabledState(rom, e.Item.Checked);

            if (romDetailsName.Text == rom.Name)
                UpdateSelectedRomDetails(rom);
        }

        private void romListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            UpdateSelectedRomDetails(rom);
        }
        #endregion

        public void RomListViewUpdateCheckedState(CuratorDataSet.ROMRow rom)
        {
            var listViewName = _romController.RomNameConstructor(rom);

            romListView.Items.Find(listViewName, false)[0].Checked = rom.Enabled;
        }

        public void UpdateRomListViewItems()
        {
            romListView.Items.Clear();

            var RomFolders = _romFolderController.GetRomFoldersForActiveConsole();

            foreach (var RomFolder in RomFolders)
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
                }
            }
        }
    }
}
