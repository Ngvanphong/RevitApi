

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

namespace CreateBeamByCad
{
    public class HandlerCreateBeam : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            List<Element> listLine = AppPanel.listSelectLine;
            string typeName = AppPanel.myFormCreate.dropNameBeam.GetItemText(AppPanel.myFormCreate.dropNameBeam.SelectedItem);
            var familySymbol = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).OfCategory(BuiltInCategory.OST_StructuralFraming)
                .Cast<FamilySymbol>().Where(x => x.Name == typeName).First();
            foreach(var item in listLine)
            {
                LocationCurve lineDetail = item.Location as LocationCurve;
                Curve cureLine = lineDetail.Curve;              
                try
                {
                    FamilyInstance familyInstance;
                    using (Transaction t= new Transaction(doc,"Create beam by cad"))
                    {
                        t.Start();
                        familyInstance = doc.Create.NewFamilyInstance(cureLine, familySymbol, null, Autodesk.Revit.DB.Structure.StructuralType.Beam);   
                        t.Commit();
                    }

                    Parameter parameter = familyInstance.LookupParameter("y Justification");
                    int postionBeam = GetPostionBeam();
                    SetValueParameter(doc, parameter, postionBeam);
                    Parameter parameterStart = familyInstance.LookupParameter("Start Level Offset");
                    if (AppPanel.myFormCreate.txtStartOffset.Text == "") AppPanel.myFormCreate.txtStartOffset.Text = "0";
                    if (AppPanel.myFormCreate.txtEndOffset.Text == "") AppPanel.myFormCreate.txtEndOffset.Text = "0";
                    SetValueParameterDouble(doc, parameterStart, double.Parse(AppPanel.myFormCreate.txtStartOffset.Text)/(0.3048 * 1000));
                    Parameter parameterEnd = familyInstance.LookupParameter("End Level Offset");
                    SetValueParameterDouble(doc, parameterEnd, double.Parse(AppPanel.myFormCreate.txtEndOffset.Text)/(0.3048 * 1000));
                }
                catch(Exception ex) { TaskDialog.Show("Error", "Not success"); }
            }


        }

        public string GetName()
        {
            return "Create beam from form";
        }
        public void SetValueParameter(Document doc,Parameter parameter, int value)
        {
            using (Transaction t = new Transaction(doc, "Set value parameter"))
            {
                t.Start();
                try { parameter.Set(value); }
                catch (Exception ex) { };
                t.Commit();
            }
        }
        public void SetValueParameterDouble(Document doc, Parameter parameter, double value)
        {
           
            using (Transaction t = new Transaction(doc, "Set value parameter"))
            {
                t.Start();
                try { parameter.Set(value); }
                catch (Exception ex) { };
                t.Commit();
            }
        }

        public int GetPostionBeam()
        {
            int result = 2;
            if (AppPanel.myFormCreate.radioLeftBeam.Checked)
            { result = 0; }
            else if (AppPanel.myFormCreate.radioCenterBeam.Checked)
            {
                result = 1;
            }            
            return result;

        }

    }
}
