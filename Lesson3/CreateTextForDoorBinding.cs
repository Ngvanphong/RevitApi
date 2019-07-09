﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Lesson3.Common;

namespace Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class CreateTextForDoorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            CreateTextForDoor textdoor = new CreateTextForDoor(uiapp);
            ValueDoorText valueText= textdoor.CreateText(ParameterCommon.Door);
            CtreateXmlParameterDoor createFile = new CtreateXmlParameterDoor();
            createFile.CreateFileTextDoor(doc, valueText);
            return Result.Succeeded;
        }
    }
}
