using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework;
using System.Windows.Forms;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetShortcutsvdfFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (steamShortcutsFileDialog.ShowDialog() == DialogResult.OK)
                _steamController.SetSteamShortcutFile(steamShortcutsFileDialog.FileName);

            this.Text = $"Curator - {_steamController.SteamShortcutsFile}";
            this.Refresh();
        }

        private void exportToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CuratorDataSet.HasChanges())
            {
                switch(MetroMessageBox.Show(this, "You have unsaved changes. Save before exporting?", "Export to Steam", MessageBoxButtons.YesNoCancel))
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
        #endregion
    }
}
