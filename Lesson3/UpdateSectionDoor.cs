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
using System.IO;
using System.Xml.Linq;

namespace Lesson3
{
    public class UpdateSectionDoor
    {
        public void UpdateMoveDoor(UIApplication uiApp, string idDoorString)
        {
            Document doc = uiApp.ActiveUIDocument.Document;
            ElementId doorId = new ElementId(int.Parse(idDoorString));
            FamilyInstance doorElement = doc.GetElement(doorId) as FamilyInstance;
            LocationPoint location = doorElement.Location as LocationPoint;
            XYZ pointDoor = location.Point;
            string pointNewString = pointDoor.ToString().TrimStart('(').TrimEnd(')').Replace(" ", string.Empty);
            var d = doorElement.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();
            var height = doorElement.Symbol.get_Parameter(BuiltInParameter.DOOR_HEIGHT).AsDouble();
            FilteredElementCollector filters = new FilteredElementCollector(doc).OfClass(typeof(ViewSection));
            Element sectionDoor = null;
            foreach (var filter in filters)
            {
                Element elementsec = filter as Element;
                if (elementsec.LookupParameter("DoorId").AsString() == idDoorString)
                {
                    sectionDoor = elementsec;
                    break;
                }

            }
            string pointSection = sectionDoor.LookupParameter("LocationDoor").AsString();
            string[] stringXYZ = pointSection.Split(',');
            XYZ pointOld = new XYZ(double.Parse(stringXYZ[0]), double.Parse(stringXYZ[1]), double.Parse(stringXYZ[2]));
            if (Math.Round(pointOld.X * 1000 * 0.3048) != Math.Round(pointDoor.X * 1000 * 0.3048)
                || Math.Round(pointOld.Y * 1000 * 0.3048) != Math.Round(pointDoor.Y * 1000 * 0.3048)
                || Math.Round(pointOld.Z * 1000 * 0.3048) != Math.Round(pointDoor.Z * 1000 * 0.3048))
            {
                using (Transaction t = new Transaction(doc, "Move Section"))
                {
                    t.Start();
                    XYZ v = pointDoor - pointOld;
                    ElementTransformUtils.MoveElement(doc, new ElementId(int.Parse(sectionDoor.Id.ToString())-1), v);
                    t.Commit();
                }
                
                ParameterRe parameter = new ParameterRe(uiApp);
                var parameterfind = sectionDoor.LookupParameter("LocationDoor");
                parameter.SetValueParameter(parameterfind, pointNewString);

            }


        }
    }
}
