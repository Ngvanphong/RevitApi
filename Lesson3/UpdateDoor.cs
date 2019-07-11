using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Lesson3.Common;
using System.IO;
using System.Xml.Linq;
namespace Lesson3
{
    public class UpdateDoor
    {
        UIApplication _uiApp;
        Document doc;
        public UpdateDoor(UIApplication uiApp)
        {
            _uiApp = uiApp;
            doc = uiApp.ActiveUIDocument.Document;
        }
        public void UpdateAll()
        {
            GetValueXml xmlFile = new GetValueXml();
            
            List<FamilyElement> listElementDoors = xmlFile.GetXmlDoor(doc);
            IList<Element> listElementDoorModel = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Doors).ToElements();
            List<ElementId> listIdModel = new List<ElementId>();
            foreach (var emodel in listElementDoorModel)
            {
                listIdModel.Add(emodel.Id);
            }

            CtreateXmlParameterDoor xmlFileCreateClass = new CtreateXmlParameterDoor();
            List<FamilyElement> listElementRemove = new List<FamilyElement>();                
            foreach (var item in listElementDoors)
            {
                if (listIdModel.Exists(x => x == item.ElementIdSection) == true)
                {



                }
                else
                {
                    IList<Element> viewCollection = new FilteredElementCollector(doc).OfClass(typeof(ViewSection)).ToElements();
                    foreach (var view in viewCollection)
                    {
                        string parameterView = view.LookupParameter("DoorId").AsString();
                        if (parameterView == item.ElementIdSection.ToString())
                        {
                            using (Transaction tv = new Transaction(doc, "Delete View"))
                            {
                                tv.Start();
                                try
                                {                                  
                                    doc.Delete(view.Id);
                                    listElementRemove.Add(item);
                                    xmlFileCreateClass.RemoveItemFileDoor(doc, item);
                                    tv.Commit();
                                }
                                catch
                                {
                                    TaskDialog.Show("Error", "Delete section or create file not finish") ;
                                    tv.Commit();
                                    return;
                                }                              
                            }
                        }
                    }
                }
            }
            listElementDoors = listElementDoors.Except(listElementRemove).ToList();
        }
    }
}
