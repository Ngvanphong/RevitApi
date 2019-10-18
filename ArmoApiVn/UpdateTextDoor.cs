using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApiVn.Common;
using System.IO;
using System.Xml.Linq;

namespace ArmoApiVn
{
    public class UpdateTextDoor
    {
        public void UpdateText(UIApplication _uiApp)
        {
            Document doc = _uiApp.ActiveUIDocument.Document;
            GetValueXml xml = new GetValueXml();
            List<ValueDoorText> listValue = new List<ValueDoorText>();
            listValue = xml.GetXmlTextDoor(doc);
            CreateTextForDoor getValueTextClass = new CreateTextForDoor(_uiApp);
            foreach (var item in listValue)
            {
                ValueDoorText valueTextModel = new ValueDoorText();
                FamilyInstance element = doc.GetElement(item.ElementIdSection) as FamilyInstance;
                if (element != null)
                {
                    valueTextModel = getValueTextClass.GetValueText(element, ParameterCommon.Door);
                    valueTextModel.TextId = item.TextId;
                    if (CheckSemillarText(item,valueTextModel)==false)
                    {
                        string name = doc.Title + "valuetext.xml";
                        string fullPath = Path.GetFullPath(name);
                        var xmlDoc = XDocument.Load(fullPath);
                        var xmlElement = xmlDoc.Element("Table").Elements("TextDoor");
                        foreach (var row in xmlElement)
                        {
                            if (row.Element("ElementId").Value == item.ElementIdSection.ToString())
                            {
                                row.Element(ParameterCommon.Door2).Value = valueTextModel.Name;
                                row.Element(ParameterCommon.Door3.Replace(" ", "")).Value = valueTextModel.Door3;
                                row.Element(ParameterCommon.Door4.Replace(" ", "")).Value = valueTextModel.Door4;
                                row.Element(ParameterCommon.Door5.Replace(" ", "")).Value = valueTextModel.Door5;
                                xmlDoc.Save(name);
                                using (Transaction t = new Transaction(doc, "Update Text Door"))
                                {
                                    t.Start();
                                    string text = ParameterCommon.Door2+": " + element.Name + "\n" + ParameterCommon.Door3+": " + valueTextModel.Door3 + "\n" + ParameterCommon.Door4+": "
                                     + valueTextModel.Door4 + "\n" + ParameterCommon.Door5+": " + valueTextModel.Door5;
                                    try
                                    {
                                        TextNote textElement = doc.GetElement(item.TextId) as TextNote;
                                        textElement.Text = text;
                                        t.Commit();
                                    }
                                    catch {
                                        t.Commit();
                                        continue; }                                   
                                }

                            }

                        }

                    }
                }

            }
        }

        public void UpdateTextForOneElement(UIApplication _uiApp,string elementId)
        {
            Document doc = _uiApp.ActiveUIDocument.Document;
            GetValueXml xml = new GetValueXml();
            List<ValueDoorText> listValue = new List<ValueDoorText>();
            listValue = xml.GetXmlTextDoor(doc);
            CreateTextForDoor getValueTextClass = new CreateTextForDoor(_uiApp);
            foreach (var item in listValue)
            {
                if (item.ElementIdSection.ToString() == elementId)
                {
                    ValueDoorText valueTextModel = new ValueDoorText();
                    FamilyInstance element = doc.GetElement(item.ElementIdSection) as FamilyInstance;
                    if (element != null)
                    {
                        valueTextModel = getValueTextClass.GetValueText(element, ParameterCommon.Door);
                        valueTextModel.TextId = item.TextId;
                        if (CheckSemillarText(item, valueTextModel)==false)
                        {
                            string name = doc.Title + "valuetext.xml";
                            string fullPath = Path.GetFullPath(name);
                            var xmlDoc = XDocument.Load(fullPath);
                            var xmlElement = xmlDoc.Element("Table").Elements("TextDoor");
                            foreach (var row in xmlElement)
                            {
                                if (row.Element("ElementId").Value == item.ElementIdSection.ToString())
                                {
                                    row.Element(ParameterCommon.Door2).Value = valueTextModel.Name;
                                    row.Element(ParameterCommon.Door3.Replace(" ", "")).Value = valueTextModel.Door3;
                                    row.Element(ParameterCommon.Door4.Replace(" ", "")).Value = valueTextModel.Door4;
                                    row.Element(ParameterCommon.Door5.Replace(" ", "")).Value = valueTextModel.Door5;
                                    xmlDoc.Save(name);
                                    using (Transaction t = new Transaction(doc, "Update Text Door"))
                                    {
                                        t.Start();
                                        string text = ParameterCommon.Door2+": " + element.Name + "\n" + ParameterCommon.Door3+": " + valueTextModel.Door3 + "\n" + ParameterCommon.Door4+": "
                                         + valueTextModel.Door4 + "\n" + ParameterCommon.Door5+": " + valueTextModel.Door5;
                                        try
                                        {
                                            TextNote textElement = doc.GetElement(item.TextId) as TextNote;
                                            textElement.Text = text;
                                            t.Commit();
                                        }
                                        catch
                                        {
                                            t.Commit();
                                            continue;
                                        }

                                    }

                                }

                            }

                        }
                    }
                }
                
            }
        }

        public bool CheckSemillarText(ValueDoorText valueDoorOld,ValueDoorText valueDoorModel) 
        {
            bool result = true;
            if (valueDoorOld.NameFamily != valueDoorModel.NameFamily || valueDoorOld.Name != valueDoorModel.Name ||
                valueDoorOld.Door3 != valueDoorModel.Door3 || valueDoorOld.Door4 != valueDoorModel.Door4 ||
                valueDoorOld.Door5 != valueDoorModel.Door5)
            {
                result = false;
            } 
            return result;

        }
    }

}

