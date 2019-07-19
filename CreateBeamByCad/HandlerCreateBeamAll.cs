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
    public class HandlerCreateBeamAll : IExternalEventHandler
    {
        public frmCreateBeamAll myFormAll;
        public void Execute(UIApplication app)
        {
            myFormAll = AppPanelAll.formCreateBeamAll;
            Document doc = app.ActiveUIDocument.Document;
            List<InformationBeam> listInforBeam = GetInformation(app);
            foreach(var item in listInforBeam)
            {
                try
                {
                    FamilyInstance familyInstance;
                    using (Transaction t = new Transaction(doc, "Create beam by cad"))
                    {
                        t.Start();
                        FamilySymbol familySymbol = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>()
                            .Where(x => x.Name == item.NameBeam).First();
                        familyInstance = doc.Create.NewFamilyInstance(item.LineCure, familySymbol, null, Autodesk.Revit.DB.Structure.StructuralType.Beam);
                        t.Commit();
                    }

                    Parameter parameter = familyInstance.LookupParameter("y Justification");                   
                    SetValueParameter(doc, parameter, 1);
                    Parameter parameterStart = familyInstance.LookupParameter("Start Level Offset");                  
                    SetValueParameterDouble(doc, parameterStart, double.Parse(item.Elevation) / (0.3048 * 1000));
                    Parameter parameterEnd = familyInstance.LookupParameter("End Level Offset");
                    SetValueParameterDouble(doc, parameterEnd, double.Parse(item.Elevation) / (0.3048 * 1000));
                }
                catch
                {
                    continue;
                }
            }
            
        }

        public string GetName()
        {
            return "Create beam all";
        }
        public List<InformationBeam> GetInformation(UIApplication uiApp)
        {
            List<InformationBeam> listInforBeam = new List<InformationBeam>();
            //myFormAll = AppPanelAll.formCreateBeamAll;
            Document doc = uiApp.ActiveUIDocument.Document;
            string lineStypeBeam = myFormAll.dropLineStyle.GetItemText(myFormAll.dropLineStyle.SelectedItem);

            var listDetailLineAll = new FilteredElementCollector(doc, doc.ActiveView.Id).OfCategory(BuiltInCategory.OST_Lines);
            List<DetailLine> listDetailLine = new List<DetailLine>();
            foreach (var item in listDetailLineAll)
            {
                try
                {
                    DetailLine detail = item as DetailLine;
                    if (detail.LineStyle.Name == lineStypeBeam)
                    {
                        listDetailLine.Add(detail);
                    }
                }
                catch
                {
                    continue;
                }
            }
            List<Curve> listLines = new List<Curve>();
            List<Curve> listLineMiddels = new List<Curve>();
            if (myFormAll.radioHorizontal.Checked)
            {
                foreach (DetailLine deline in listDetailLine)
                {
                    Curve line = deline.GeometryCurve;
                    XYZ v = (line.GetEndPoint(1) - line.GetEndPoint(0));
                    if (v.Normalize().IsAlmostEqualTo(XYZ.BasisX) && v.GetLength() > double.Parse(myFormAll.txtMinimun.Text) / (0.3048 * 1000))
                    {
                        listLines.Add(line);
                    }
                }

            }
            else if (myFormAll.radioVertical.Checked)
            {
                foreach (DetailLine deline in listDetailLine)
                {
                    Curve line = deline.GeometryCurve;
                    XYZ v = (line.GetEndPoint(1) - line.GetEndPoint(0));
                    if (v.Normalize().IsAlmostEqualTo(XYZ.BasisY) && v.GetLength() > double.Parse(myFormAll.txtMinimun.Text) / (0.3048 * 1000))
                    {
                        listLines.Add(line);
                    }
                }
            }
            else
            {
                foreach (DetailLine deline in listDetailLine)
                {
                    Curve line = deline.GeometryCurve;
                    XYZ v = (line.GetEndPoint(1) - line.GetEndPoint(0));
                    if (v.GetLength() > double.Parse(myFormAll.txtMinimun.Text) / (0.3048 * 1000))
                    {
                        listLines.Add(line);
                    }
                }
            }

            foreach (var item in listLines)
            {
                if (myFormAll.radioHorizontal.Checked)
                {
                    Curve middleLineMid = GetMinLine(item, listLines);
                    if (CheckExitMiddle(listLineMiddels, middleLineMid) == false)
                    {
                        listLineMiddels.Add(middleLineMid);
                    }
                }
                else if (myFormAll.radioVertical.Checked)
                {
                    Curve middleLineMid = GetMinLine(item, listLines);
                    if (CheckExitMiddle(listLineMiddels, middleLineMid) == false)
                    {
                        listLineMiddels.Add(middleLineMid);
                    }
                }
                else
                {

                }
            }
            string textStype = myFormAll.dropTextStyle.GetItemText(myFormAll.dropTextStyle.SelectedItem);
            foreach (var item in listLineMiddels)
            {
                InformationBeam inforBeam = new InformationBeam();
                inforBeam = GetText(doc, item, textStype);
                listInforBeam.Add(inforBeam);
            }
            return listInforBeam;

        }
        public Curve GetMinLine(Curve line, List<Curve> listLine)
        {
            double minlength = 0;
            Curve result = null;
            XYZ start = line.GetEndPoint(0);
            XYZ end = line.GetEndPoint(1);
            XYZ mid = (start + end) / 2;
            if (myFormAll.radioHorizontal.Checked)
            {
                foreach (var item in listLine)
                {
                    XYZ sitem = item.GetEndPoint(0);
                    XYZ eitem = item.GetEndPoint(1);
                    XYZ midItem = (sitem + eitem) / 2;
                    XYZ v = (midItem + mid) / 2;
                    double distance = (mid.Y - midItem.Y) * (mid.Y - midItem.Y);
                    if (minlength == 0)
                    {
                        minlength = distance;
                        result = Line.CreateBound(new XYZ(start.X, v.Y, start.Z), new XYZ(end.X, v.Y, end.Z));
                    }
                    else
                    {
                        if (minlength > distance)
                        {
                            minlength = distance;
                            result = Line.CreateBound(new XYZ(start.X, v.Y, start.Z), new XYZ(end.X, v.Y, end.Z));
                        }
                    }

                }
            }
            else if (myFormAll.radioVertical.Checked)
            {
                foreach (var item in listLine)
                {
                    XYZ sitem = item.GetEndPoint(0);
                    XYZ eitem = item.GetEndPoint(1);
                    XYZ midItem = (sitem + eitem) / 2;
                    XYZ v = (midItem + mid) / 2;
                    double distance = (mid.X - midItem.X) * (mid.X - midItem.X);
                    if (minlength == 0)
                    {
                        minlength = distance;
                        result = Line.CreateBound(new XYZ(v.X / 2, start.Y, start.Z), new XYZ(v.X / 2, end.Y, end.Z));
                    }
                    else
                    {
                        if (minlength > distance)
                        {
                            minlength = distance;
                            result = Line.CreateBound(new XYZ(v.X / 2, start.Y, start.Z), new XYZ(v.X / 2, end.Y, end.Z));
                        }
                    }
                }
            }
            else
            {
                foreach (var item in listLine)
                {
                    XYZ sitem = item.GetEndPoint(0);
                    XYZ eitem = item.GetEndPoint(1);
                    XYZ midItem = (sitem + eitem) / 2;
                    XYZ v = (midItem + mid) / 2;
                    double distance = (mid.X - midItem.X) * (mid.X - midItem.X) + (mid.Y - midItem.Y) * (mid.Y - midItem.Y);
                    if (minlength == 0)
                    {
                        minlength = distance;
                        result = null;
                    }
                    else
                    {
                        if (minlength > distance)
                        {
                            minlength = distance;
                            result = null;
                        }
                    }
                }
            }
            return result;

        }
        public bool CheckExitMiddle(List<Curve> listMiddle, Curve middle)
        {
            bool result = false;
            XYZ start = middle.GetEndPoint(0);
            XYZ end = middle.GetEndPoint(1);
            XYZ midpoint = (start + end) / 2;
            foreach (var item in listMiddle)
            {
                XYZ sitem = item.GetEndPoint(0);
                XYZ eitem = item.GetEndPoint(1);
                XYZ midpointitem = (sitem + eitem) / 2;
                if (midpoint.X == midpointitem.X && midpoint.Y == midpointitem.Y)
                {
                    result = true;
                    return result;
                }
            }
            return result;
        }
        public InformationBeam GetText(Document doc, Curve lineMiddle, string textStyle)
        {

            InformationBeam infor = new InformationBeam();
            XYZ startLine = lineMiddle.GetEndPoint(0);
            XYZ endLine = lineMiddle.GetEndPoint(1);
            XYZ middlePoint = (startLine + endLine) / 2;
            IEnumerable<TextNode> collectionText;
            if (myFormAll.radioHorizontal.Checked)
            {
                collectionText = new FilteredElementCollector(doc, doc.ActiveView.Id).OfClass(typeof(TextNode)).Cast<TextNode>()
                     .Where(x => x.GetType().Name == textStyle).Where(x => x.UpDirection.IsAlmostEqualTo(XYZ.BasisY));
            }
            else if (myFormAll.radioVertical.Checked)
            {
                collectionText = new FilteredElementCollector(doc, doc.ActiveView.Id).OfClass(typeof(TextNode)).Cast<TextNode>()
                    .Where(x => x.GetType().Name == textStyle).Where(x => x.UpDirection.IsAlmostEqualTo(XYZ.BasisX));
            }
            else
            {
                collectionText = new FilteredElementCollector(doc, doc.ActiveView.Id).OfClass(typeof(TextNode)).Cast<TextNode>()
                   .Where(x => x.GetType().Name == textStyle);
            }

            double minDistance = 0;
            string valueText = "";
            foreach (TextNode textitem in collectionText)
            {
                XYZ pointText = textitem.Position;
                double distance = Math.Sqrt((middlePoint.X - pointText.X) * (middlePoint.X - pointText.X) + (middlePoint.Y - pointText.Y) * (middlePoint.X - pointText.X));
                if (minDistance == 0)
                {
                    minDistance = distance;
                    valueText = textitem.Text;
                }
                if (minDistance > distance)
                {
                    minDistance = distance;
                    valueText = textitem.Text;
                }
            }
            string[] arrayText = valueText.Split('(');
            string nameBeam = arrayText[0].Trim();
            string elevationBeam = myFormAll.txtOffsetLevelBeam.Text.ToString();
            if (arrayText.Count() > 1)
            {
                string beamElevationCad = arrayText[1].Replace(".", "").Replace(",", "").Replace(")", "").Trim();
                elevationBeam = beamElevationCad;
            }
            infor.Elevation = elevationBeam;
            infor.NameBeam = nameBeam;
            infor.LineCure = Line.CreateBound(startLine, endLine);
            return infor;
        }
        public void SetValueParameter(Document doc, Parameter parameter, int value)
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

    }

    public class InformationBeam
    {
        public Curve LineCure { set; get; }

        public string Elevation { set; get; }

        public string NameBeam { get; set; }

    }
}
