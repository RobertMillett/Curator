using System;
using System.Linq;
using System.Windows.Forms;
using Curator.Data;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void AddRomFolder_Button_Click(object sender, EventArgs e)
        {
            if (romFolderDialog.ShowDialog() == DialogResult.OK)
            {
                CuratorDataSet.RomFolder.Rows.Add(null, romFolderDialog.SelectedPath, ActiveConsole.Id);
                UpdateConsoleDetailsWithRomFolders();
            }
        }
        #endregion

        private void UpdateConsoleDetailsWithRomFolders()
        {
            romFolderListBox.Items.Clear();

            foreach (var RomFolder in CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id))
            {
                var RomFolderPath = RomFolder.Path;
                if (!romFolderListBox.Items.Contains(RomFolderPath))
                    romFolderListBox.Items.Add(RomFolderPath);
            }

            GetRoms();
        }
    }
}
