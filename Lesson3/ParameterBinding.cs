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
   public class ParameterBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            ParameterRe parameter = new ParameterRe(uiapp);

            //for column architecter
            parameter.CreateParamerterRe("group_first", "locationd", BuiltInCategory.OST_Columns);

            return Result.Succeeded;
        }
    }
}
