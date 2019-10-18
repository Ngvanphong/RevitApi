using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ArmoApiVn.Common;
using Common.Extension;
using System.Windows.Forms;

namespace ArmoApiVn.SettingDoor
{
    [Transaction(TransactionMode.Manual)]
    public class SettingDoorBinding : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            if (CheckLicenseApi.CheckLicense() == true)
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                AppPenalSettingDoor.nameDoorSetting = doc.Title + "DoorSetting.xml";
                AppPenalSettingDoor.ShowSettingDoor();
                GetInforDoor(doc);
                GetPropertiesInfor(doc);
                GetTypeNoteSection(doc);
            }
            return Result.Succeeded;
        }

        public void GetInforDoor(Document doc)
        {
            
            IEnumerable<Family> families = new FilteredElementCollector(doc).OfClass(typeof(Family))
                .Cast<Family>().Where<Family>(f => LookupParamaterRe.FamilyFirstSymbolCategoryEquals(f, BuiltInCategory.OST_Doors));
            foreach(var fa in families.OrderBy(x=>x.Name))
            {
                ListViewItem item = new ListViewItem(fa.Name);
                AppPenalSettingDoor.myFormSettingDoor.listViewFamilyDoor.Items.Add(item);
            }

        }
        public void GetPropertiesInfor(Document doc)
        {
            IEnumerable<Family> families = new FilteredElementCollector(doc).OfClass(typeof(Family))
               .Cast<Family>().Where<Family>(f => LookupParamaterRe.FamilyFirstSymbolCategoryEquals(f, BuiltInCategory.OST_Doors));
            List<string> listParameter = new List<string>();
            foreach( Family family in families)
            {
                FamilySymbolFilter filterFamSym = new FamilySymbolFilter(family.Id);
                FilteredElementCollector famSymbols = new FilteredElementCollector(doc);
                famSymbols.WherePasses(filterFamSym);
                foreach (FamilySymbol famSymbol in famSymbols)
                {
                    var paras = famSymbol.Parameters;
                    foreach(Parameter pa in paras)
                    {
                        if (!listParameter.Exists(x => x == pa.Definition.Name))
                        {
                            listParameter.Add(pa.Definition.Name);
                        }
                    }                    
                }
            }
            foreach(string name in listParameter.OrderBy(x=>x))
            {
                ListViewItem item = new ListViewItem(name);
                AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Add(item);
            }
        }

        public void GetTypeNoteSection(Document doc)
        {
            var typeSection  = new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
                .Where(x=>x.ViewFamily==ViewFamily.Section).OrderBy(x=>x.Name).ToList();
            var typeNote = new FilteredElementCollector(doc).OfClass(typeof(TextNoteType)).Cast<TextNoteType>().OrderBy(x=>x.Name).ToList();
            foreach(var typess in typeSection)
            {
                AppPenalSettingDoor.myFormSettingDoor.comboBoxSectionType.Items.Add( typess.Name );
            }
            foreach (var typeText in typeNote)
            {
                AppPenalSettingDoor.myFormSettingDoor.comboBoxTextNoteType.Items.Add(typeText.Name);
            }
        }
    }
}
