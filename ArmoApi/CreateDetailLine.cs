using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace ArmoApi
{
    [Transaction(TransactionMode.Manual)]
    public class CreateDetailLine: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            XYZ startPoint = new XYZ(0, 0, 0);
            XYZ endPoint = new XYZ(10, 10, 0);
            Line geomLine = Line.CreateBound(startPoint, endPoint);
            XYZ end0 = new XYZ(1, 0, 0);
            XYZ end1 = new XYZ(10, 10, 0);
            XYZ pointOnCurve = new XYZ(10, 0, 0);
            Arc geomArc = Arc.Create(end0, end1, pointOnCurve);

            XYZ normal = new XYZ(1, 1, 0);
            XYZ origin = new XYZ(0, 0, 0);

            DetailLine detailLine;
            DetailArc detailArc;
           
            using (Transaction t = new Transaction(doc,"create detailine"))
            {
                t.Start();
                //set plane working
                Plane plane = Plane.CreateByNormalAndOrigin(normal, origin);
                SketchPlane sketchPlane = SketchPlane.Create(doc, plane);
                //doc.ActiveView.SketchPlane = sketchPlane;

                //create detail
                detailLine = doc.Create.NewDetailCurve(doc.ActiveView, geomLine) as DetailLine;
                detailArc = doc.Create.NewDetailCurve(doc.ActiveView, geomArc) as DetailArc;
                t.Commit();
            }           
            return Result.Succeeded;
        }
    }
}
