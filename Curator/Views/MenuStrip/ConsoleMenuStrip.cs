using System;
using Curator.Views.CustomDialogs;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var consoleName = AddConsoleDialog.ShowDialog(this);

            if (!string.IsNullOrEmpty(consoleName))
                AddConsole(consoleName, sender, e);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var consoleName = RemoveConsoleDialog.ShowDialog();

            if (!string.IsNullOrEmpty(consoleName))
                RemoveConsole(consoleName, sender, e);
        }
        #endregion
    }
}
