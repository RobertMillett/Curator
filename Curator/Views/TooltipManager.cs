using System.Windows.Forms;

namespace Curator
{
    public partial class Form1
    {
        const string romDetailsFetchGridImageButton_tooltip = "Fetch Grid Image from http://www.steamgriddb.com";
        const string fetchRomsButton_tooltip = "Scan ROM Folders for new ROMs";
        const string emulatorFlags_tooltip = "Command-line Arguments that your Emulator uses to change the way it launches.";
        const string romFlags_tooltip = "Command-line Arguments added after the ROM path. This setting effects all ROMs for this Console!";
        const string additionalRomFlags_tooltip = "Command-line Arguments added after the ROM path. This setting will only effect this ROM.";
        const string overrideRomFlags_tooltip = "Ticked: This ROM will use its own ROM Flags (if any) instead of the Console ROM Flags.\nUnticked: This ROM will use its own ROM Flags (if any) in addition to the Console ROM Flags (if any).";

        public void SetToolTips()
        {
            SetToolTip(romDetailsFetchGridImageButton, romDetailsFetchGridImageButton_tooltip);
            SetToolTip(fetchRomsButton, fetchRomsButton_tooltip);
            SetToolTip(emulatorFlags_LabelBox, emulatorFlags_tooltip);
            SetToolTip(romFlags_LabelBox, romFlags_tooltip);
            SetToolTip(romDetailsAdditionalRomFlags_labelBox, additionalRomFlags_tooltip);
            SetToolTip(romDetailsOverride, overrideRomFlags_tooltip);
        }       

        public void SetToolTip(Control control, string message)
        {
            var tooltip = new ToolTip();
            tooltip.Active = true;
            tooltip.SetToolTip(control, message);
        }
    }
}
