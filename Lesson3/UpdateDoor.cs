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
            bool statusUpdate = true;
            GetValueXml xmlFile = new GetValueXml();
            LookupParamaterRe lookParaClass = new LookupParamaterRe(_uiApp);
            UpdateSectionDoor updateSectionClass = new UpdateSectionDoor();
            CreateSectionByElementId createSectionClass = new CreateSectionByElementId(_uiApp);
            UpdateTextDoor updateTextClass = new UpdateTextDoor();
            CtreateXmlParameterDoor xmlFileCreateClass = new CtreateXmlParameterDoor();

            List<FamilyElement> listElementDoorsOld = xmlFile.GetXmlDoor(doc);
            foreach(var xmlElemnt in listElementDoorsOld)
            {
                bool checkexitModel = CheckViewModel(xmlElemnt);
                if (checkexitModel==false)
                {
                    xmlFileCreateClass.RemoveItemFileDoor(doc, xmlElemnt);
                }
            }
            List<FamilyElement> listElementDoors = xmlFile.GetXmlDoor(doc);

            IList<Element> listElementDoorModel = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Doors).ToElements();
            List<ElementId> listIdModel = new List<ElementId>();
            foreach (var emodel in listElementDoorModel)
            {
                listIdModel.Add(emodel.Id);
            }
 
            int totalSectionCreated = 0;
            foreach (var item in listElementDoors)
            {
                if (listIdModel.Exists(x => x == item.ElementIdSection) == true)
                {
                    FamilyInstance elementDoor = doc.GetElement(item.ElementIdSection) as FamilyInstance;
                    FamilyElement paraElementModel = lookParaClass.LookValuePramaterForOneElement(ParameterCommon.Door, BuiltInCategory.OST_Doors, elementDoor);
                    if (paraElementModel.NameFamily == item.NameFamily && paraElementModel.NameTypeFamily == item.NameTypeFamily)
                    {
                        updateSectionClass.UpdateMoveDoor(_uiApp, item.ElementIdSection.ToString());
                    }
                    else
                    {
                        if (CheckFamilyAndType(paraElementModel.NameFamily, paraElementModel.NameTypeFamily, listElementDoors))
                        {
                            bool deleteView = DeleteView(item);
                            if (deleteView)
                           {                              
                                xmlFileCreateClass.RemoveItemFileDoor(doc, item);
                            }else
                            {
                                statusUpdate = false;
                            }
                        }
                        else
                        {
                            updateSectionClass.UpdateMoveDoor(_uiApp, item.ElementIdSection.ToString());
                            updateTextClass.UpdateText(_uiApp);
                            xmlFileCreateClass.RemoveItemFileDoor(doc, item);
                            xmlFileCreateClass.SaveFileDoor(doc, paraElementModel);
                        }
                    }
                }
                else
                {
                    bool deleteView2 = DeleteView(item);
                    if (deleteView2)
                    {                       
                        xmlFileCreateClass.RemoveItemFileDoor(doc, item);
                    }else
                    {
                        statusUpdate = false;
                    }

                }
            }
            if (statusUpdate)
            {

                List<FamilyElement> listElementDoorXmlNew = xmlFile.GetXmlDoor(doc);
                List<FamilyElement> listFamilyModel = lookParaClass.LookValuePramater(ParameterCommon.Door, BuiltInCategory.OST_Doors);
                List<FamilyElement> listFamilyNew = new List<FamilyElement>();
                foreach (var listModel in listFamilyModel)
                {
                    if (CheckFamilyAndType(listModel.NameFamily, listModel.NameTypeFamily, listElementDoorXmlNew) == false)
                    {
                        totalSectionCreated += 1;
                        createSectionClass.CreteSectionForOneElement(listModel);
                        xmlFileCreateClass.SaveFileDoor(doc, listModel);
                    }
                }
                if (totalSectionCreated > 0) TaskDialog.Show("AddSection", "Section added: " + totalSectionCreated);
            }else
            {
                TaskDialog.Show("Error", "You go out DeleteView");
            }

        }

        public bool CheckFamilyAndType(string family, string type, List<FamilyElement> listFamilies)
        {
            bool result = false;
            foreach (var item in listFamilies)
            {
                if (family == item.NameFamily && type == item.NameTypeFamily)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }
        public bool DeleteView(FamilyElement familyElement)
        {
            var result = true;
            IList<Element> viewCollection = new FilteredElementCollector(doc).OfClass(typeof(ViewSection)).ToElements();
            foreach (var view in viewCollection)
            {
                string parameterView = view.LookupParameter("DoorId").AsString();
                if (parameterView == familyElement.ElementIdSection.ToString())
                {
                    using (Transaction tv = new Transaction(doc, "Delete View"))
                    {
                        tv.Start();
                        try
                        {
                            doc.Delete(view.Id);
                            tv.Commit();
                        }
                        catch
                        {
                            TaskDialog.Show("Error", "Delete section or create file not finish");
                            result = false;
                            tv.Commit();
                            return result;
                        }
                    }
                }
            }
            return result;
        }
        public bool CheckViewModel(FamilyElement elementXml)
        {
            var result = false;
            IList<Element> listViewModel = new FilteredElementCollector(doc).OfClass(typeof(ViewSection)).ToElements();
            foreach (var item in listViewModel)
            {
                if (item.LookupParameter("DoorId").AsString() == elementXml.ElementIdSection.ToString())
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
