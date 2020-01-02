using Curator.Views.CustomDialogs;
using System.Windows.Forms;
using System;

namespace Curator
{
    public partial class Form1
    {
        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var console = AddROMFolderDialog.ShowDialog(this);

            UpdateRomFolderView();

            if (console == null)
                return;

            if (PromptToFilter.ShowDialog(this, console) == DialogResult.OK)
            {
                if (FilterForConsoleROMDialog.ShowDialog(this) == DialogResult.OK)
                    UpdateRomFolderView();
            }   
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var romFolderName = RemoveROMFolderDialog.ShowDialog(this);

            UpdateRomFolderView();
        }
    }
}
