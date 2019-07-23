using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Common.Extension;

namespace MainProject.Button
{
   public class ExtendBeamButton
    {
        public void ExtendButton(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApiVn";
            const string ribbonPanel = "CreateBeam";
            try
            {
                application.CreateRibbonTab(ribbonTag);
            }
            catch (Exception ex) { }
            RibbonPanel panel = null;
            List<RibbonPanel> panels = application.GetRibbonPanels(ribbonTag);
            foreach (RibbonPanel pl in panels)
            {
                if (pl.Name == ribbonPanel)
                {
                    panel = pl;
                    break;
                }
            }
            if (panel == null)
            {
                panel = application.CreateRibbonPanel(ribbonTag, ribbonPanel);
            }
            Image img = Properties.Resources.full_screen_arrows;
            ImageSource imgSrc = ImageButton.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("ExtendBeam", "ExtendBeam", Assembly.GetExecutingAssembly().Location, "MainProject.ExtendBeamBinding")
            {
                ToolTip = "Extend beam",
                LongDescription = "Extend beam auto",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;

        }
    }
}
