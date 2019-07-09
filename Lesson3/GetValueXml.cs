using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ExtendedXmlSerializer.Configuration;
using ExtendedXmlSerializer.ExtensionModel.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lesson3
{
   public class GetValueXml
    {
        public List<FamilyElement> GetXmlDoor(Document doc)
        {
            List<FamilyElement> listElements=new List<FamilyElement>();
            string name = doc.Title + ".xml";
            try
            {
                string fullPath = Path.GetFullPath(name);
                var xmlDoc = XDocument.Load(fullPath);
                var xmlElement = xmlDoc.Element("Table").Elements("Door");
                foreach (var item in xmlElement)
                {
                    FamilyElement familyElement = new FamilyElement();
                    familyElement.NameFamily = item.Element("NameFamily").Value;
                    familyElement.ElementIdSection = new ElementId(int.Parse(item.Element("Id").Value));
                    familyElement.NameTypeFamily = item.Element("NameTypeFamily").Value;
                    familyElement.Width = item.Element("Width").Value;
                    familyElement.Height = item.Element("Height").Value;
                    familyElement.Door_W = item.Element("Door_W").Value;
                    listElements.Add(familyElement);
                }
            }
            catch
            {
                TaskDialog.Show("Error", "Contact to creater");
            }
            
            return listElements;
        }
        public List<ValueDoorText> GetXmlTextDoor(Document doc)
        {
            List<ValueDoorText> listValue = new List<ValueDoorText>();
            string name = doc.Title + "valuetext.xml";    
            try {
                string fullPath = Path.GetFullPath(name);
                var xmlDoc = XDocument.Load(fullPath);
                var xmlElement = xmlDoc.Element("Table").Elements("TextDoor");
                foreach (var item in xmlElement)
                {
                    ValueDoorText valueText = new ValueDoorText();
                    valueText.TextId = new ElementId(int.Parse(item.Element("Id").Value));
                    valueText.Name = item.Element("Name").Value;
                    valueText.ElementIdSection = new ElementId(int.Parse(item.Element("ElementId").Value));
                    valueText.Width = item.Element("Width").Value;
                    valueText.Height = item.Element("Height").Value;
                    valueText.Door_W = item.Element("Door_W").Value;
                    listValue.Add(valueText);
                }
            }
            catch
            {
                TaskDialog.Show("Error", "Contact to creater");
            }
            
            return listValue;
        }
    }
}
