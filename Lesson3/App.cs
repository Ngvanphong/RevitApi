#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media;
#endregion

namespace Lesson3
{
    class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {

            const string ribbonTag = "ArmoApi";
            const string ribbonPanel = "Example";
            try
            {
                application.CreateRibbonTab(ribbonTag);
            }
            catch (Exception ex){ }
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
            ImageSource imgSrc = GetImageSource(img);
            PushButtonData btnData = new PushButtonData("MyButton", "Hello", Assembly.GetExecutingAssembly().Location, "Lesson3.Command")
            {
                ToolTip = "Revit commant",
                LongDescription="Revit first",
                Image=imgSrc,
                LargeImage=imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
            return Result.Succeeded;
        }

        private BitmapSource GetImageSource (Image img)
        {           
            BitmapImage bmp = new BitmapImage();
            using (MemoryStream ms= new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = ms;
                bmp.EndInit();
            }
            return bmp;
        }
    }
}
