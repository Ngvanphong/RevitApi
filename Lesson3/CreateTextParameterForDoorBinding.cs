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
    public class CreateTextParameterForDoorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<FamilyElement> listElemtParamenter = new List<FamilyElement>();
            LookupParamaterRe lookupClass = new LookupParamaterRe(uiapp);
            listElemtParamenter = lookupClass.LookValuePramater(ParameterCommon.Door, BuiltInCategory.OST_Doors);
            CtreateXmlParameterDoor createFileclass = new CtreateXmlParameterDoor();
            createFileclass.CreateFileDoor(doc,listElemtParamenter);
            CreateSectionByElementId createViewClass = new CreateSectionByElementId(uiapp);
            createViewClass.CreteListSection(listElemtParamenter);          
            return Result.Succeeded;
        }
    }
}
