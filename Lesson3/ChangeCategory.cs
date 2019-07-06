using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace Lesson3
{
    [Transaction(TransactionMode.Manual)]
    public class ChangeCategory : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uIApp = commandData.Application;
            Application app = uIApp.Application;
            UIDocument uIDoc = uIApp.ActiveUIDocument;
            Document doc = uIDoc.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> collection = collector.OfClass(typeof(Family)).ToElements();
           
            IList<Element> referRec = uIDoc.Selection.PickElementsByRectangle();
           

            if (referRec != null)
            {
                foreach(Element item in referRec)
                {
                    using (Transaction t = new Transaction(doc, "chageca"))
                    {
                        t.Start();
                        try
                        {
                            var familyName = item as FamilyInstance;
                            string FaminyNmaes = familyName.Symbol.Family.FamilyCategory.Name;

                        }
                        catch (Exception ex)
                        {
                            var eroor = ex.Message;
                        }
                        t.Commit();
                    }
                      
                }
                
            }
            return Result.Succeeded;

        }
      
        

      
    }
}
