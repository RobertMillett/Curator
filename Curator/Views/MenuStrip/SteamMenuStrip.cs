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
        private static string SteamGridDbMessage = "ROM images are fetched from http://www.steamgriddb.com/. Please ensure your ROM's title exactly matches. If your game is found but no images are available please log into http://www.steamgriddb.com/ and upload one.";

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

            if (MetroMessageBox.Show(this, "Overwrite current Shortcuts file?", "Export to Steam", MessageBoxButtons.OKCancel) == DialogResult.OK)
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
            if (MetroMessageBox.Show(this, SteamGridDbMessage, "Curator", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {                
                foreach (var rom in _romController.GetAllRomsWhere(x => x.Enabled == true))
                {
                    await SteamGridDbClient.FetchGamePictures(rom);
                }
            }
        }
        #endregion
    }
}
