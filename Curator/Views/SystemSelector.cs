using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace Curator
{
    public partial class Form1
    {
        private void AddConsole_Button_Click(object sender, EventArgs e)
        {
            _consoleController.AddConsole(comboBox1.Text);

            //This will always set to the most recently added item as it is added to the bottom of the list.
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
        }

        private void DeleteConsole_Button_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "This will delete the console and all of it's associated data!", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var RomFolderRows = CuratorDataSet.RomFolder.Where(x => x.Console_Id == ActiveConsole.Id);
                foreach (var row in RomFolderRows)
                {
                    CuratorDataSet.RomFolder.Rows.Remove(row);
                }

                CuratorDataSet.Console.Rows.Remove(ActiveConsole);
            }
        }
    }
}
