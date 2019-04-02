using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator
{
    public partial class Form1
    {
        public void ShowLoading(string message, bool romDetails)
        {
            loadingPicturesSpinner.Visible = true;

            if (romDetails)
            {
                romDetailsPictureIndex.Visible = false;
                loadingRomDetailsPicture.Visible = true;
            }                

            taskLabel.Text = message;
            taskLabel.Visible = true;
        }

        public void HideLoading()
        {
            loadingPicturesSpinner.Visible = false;
            loadingRomDetailsPicture.Visible = false;
            taskLabel.Text = "";
            taskLabel.Visible = false;

            romDetailsPictureIndex.Visible = true;
        }
    }
}
