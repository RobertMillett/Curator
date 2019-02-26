using System.Linq;
using System.IO;
using System.Windows.Forms;
using Curator.Data;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void RomEnabled(object sender, ItemCheckEventArgs e)
        {
            var rom = CuratorDataSet.ROM.Where(x => _romController.RomNameConstructor(x) == romListView.Items[e.Index].Text).First();
            rom.Enabled = e.NewValue == CheckState.Checked;
        }
        #endregion

        public void UpdateRomListViewItems()
        {
            romListView.Items.Clear();

            var RomFolders = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);

            foreach (var RomFolder in RomFolders)
            {
                foreach (var romItem in CuratorDataSet.ROM.ToList())
                {
                    if (romItem.RomFolder_Id == RomFolder.Id)
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
}
