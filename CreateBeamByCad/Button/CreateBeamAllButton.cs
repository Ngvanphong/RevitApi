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
   public class CreateBeamAllButton
    {
        public void CreateButtonAll(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApiVn";
            const string ribbonPanel = "Beam";
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
            Image img = Properties.Resources.nine_black_tiles;
            ImageSource imgSrc = ImageButton.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("BeamCreateAll", "CreateAll", Assembly.GetExecutingAssembly().Location, "CreateBeamByCad.CreateBeamAllBinding")
            {
                ToolTip = "Create beam all",
                LongDescription = "Create beam all",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        
    }
    }
}
