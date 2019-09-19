using System;
using Curator.Data;
using MetroFramework;
using System.Windows.Forms;
using Curator.Data.SteamDb;
using System.Linq;
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
                AttemptSteamExport();
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
                var roms = _romController.GetAllRomsWhere(x => x.Enabled == true).ToList();
                for (var i = 0; i < roms.Count(); i++)
                {
                    var rom = roms[i];
                    var message = $"Fetching Grid Images for ROM {i+1}/{roms.Count()}: '{rom.Name}'";
                    ShowLoading(message, false);
                    await Task.Run(() => SteamGridDbClient.FetchGamePictures(rom));
                }
            }

            HideLoading();
        }
        #endregion

        private void AttemptSteamExport()
        {
            if (CuratorDataSet.Console.Where(x => string.IsNullOrEmpty(x.EmulatorPath)).Count() == CuratorDataSet.Console.Count)
            {
                MetroMessageBox.Show(this, "None of your Consoles have their Emulator Path set! Cannot export to Steam!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var console in CuratorDataSet.Console.ToList())
            {
                if (string.IsNullOrEmpty(console.EmulatorPath))
                {
                    if (MetroMessageBox.Show(this, $"The '{console.Name}' Console does not have its Emulator Path set. ROMs will not be imported into Steam for this console.", "Warning", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        return;
                }                    
            }

            try
            {
                _steamController.ExportToSteam();
                ShowSteamModifiedMessage();
            }
            catch (Exception ex)
            {
                ShowSteamExportFailedMessage(ex.Message);
            }
        }
    }
}
