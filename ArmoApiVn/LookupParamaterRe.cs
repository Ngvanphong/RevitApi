﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApiVn.Common;
using System.IO;
using System.Xml.Linq;
using ArmoApiVn.SettingDoor;
using System.Windows.Forms;

namespace ArmoApiVn
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
            IEnumerable<XElement> xmlElement=null;
            try
            {
                string name = _doc.Title + "DoorSetting.xml";
                string fullPath = Path.GetFullPath(name);
                var xmlDoc = XDocument.Load(fullPath);
                xmlElement = xmlDoc.Element("Table").Elements("FamilyDoor");
            }
            catch {}
            
            foreach (Family family in families)
            {
                FamilySymbolFilter filterFamSym = new FamilySymbolFilter(family.Id);
                FilteredElementCollector famSymbols = new FilteredElementCollector(_doc);
                famSymbols.WherePasses(filterFamSym);

                foreach (FamilySymbol famSymbol in famSymbols)
                {
                    foreach (FamilyInstance item in elementCategories)
                    {

                        if (CheckFamilyExcept(item.Symbol.Name,xmlElement) == false)
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
                foreach (string para in arrListStrParameter)
                {
                    var parameterfind = element.Symbol.LookupParameter(para);
                    if (parameterfind == null) parameterfind = element.LookupParameter(para);
                    string valueParameter = ParameterToString(parameterfind);
                    if (para == ParameterCommon.Door3)
                    {
                        item.Door3 = valueParameter;
                    }
                    else if (para == ParameterCommon.Door4)
                    {
                        item.Door4 = valueParameter;
                    }
                    else if (para == ParameterCommon.Door5)
                    {
                        item.Door5 = valueParameter;
                    }
                }

            }

            return familyElemtents;

        }

        public FamilyElement LookValuePramaterForOneElement(string parameter, BuiltInCategory categoryType, FamilyInstance elementDoor)
        {

            FamilyElement familyElemtent = new FamilyElement();
            familyElemtent.NameFamily = elementDoor.Symbol.FamilyName;
            familyElemtent.NameTypeFamily = elementDoor.Name;
            familyElemtent.ElementIdSection = elementDoor.Id;
            //Symbol if us share parameter
            string[] arrListStrParameter = parameter.Split(';');
            foreach (string para in arrListStrParameter)
            {
                var parameterfind = elementDoor.Symbol.LookupParameter(para);
                if (parameterfind == null) parameterfind = elementDoor.LookupParameter(para);
                string valueParameter = ParameterToString(parameterfind);
                if (para == ParameterCommon.Door3)
                {
                    familyElemtent.Door3 = valueParameter;
                }
                else if (para == ParameterCommon.Door4)
                {
                    familyElemtent.Door4 = valueParameter;
                }
                else if (para == ParameterCommon.Door5)
                {
                    familyElemtent.Door5 = valueParameter;
                }
            }
            return familyElemtent;

        }

       public static bool FamilyFirstSymbolCategoryEquals(Family f, BuiltInCategory bic)
        {
            Document doc = f.Document;

            ISet<ElementId> ids = f.GetFamilySymbolIds();

            Category cat = (0 == ids.Count) ? null : doc.GetElement(ids.First<ElementId>()).Category;

            return null != cat && cat.Id.IntegerValue.Equals((int)bic);
        }

        public static bool CheckFamilyExcept(string familyName,IEnumerable<XElement> xmlElement)
        {
            bool except = false;
            try
            { 
                if (xmlElement.Count() > 0)
                {
                    if (xmlElement.Where(x => x.Value == familyName).Count() > 0)
                    {
                        except = true;
                    }
                }
            }
            catch
            {
                return except;
            }           
            return except;
        }

        public static string ParameterToString(Parameter param)
        {
            string val = "none";

            if (param == null)
            {
                return val;
            }

            switch (param.StorageType)
            {
                case StorageType.Double:
                    double dVal = param.AsDouble() * 0.3048 * 1000;
                    val = dVal.ToString();
                    break;
                case StorageType.Integer:
                    string iVal = param.AsValueString();
                    val = iVal;
                    break;
                case StorageType.String:
                    string sVal = param.AsString();
                    val = sVal;
                    break;
                case StorageType.ElementId:
                    string idVal = param.AsValueString();
                    val = idVal;
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
        //Change when add parameter;
        public string Door3 { get; set; }
        public string Door4 { get; set; }
        public string Door5 { get; set; }

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
