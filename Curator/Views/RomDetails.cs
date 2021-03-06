﻿using System;
using System.Diagnostics;
using System.IO;
using Curator.Data;
using System.Collections.Generic;
using Curator.Data.SteamDb;
using MetroFramework;
using System.Threading.Tasks;
using System.Windows.Forms;
using Curator.Views.CustomDialogs;

namespace Curator
{
    public partial class Form1
    {
        private List<string> GridPictureImageLocations;
        private List<string> LibraryPictureImageLocations;

        #region Event Handlers
        private void romDetailsNextBigPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];
            NavigateGridPictures(rom, x => x + 1);
        }

        private void romDetailsPrevBigPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];
            NavigateGridPictures(rom, x => x - 1);
        }

        private void romDetailsNextLibraryPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];
            NavigateLibraryPictures(rom, x => x + 1);
        }

        private void romDetailsPrevLibraryPicture_Button_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];
            NavigateLibraryPictures(rom, x => x - 1);
        }

        private void romDetailsEnabledToggle_CheckedChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

            if (rom.Enabled != romDetailsEnabledToggle.Checked)
                rom.Enabled = romDetailsEnabledToggle.Checked;

            RomListViewUpdateCheckedState(rom);            
        }

        private void romDetailsName_Leave(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

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

        private void romDetailsCustomArgs_TextChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

            rom.CustomArgs = romDetailsCustomArgs.Text;

            romDetailsPathPreview.Text = _steamController.GetExePath(rom, ActiveConsole);
        }

        private void romDetailsOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

            if (rom.OverrideArgs != romDetailsOverride.Checked)
                rom.OverrideArgs = romDetailsOverride.Checked;

            romDetailsPathPreview.Text = _steamController.GetExePath(rom, ActiveConsole);
        }

        private void romDetails_helpToolStripButton_Click(object sender, EventArgs e)
        {
            GridsNotFoundDialog.ShowDialog();
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
            romDetailsLibraryPicture.ImageLocation = rom.LibraryPicture;
            romDetailsPathPreview.Text = _steamController.GetExePath(rom, ActiveConsole);

            await LoadGridPictures(rom);

            NavigateGridPictures(rom, x => x + 0);
            NavigateLibraryPictures(rom, x => x + 0);
        }

        private void SetAllDetailsEmpty()
        {
            romDetailsName.Text = string.Empty;
            romDetailsFolder.Text = string.Empty;
            romDetailsCustomArgs.Text = string.Empty;
            romDetailsOverride.Checked = false;
            romDetailsEnabledToggle.Checked = false;
            romDetailsGridPicture.ImageLocation = string.Empty;
            romDetailsLibraryPicture.ImageLocation = string.Empty;
            romDetailsPictureIndex.Text = string.Empty;
            romDetailsPictureIndex.Cursor = Cursors.Arrow;
            romDetailsPictureIndex.UseStyleColors = false;
            romDetailsPictureIndex.Style = MetroColorStyle.Default;
            romDetailsPathPreview.Text = string.Empty;
            romDetails_helpToolStripButton.Visible = false;
            romDetailsLibraryPictureIndex.Visible = false;            
        }

        private void NavigateGridPictures(CuratorDataSet.ROMRow rom, Func<int, int> direction)
        {
            var currentIndex = GridPictureImageLocations.IndexOf(romDetailsGridPicture.ImageLocation);

            int newIndex = GetNewIndex(currentIndex, direction,  GridPictureImageLocations);

            if (newIndex >= 0)
            {
                romDetailsGridPicture.ImageLocation = GridPictureImageLocations[newIndex];

                if (rom.GridPicture != GridPictureImageLocations[newIndex])
                    _romController.SetGridImage(rom, GridPictureImageLocations[newIndex]);
            }   

            romDetailsPictureIndex.Text = $"{newIndex} of {GridPictureImageLocations.Count -1}";

            var romDetailsPictureIndexToolTip = new ToolTip();
            romDetailsPictureIndexToolTip.Active = false;
            romDetailsPictureIndex.Style = MetroColorStyle.Default;
            romDetailsPictureIndex.UseStyleColors = false;
            romDetails_helpToolStripButton.Visible = false;

            //1 means nothing was found, as a blank image is always added to the collection
            if (GridPictureImageLocations.Count == 1)
            {
                romDetails_helpToolStripButton.Visible = true;
            }
        }

        private void NavigateLibraryPictures(CuratorDataSet.ROMRow rom, Func<int, int> direction)
        {
            var currentIndex = LibraryPictureImageLocations.IndexOf(romDetailsLibraryPicture.ImageLocation);

            int newIndex = GetNewIndex(currentIndex, direction, LibraryPictureImageLocations);

            if (newIndex >= 0)
            {
                romDetailsLibraryPicture.ImageLocation = LibraryPictureImageLocations[newIndex];

                if (rom.GridPicture != LibraryPictureImageLocations[newIndex])
                    _romController.SetLibraryImage(rom, LibraryPictureImageLocations[newIndex]);
            }

            romDetailsLibraryPictureIndex.Text = $"{newIndex} of {LibraryPictureImageLocations.Count - 1}";
            romDetailsLibraryPictureIndex.Visible = true;
        }

        private int GetNewIndex(int currentIndex, Func<int, int> direction,  List<string> imageLocations)
        {
            if (direction(currentIndex) > imageLocations.Count - 1)
                return 0;

            if (direction(currentIndex) < 0)
                return imageLocations.Count - 1;

            return direction(currentIndex);
        }

        private async Task LoadGridPictures(CuratorDataSet.ROMRow rom)
        {
            await LoadBigPictureImages(rom);
            await LoadLibraryPic(rom);
        }

        private async Task LoadBigPictureImages(CuratorDataSet.ROMRow rom)
        {
            await Task.Run(() =>
            {
                GridPictureImageLocations = new List<string> { string.Empty };

                var imageDirectory = Path.Combine(SteamGridDbClient.ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName).TrimEnd(' '), "Big Picture").TrimEnd(' ');
                if (!Directory.Exists(imageDirectory))
                    return;

                GridPictureImageLocations.AddRange(Directory.GetFiles(imageDirectory));
            });
        }

        private async Task LoadLibraryPic(CuratorDataSet.ROMRow rom)
        {
            await Task.Run(() =>
            {
                LibraryPictureImageLocations = new List<string> { string.Empty };

                var imageDirectory = Path.Combine(SteamGridDbClient.ImageLocation, Path.GetFileNameWithoutExtension(rom.FileName).TrimEnd(' '), "Library").TrimEnd(' ');
                if (!Directory.Exists(imageDirectory))
                    return;

                LibraryPictureImageLocations.AddRange(Directory.GetFiles(imageDirectory));
            });
        }

        private async void romDetailsFetchGridImageButton_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

            romDetailsPictureIndex.Text = "";
            ShowLoading($"Fetching Grid Images for ROM 1/1: '{rom.Name}'", true);

            await Task.Run(() => SteamGridDbClient.FetchGamePictures(rom));

            await Task.Run(() => LoadGridPictures(rom));

            HideLoading();

            NavigateGridPictures(rom, x => x + 0);
            NavigateLibraryPictures(rom, x => x + 0);
        }       

        private void romDetailsTestButton_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = romListRoms[romListView.FocusedItem.Index];

            var exePath = _steamController.GetExePath(rom, ActiveConsole);
            var emulatorPath = exePath.Substring(1, ActiveConsole.EmulatorPath.Length);
            var args = exePath.Substring(ActiveConsole.EmulatorPath.Length + 2);

            try
            {
                using (var myProcess = new Process())
                {
                    var directory = Path.GetDirectoryName(emulatorPath);                    

                    var startinfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = emulatorPath,
                        CreateNoWindow = true,
                        Arguments = args,
                        WorkingDirectory = directory
                    };

                    myProcess.StartInfo = startinfo;

                    myProcess.Start();
                    myProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                ShowPathTestFailureMessage(ex.Message);
            }
        }
    }
}
