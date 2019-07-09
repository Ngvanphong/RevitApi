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
using System.Xml;

namespace Lesson3
{
    public class CreateTextForDoor
    {
        UIApplication _uIApp;
        public CreateTextForDoor(UIApplication uIApp)
        {
            _uIApp = uIApp;
        }
        public ValueDoorText CreateText(string parameter)
        {
            ValueDoorText result = new ValueDoorText();
            Document doc = _uIApp.ActiveUIDocument.Document;
            Category cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Doors);
            Reference refDoor = _uIApp.ActiveUIDocument.Selection.PickObject(ObjectType.Element);
            FamilyInstance door = doc.GetElement(refDoor) as FamilyInstance;
            LocationPoint locationDoor = door.Location as LocationPoint;
            XYZ pointDoor = locationDoor.Point;
            if (door.Category.Name == cat.Name)
            {
                using (Transaction t = new Transaction(doc, "Create Text Door"))
                {
                    t.Start();
                    TextNoteType typeText = new FilteredElementCollector(doc)
                        .OfClass(typeof(TextNoteType)).Where(x => x.Name == "2.5mm Arial").First() as TextNoteType;
                   
                    if (typeText == null)
                    { TaskDialog.Show("Create Text", "Create Text: 2.5mm Arial "); }
                    else
                    {
                        ValueDoorText valuePara = GetValueText(door, parameter);
                        string text = "Name: " + door.Name + "\n" + "With: " + valuePara.Width + "\n" + "Height: "
                            + valuePara.Height + "\n" + "Door_W: " + valuePara.Door_W;                                   
                        TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, pointDoor,0.18, text,typeText.Id);
                        valuePara.TextId = textNote.Id;
                        valuePara.Name = door.Name;
                        result = valuePara;                  
                    };
                    t.Commit();
                }
            }
            return result;
        }

        public ValueDoorText GetValueText(FamilyInstance element,string parameter)
        {
            ValueDoorText valueText = new ValueDoorText();
            valueText.ElementIdSection = element.Id;
            valueText.Name = element.Name;
            string[] arrListStrParameter = parameter.Split(';');
            foreach (string para in arrListStrParameter)
            {
                var parameterfind = element.Symbol.LookupParameter(para);
                if (parameterfind == null) parameterfind = element.LookupParameter(para);
                string valueParameter = LookupParamaterRe.ParameterToString(parameterfind);
                switch (para)
                {
                    case "Width":
                        valueText.Width = valueParameter;
                        break;
                    case "Height":
                        valueText.Height = valueParameter;
                        break;
                    case "Door_W":
                        valueText.Door_W = valueParameter;
                        break;
                }
            }
            
            return valueText;
        }
    }
    public class ValueDoorText
    {
        public string Name { get; set; }
        public ElementId ElementIdSection { set; get; }
        public ElementId TextId { get; set; }      
        public string Width { get; set; }
        public string Height { get; set; }
        public string Door_W { get; set; }
    }


}
