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
    public class CreateTag : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Reference refrer = uidoc.Selection.PickObject(ObjectType.Element) ;
            Wall wall = doc.GetElement(refrer) as Wall;
            if(wall != null)
            {
                View view = doc.ActiveView;
                TagMode tag_mode1 = TagMode.TM_ADDBY_CATEGORY;
                TagOrientation tagOrn = TagOrientation.Horizontal;

                LocationCurve wallLoc = wall.Location as LocationCurve;
                XYZ wallStart = wallLoc.Curve.GetEndPoint(0);
                XYZ wallEnd = wallLoc.Curve.GetEndPoint(1);
                XYZ walMid = wallLoc.Curve.Evaluate(0.5, true);
                using(Transaction t= new Transaction(doc,"Create Tag"))
                {
                    t.Start();
                    IndependentTag newTag = IndependentTag.Create(doc,view.Id, refrer, true, tag_mode1, tagOrn, walMid);

                    newTag.LeaderEndCondition = LeaderEndCondition.Free;
                    XYZ elbowPnt = walMid + new XYZ(5, 5, 0);
                    newTag.LeaderElbow = elbowPnt;
                    XYZ headerPnt = walMid + new XYZ(10, 5, 0);
                    newTag.TagHeadPosition = headerPnt;
                    t.Commit();
                }
                
            }
            
            return Result.Succeeded;
        }
    }
}
