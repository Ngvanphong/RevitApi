﻿using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ArmoApi
{
  public  class CreateSectionForDoorButton
    {
        public CreateSectionForDoorButton()
        {

        }
        public void CreateSection(UIControlledApplication application)
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
            Image img = ArmoApi.Properties.Resources.iconfinder_Create_132699;
            ImageSource imgSrc = EntensionMethod.GetImageSource(img);
            PushButtonData btnData = new PushButtonData("DoorCreate", "Create", Assembly.GetExecutingAssembly().Location, "ArmoApi.CreateTextParameterForDoorBinding")
            {
                ToolTip = "Create section for door",
                LongDescription = "Create section for door",
                Image = imgSrc,
                LargeImage = imgSrc,
            };

            PushButton button = panel.AddItem(btnData) as PushButton;
            button.Enabled = true;
        }
    }
}
