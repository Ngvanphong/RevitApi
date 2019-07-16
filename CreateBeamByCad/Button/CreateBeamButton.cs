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

namespace CreateBeamByCad.Button
{
   public class CreateBeamButton
    {
        public CreateBeamButton()
        {

        }
        public void CreateBeam(UIControlledApplication application)
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
            Image img = Properties.Resources.beam;
            ImageSource imgSrc = ImageButton.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("BeamCreate", "CreateBeam", Assembly.GetExecutingAssembly().Location, "CreateBeamByCad.CreateBeamBinding")
            {
                ToolTip = "Create beam by cad",
                LongDescription = "Select cad to create beam",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        }
    }
}
