using System;
using System.Windows.Forms;
using MetroFramework;

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
            try
            {
                if (MetroMessageBox.Show(this, $"Save all ROM and Console details?", "Curator", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _saveLoadController.Save();
                    ShowSaveSuccessMessage();
                }
            }
            catch (Exception ex)
            {
                ShowSaveFailureMessage(ex.Message);
            }
        }

        private void rOMDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MetroMessageBox.Show(this, $"Save all current {ActiveConsole.Name} ROM details?", "Curator", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var roms = _romController.GetRomsForConsole(ActiveConsole);
                    _saveLoadController.SaveRomsForActiveConsole(roms);
                    ShowSaveSuccessMessage();
                }
            }
            catch (Exception ex)
            {
                ShowSaveFailureMessage(ex.Message);
            }
            
        }

        private void systemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MetroMessageBox.Show(this, $"Save all current {ActiveConsole.Name} details?", "Curator", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _saveLoadController.SaveActiveConsole();
                    ShowSaveSuccessMessage();
                }
            }
            catch (Exception ex)
            {
                ShowSaveFailureMessage(ex.Message);
            }
            
        }
        #endregion
    }
}
