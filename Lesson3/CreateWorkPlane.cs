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

namespace Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class CreateWorkPlane : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            using (Transaction t= new Transaction(doc,"Create WorkPlane")){
                t.Start();
                Plane plane = Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
                SketchPlane sp = SketchPlane.Create(doc, plane);
                doc.ActiveView.SketchPlane = sp;
                t.Commit();

            }

            return Result.Succeeded;

        }
    }
}
