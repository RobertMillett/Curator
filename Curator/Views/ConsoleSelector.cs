using System;
using System.Windows.Forms;
using MetroFramework;
using Curator.Data;

namespace Curator
{
    public partial class Form1
    {
        #region Event Handlers        

        private void console_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ConsoleHasChanged(sender, e);
        }

        private void AddConsole_Button_Click(object sender, EventArgs e)
        {
            AddConsole(console_ComboBox.Text, sender, e);
        }

        private void DeleteConsole_Button_Click(object sender, EventArgs e)
        {
            RemoveConsole(console_ComboBox.Text, sender, e);
        }
        #endregion

        private void AddConsole(string consoleName, object sender, EventArgs e)
        {
            var consoleAdded = _consoleController.Add(consoleName);

            if (!consoleAdded)
                return;

            //This will always set to the most recently added item as it is added to the bottom of the list.
            console_ComboBox.SelectedIndex = console_ComboBox.Items.Count - 1;

            _consoleController.SetActiveConsole(console_ComboBox.SelectedItem as CuratorDataSet.ConsoleRow);

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
                        _steamController.DeleteShortcutsByTag(console_ComboBox.Text);
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
