﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApiVn.Common;

namespace ArmoApiVn
{
    [Transaction(TransactionMode.Manual)]
    public class UpdateTextDoorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            string name = doc.Title + "DoorSetting.xml";
            GetProperites.UpdateProperties(name);
            UpdateTextDoor updateText = new UpdateTextDoor();
            updateText.UpdateText(uiapp);
            return Result.Succeeded;
        }
    }
}
