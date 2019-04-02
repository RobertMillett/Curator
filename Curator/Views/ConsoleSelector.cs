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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsoleHasChanged(sender, e);
        }

        private void AddConsole_Button_Click(object sender, EventArgs e)
        {
            AddConsole(comboBox1.Text, sender, e);
        }

        private void DeleteConsole_Button_Click(object sender, EventArgs e)
        {
            RemoveConsole(comboBox1.Text, sender, e);
        }
        #endregion

        private void AddConsole(string consoleName, object sender, EventArgs e)
        {
            var consoleAdded = _consoleController.Add(consoleName);

            if (!consoleAdded)
                return;

            //This will always set to the most recently added item as it is added to the bottom of the list.
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

            _consoleController.SetActiveConsole(comboBox1.Text);

            ConsoleHasChanged(sender, e);
        }

        private void RemoveConsole(string consoleName, object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "This will delete the console and all of it's associated data!", "Are you sure?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (_steamController.ShortcutsContainConsole(consoleName))
                {
                    var deleteFromSteam = MetroMessageBox.Show(this, $"You have existing Shortcuts with the label '{consoleName}'. Remove these Shortcuts from Steam?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (deleteFromSteam == DialogResult.Yes)
                    {
                        _steamController.DeleteShortcutsByTag(comboBox1.Text);
                        ShowSteamModifiedMessage();
                    }

                    if (deleteFromSteam == DialogResult.Cancel)
                        return;
                }

                _consoleController.Remove(consoleName);
                ConsoleHasChanged(sender, e);
            }
        }
    }
}
