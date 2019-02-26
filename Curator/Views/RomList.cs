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
            var rom = CuratorDataSet.ROM.Where(x => RomNameConstructor(x) == romListView.Items[e.Index].Text).First();
            rom.Enabled = e.NewValue == CheckState.Checked;
        }
        #endregion

        private string RomNameConstructor(CuratorDataSet.ROMRow romItem)
        {
            return $"{romItem.Name} ({romItem.Extension.Trim('.').ToUpper()})";
        }

        private void GetRoms()
        {
            if (ActiveConsole == null)
                return;

            var RomFolders = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);

            foreach (var RomFolder in RomFolders)
            {
                var romList = Directory.GetFiles(RomFolder.Path);

                foreach (var rom in romList)
                {
                    var romName = Path.GetFileNameWithoutExtension(rom);
                    if (!CuratorDataSet.ROM.Where(x => x.Name == romName).Any())
                    {
                        var romRow = CuratorDataSet.ROM.NewROMRow();
                        romRow.Name = romName;
                        romRow.Extension = Path.GetExtension(rom);
                        romRow.RomFolder_Id = RomFolder.Id;
                        romRow.Enabled = true;

                        CuratorDataSet.ROM.Rows.Add(romRow);
                    }
                }
            }

            UpdateRomListViewItems();
        }

        private void UpdateRomListViewItems()
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
                            Text = RomNameConstructor(romItem),
                            Checked = romItem.Enabled
                        };

                        romListView.Items.Add(romListViewItem);
                    }
                }
            }
        }
    }
}
