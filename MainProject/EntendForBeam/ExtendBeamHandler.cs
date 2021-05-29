﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using MainProject.EntendForBeam;
using System.Windows.Forms;

namespace MainProject
{
    public class ExtendBeamHandler : IExternalEventHandler
    {

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            IEnumerable<FamilyInstance> listBeamAllsExnum = new FilteredElementCollector(doc, doc.ActiveView.Id).OfClass(typeof(FamilyInstance))
                .OfCategory(BuiltInCategory.OST_StructuralFraming).Cast<FamilyInstance>();
            List<FamilyInstance> listBeamAlls = new List<FamilyInstance>();
            foreach (var item in listBeamAllsExnum)
            {
                listBeamAlls.Add(item);
            }
            var listBeamSelectIds = app.ActiveUIDocument.Selection.GetElementIds();
            List<FamilyInstance> listBeamSelect = new List<FamilyInstance>();
            foreach(ElementId id in listBeamSelectIds)
            {
                try
                {
                    FamilyInstance fa = doc.GetElement(id) as FamilyInstance;
                    if(fa.Category.Name== "Structural Framing")
                    {
                        listBeamSelect.Add(fa);
                    }
                }
                catch { continue; }
            }

            List<BeamConnect> listExtends = new List<BeamConnect>();
            if (listBeamSelect.Count > 0)
            {
                foreach (FamilyInstance beam in listBeamSelect)
                {
                    BeamConnect beamConectItem = new BeamConnect();
                    beamConectItem = GetInforExtend(beam, listBeamAlls);
                    if (beamConectItem.StartBeam != null || beamConectItem.EndBeam != null)
                    {
                        listExtends.Add(beamConectItem);
                    }
                }
            }
            else
            {
                string warning = "Do you want to extend all beam on view";
                DialogResult result = MessageBox.Show(warning, "Extend Beam", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                foreach (FamilyInstance beam in listBeamAlls)
                {
                    BeamConnect beamConectItem = new BeamConnect();
                    beamConectItem = GetInforExtend(beam, listBeamAlls);
                    if (beamConectItem.StartBeam != null || beamConectItem.EndBeam != null)
                    {
                        listExtends.Add(beamConectItem);
                    }
                }
            }
           
            foreach (var extend in listExtends)
            {
                using (Transaction t = new Transaction(doc, "CreateBeam"))
                {
                    t.Start();
                    try
                    {
                        FamilyInstance beamMain = doc.GetElement(extend.BeamMainId) as FamilyInstance;
                        LocationCurve lineLoc = beamMain.Location as LocationCurve;
                        if (extend.StartBeam != null && extend.EndBeam != null)
                        {
                            LocationCurve extendStart = extend.StartBeam.Location as LocationCurve;
                            Curve lineExtendStart = extendStart.Curve;
                            LocationCurve extendEnd = extend.EndBeam.Location as LocationCurve;
                            Curve lineExtendEnd = extendEnd.Curve;
                            if (AppPanelExtend.myFormExtend.radioHorizontalExtend.Checked)
                            {
                                XYZ startNew = new XYZ(lineExtendStart.GetEndPoint(0).X, extend.StartPoint.Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(lineExtendEnd.GetEndPoint(0).X, extend.EndPoint.Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                            else
                            {
                                XYZ startNew = new XYZ(extend.StartPoint.X, lineExtendStart.GetEndPoint(0).Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(extend.EndPoint.X, lineExtendEnd.GetEndPoint(0).Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                        }
                        else if (extend.StartBeam != null && extend.EndBeam == null)
                        {
                            LocationCurve extendStart = extend.StartBeam.Location as LocationCurve;
                            Curve lineExtendStart = extendStart.Curve;
                            if (AppPanelExtend.myFormExtend.radioHorizontalExtend.Checked)
                            {
                                XYZ startNew = new XYZ(lineExtendStart.GetEndPoint(0).X, extend.StartPoint.Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(extend.EndPoint.X, extend.EndPoint.Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                            else
                            {
                                XYZ startNew = new XYZ(extend.StartPoint.X, lineExtendStart.GetEndPoint(0).Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(extend.EndPoint.X, extend.EndPoint.Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                        }
                        else
                        {
                            LocationCurve extendEnd = extend.EndBeam.Location as LocationCurve;
                            Curve lineExtendEnd = extendEnd.Curve;
                            if (AppPanelExtend.myFormExtend.radioHorizontalExtend.Checked)
                            {
                                XYZ startNew = new XYZ(extend.StartPoint.X, extend.StartPoint.Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(lineExtendEnd.GetEndPoint(0).X, extend.EndPoint.Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                            else
                            {
                                XYZ startNew = new XYZ(extend.StartPoint.X, extend.StartPoint.Y, extend.StartPoint.Z);
                                XYZ endNew = new XYZ(extend.EndPoint.X, lineExtendEnd.GetEndPoint(0).Y, extend.EndPoint.Z);
                                lineLoc.Curve = Line.CreateBound(startNew, endNew);
                            }
                        }

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
        public string GetName()
        {
            return "ExtendForBeam";
        }
        public BeamConnect GetInforExtend(FamilyInstance beam, List<FamilyInstance> listBeam)
        {
            BeamConnect beamConect = new BeamConnect();
            beamConect.BeamMainId = beam.Id;
            LocationCurve line = beam.Location as LocationCurve;
            XYZ end1 = line.Curve.GetEndPoint(0);
            XYZ end2 = line.Curve.GetEndPoint(1);
            XYZ vector = end1 - end2;
            XYZ min;
            XYZ max;
            double distanceMin = 300000000;
            double distanceMax = 300000000;
            if (AppPanelExtend.myFormExtend.radioHorizontalExtend.Checked)
            {
                if (vector.Normalize().IsAlmostEqualTo(XYZ.BasisX) || vector.Normalize().IsAlmostEqualTo(-XYZ.BasisX))
                {
                    if (end1.X < end2.X)
                    {
                        min = end1;
                        max = end2;
                    }
                    else
                    {
                        min = end2;
                        max = end1;
                    }
                    beamConect.StartPoint = min;
                    beamConect.EndPoint = max;
                    foreach (var item in listBeam)
                    {
                        LocationCurve lineItem = item.Location as LocationCurve;
                        XYZ itemPoint = lineItem.Curve.GetEndPoint(0);
                        bool checkTrue = CheckExtendBeam(min, max, item);
                        if (checkTrue)
                        {
                            if (itemPoint.X < min.X)
                            {
                                double dist = Math.Abs(min.X - itemPoint.X);
                                if (dist <= double.Parse(AppPanelExtend.myFormExtend.txtMaximunExtend.Text) / (0.3048 * 1000))
                                {
                                    if (distanceMin > dist)
                                    {
                                        distanceMin = dist;
                                        beamConect.StartBeam = item;
                                    }
                                }
                            }
                            else
                            {
                                double distma = Math.Abs(max.X - itemPoint.X);
                                if (distma <= double.Parse(AppPanelExtend.myFormExtend.txtMaximunExtend.Text) / (0.3048 * 1000))
                                {
                                    if (distanceMax > distma)
                                    {
                                        distanceMax = distma;
                                        beamConect.EndBeam = item;
                                    }
                                }
                            }
                        }

                    }
                }

            }
            else
            {
                if (vector.Normalize().IsAlmostEqualTo(XYZ.BasisY) || vector.Normalize().IsAlmostEqualTo(-XYZ.BasisY))
                {
                    if (end1.Y < end2.Y)
                    {
                        min = end1;
                        max = end2;
                    }
                    else
                    {
                        min = end2;
                        max = end1;
                    }
                    beamConect.StartPoint = min;
                    beamConect.EndPoint = max;
                    foreach (var item in listBeam)
                    {
                        LocationCurve lineItem = item.Location as LocationCurve;
                        XYZ itemPoint = lineItem.Curve.GetEndPoint(0);
                        bool checkTrue = CheckExtendBeam(min, max, item);
                        if (checkTrue)
                        {
                            if (itemPoint.Y < min.Y)
                            {
                                double dist = Math.Abs(min.Y - itemPoint.Y);
                                if (dist <= double.Parse(AppPanelExtend.myFormExtend.txtMaximunExtend.Text) / (0.3048 * 1000))
                                {
                                    if (distanceMin > dist)
                                    {
                                        distanceMin = dist;
                                        beamConect.StartBeam = item;
                                    }
                                }
                            }
                            else
                            {
                                double distma = Math.Abs(max.Y - itemPoint.Y);
                                if (distma <= double.Parse(AppPanelExtend.myFormExtend.txtMaximunExtend.Text) / (0.3048 * 1000))
                                {
                                    if (distanceMax > distma)
                                    {
                                        distanceMax = distma;
                                        beamConect.EndBeam = item;
                                    }
                                }
                            }
                        }

                    }
                }

            }
            return beamConect;

        }
        public bool CheckExtendBeam(XYZ pointMin, XYZ pointMax, FamilyInstance elementExtend)
        {
            bool result = false;
            LocationCurve line = elementExtend.Location as LocationCurve;
            XYZ end1 = line.Curve.GetEndPoint(0);
            XYZ end2 = line.Curve.GetEndPoint(1);
            XYZ start;
            XYZ end;           
            XYZ v = (end1 - end2).Normalize();
            if (AppPanelExtend.myFormExtend.radioHorizontalExtend.Checked)
            {
                if (end1.Y < end2.Y)
                {
                    start = end1;
                    end = end2;
                }
                else
                {
                    start = end2;
                    end = end1;
                }
                if (v.IsAlmostEqualTo(XYZ.BasisY) || v.IsAlmostEqualTo(-XYZ.BasisY))
                {
                    if ((!(end1.X >= pointMin.X && end1.X <= pointMax.X)) && (start.Y <= pointMin.Y && end.Y >= pointMin.Y))
                    {
                        result = true;
                    }
                }
            }
            else
            {
                if (end1.X < end2.X)
                {
                    start = end1;
                    end = end2;
                }
                else
                {
                    start = end2;
                    end = end1;
                }
                if (v.IsAlmostEqualTo(XYZ.BasisX) || v.IsAlmostEqualTo(-XYZ.BasisX))
                {
                    if ((!(end1.Y >= pointMin.Y && end1.Y <= pointMax.Y)) && (start.X <= pointMin.X && end.X >= pointMin.X))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

    }


    public class BeamConnect
    {
        public ElementId BeamMainId { set; get; }

        public XYZ StartPoint { get; set; }
        public FamilyInstance StartBeam { set; get; }

        public XYZ EndPoint { set; get; }
        public FamilyInstance EndBeam { get; set; }

    }


}
