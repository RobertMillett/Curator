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

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _saveLoadController.Save();
        }

        private void rOMDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var roms = _romController.GetRomsForActiveConsole();
            _saveLoadController.SaveRomsForActiveConsole(roms);
        }
        #endregion
    }
}
