using System.Windows.Forms;
using System.Linq;

namespace Curator.Views.CustomDialogs
{
    public static class AddROMFolderDialog
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

            Label consoleNameLabel = new Label() { Left = 18, Top = 20, Text = "Select Console" };
            var consoleSelector = new ComboBox() { Left = 20, Top = 40, Width = 150 };
            consoleSelector.Items.AddRange(Form1._consoleController.GetAllConsoles().Select(x => x.Name).ToArray());

            var romFolderPathDisplayBox = new TextBox
            {
                ReadOnly = true,
                Left = 20, Top = 95, Width = 380
            };

            var romFolderFileSelector = new Button()
            {
                Text = "Select Folder",
                Left = 18, Top = 70, Width = 80                
            };

            var romFolderToAdd = string.Empty;

            romFolderFileSelector.Click += (sender, e) =>
            {
                var folderBrowser = new FolderBrowserDialog();
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    romFolderToAdd = folderBrowser.SelectedPath;
                    romFolderPathDisplayBox.Text = folderBrowser.SelectedPath;
                }
            };

            Button okButton = new Button() { Text = "Ok", Left = 350, Width = 50, Top = 130 };

            okButton.Click += (sender, e) =>
            {
                var console = Form1._consoleController.GetAllConsoles().First(x => x.Name == consoleSelector.Text);

                if (console == null)
                {
                    ShowError(consoleSelector, "Console cannot be null");
                }

                Form1._romFolderController.AddToConsole(romFolderToAdd, console);
            };


            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = 130, DialogResult = DialogResult.Cancel };
            
            okButton.Click += (sender, e) => {
                if (string.IsNullOrWhiteSpace(consoleSelector.Text))
                {
                    ShowError(consoleSelector, "Name cannot be null");
                }
                else
                {
                    prompt.DialogResult = DialogResult.OK;
                }
            };

            prompt.Controls.Add(consoleSelector);
            prompt.Controls.Add(okButton);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(consoleNameLabel);
            prompt.Controls.Add(romFolderFileSelector);
            prompt.Controls.Add(romFolderPathDisplayBox);

            prompt.AcceptButton = okButton;
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
