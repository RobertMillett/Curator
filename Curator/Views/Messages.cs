using System.Collections.Generic;
using System.Windows.Forms;
using MetroFramework;

namespace Curator
{
    public partial class Form1
    {
        private const string AreYouSure = "Are you sure?";

        private void ShowSteamExportFailedMessage(string message)
        {
            MetroMessageBox.Show(this, $"Overwriting Steam Shortcuts.vdf has failed! Exception:\n{message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSteamModifiedMessage()
        {
            MetroMessageBox.Show(this, "Your Steam Shortcuts have been successfully updated. Please re-launch Steam to see the changes.", "Shortcuts Modified", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowSaveSuccessMessage()
        {
            MetroMessageBox.Show(this, "Save Successful", "Curator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowSaveFailureMessage(string message)
        {
            MetroMessageBox.Show(this, $"Save failed. Exception: \n{message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult ShowDeleteRomFolderConfirmationMessage(string romFolderPath)
        {
            return MetroMessageBox.Show(this, $"This will remove the following ROM Folder and all it's associated ROMs from the {ActiveConsole.Name} console!\n{romFolderPath}", AreYouSure, MessageBoxButtons.OKCancel);
        }

        public void ShowPathTestFailureMessage(string message)
        {
            MetroMessageBox.Show(this, $"Attempting to open your ROM threw the following error:\n{message}", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult ShowInvalidROMFolderMessage(List<Data.CuratorDataSet.RomFolderRow> romFolders)
        {
            var folderList = string.Empty;

            foreach (var folder in romFolders)
            {
                folderList += $"{folder.Path}\n";
            }

           return MetroMessageBox.Show(
               this, 
               $"The following ROM Folders can no longer be found:\n{folderList}Do you wish to delete these ROM Folders (and all associated ROMs) now?", "Warning!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
