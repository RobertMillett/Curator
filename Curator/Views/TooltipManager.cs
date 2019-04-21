using System.Windows.Forms;

namespace Curator
{
    public partial class Form1
    {
        const string romDetailsFetchGridImageButton_tooltip = "Fetch Grid Image from http://www.steamgriddb.com";
        const string fetchRomsButton_tooltip = "Scan ROM Folders for new ROMs";

        public void SetToolTips()
        {
            SetToolTip(romDetailsFetchGridImageButton, romDetailsFetchGridImageButton_tooltip);
            SetToolTip(fetchRomsButton, fetchRomsButton_tooltip);
        }       

        public void SetToolTip(Control control, string message)
        {
            var tooltip = new ToolTip();
            tooltip.Active = true;
            tooltip.SetToolTip(control, message);
        }
    }
}
