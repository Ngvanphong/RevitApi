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
using System.Xml.Linq;
using System.Xml.Serialization;
using ArmoApiVn.Common;

namespace ArmoApiVn
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
                    familyElement.Door3 = item.Element(ParameterCommon.Door3.Replace(" ", "")).Value;
                    familyElement.Door4 = item.Element(ParameterCommon.Door4.Replace(" ", "")).Value;
                    familyElement.Door5 = item.Element(ParameterCommon.Door5.Replace(" ", "")).Value;
                    listElements.Add(familyElement);
                }
            }
            catch (Exception ex)
            {
                
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
                    valueText.NameFamily = item.Element("NameFamily").Value;
                    valueText.ElementIdSection = new ElementId(int.Parse(item.Element("ElementId").Value));
                    valueText.Door3 = item.Element(ParameterCommon.Door3.Replace(" ", "")).Value;
                    valueText.Door4 = item.Element(ParameterCommon.Door4.Replace(" ", "")).Value;
                    valueText.Door5 = item.Element(ParameterCommon.Door5.Replace(" ", "")).Value;
                    listValue.Add(valueText);
                }
            }
            catch
            {
               
            }
            
            return listValue;
        }
    }
}
