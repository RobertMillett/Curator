using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Views.CustomDialogs
{
    public static class RemoveConsoleDialog
    {
        public static string ShowDialog()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Remove Console",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label consoleNameLabel = new Label() { Left = 20, Top = 20, Text = "Console" };
            var consoleList = new ComboBox() { Left = 20, Top = 40, Width = 440};
            consoleList.DropDownStyle = ComboBoxStyle.DropDownList;

            var consoles = Form1._consoleController.GetAllConsoles();
            foreach (var console in consoles)
            {
                consoleList.Items.Add(console.Name);
            }

            if (consoles.Any())
                consoleList.SelectedIndex = 0;

            Button deleteButton = new Button() { Text = "Delete", Left = 350, Width = 50, Top = 70, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 410, Width = 50, Top = 70, DialogResult = DialogResult.Cancel };
                        
            prompt.Controls.Add(consoleList);
            prompt.Controls.Add(deleteButton);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(consoleNameLabel);

            prompt.AcceptButton = deleteButton;
            prompt.CancelButton = cancelButton;

            return prompt.ShowDialog() == DialogResult.OK ? consoleList.Text : string.Empty;
        }
    }
}
