using Curator.Views.CustomDialogs;
using System;

namespace Curator
{
    public partial class Form1
    {
        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var romFolderName = AddROMFolderDialog.ShowDialog(this);

            UpdateRomFolderView();
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var romFolderName = RemoveROMFolderDialog.ShowDialog(this);

            UpdateRomFolderView();
        }
    }
}
