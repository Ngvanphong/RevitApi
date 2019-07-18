using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Visual;
namespace MainProject.ChangeColorComponent
{
   public class ChangeColor
    {
        public void ChangeColorComponent(UIApplication uiApp)
        {
            Document doc = uiApp.ActiveUIDocument.Document;
            UIDocument uIDoc = uiApp.ActiveUIDocument;
            Reference refer = uIDoc.Selection.PickObject(ObjectType.Element);
            Element element = doc.GetElement(refer);
            Material m = GetMaterialValue(doc, "MaterialIFC");                 
            using (Transaction t = new Transaction(doc, "Change meterial"))
            {
                t.Start();                                
                ICollection<ElementId> Lmats = element.GetMaterialIds(false);
                foreach (ElementId mat in Lmats)
                {
                    Element elementMaterial = doc.GetElement(mat);
                    Material ma = GetMaterialValue(doc, elementMaterial.Name) as Material;
                    ma = m;
                    try { element.LookupParameter("Structural Material").Set(m.Id); } catch { }
                    try
                    {
                        var instance = element as FamilyInstance;
                        var symtem = instance.Symbol.LookupParameter("Structural Material");
                        symtem.Set(m.Id);
                    }
                    catch { }
                    ;
                }

                t.Commit();
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
