using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Curator.Views.CustomDialogs
{
    public static class GridsNotFoundDialog
    {
        public static void ShowDialog()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 220,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Grids Not Found",
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label messageLabel = new Label() {
                Left = 20,
                Top = 20,
                Height = 120,
                Width = 460,
                Text = "No Grid Images were found for this ROM.\n\nPlease ensure your ROM's Steam Name exactly matches a game on https://www.steamgriddb.com\n\nIf your game does not yet exist on Steam Grid DB, you can create a new entry for it at https://www.steamgriddb.com/request and upload your own Grid Image, then click the refresh here."
            };

            Button goToSteamGridDbButton = new Button() { Text = "Open SteamGridDb", Left = 175, Width = 150, Top = 140 };

            goToSteamGridDbButton.Click += (sender, e) =>
            {
                Process.Start("https://www.steamgriddb.com");
            };

            prompt.Controls.Add(messageLabel);
            prompt.Controls.Add(goToSteamGridDbButton);

            prompt.ShowDialog();
        }
    }
}
