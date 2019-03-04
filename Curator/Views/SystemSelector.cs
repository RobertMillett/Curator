﻿using System;
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
                var deleteFromSteam = MetroMessageBox.Show(this, $"Also remove all '{comboBox1.Text}' ROM's from Steam as well?", "Are you sure?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (deleteFromSteam == DialogResult.Yes)
                {
                    _steamController.DeleteShortcutsByTag(comboBox1.Text);
                    ShowSteamModifiedMessage();
                }

                if (deleteFromSteam == DialogResult.Cancel)
                    return;

                _consoleController.Remove(comboBox1.Text);


            }
        }
        #endregion
    }
}
