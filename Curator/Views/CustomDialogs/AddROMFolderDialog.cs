using System.Windows.Forms;
using System.Linq;
using Curator.Data;

namespace Curator.Views.CustomDialogs
{
    public static class AddROMFolderDialog
    {
        public static CuratorDataSet.ConsoleRow console;

        public static CuratorDataSet.ConsoleRow ShowDialog(IWin32Window window)
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
            var consoleSelector = new ComboBox() { Left = 20, Top = 40, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            consoleSelector.Items.AddRange(Form1._consoleController.GetAllConsoles().Select(x => x.Name).ToArray());
            

            if (Form1.ActiveConsole != null)
                consoleSelector.SelectedItem = consoleSelector.Items[consoleSelector.Items.IndexOf(Form1.ActiveConsole.Name)];

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

            var includeSubFoldersCheckbox = new CheckBox()
            {
                Text = "Include Sub-Folders",
                Left = 20,    
                Top = 130,
                Width = 150,
                CheckAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            Button okButton = new Button() { Text = "Ok", Left = 350, Width = 50, Top = 130 };

            okButton.Click += (sender, e) =>
            {
                console = Form1._consoleController.GetAllConsoles().FirstOrDefault(x => x.Name == consoleSelector.Text);

                if (console == null)
                    return;

                Form1._romFolderController.AddToConsole(romFolderToAdd, console, includeSubFoldersCheckbox.Checked);
            };

            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = 130, DialogResult = DialogResult.Cancel };
            
            okButton.Click += (sender, e) => {
                if (string.IsNullOrWhiteSpace(consoleSelector.Text))
                {
                    ShowError(consoleSelector, "Please select a Console");
                    return;
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
            prompt.Controls.Add(includeSubFoldersCheckbox);

            prompt.AcceptButton = okButton;
            prompt.CancelButton = cancelButton;
            
            return prompt.ShowDialog() == DialogResult.OK ? console : null;
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
