using System;
using System.Windows.Forms;
using System.Linq;
using Curator.Data;

namespace Curator
{   
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

            var rom = _romController.GetRom(romListView?.FocusedItem?.Text);
            UpdateSelectedRomDetails(rom);
        }

        private void romArgsTextBox_Leave(object sender, EventArgs e)
        {
            _consoleController.SetRomArgs(romArgsTextBox.Text);

            var rom = _romController.GetRom(romListView?.FocusedItem?.Text);
            UpdateSelectedRomDetails(rom);
        }
        #endregion

        private void ConsoleHasChanged(object sender, EventArgs e)
        {
            var newConsole = (console_ComboBox.SelectedItem as System.Data.DataRowView).Row as CuratorDataSet.ConsoleRow;

            _consoleController.SetActiveConsole(newConsole);
            UpdateConsoleDetailsView(sender, e);
            UpdateRomListViewItems();
            UpdateSelectedRomDetails(null);

            emulatorPathToolStrip.Enabled = ActiveConsole != null;
            romFolderToolStrip.Enabled = ActiveConsole != null;
        }

        private void UpdateConsoleDetailsView(object sender, EventArgs e)
        {
            systemDetailsName.Text = ActiveConsole?.Name;
            emulatorPathTextBox.Text = ActiveConsole?.EmulatorPath;
            emulatorArgsTextBox.Text = ActiveConsole?.EmulatorArgs;
            romArgsTextBox.Text = ActiveConsole?.RomArgs;

            UpdateConsoleDetailsWithRomFolders();
        }
    }
}
