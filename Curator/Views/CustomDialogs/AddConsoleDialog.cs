using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Views.CustomDialogs
{
    public static class AddConsoleDialog
    {
        public static string ShowDialog(IWin32Window window)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add Console",
                StartPosition = FormStartPosition.CenterScreen
            };

            Label consoleNameLabel = new Label() { Left = 20, Top = 20, Text = "Name" };
            TextBox consoleNameTextBox = new TextBox() { Left = 20, Top = 40, Width = 440 };

            Button okButton = new Button() { Text = "Ok", Left = 350, Width = 50, Top = 70 };
            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = 70, DialogResult = DialogResult.Cancel };
            
            okButton.Click += (sender, e) => {
                if (string.IsNullOrWhiteSpace(consoleNameTextBox.Text))
                {
                    ShowError(consoleNameTextBox, "Name cannot be null");
                }
                else
                {
                    prompt.DialogResult = DialogResult.OK;
                }
            };

            prompt.Controls.Add(consoleNameTextBox);
            prompt.Controls.Add(okButton);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(consoleNameLabel);

            prompt.AcceptButton = okButton;
            prompt.CancelButton = cancelButton;
            
            return prompt.ShowDialog() == DialogResult.OK ? consoleNameTextBox.Text : string.Empty;
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
