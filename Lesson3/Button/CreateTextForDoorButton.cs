using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lesson3.Button
{
  public  class CreateTextForDoorButton
    {
        public void CreateText(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApi";
            const string ribbonPanel = "DoorSection";
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
            Image img = Properties.Resources.iconfinder_48_62705;
            ImageSource imgSrc = EntensionMethod.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("DoorTag", "TagDoor", Assembly.GetExecutingAssembly().Location, "Lesson3.CreateTextForDoorBinding")
            {
                ToolTip = "Create tag for door",
                LongDescription = "Create tag for door",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        }
    }
}
