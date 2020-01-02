using System.Linq;
using System.Windows.Forms;

namespace Curator.Views.CustomDialogs
{
    public static class PromptToFilter
    {
        public static DialogResult ShowDialog(IWin32Window window, Data.CuratorDataSet.ConsoleRow console)
        {
            var form = new Form
            {
                Width = 280,
                Height = 100,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Add File Filter?",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label extensionsList = new Label() { Left = 20, Top = 20, Width = 460, Text = "Files found with the following extensions:\n\n" };

            var roms = Form1._romController.GetRomsForConsole(console, filtered: false);
            var extensions = roms.Select(x => x.Extension).Distinct().ToList();

            foreach (var extentension in extensions)
            {
                extensionsList.Text += $"{extentension}\n";
                extensionsList.Height += 14;
            }

            form.Height += extensionsList.Height;

            var saveFilterButton = new Button { Left = 20, Width = 100, Top = form.Height - 70,
                Text = "Create Filter",
                DialogResult = DialogResult.OK
            };

            var cancelButton = new Button { Left = saveFilterButton.Left + saveFilterButton.Width + 20, Width = 100, Top = form.Height - 70,
                Text = "Done",
                DialogResult = DialogResult.Cancel
            };

            form.Controls.Add(extensionsList);
            form.Controls.Add(saveFilterButton);
            form.Controls.Add(cancelButton);

            return form.ShowDialog();
        }
    }
}
