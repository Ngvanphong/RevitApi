using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
namespace CreateBeamByCad
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamAllBinding : IExternalCommand
    {
        public frmCreateBeamAll myFormAll;
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
           
            UIApplication uiApp = commandData.Application;
            AppPanelAll.ShowCreateForm(uiApp);
            Document doc = uiApp.ActiveUIDocument.Document;
            myFormAll = AppPanelAll.formCreateBeamAll;
            var textType = new FilteredElementCollector(doc).OfClass(typeof(TextNoteType));
            myFormAll.dropTextStyle.DisplayMember = "Text";
            myFormAll.dropTextStyle.ValueMember = "Value";
            foreach (var item in textType)
            {
                myFormAll.dropTextStyle.Items.Add(new { Text = item.Name, Value = item.Name });
            }

            var colectionDetail = new FilteredElementCollector(doc, doc.ActiveView.Id).OfCategory(BuiltInCategory.OST_Lines);
            List<string> lineStyleName = new List<string>();
            foreach (var item in colectionDetail)
            {
                try
                {
                    DetailLine itemdetail = item as DetailLine;
                    string name = itemdetail.LineStyle.Name;
                    if (lineStyleName.Exists(x => x == name) == false)
                    {
                        lineStyleName.Add(name);
                    }
                }
                catch { continue; }

            }
            myFormAll.dropLineStyle.DisplayMember = "Text";
            myFormAll.dropLineStyle.ValueMember = "Value";
            foreach (var item in lineStyleName)
            {
                myFormAll.dropLineStyle.Items.Add(new { Text = item, Value = item });
            }

            return Result.Succeeded;
        }
       

    }
   
}
