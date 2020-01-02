using System;
using System.Windows.Forms;
using Curator.Views.CustomDialogs;
using Curator.Data;

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
            var console = (console_ComboBox.SelectedItem as System.Data.DataRowView)?.Row as CuratorDataSet.ConsoleRow;

            if (console != null)
                RemoveConsole(console, sender, e);
        }

        private void filterROMsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FilterForConsoleROMDialog.ShowDialog(this) == DialogResult.OK)
            {
                UpdateRomListViewItems();
            }
        }
        #endregion
    }
}
