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
        #region Event Handlers
        private void AddConsole_Button_Click(object sender, EventArgs e)
        {
            var consoleAdded = _consoleController.Add(comboBox1.Text);

            //This will always set to the most recently added item as it is added to the bottom of the list. 
            if (!consoleAdded)
                return;

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

            _consoleController.SetActiveConsole(comboBox1.Text);

            UpdateConsoleDetailsView(sender, e);
        }

        private void DeleteConsole_Button_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "This will delete the console and all of it's associated data!", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _consoleController.Remove(comboBox1.Text);
            }
        }
        #endregion
    }
}
