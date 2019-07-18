using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Extension;
using System.Drawing;
using System.Windows.Media;

namespace IFCInformation.Button
{
   public class ButtonChangeColor
    {
        public void ButtonChageColor(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApiVn";
            const string ribbonPanel = "IFC";
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
            Image img = Properties.Resources.iconfinder_Pie_chart_132646;
            ImageSource imgSrc = ImageButton.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("ChangeColor", "ChangeColor", Assembly.GetExecutingAssembly().Location, "IFCInformation.IFCChangeColorBinding")
            {
                ToolTip = "Change color",
                LongDescription = "Select ifc change color",
                Image = imgSrc,
                LargeImage = imgSrc,
            };
            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        }
    }
}
