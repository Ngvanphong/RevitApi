using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Threading;
using Common.Extension;

namespace CreateBeamByCad
{
    [Transaction(TransactionMode.Manual)]
    public class CreateBeamAllBinding : IExternalCommand
    {
      
        public  Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (CheckLicenseApi.CheckLicense() == false)
            {
                return Result.Succeeded;
            }
            UIApplication uiApp = commandData.Application;          
            Document doc = uiApp.ActiveUIDocument.Document;
            AppPanelAll.ShowCreateForm(uiApp);
            //Thread newThread = new Thread(DoSomething);
            //newThread.Start();
            GetForm getForm = new GetForm(uiApp,doc);
            getForm.GetInforForm();
            //newThread.Abort();         
            return Result.Succeeded;
        } 
                    
        public void DoSomething()
        {          
            using (frmLoad frm = new frmLoad())
            {
                frm.ShowDialog();
                AppPanelAll.frmLoadProgess = frm;
            }
        } 
    }
    public class GetForm
    {
        public static frmCreateBeamAll myFormAll;
        Document doc;
        UIApplication uiApp;
        public GetForm(UIApplication _uiApp,Document _doc)
        {
            uiApp = _uiApp;
            doc = _doc;
        }
        public bool  GetInforForm()
        {
            bool result = false;                   
            myFormAll = AppPanelAll.formCreateBeamAll;
            var colectionText= new FilteredElementCollector(doc,doc.ActiveView.Id).OfClass(typeof(TextNote)).Cast<TextNote>();
            List<string> textType = new List<string>();
            foreach (var text in colectionText)
            {
                if (!textType.Exists(x => x == text.Name))
                {                    
                    textType.Add(text.Name);
                }
            }         
            myFormAll.dropTextStyle.DisplayMember = "Text";
            myFormAll.dropTextStyle.ValueMember = "Value";
            foreach (var item in textType)
            {
                myFormAll.dropTextStyle.Items.Add(new { Text = item, Value = item});
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
            result = true;
            return result;        
        }
    }
   
}
