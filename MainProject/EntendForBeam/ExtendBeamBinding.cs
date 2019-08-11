using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using MainProject.EntendForBeam;
using Common.Extension;

namespace MainProject
{
    [Transaction(TransactionMode.Manual)]
    public class ExtendBeamBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (CheckLicenseApi.CheckLicense() == false)
            {
                return Result.Succeeded;
            }
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            AppPanelExtend.ShowExtendForm(uiapp);

            return Result.Succeeded;
        }
    }
}
