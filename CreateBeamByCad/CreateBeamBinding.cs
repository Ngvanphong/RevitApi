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

namespace CreateBeamByCad
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamBinding : IExternalCommand
    {
        public frmCreateBeamCad myForm;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //if (CheckLicenseApi.CheckLicense() == false)
            //{
            //    return Result.Succeeded;
            //}
            try
            {
                 UIApplication uiApp = commandData.Application;
                AppPanel.ShowCreateForm(uiApp);
                myForm = AppPanel.myFormCreate;
                Document doc = uiApp.ActiveUIDocument.Document;
                var beamElement = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_StructuralFraming);              
                myForm.dropNameBeam.DisplayMember = "Text";
                myForm.dropNameBeam.ValueMember = "Value";
                foreach (var item in beamElement)
                {
                    myForm.dropNameBeam.Items.Add(new { Text = item.Name, Value = item.Name });
                }

               
            }
            catch (Exception ex) { }
            return Result.Succeeded;
        }
    }
   
}
