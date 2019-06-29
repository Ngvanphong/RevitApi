using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
namespace Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class GetElementR : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //get application from commandData
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            //get document acttive
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;
            //get document setting to get category
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ElementCategoryFilter filters = new ElementCategoryFilter(BuiltInCategory.OST_Walls);
            IList<Element> walls = collector.WherePasses(filters).WhereElementIsNotElementType().ToElements();
            string prompt = "tuong hien tai du an:\n";
            foreach(var item in walls)
            {
                prompt = prompt + item.Name + "\n";
            };
            TaskDialog.Show("Revit",prompt);
            return Result.Succeeded;  
            
        }
    }
}
