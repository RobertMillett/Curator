using System.Windows.Forms;
using System;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void RomEnabled(object sender, ItemCheckEventArgs e)
        {
            _romController.SetRomEnabledState(romListView.Items[e.Index].Text, e.NewValue == CheckState.Checked);
        }

        private void romListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            UpdateSelectedRomDetails(rom);
        }
        #endregion

        public void UpdateRomListViewItems()
        {
            romListView.Items.Clear();

            var RomFolders = _romFolderController.GetRomFoldersForActiveConsole();

            foreach (var RomFolder in RomFolders)
            {
                foreach (var romItem in _romController.GetRomsByRomFolderId(RomFolder.Id))
                {
                    var romListViewItem = new ListViewItem
                    {
                        Text = _romController.RomNameConstructor(romItem),
                        Checked = romItem.Enabled
                    };

                    romListView.Items.Add(romListViewItem);
                }
            }
        }
    }
}
