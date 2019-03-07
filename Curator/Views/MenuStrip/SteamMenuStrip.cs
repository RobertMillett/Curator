using System;
using Curator.Data;
using MetroFramework;
using System.Windows.Forms;
using Curator.Data.SteamDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void exportShortcutsToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CuratorDataSet.HasChanges())
            {
                switch (MetroMessageBox.Show(this, "You have unsaved changes. Save before exporting?", "Export to Steam", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        _saveLoadController.Save();
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }

            if (MetroMessageBox.Show(this, "Override current Shortcuts file?", "Export to Steam", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _steamController.ExportToSteam();
            }
        }

        private void setShortcutsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (steamShortcutsFileDialog.ShowDialog() == DialogResult.OK)
                _steamController.SetSteamShortcutFile(steamShortcutsFileDialog.FileName);

            this.Text = $"Curator - {_steamController.SteamShortcutsFile}";
            this.Refresh();
        }

        private async void getGridPicturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var rom in _romController.GetAllRomsWhere(x => x.Enabled == true))
            {
                await SteamGridDbClient.FetchGamePictures(rom);
            }
        }
        #endregion
    }
}
