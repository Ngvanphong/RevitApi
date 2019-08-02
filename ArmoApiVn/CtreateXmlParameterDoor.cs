using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.IO;
using System.Xml.Linq;
using Autodesk.Revit.ApplicationServices;




namespace ArmoApiVn
{
    public class CtreateXmlParameterDoor
    {

        public void CreateFileDoor(UIApplication uiApp, List<FamilyElement> listElementDoors)
        {
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            string name = doc.Title + ".xml";            
            XmlTextWriter writer = new XmlTextWriter(name, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Table");
            foreach (var item in listElementDoors)
            {
                CreateNodeFile(item, writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

        }
        public void SaveFileDoor(Document doc,FamilyElement listElementDoor)
        {
            string name = doc.Title + ".xml";
            string fullPath = Path.GetFullPath(name);
            var xmlDoc = XDocument.Load(fullPath);
            XElement elem = new XElement("Door",
            new XElement("Id", listElementDoor.ElementIdSection.ToString()),
            new XElement("NameFamily", listElementDoor.NameFamily),
            new XElement("NameTypeFamily", listElementDoor.NameTypeFamily),
            new XElement("Width", listElementDoor.Width),
            new XElement("Height", listElementDoor.Height),
            new XElement("Door_W", listElementDoor.Door_W)
         );
            xmlDoc.Element("Table").Add(elem);
            var xmltex = xmlDoc.Element("Table");
            xmlDoc.Save(name);

        }
        public void RemoveItemFileDoor(Document doc, FamilyElement listElementDoor)
        {
            string name = doc.Title + ".xml";
            string fullPath = Path.GetFullPath(name);
            var xmlDoc = XDocument.Load(fullPath);
            var doorLists = xmlDoc.Element("Table").Elements("Door");
            foreach(var item in doorLists)
            {
                if(item.Element("Id").Value== listElementDoor.ElementIdSection.ToString())
                {
                    item.Remove();
                    break;                   
                }
            }   
            xmlDoc.Save(name);
        }
        public void CreateNodeFile(FamilyElement element, XmlTextWriter writer)
        {
            writer.WriteStartElement("Door");
            writer.WriteStartElement("Id");
            writer.WriteString(element.ElementIdSection.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("NameFamily");
            writer.WriteString(element.NameFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("NameTypeFamily");
            writer.WriteString(element.NameTypeFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("Width");
            writer.WriteString(element.Width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(element.Height);
            writer.WriteEndElement();
            writer.WriteStartElement("Door_W");
            writer.WriteString(element.Door_W);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public void CreateFileTextDoor(Document doc, ValueDoorText valueText)
        {            
            string name = doc.Title + "valuetext.xml";
            string fullPath = Path.GetFullPath(name);
            try
            {
           
                var xmlDoc = XDocument.Load(fullPath);              
                XElement elem = new XElement("TextDoor",
                new XElement("Id", valueText.TextId.ToString()),
                new XElement("Name", valueText.Name),
                new XElement("NameFamily", valueText.NameFamily),
                new XElement("ElementId", valueText.ElementIdSection.ToString()),
                new XElement("Width", valueText.Width),
                new XElement("Height", valueText.Height),
                new XElement("Door_W", valueText.Door_W)
             );
                xmlDoc.Element("Table").Add(elem);
                var xmltex = xmlDoc.Element("Table");
                xmlDoc.Save(name);
            }
            catch (Exception ex)
            {
                XmlTextWriter writer = new XmlTextWriter(name, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Table");
                CreateNodeText(valueText, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }

        }
        public void CreateNodeText(ValueDoorText element, XmlTextWriter writer)
        {
            writer.WriteStartElement("TextDoor");
            writer.WriteStartElement("Id");
            writer.WriteString(element.TextId.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Name");
            writer.WriteString(element.Name);
            writer.WriteEndElement();
            writer.WriteStartElement("NameFamily");
            writer.WriteString(element.NameFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("ElementId");
            writer.WriteString(element.ElementIdSection.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Width");
            writer.WriteString(element.Width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(element.Height);
            writer.WriteEndElement();
            writer.WriteStartElement("Door_W");
            writer.WriteString(element.Door_W);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
   
}
