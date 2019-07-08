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

        private void romDetailsCustomArgs_TextChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            rom.CustomArgs = romDetailsCustomArgs.Text;

            romDetailsPathPreview.Text = _steamController.GetExePath(rom, ActiveConsole);
        }

        private void romDetailsOverride_CheckedChanged(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

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
            romDetailsPathPreview.Text = _steamController.GetExePath(rom, ActiveConsole);

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
            romDetailsPictureIndex.Cursor = Cursors.Arrow;
            romDetailsPictureIndex.UseStyleColors = false;
            romDetailsPictureIndex.Style = MetroColorStyle.Default;
            romDetailsPathPreview.Text = string.Empty;
            romDetails_helpToolStripButton.Visible = false;
        }

        private void NavigatePictures(CuratorDataSet.ROMRow rom, Func<int, int> direction)
        {
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
            romDetailsPictureIndex.Style = MetroColorStyle.Default;
            romDetailsPictureIndex.UseStyleColors = false;
            romDetails_helpToolStripButton.Visible = false;

            //1 means nothing was found, as a blank image is always added to the collection
            if (GridPictureImageLocations.Count == 1)
            {
                romDetails_helpToolStripButton.Visible = true;
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

        private async void romDetailsFetchGridImageButton_Click(object sender, EventArgs e)
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

        private void romDetailsTestButton_Click(object sender, EventArgs e)
        {
            if (romListView.FocusedItem == null)
                return;

            var rom = _romController.GetRom(romListView.FocusedItem.Text);

            var exePath = _steamController.GetExePath(rom, ActiveConsole);
            var emulatorPath = exePath.Substring(1, ActiveConsole.EmulatorPath.Length);
            var args = exePath.Substring(ActiveConsole.EmulatorPath.Length + 2);

            try
            {
                using (var myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = emulatorPath;
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.Arguments = args;

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
