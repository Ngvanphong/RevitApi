using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace Lesson3
{
    [Transaction(TransactionMode.Manual)]
    class GetElementMonitor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            Application app = uIApp.Application;
            UIDocument uIDoc = uIApp.ActiveUIDocument;
            Document doc = uIDoc.Document;

            Category cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_MechanicalEquipment);

            Reference refer = uIDoc.Selection.PickObject(ObjectType.Element, new SelectionFilterCategory(cat));

            Element element = doc.GetElement(refer);

            IList<Element> referRec = uIDoc.Selection.PickElementsByRectangle(new SelectionFilterCategory(cat));

            return Result.Succeeded;
        }
    }
}
