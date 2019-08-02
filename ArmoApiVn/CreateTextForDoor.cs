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
using ArmoApiVn.Common;

namespace ArmoApiVn
{
    public class CreateTextForDoor
    {
        UIApplication _uIApp;
        public CreateTextForDoor(UIApplication uIApp)
        {
            _uIApp = uIApp;
        }
        public ValueDoorText CreateText(string parameter,out bool success)
        {
            success = true;
            ValueDoorText result = new ValueDoorText();
            try
            {
                Document doc = _uIApp.ActiveUIDocument.Document;
                Category cat = doc.Settings.Categories.get_Item(BuiltInCategory.OST_Doors);
                Reference refDoor = _uIApp.ActiveUIDocument.Selection.PickObject(ObjectType.Element, new SelectionFilterCategory(cat));
                FamilyInstance door = doc.GetElement(refDoor) as FamilyInstance;
                double with = door.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH).AsDouble();
                LocationPoint locationDoor = door.Location as LocationPoint;
                XYZ pointDoor = locationDoor.Point;
                using (Transaction t = new Transaction(doc, "Create WorkPlane"))
                {
                    t.Start();
                    Plane plane = Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
                    SketchPlane sp = SketchPlane.Create(doc, plane);
                    doc.ActiveView.SketchPlane = sp;
                    t.Commit();

                }
                XYZ getPoint = _uIApp.ActiveUIDocument.Selection.PickPoint();
                XYZ pointText = new XYZ(getPoint.X, getPoint.Y, getPoint.Z);
                if (door.Category.Name == cat.Name)
                {
                    using (Transaction t = new Transaction(doc, "Create Text Door"))
                    {
                        t.Start();
                        TextNoteType typeText = new FilteredElementCollector(doc)
                            .OfClass(typeof(TextNoteType)).Where(x => x.Name == ParameterCommon.TextTypeChose).First() as TextNoteType;

                        if (typeText == null)
                        { TaskDialog.Show("Create Text", "Create Text: "+ParameterCommon.TextTypeChose); }
                        else
                        {
                            ValueDoorText valuePara = GetValueText(door, parameter);
                            string text = ParameterCommon.Door2+": " + door.Name + "\n" + ParameterCommon.Door3+": " + valuePara.Door3 + "\n" + ParameterCommon.Door4+": "
                                + valuePara.Door4+ "\n" + ParameterCommon.Door5+": " + valuePara.Door5;
                            TextNote textNote = TextNote.Create(doc, doc.ActiveView.Id, pointText, 0.18, text, typeText.Id);
                            valuePara.TextId = textNote.Id;
                            valuePara.Name = door.Name;
                            result = valuePara;
                        };
                        t.Commit();
                    }
                }
            }
            catch
            {
                success = false;
                TaskDialog.Show("Error", "Seclect an door or don't create: "+ParameterCommon.TextTypeChose);
            }
                       
            return result;
        }

        public ValueDoorText GetValueText(FamilyInstance element,string parameter)
        {
            ValueDoorText valueText = new ValueDoorText();
            valueText.ElementIdSection = element.Id;
            valueText.Name = element.Name;
            valueText.NameFamily = element.Symbol.FamilyName;
            string[] arrListStrParameter = parameter.Split(';');
            foreach (string para in arrListStrParameter)
            {
                var parameterfind = element.Symbol.LookupParameter(para);
                if (parameterfind == null) parameterfind = element.LookupParameter(para);
                string valueParameter = LookupParamaterRe.ParameterToString(parameterfind);
                switch (para)
                {
                    case ParameterCommon.Door3:
                        valueText.Door3 = valueParameter;
                        break;
                    case ParameterCommon.Door4:
                        valueText.Door4 = valueParameter;
                        break;
                    case ParameterCommon.Door5:
                        valueText.Door5 = valueParameter;
                        break;
                }
            }
            
            return valueText;
        }
    }
    public class ValueDoorText
    {
        public string Name { get; set; }
        public string NameFamily { get; set; }
        public ElementId ElementIdSection { set; get; }
        public ElementId TextId { get; set; }  
        //Set similar constants door    
        public string Door3 { get; set; }

        public string Door4 { get; set; }

        public string Door5 { get; set; }
    }


}
