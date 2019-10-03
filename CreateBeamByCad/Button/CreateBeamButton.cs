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
            Image img = Properties.Resources.beam;
            ImageSource imgSrc = ImageButton.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("BeamCreate", "CreateBeam", Assembly.GetExecutingAssembly().Location, "CreateBeamByCad.CreateBeamBinding")
            {
                ToolTip = "Create beam by cad",
                LongDescription = "Select cad to create beam",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            Image img2 = Properties.Resources.network;
            ImageSource imgSrc2 = ImageButton.GetImageSource(img2);
            PushButtonData btnData2 = new PushButtonData("BeamCreateAll", "CreateAll", Assembly.GetExecutingAssembly().Location, "CreateBeamByCad.CreateBeamAllBinding")
            {
                ToolTip = "Create beam all",
                LongDescription = "Create beam all",
                Image = imgSrc2,
                LargeImage = imgSrc2,
            };

            SplitButtonData splitData = null;
            SplitButton splitButton = null;
            try
            {
                splitData = new SplitButtonData("Beams", "Beams");
                splitButton = panel.AddItem(splitData) as SplitButton;
            }
            catch
            {
                splitButton = panel.GetItems().Where(x => x.ItemText == "Beams").ToList().First() as SplitButton;
            };
            splitButton.IsSynchronizedWithCurrentItem = true;
            splitButton.AddPushButton(btnData);
            splitButton.AddPushButton(btnData2);

        }
    }
}
