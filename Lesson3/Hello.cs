using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Lesson3
{
  public  class Hello
    {
        public Hello()
        {

        }
        public void HelloMethod(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApi";
            const string ribbonPanel = "Example";
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
            Image img = Properties.Resources.laptop;
            ImageSource imgSrc = EntensionMethod.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("MyButton", "Hello buntton", Assembly.GetExecutingAssembly().Location, "Lesson3.GetElementR")
            {
                ToolTip = "Revit commant",
                LongDescription = "Revit first",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        }
    }
}
