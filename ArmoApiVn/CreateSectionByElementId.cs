using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.IO;
using ArmoApiVn.Common;
using Common.Extension;

namespace ArmoApiVn
{
    [Transaction(TransactionMode.Manual)]
    public class CreateSectionByElementId
    {
        public UIApplication _uiApp;
        public CreateSectionByElementId(UIApplication uiApp)
        {
            _uiApp = uiApp;
        }

        public bool CreteListSection(List<FamilyElement> elements)
        {          
            bool result = true;
            try
            {
                Document doc = _uiApp.ActiveUIDocument.Document;
                foreach (var item in elements)
                {
                    try
                    {
                        FamilyInstance element = doc.GetElement(item.ElementIdSection) as FamilyInstance;
                        var width = element.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();//check again
                        var height = element.Symbol.get_Parameter(BuiltInParameter.DOOR_HEIGHT).AsDouble();//check again
                        if (height == 0)
                        {
                            height = 8.62;
                        }
                        var hostWall = element.Host;
                        var localPonint = element.Location as LocationPoint;
                        LocationCurve locationCurve = hostWall.Location as LocationCurve;
                        Curve curve = locationCurve.Curve;
                        Line lineWall = curve as Line;
                        ViewFamilyType familyView = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Where(x => x.Name == ParameterCommon.SectionStypeChose).First() as ViewFamilyType;
                        BoundingBoxXYZ bb = element.get_BoundingBox(null);
                        Double d = width;
                        XYZ pt = lineWall.GetEndPoint(0);
                        XYZ qt = lineWall.GetEndPoint(1);
                        XYZ v = qt - pt;
                        XYZ md = null;
                        Double offset = 0.05 * d;
                        XYZ min = new XYZ(-2 * d, -height, -offset);
                        XYZ max = new XYZ(2 * d, 2 * height, offset);
                        try
                        {
                            md = localPonint.Point;//check agian
                        }
                        catch
                        {
                            md = (pt+qt) / 2;
                            d = Math.Sqrt((v.X * v.X + v.Y * v.Y + v.Z * v.Z));
                            offset = 1.436;
                            min = new XYZ(-d/2, -height, -offset);
                            max = new XYZ(d/2, 2 * height, offset);
                        }
                        //XYZ p;
                        //XYZ q;
                        ////Get location two point of door
                        //if (v.X == 0)
                        //{
                        //    p = new XYZ(md.X, md.Y - d / 2, md.Z);
                        //    q = new XYZ(md.X, md.Y + d / 2, md.Z);
                        //}else if (v.Y == 0)
                        //{
                        //    p = new XYZ(md.X - d / 2, md.Y, md.Z);
                        //    q = new XYZ(md.X + d / 2, md.Y, md.Z);
                        //}else
                        //{
                        //    var a = d*d / (1 + v.Y*v.Y/(v.X*v.X));
                        //    p = new XYZ(md.X + Math.Sqrt(a) / 2, md.Y + v.Y * Math.Sqrt(a) / (2 * v.X),md.Z);
                        //    q = new XYZ(md.X - Math.Sqrt(a) / 2, md.Y - v.Y * Math.Sqrt(a) / (2 * v.X), md.Z);
                        //}
                        //XYZ vc = q - p;
                        XYZ walldir = v.Normalize();
                        XYZ up = XYZ.BasisZ;
                        XYZ vierdir = walldir.CrossProduct(up);
                        Transform tTran = Transform.Identity;
                        tTran.Origin = md;
                        tTran.BasisX = walldir;
                        tTran.BasisY = up;
                        tTran.BasisZ = vierdir;
                        BoundingBoxXYZ sectionBox = new BoundingBoxXYZ();
                        sectionBox.Transform = tTran;
                        sectionBox.Min = min;
                        sectionBox.Max = max;

                        using (Transaction t = new Transaction(doc))
                        {
                            t.Start("Create Section");
                                ViewSection view = ViewSection.CreateSection(doc, familyView.Id, sectionBox);
                                view.Name = element.Id.ToString();
                            t.Commit();
                        }
                        ParameterRe parameter = new ParameterRe(_uiApp);
                        string pointDoor = md.ToString().TrimStart('(').TrimEnd(')').Replace(" ", string.Empty);

                        Element viewElement = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Views)
                            .Where(x => x.Name == element.Id.ToString()).First();
                        var parameterfind = viewElement.LookupParameter("LocationDoor");

                        if (parameterfind == null)
                        {
                            parameter.CreateParamerterRe("Door", "LocationDoor", BuiltInCategory.OST_Views);
                            parameterfind = viewElement.LookupParameter("LocationDoor");
                        }
                        parameter.SetValueParameter(parameterfind, pointDoor);

                        var parameterDoorId = viewElement.LookupParameter("DoorId");
                        if (parameterDoorId == null)
                        {
                            parameter.CreateParamerterRe("Door", "DoorId", BuiltInCategory.OST_Views);
                            parameterDoorId = viewElement.LookupParameter("DoorId");
                        }

                        parameter.SetValueParameter(parameterDoorId, item.ElementIdSection.ToString());
                    }
                    catch
                    {
                        TaskDialog.Show("Error", "Id of error door is: "+item.ElementIdSection);
                        result = false;
                        return result;
                    }
                    

                }
            }
            catch(Exception ex)
            {
                result = false;
                TaskDialog.Show("Error", "You must delete section");
                return result;
            }
            return result;

        }

