using System;
using System.Windows.Forms;
using System.Linq;
using Curator.Data;

namespace Curator
{
    [System.ComponentModel.DesignerCategory("")]
    public partial class Form1
    {
        #region Event Handlers
        private void systemDetailsName_Leave(object sender, EventArgs e)
        {
            _consoleController.UpdateName(systemDetailsName.Text);
        }

        private void AddEmulatorPath_Button_Click(object sender, EventArgs e)
        {
            if (emulatorPathFileDialog.ShowDialog() == DialogResult.OK)
            {
                _consoleController.AddEmulatorPath(emulatorPathFileDialog.FileName);
                this.Refresh();

                emulatorPathTextBox.Text = emulatorPathFileDialog.FileName;
            }
        }

        private void emulatorArgsTextBox_Leave(object sender, EventArgs e)
        {
            _consoleController.SetEmulatorArgs(emulatorArgsTextBox.Text);
        }

        private void romArgsTextBox_Leave(object sender, EventArgs e)
        {
            _consoleController.SetRomArgs(romArgsTextBox.Text);
        }
        #endregion

        private void ConsoleHasChanged(object sender, EventArgs e)
        {
            _consoleController.SetActiveConsole(comboBox1.Text);
            UpdateConsoleDetailsView(sender, e);
        }

        private void UpdateConsoleDetailsView(object sender, EventArgs e)
        {
            if (ActiveConsole != null)
            {
                systemDetailsName.Text = ActiveConsole.Name;
                emulatorPathTextBox.Text = ActiveConsole.EmulatorPath;
                emulatorArgsTextBox.Text = ActiveConsole.EmulatorArgs;
                romArgsTextBox.Text = ActiveConsole.RomArgs;

                UpdateConsoleDetailsWithRomFolders();
            }
        }
    }
}
