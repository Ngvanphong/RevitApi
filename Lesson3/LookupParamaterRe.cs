using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace Lesson3
{
    public class LookupParamaterRe
    {
        UIApplication _uiApp;
        Document _doc;
        public LookupParamaterRe(UIApplication uiApp)
        {
            _uiApp = uiApp;
            _doc = _uiApp.ActiveUIDocument.Document;
        }

        public List<FamilyElement> LookValuePramater(string parameter, BuiltInCategory categoryType)
        {

            FilteredElementCollector collector = new FilteredElementCollector(_doc);
            ElementCategoryFilter filters = new ElementCategoryFilter(categoryType);
            IList<Element> elementCategories = collector.OfClass(typeof(FamilyInstance)).WherePasses(filters).ToElements();

            IEnumerable<Family> families = new FilteredElementCollector(_doc).OfClass(typeof(Family))
                .Cast<Family>().Where<Family>(f => FamilyFirstSymbolCategoryEquals(f, categoryType));

            List<FamilyElement> familyElemtents = new List<FamilyElement>();

            foreach (Family family in families)
            {
                FamilySymbolFilter filterFamSym = new FamilySymbolFilter(family.Id);
                FilteredElementCollector famSymbols = new FilteredElementCollector(_doc);
                famSymbols.WherePasses(filterFamSym);

                foreach (FamilySymbol famSymbol in famSymbols)
                {
                    foreach (FamilyInstance item in elementCategories)
                    {

                        if ( item.Symbol.FamilyName != "枠")
                        {
                            if (item.Symbol.FamilyName == family.Name)
                            {
                                FamilyElement familyElement = new FamilyElement(family.Name, item.Name, item.Id);
                                if (familyElemtents.Exists(x => (x.NameFamily == family.Name && x.NameTypeFamily == item.Name)) == false)
                                {
                                    familyElemtents.Add(familyElement);
                                }

                            }
                        }

                    }
                }

            }


            foreach (var item in familyElemtents)
            {

                FamilyInstance element = _doc.GetElement(item.ElementIdSection) as FamilyInstance;
                //Symbol if us share parameter
                string[] arrListStrParameter = parameter.Split(';');
                foreach(string para in arrListStrParameter)
                {
                    var parameterfind = element.Symbol.LookupParameter(para);
                    if (parameterfind == null) parameterfind = element.LookupParameter(para);
                    string valueParameter = ParameterToString(parameterfind);
                    switch (para)
                    {
                        case "Width":
                            item.Width = valueParameter;
                                break;
                        case "Height":
                            item.Height = valueParameter;
                            break;
                        case "Door_W":
                            item.Door_W = valueParameter;
                            break;                           
                    }
                }
               
            }

            return familyElemtents;

        }

        static bool FamilyFirstSymbolCategoryEquals(Family f, BuiltInCategory bic)
        {
            Document doc = f.Document;

            ISet<ElementId> ids = f.GetFamilySymbolIds();

            Category cat = (0 == ids.Count) ? null : doc.GetElement(ids.First<ElementId>()).Category;

            return null != cat && cat.Id.IntegerValue.Equals((int)bic);
        }

        public static string ParameterToString(Parameter param)
        {
            string val = "none";

            if (param == null)
            {
                return val;
            }

            // To get to the parameter value, we need to pause it depending on its storage type 

            switch (param.StorageType)
            {
                case StorageType.Double:
                    double dVal = param.AsDouble() * 0.3048*1000;
                    val = dVal.ToString();
                    break;
                case StorageType.Integer:
                    int iVal = param.AsInteger();
                    val = iVal.ToString();
                    break;
                case StorageType.String:
                    string sVal = param.AsString();
                    val = sVal;
                    break;
                case StorageType.ElementId:
                    ElementId idVal = param.AsElementId();
                    val = idVal.IntegerValue.ToString();
                    break;
                case StorageType.None:
                    break;
            }
            return val;
        }
    }

    public class FamilyElement
    {
        public string NameFamily { get; set; }
        public string NameTypeFamily { set; get; }
        public ElementId ElementIdSection { set; get; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Door_W { get; set; }

        public FamilyElement()
        {

        }

        public FamilyElement(string nameFamily, string nameTypeFamily, ElementId elementIdSection)
        {
            NameFamily = nameFamily;
            NameTypeFamily = nameTypeFamily;
            ElementIdSection = elementIdSection;
           
        }
    }


}
