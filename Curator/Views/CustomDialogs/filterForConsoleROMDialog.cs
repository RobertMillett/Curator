using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace Curator.Views.CustomDialogs
{
    public static class FilterForConsoleROMDialog
    {
        private static Data.CuratorDataSet.ConsoleRow selectedConsole;

        public static DialogResult ShowDialog(IWin32Window window)
        {
            Form prompt = new Form()
            {
                Width = 220,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add Filter",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label consoleNameLabel = new Label() { Left = 18, Top = 20, Text = "Select Console" };
            var consoleSelector = new ComboBox() { Left = 20, Top = 40, Width = 150 };
            consoleSelector.Items.AddRange(Form1._consoleController.GetAllConsoles().Select(x => x.Name).ToArray());
            consoleSelector.DropDownStyle = ComboBoxStyle.DropDownList;

            var fileExtensionChecklist = new CheckedListBox
            {
                Left = 20,
                Top = 103,
                Height = 30
            };

            var filterLabel = new Label
            {
                Left = 18,
                Top = 80,
                Width = 200,
                Text = "File Extensions:"
            };

            consoleSelector.SelectedIndexChanged += (sender, e) =>
            {
                selectedConsole = Form1._consoleController.GetAllConsoles().Where(x => x.Name == consoleSelector.Text).FirstOrDefault();
                var roms = Form1._romController.GetRomsForConsole(selectedConsole, filtered: false);
                var extensions = roms.Select(x => x.Extension).Distinct().ToList();
                
                fileExtensionChecklist.Items.Clear();
                fileExtensionChecklist.Items.AddRange(extensions.ToArray());

                var height = 15;
                if (fileExtensionChecklist.Items.Count > 0)
                {
                    height = fileExtensionChecklist.GetItemRectangle(0).Height * fileExtensionChecklist.Items.Count;
                }

                fileExtensionChecklist.ClientSize = new Size(fileExtensionChecklist.ClientSize.Width, height);

                prompt.Height = (fileExtensionChecklist.Items.Count * 15) + 200;
            };

            consoleSelector.SelectedIndexChanged += (sender, e) =>
            {
                for (var i = 0; i < fileExtensionChecklist.Items.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(selectedConsole.Filter))
                    {
                        if (selectedConsole.Filter.Contains(fileExtensionChecklist.GetItemText(fileExtensionChecklist.Items[i])))
                            fileExtensionChecklist.SetItemChecked(i, true);
                        continue;
                    }

                    fileExtensionChecklist.SetItemChecked(i, true);
                }
            };

            if (Form1.ActiveConsole != null)
                consoleSelector.SelectedItem = consoleSelector.Items[consoleSelector.Items.IndexOf(Form1.ActiveConsole.Name)];

            Button saveFilterButton = new Button() { Text = "Save Filter", Left = 20, Width = 100, Top = prompt.Height - 70 };

            saveFilterButton.Click += (sender, e) =>
            {
                var error = false;
                if (fileExtensionChecklist.CheckedItems.Count == 0)
                {
                    ShowError(fileExtensionChecklist, "At least 1 extension must be allowed");
                    error = true;
                }

                if (string.IsNullOrWhiteSpace(consoleSelector.Text))
                {
                    ShowError(consoleSelector, "Please select a Console");
                    error = true;
                } 

                if (error)
                    return;

                string fileExtensions = "";

                foreach (var item in fileExtensionChecklist.CheckedItems)
                {
                    fileExtensions += fileExtensionChecklist.GetItemText(item) + ',';
                }

                fileExtensions = fileExtensions.TrimEnd(',');

                Form1._consoleController.SaveFilterForConsole(selectedConsole, fileExtensions);

                prompt.DialogResult = DialogResult.OK;
                return;
            };

            Button cancelButton = new Button() { Text = "Cancel", Left = 130, Width = 50, Top = prompt.Height - 70, DialogResult = DialogResult.Cancel };            
           
            prompt.ClientSizeChanged += (sender, e) =>
            {
                cancelButton.Top = prompt.Height - 70;
                saveFilterButton.Top = prompt.Height - 70;
            };

            prompt.Controls.Add(consoleSelector);
            prompt.Controls.Add(saveFilterButton);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(consoleNameLabel);
            prompt.Controls.Add(filterLabel);
            prompt.Controls.Add(fileExtensionChecklist);

            prompt.AcceptButton = saveFilterButton;
            prompt.CancelButton = cancelButton;
            
            return prompt.ShowDialog();
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
