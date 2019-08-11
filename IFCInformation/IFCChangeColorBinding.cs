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
using Common.Extension;

namespace IFCInformation
{
    [Transaction(TransactionMode.Manual)]
    public class IFCChangeColorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (CheckLicenseApi.CheckLicense() == false)
            {
                return Result.Succeeded;
            }
            UIApplication uiApp = commandData.Application;           
            Document doc = uiApp.ActiveUIDocument.Document;
            IFCChangeColor changeColorClass = new IFCChangeColor();
            changeColorClass.ChangeColorIFC(uiApp);
            return Result.Succeeded;
        }
    }
}
