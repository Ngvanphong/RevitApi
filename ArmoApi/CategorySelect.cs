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
    public class CategorySelect : IExternalCommand
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
            Settings documentSettting = doc.Settings;
            Categories groups = documentSettting.Categories;

            string promt = "So luong categories trong revit:" + groups.Size;
            Category floorCategory = groups.get_Item(BuiltInCategory.OST_Floors);
            promt +="chon category: "+ floorCategory.Name;
            TaskDialog.Show("Revit", promt);
            return Result.Succeeded;
        }
    }
}