        public void CreteSectionForOneElement(FamilyElement familiInstance)
        {
            Document doc = _uiApp.ActiveUIDocument.Document;

            FamilyInstance element = doc.GetElement(familiInstance.ElementIdSection) as FamilyInstance;
            var width = element.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();
            var height = element.Symbol.get_Parameter(BuiltInParameter.DOOR_HEIGHT).AsDouble();
            if (height == 0)
            {
                height = 8.62;
            }
            var localPonint = element.Location as LocationPoint;
            var hostWall = element.Host;
            LocationCurve locationCurve = hostWall.Location as LocationCurve;
            Curve curve = locationCurve.Curve;
            Line lineWall = curve as Line;
            ViewFamilyType familyView = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Where(x => x.Name == ParameterCommon.SectionStypeChose).First() as ViewFamilyType;
            BoundingBoxXYZ bb = element.get_BoundingBox(null);
            Double d = width;
            XYZ pt = lineWall.GetEndPoint(0);
            XYZ qt = lineWall.GetEndPoint(1);
            XYZ v = qt - pt;
            XYZ md = null;
            Double offset = 0.05 * d;
            XYZ min = new XYZ(-2 * d, -height, -offset);
            XYZ max = new XYZ(2 * d, 2 * height, offset);
            try
            {
                md = localPonint.Point;
            }
            catch
            {
                md = (pt + qt) / 2;
                d = Math.Sqrt((v.X * v.X + v.Y * v.Y + v.Z * v.Z));
                offset = 1.436;
                min = new XYZ(-d / 2, -height, -offset);
                max = new XYZ(d / 2, 2 * height, offset);
            }
            
            XYZ walldir = v.Normalize();
            XYZ up = XYZ.BasisZ;
            XYZ vierdir = walldir.CrossProduct(up);
            Transform tTran = Transform.Identity;
            tTran.Origin = md;
            tTran.BasisX = walldir;
            tTran.BasisY = up;
            tTran.BasisZ = vierdir;
            BoundingBoxXYZ sectionBox = new BoundingBoxXYZ();
            sectionBox.Transform = tTran;
            sectionBox.Min = min;
            sectionBox.Max = max;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create Section");
                ViewSection view = ViewSection.CreateSection(doc, familyView.Id, sectionBox);
                view.Name = element.Id.ToString();
                t.Commit();
            }
            ParameterRe parameter = new ParameterRe(_uiApp);
            string pointDoor = md.ToString().TrimStart('(').TrimEnd(')').Replace(" ", string.Empty);

            Element viewElement = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Views)
                .Where(x => x.Name == element.Id.ToString()).First();
            var parameterfind = viewElement.LookupParameter("LocationDoor");          
            if (parameterfind == null)
            {
                parameter.CreateParamerterRe("Door", "LocationDoor", BuiltInCategory.OST_Views);
                parameterfind = viewElement.LookupParameter("LocationDoor");
               
            }           
            parameter.SetValueParameter(parameterfind, pointDoor);
            var parameterDoorId = viewElement.LookupParameter("DoorId");
            if (parameterDoorId == null)
            {
                parameter.CreateParamerterRe("Door", "DoorId", BuiltInCategory.OST_Views);
                parameterDoorId = viewElement.LookupParameter("DoorId");
            }
            parameter.SetValueParameter(parameterDoorId, familiInstance.ElementIdSection.ToString());


        }
    }
}
