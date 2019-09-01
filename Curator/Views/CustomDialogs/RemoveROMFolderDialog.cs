using System.Windows.Forms;
using System.Linq;

namespace Curator.Views.CustomDialogs
{
    public static class RemoveROMFolderDialog
    {
        public static string ShowDialog(IWin32Window window)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add ROM Folder",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var consoleNameLabel = new Label() { Left = 18, Top = 20, Text = "Select Console" };
            var consoleSelector = new ComboBox() { Left = 20, Top = 40, Width = 150 };
            consoleSelector.Items.AddRange(Form1._consoleController.GetAllConsoles().Select(x => x.Name).ToArray());

            var romFolderLabel = new Label() { Left = 18, Top = 80, Text = "Select ROM Folder" };

            var romFolderSelector = new ComboBox()
            {
                Left = 20,
                Top = 100,
                Width = 440
            };

           Data.CuratorDataSet.ConsoleRow selectedConsole = null;

            consoleSelector.SelectedIndexChanged += (sender, e) =>
            {
                selectedConsole = Form1._consoleController.GetAllConsoles().First(x => x.Name == consoleSelector.Text);
                var romFolders = Form1._romFolderController.GetRomFoldersForConsole(selectedConsole).Select(x => x.Path).ToArray();

                romFolderSelector.Items.Clear();
                romFolderSelector.Items.AddRange(romFolders);
            };                  

            var deleteButton = new Button() { Text = "Delete", Left = 350, Width = 50, Top = 130 };

            deleteButton.Click += (sender, e) =>
            {
                if (selectedConsole == null)
                {
                    ShowError(consoleSelector, "Please select a Console");
                    return;
                }                    

                var romFolder = Form1._romFolderController.GetRomFoldersForConsole(selectedConsole).FirstOrDefault(x => x.Path == romFolderSelector.Text);

                if (romFolder == null)
                {
                    ShowError(romFolderSelector, "Please select a ROM Folder");
                    return;
                }                    

                Form1._romFolderController.Remove(romFolder);
                prompt.DialogResult = DialogResult.OK;
            };

            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = 130 };

            prompt.Controls.Add(consoleSelector);
            prompt.Controls.Add(deleteButton);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(consoleNameLabel);
            prompt.Controls.Add(romFolderSelector);
            prompt.Controls.Add(romFolderLabel);

            prompt.AcceptButton = deleteButton;
            prompt.CancelButton = cancelButton;
            
            return prompt.ShowDialog() == DialogResult.OK ? consoleSelector.Text : string.Empty;
        }

        private static void ShowError(Control control, string message)
        {
            var errorProvider = new ErrorProvider();
            errorProvider.SetIconAlignment(control, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(control, 2);
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.SetError(control, message);
        }
    }
}
