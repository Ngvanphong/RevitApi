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
namespace ArmoApiVn
{
    [Transaction(TransactionMode.Manual)]
    public class CreateSectionViewClick : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
           
            Reference refWal = uidoc.Selection.PickObject(ObjectType.Element);
            Wall wall = doc.GetElement(refWal) as Wall;
            if (wall != null)
            {
                LocationCurve locationCurve = wall.Location as LocationCurve;
                Curve curve = locationCurve.Curve;
                Line lineWall = curve as Line;
                ViewFamilyType familyView = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Where(x => x.Name == "Building Section").First() as ViewFamilyType;
                XYZ p = lineWall.GetEndPoint(0);
                XYZ q = lineWall.GetEndPoint(1);
                XYZ v = q - p;
                BoundingBoxXYZ bb = wall.get_BoundingBox(null);
                Double minZ = bb.Min.Z;
                Double maxZ = bb.Max.Z;
                Double w = v.GetLength();
                Double h = maxZ - minZ;
                Double d = wall.WallType.Width;
                Double offset = 0.1 * w;
                XYZ min = new XYZ(-2*w, minZ - offset, 0);
                XYZ max = new XYZ(2*w, maxZ + offset, offset);
                XYZ midpoint = p + 0.5 * v;
                XYZ walldir = v.Normalize();
                XYZ up = XYZ.BasisZ;
                XYZ vierdir = walldir.CrossProduct(up);
                Transform tTran = Transform.Identity;
                tTran.Origin = midpoint;
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
                   ViewSection viewGoto= ViewSection.CreateSection(doc, familyView.Id, sectionBox);                
                    t.Commit();
                    uidoc.ActiveView = viewGoto;
                    uidoc.RefreshActiveView();
                }
      
            }
            return Result.Succeeded;

        }
    }
}
