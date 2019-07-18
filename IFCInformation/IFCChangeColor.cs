using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Visual;

namespace IFCInformation
{
   public class IFCChangeColor
    {
        public void ChangeColorIFC(UIApplication uiApp)
        {
            try
            {
                Document doc = uiApp.ActiveUIDocument.Document;
                UIDocument uIDoc = uiApp.ActiveUIDocument;
                Reference refer = uIDoc.Selection.PickObject(ObjectType.Element);
                Element element = doc.GetElement(refer);
                Material m = GetMaterialValue(doc, "MaterialIFC");
                var pSolidFillPattern = new FilteredElementCollector(doc).OfClass((typeof(FillPatternElement)))
                    .OfType<FillPatternElement>().Where<FillPatternElement>(p => p.GetFillPattern().IsSolidFill).ToList().First();
                Color color;
                using (Transaction t = new Transaction(doc, "Change meterial"))
                {
                    t.Start();
                    element.Category.Material = m;
                    color = m.Color;
                    OverrideGraphicSettings setting = new OverrideGraphicSettings();
                    setting.SetProjectionLineColor(color);
                    setting.SetProjectionFillColor(color);
                    setting.SetCutFillColor(color);
                    setting.SetCutLineColor(color);
                    setting.SetCutFillPatternId(pSolidFillPattern.Id);
                    setting.SetCutFillPatternId(pSolidFillPattern.Id);
                    setting.SetProjectionFillPatternVisible(true);
                    setting.SetCutFillPatternVisible(true);
                    setting.SetSurfaceTransparency(0);
                    doc.ActiveView.SetElementOverrides(element.Id, setting);
                    ICollection<ElementId> Lmats = element.GetMaterialIds(false);
                    foreach (ElementId mat in Lmats)
                    {
                        Element elementMaterial = doc.GetElement(mat);
                        Material ma = GetMaterialValue(doc, elementMaterial.Name) as Material;
                        ma.Color = color;                     
                    }
                    t.Commit();
                }
            }
            catch
            {
                TaskDialog.Show("Error", "You must create MaterialIFC in Materials");
            }
        }
        public Material GetMaterialValue(Document doc, string matName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Material mat = null;
            var materials = collector.WherePasses(new ElementClassFilter(typeof(Material))).Cast<Material>();
            foreach (Material m in materials)
            {
                if (m.Name == matName)
                {
                    mat = m;
                    break;
                }
            }

            return mat;
        }
    }
    
}
