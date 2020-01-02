using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using MetroFramework;

namespace Curator.Views.CustomDialogs
{
    public static class RemoveROMFolderDialog
    {
        private static Data.CuratorDataSet.ConsoleRow selectedConsole = null;
        public static string ShowDialog(IWin32Window window)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 50,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add ROM Folder",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var consoleNameLabel = new Label() { Left = 18, Top = 20, Text = "Select Console" };
            var consoleSelector = new ComboBox() { Left = 20, Top = 40, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            consoleSelector.Items.AddRange(Form1._consoleController.GetAllConsoles().Select(x => x.Name).ToArray());

            var romFolderLabel = new Label() { Left = 18, Top = 80, Text = "Select ROM Folder" };

            var romFolderSelector = new CheckedListBox
            {
                Left = 20,
                Top = 100,
                Width = 440
            };

            consoleSelector.SelectedIndexChanged += (sender, e) =>
            {
                selectedConsole = Form1._consoleController.GetAllConsoles().First(x => x.Name == consoleSelector.Text);
                var romFolders = Form1._romFolderController.GetRomFoldersForConsole(selectedConsole).Select(x => x.Path).ToArray();

                romFolderSelector.Items.Clear();
                romFolderSelector.Items.AddRange(romFolders);

                var height = 15;
                if (romFolderSelector.Items.Count > 0)
                {
                    height = romFolderSelector.GetItemRectangle(0).Height * romFolderSelector.Items.Count;
                }

                romFolderSelector.ClientSize = new Size(romFolderSelector.ClientSize.Width, height);

                prompt.Height = romFolderSelector.Height + 180;
            };                  

            if (Form1.ActiveConsole != null)
                consoleSelector.SelectedItem = consoleSelector.Items[consoleSelector.Items.IndexOf(Form1.ActiveConsole.Name)];            

            var deleteButton = new Button() { Text = "Delete Selected", Left = 300, Width = 100, Top = prompt.Height - 70 };

            deleteButton.Click += (sender, e) =>
            {
                if (selectedConsole == null)
                {
                    ShowError(consoleSelector, "Please select a Console");
                    return;
                }

                var romFolders = new List<Data.CuratorDataSet.RomFolderRow>();
                foreach (var item in romFolderSelector.CheckedItems)
                {
                    var path = romFolderSelector.GetItemText(item);
                    romFolders.Add(Form1._romFolderController.GetRomFoldersForConsole(selectedConsole).FirstOrDefault(x => x.Path == path));
                }
                
                if (romFolders.Count > 0)
                {
                    var confirm = MetroMessageBox.Show(window, "Are you sure you want to delete these ROM Folders?", "ROM Folder Deletion", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                    if (confirm == DialogResult.OK)
                    {
                        foreach (var romFolder in romFolders)
                            Form1._romFolderController.Remove(romFolder);

                        prompt.DialogResult = confirm;
                    }
                }
            };

            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = prompt.Height - 70 };

            prompt.ClientSizeChanged += (sender, e) =>
            {
                cancelButton.Top = prompt.Height - 70;
                deleteButton.Top = prompt.Height - 70;
            };

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
