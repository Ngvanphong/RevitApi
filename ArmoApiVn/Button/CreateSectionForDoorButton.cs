using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArmoApiVn
{
  public  class CreateSectionForDoorButton
    {
        public CreateSectionForDoorButton()
        {

        }
        public void CreateSection(UIControlledApplication application)
        {
            const string ribbonTag = "ArmoApiVn";
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
            Image img = ArmoApiVn.Properties.Resources.iconfinder_Create_132699;
            ImageSource imgSrc = EntensionMethod.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("DoorCreate", "Create", Assembly.GetExecutingAssembly().Location, "ArmoApiVn.CreateTextParameterForDoorBinding")
            {
                ToolTip = "Create section for door",
                LongDescription = "Create section for door",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            Image img2 = ArmoApiVn.Properties.Resources.iconfinder_48_62705;
            ImageSource imgSrc2 = EntensionMethod.GetImageSource(img2);
            PushButtonData btnData2 = new PushButtonData("DoorTag", "TagDoor", Assembly.GetExecutingAssembly().Location, "ArmoApiVn.CreateTextForDoorBinding")
            {
                ToolTip = "Create tag for door",
                LongDescription = "Create tag for door",
                Image = imgSrc2,
                LargeImage = imgSrc2,
            };

            Image img3 = ArmoApiVn.Properties.Resources.iconfinder_back_undo_62658;
            ImageSource imgSrc3 = EntensionMethod.GetImageSource(img3);
            PushButtonData btnData3 = new PushButtonData("UpdateDoor", "UpdateDoor", Assembly.GetExecutingAssembly().Location, "ArmoApiVn.UpdateDoorBinding")
            {
                ToolTip = "Update section and tag for door",
                LongDescription = "Update section and tag for door",
                Image = imgSrc3,
                LargeImage = imgSrc3,
            };

            Image img4 = ArmoApiVn.Properties.Resources.icons8_gear_32;
            ImageSource imgSrc4 = EntensionMethod.GetImageSource(img4);
            PushButtonData btnData4 = new PushButtonData("SettingDoor", "SettingDoor", Assembly.GetExecutingAssembly().Location, "ArmoApiVn.SettingDoor.SettingDoorBinding")
            {
                ToolTip = "Setting to create section door",
                LongDescription = "Setting to create section door",
                Image = imgSrc4,
                LargeImage = imgSrc4,
            };

            SplitButtonData splitData = new SplitButtonData("Doors", "Doors");
            SplitButton splitButton = panel.AddItem(splitData) as SplitButton;
            splitButton.IsSynchronizedWithCurrentItem = true;
            splitButton.AddPushButton(btnData);
            splitButton.AddPushButton(btnData2);
            splitButton.AddPushButton(btnData3);
            splitButton.AddPushButton(btnData4);


        }
    }
}
