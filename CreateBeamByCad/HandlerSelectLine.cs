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
using Common;

namespace CreateBeamByCad
{
    public class HandlerSelectLine : IExternalEventHandler
    {
         
        public void Execute(UIApplication app)
        {

            UIDocument uiDoc = app.ActiveUIDocument;
            Document doc = uiDoc.Document;
            Category cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Lines);
            IList<Reference> referElement = uiDoc.Selection.PickObjects(ObjectType.Element, new FilterSelectionMonitor(cat));
            List<Element> listDetailLine = new List<Element>();
            foreach(var item in referElement)
            {
                listDetailLine.Add(doc.GetElement(item));
            }
            AppPanel.listSelectLine = listDetailLine;
            if (AppPanel.listSelectLine.Count() > 0)
            {
                AppPanel.myFormCreate.btnBeamCreating.Enabled = true;
            }
            else
            {
                AppPanel.myFormCreate.btnBeamCreating.Enabled = false;
            }
        }

        public string GetName()
        {
            return "Select line from cad";
        }
    }
}
