using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApi.Common;
using System.IO;
using System.Xml.Linq;

namespace ArmoApi
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
                    if (item.NameFamily!=valueTextModel.NameFamily||item.Name!=valueTextModel.Name)
                    {
                        string name = doc.Title + "valuetext.xml";
                        string fullPath = Path.GetFullPath(name);
                        var xmlDoc = XDocument.Load(fullPath);
                        var xmlElement = xmlDoc.Element("Table").Elements("TextDoor");
                        foreach (var row in xmlElement)
                        {
                            if (row.Element("ElementId").Value == item.ElementIdSection.ToString())
                            {
                                row.Element("Name").Value = valueTextModel.Name;
                                row.Element("Width").Value = valueTextModel.Width;
                                row.Element("Height").Value = valueTextModel.Height;
                                row.Element("Door_W").Value = valueTextModel.Door_W;
                                xmlDoc.Save(name);
                                using (Transaction t = new Transaction(doc, "Update Text Door"))
                                {
                                    t.Start();
                                    string text = "Name: " + element.Name + "\n" + "With: " + valueTextModel.Width + "\n" + "Height: "
                                     + valueTextModel.Height + "\n" + "Door_W: " + valueTextModel.Door_W;
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
                        if (item.NameFamily!=valueTextModel.NameFamily || item.Name != valueTextModel.Name)
                        {
                            string name = doc.Title + "valuetext.xml";
                            string fullPath = Path.GetFullPath(name);
                            var xmlDoc = XDocument.Load(fullPath);
                            var xmlElement = xmlDoc.Element("Table").Elements("TextDoor");
                            foreach (var row in xmlElement)
                            {
                                if (row.Element("ElementId").Value == item.ElementIdSection.ToString())
                                {
                                    row.Element("Name").Value = valueTextModel.Name;
                                    row.Element("Width").Value = valueTextModel.Width;
                                    row.Element("Height").Value = valueTextModel.Height;
                                    row.Element("Door_W").Value = valueTextModel.Door_W;
                                    xmlDoc.Save(name);
                                    using (Transaction t = new Transaction(doc, "Update Text Door"))
                                    {
                                        t.Start();
                                        string text = "Name: " + element.Name + "\n" + "With: " + valueTextModel.Width + "\n" + "Height: "
                                         + valueTextModel.Height + "\n" + "Door_W: " + valueTextModel.Door_W;
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
    }

}

