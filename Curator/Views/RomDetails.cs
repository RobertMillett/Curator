using System;
using System.IO;
using Curator.Data;
using System.Collections.Generic;
using Curator.Data.SteamDb;
using MetroFramework;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public partial class Form1
    {
        private List<string> GridPictureImageLocations;

        #region Event Handlers
        private void romDetailsNextPicture_Button_Click(object sender, EventArgs e)
        {
            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            NavigatePictures(rom, x => x + 1);
        }

        private void romDetailsPrevPicture_Button_Click(object sender, EventArgs e)
        {
            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            NavigatePictures(rom, x => x - 1);
        }

        private void romDetailsEnabledToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);
            rom.Enabled = romDetailsEnabledToggle.Checked;

            RomListViewUpdateCheckedState(rom);
        }

        private void romDetailsName_Leave(object sender, EventArgs e)
        {
            var confirm = MetroMessageBox.Show(this, "This will update the file name on disk. Continue?", "Rename ROM", MessageBoxButtons.OKCancel);

            if (confirm == DialogResult.Cancel)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            try
            {
                _romController.RenameRom(rom, romDetailsName.Text);

                _romController.LoadRoms();

                UpdateRomListViewItems();
                romListView.FocusedItem = romListView.Items.Find(_romController.RomNameConstructor(rom), false)[0];
                romListView.Items.Find(_romController.RomNameConstructor(rom), false)[0].Selected = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"Failed to overwrite ROM file name! Error: {ex.Message}", "Error!", MessageBoxButtons.OK);
            }
        }
        #endregion

        public void UpdateSelectedRomDetails(CuratorDataSet.ROMRow rom)
        {
            romDetailsName.Text = rom.Name;
            romDetailsFolder.Text = _romFolderController.GetRomFolderById(rom.RomFolder_Id).Path;
            romDetailsCustomArgs.Text = rom.CustomArgs;
            romDetailsOverride.Checked = rom.OverrideArgs;
            romDetailsEnabledToggle.Checked = rom.Enabled;
            romDetailsGridPicture.ImageLocation = rom.GridPicture;

            LoadGridPictures(rom);

            NavigatePictures(rom, x => x + 0);
        }

        private void NavigatePictures(CuratorDataSet.ROMRow rom, Func<int, int> direction)
        {
            var currentIndex = GridPictureImageLocations.IndexOf(romDetailsGridPicture.ImageLocation);

            int newIndex = GetNewIndex(currentIndex, direction);

            if (newIndex >= 0)
            {
                romDetailsGridPicture.ImageLocation = GridPictureImageLocations[newIndex];
                _romController.SetRomImage(rom, GridPictureImageLocations[newIndex]);
            }   

            romDetailsPictureIndex.Text = $"{newIndex +1} of {GridPictureImageLocations.Count}";
        }

        private int GetNewIndex(int currentIndex, Func<int, int> direction)
        {
            if (currentIndex == -1)
                return currentIndex;

            if (direction(currentIndex) > GridPictureImageLocations.Count - 1)
                return 0;

            if (direction(currentIndex) < 0)
                return GridPictureImageLocations.Count - 1;

            return direction(currentIndex);
        }

        private void LoadGridPictures(CuratorDataSet.ROMRow rom)
        {
            GridPictureImageLocations = new List<string>();

            var imageDirectory = Path.Combine(SteamGridDbClient.ImageLocation, rom.Name);
            if (!Directory.Exists(imageDirectory))
                return;

            GridPictureImageLocations.AddRange(Directory.GetFiles(imageDirectory));
        }
    }
}
