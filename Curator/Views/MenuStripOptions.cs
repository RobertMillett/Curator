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
            SetSteamShortcutFile();
        }

        private void exportToSteamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "Override current Shortcuts file?", "Export to Steam", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ExportToSteam();
            }
        }
        #endregion
    }
}
