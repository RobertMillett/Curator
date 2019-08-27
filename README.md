# Curator
A Windows Application to help with importing your emulation collection into Steam, for easy viewing / playing.

*Note: Curator is not intended to handle very large collections, but does have bulk ROM import and bulk image download functionality.*

## How to use

1. Open curator.exe, it will prompt you to find your shortcuts.vdf file (usually located in C:\programs(x86)\steam\userdata\\{userId}\config)
2. Add a Console using one of the below methods:
	* Console Header Menu -> Add
	* Type your console name in the Console Dropdown box  -> click the '+' icon
3. Add the path to your console's emulator by clicking the Open Folder button next to the Emulator Path text box
4. Add the path to a folder containing your console's ROMs by clicking the Open Folder button next to the ROM Folders text box
	* You can add as many ROM Folders as you like. Curator does not traverse down sub-folders looking for ROMs.
5. Go through the list of files in the ROMs list and choose which ones you want to import to Steam (by default all files found in Step 4 will be imported to Steam, this is likely not what you want as many ROMs come with multiple files, only one of which is bootable):
	* Click on an entry in the ROMs list
	* In the ROM Details panel, switch the Enabled switch to OFF if you don't want this file imported into Steam.
6. Export your ROMs to Steam using one of the below methods: 
	* Click on the Steam Header Menu -> Export ROMs to Steam 
    * Closing the program will prompt you to export to Steam 
7. Restart Steam (if it was running)
    
  ___

### Additional Extras
* Curator will tag each ROM imported to Steam with the name of the Console it runs on for easier organisation.
* Curator will automatically backup your 'shorcuts.vdf' files. A folder named Curator Backups is created in the same directory as your 'shortcuts.vdf' file and contains time-stamped versions of each modification - in case you ever need to go back to an earlier version.
* Curator can automatically download Steam Grid Images for all your ROMs. A guide on doing so is in its own section below.
* You will likely want to set Emulator Flags to improve your experience. These are command-line options that will be used when creating the shortcut, and depending on your Emulator can do different things like open in full-screen, open without popping up the Emulator UI, and other useful features. For a list of valid Emulator Flags visit the documentation pages of your Emulator. E.g. Dolphin's Emulator Flags are located here: https://wiki.dolphin-emu.org/index.php?title=Help:Contents#Command_Line_Options
* You can also specify "Console ROM Flags". These are command-line options that will apply to all ROMs for a given Console. These are the same as Emulator Flags but are placed after the ROM path in the shortcut. Depending on your Emulator these can do different things like launching ROMs with their own config files, or resolutions. Valid ROM Flags will also be located on the documentation pages of your Emulator but are not supported by all Emulators.
* You can also specify ROM Flags that will only be applied to a single ROM. In the ROM Details panel add the ROM Flag you want to be applied to only this ROM in the "Additional ROM Flags" text box.  
  * "Additional ROM Flags" can also override, or combine with, the "Console ROM Flags". Ticking the "Override Console ROM Flags" box in the ROM Details panel will make this specific ROM ignore whatever is set in the "Console ROM Flags" text box, and use it's own ROM Flags instead. Leaving this un-ticked will result in this ROM using both the "Console ROM Flags" and it's own ROM Flags together.
* You can preview the shortcut that will be imported into Steam by clicking on a ROM in the ROM List and viewing the "Combined Shortcut Preview" text box in the ROM Details panel. This preview is a formatted as: "{Emulator Path} {Emulator Flags} {ROM Path} {ROM Flags}". Emulator Flags, and ROM Flags are optional, and may be blank.
  * You can test if the shortcut that will be imported into Steam will work by clicking the "Test!" button in the ROM Details Panel when you have a ROM selected. If an error is displayed, check your paths / Flags in Curator and your Emulator's default settings.
  
  ___
  
### Downloading Steam Grid Images
* Grid Images are downloaded from [Steam Grid DB](http://steamgriddb.com). 
* Images are only downloaded for ROMs that you have Enabled in the ROM Details Panel
* In order for the download to succeed your ROM's Steam Name must exactly match a game on Steam Grid DB.
* You can change your ROM's Steam Name in the ROM Details Panel, this will not change the file-name.
* Curator does not support setting local images as your Steam Grid pictures.
* If your game is not on Steam Grid DB, you can easily add it to their website, and upload your own Steam Grid.
* Curator will download all Grid Images associated to a matching game.
* You can choose which image you want to see in Steam by clicking the arrow buttons in the ROM Details Panel in Curator
* Images are stored in C:\Users\{username}\AppData\Roaming\Curator\Images if you want to delete them

To Download Grid Images for **all** currently Enabled ROMs:
* Click on the Steam Header Menu -> Get Grid Pictures
* Nb - If you have a large collection, this could take a long time!

To Download Grid Images for a single ROM:
* Click on the ROM in the ROM List to view it in the ROM Details Panel
* Click on the Refresh button in the ROM Details Panel

___

### How it works
Curator works by modifying the 'shortcuts.vdf' file created and managed by Steam. For each ROM you have chosen to import, an entry will be added to this file using the following format:

`{path-to-emulator} {emulator-flags*} {path-to-rom} {rom-flags*}`

\* \- optional

This is the same as using the 'Add a Non-Steam Game to My Library' function in Steam.
