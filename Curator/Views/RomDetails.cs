using System;
using System.IO;
using Curator.Data;
using System.Collections.Generic;
using Curator.Data.SteamDb;
using MetroFramework;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Curator
{
    public partial class Form1
    {
        private List<string> GridPictureImageLocations;

        #region Event Handlers
        private void romDetailsNextPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            NavigatePictures(rom, x => x + 1);
        }

        private void romDetailsPrevPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            NavigatePictures(rom, x => x - 1);
        }

        private void romDetailsEnabledToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            if (rom.Enabled != romDetailsEnabledToggle.Checked)
                rom.Enabled = romDetailsEnabledToggle.Checked;

            RomListViewUpdateCheckedState(rom);
        }

        private void romDetailsName_Leave(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;            

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            try
            {
                _romController.RenameRom(rom, romDetailsName.Text);

                UpdateRomListViewItems();
                romListView.FocusedItem = romListView.Items.Find(_romController.RomNameConstructor(rom), false)[0];
                romListView.Items.Find(_romController.RomNameConstructor(rom), false)[0].Selected = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"Failed to overwrite ROM file name! Error: {ex.Message}", "Error!", MessageBoxButtons.OK);
            }
        }

        private void romDetailsCustomArgs_Leave(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            rom.CustomArgs = romDetailsCustomArgs.Text;
        }
        #endregion

        public async void UpdateSelectedRomDetails(CuratorDataSet.ROMRow rom)
        {
            if (rom == null)
            {
                SetAllDetailsEmpty();
                return;
            }                

            romDetailsName.Text = rom.Name;
            romDetailsFolder.Text = _romFolderController.GetRomFolderById(rom.RomFolder_Id).Path;
            romDetailsCustomArgs.Text = rom.CustomArgs;
            romDetailsOverride.Checked = rom.OverrideArgs;
            romDetailsEnabledToggle.Checked = rom.Enabled;
            romDetailsGridPicture.ImageLocation = rom.GridPicture;

            await LoadGridPictures(rom);

            NavigatePictures(rom, x => x + 0);
        }

        private void SetAllDetailsEmpty()
        {
            romDetailsName.Text = string.Empty;
            romDetailsFolder.Text = string.Empty;
            romDetailsCustomArgs.Text = string.Empty;
            romDetailsOverride.Checked = false;
            romDetailsEnabledToggle.Checked = false;
            romDetailsGridPicture.ImageLocation = string.Empty;
            romDetailsPictureIndex.Text = string.Empty;
        }

        private void NavigatePictures(CuratorDataSet.ROMRow rom, Func<int, int> direction)
        {
            romDetailsPictureIndex.Enabled = false;

            var currentIndex = GridPictureImageLocations.IndexOf(romDetailsGridPicture.ImageLocation);

            int newIndex = GetNewIndex(currentIndex, direction);

            if (newIndex >= 0)
            {
                romDetailsGridPicture.ImageLocation = GridPictureImageLocations[newIndex];

                if (rom.GridPicture != GridPictureImageLocations[newIndex])
                    _romController.SetRomImage(rom, GridPictureImageLocations[newIndex]);
            }   

            romDetailsPictureIndex.Text = $"{newIndex} of {GridPictureImageLocations.Count -1}";

            var romDetailsPictureIndexToolTip = new ToolTip();
            romDetailsPictureIndexToolTip.Active = false;

            //1 means nothing was found, as a blank image is always added to the collection
            if (GridPictureImageLocations.Count == 1)
            {
                romDetailsPictureIndex.Enabled = true;
                romDetailsPictureIndex.Click += romDetailsPictureIndex_Click;
                romDetailsPictureIndex.Text += " *";
                romDetailsPictureIndexToolTip.Active = true;
                romDetailsPictureIndexToolTip.SetToolTip(romDetailsPictureIndex, "This ROM was either not found, or has no images associated with it.\nPlease check that your ROM name exactly matches a game on http://www.steamgriddb.com");
            }
        }

        private int GetNewIndex(int currentIndex, Func<int, int> direction)
        {
            if (direction(currentIndex) > GridPictureImageLocations.Count - 1)
                return 0;

            if (direction(currentIndex) < 0)
                return GridPictureImageLocations.Count - 1;

            return direction(currentIndex);
        }

        private async Task LoadGridPictures(CuratorDataSet.ROMRow rom)
        {
            await Task.Run(() =>
            {
                GridPictureImageLocations = new List<string> { string.Empty };

                var imageDirectory = Path.Combine(SteamGridDbClient.ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName));
                if (!Directory.Exists(imageDirectory))
                    return;

                GridPictureImageLocations.AddRange(Directory.GetFiles(imageDirectory));
            });            
        }

        private async void metroButton1_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            romDetailsPictureIndex.Text = "";
            ShowLoading($"Fetching Grid Images for ROM 1/1: '{rom.Name}'", true);

            await Task.Run(() => SteamGridDbClient.FetchGamePictures(rom));

            await Task.Run(() => LoadGridPictures(rom));

            HideLoading();

            NavigatePictures(rom, x => x + 0);
        }

        private void romDetailsPictureIndex_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.steamgriddb.com/");
        }
    }
}
