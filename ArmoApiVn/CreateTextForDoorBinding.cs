using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApiVn.Common;
using Common.Extension;

namespace ArmoApiVn
{
    [Transaction(TransactionMode.Manual)]
    public class CreateTextForDoorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //if (CheckLicenseApi.CheckLicense() == false)
            //{
            //    return Result.Succeeded;
            //}
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            string name = doc.Title + "DoorSetting.xml";
            GetProperites.UpdateProperties(name);
            CreateTextForDoor textdoor = new CreateTextForDoor(uiapp);
            bool success;
            ValueDoorText valueText= textdoor.CreateText(ParameterCommon.Door,out success);
            if (success)
            {
                CtreateXmlParameterDoor createFile = new CtreateXmlParameterDoor();
                createFile.CreateFileTextDoor(doc, valueText);
            }            
            return Result.Succeeded;
        }
    }
}
