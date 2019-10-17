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
            }
            return Result.Succeeded;
        }

        public void GetInforDoor(Document doc)
        {
            
            IEnumerable<Family> families = new FilteredElementCollector(doc).OfClass(typeof(Family))
                .Cast<Family>().Where<Family>(f => LookupParamaterRe.FamilyFirstSymbolCategoryEquals(f, BuiltInCategory.OST_Doors));
            foreach(var fa in families)
            {
                ListViewItem item = new ListViewItem(fa.Name);
                AppPenalSettingDoor.myFormSettingDoor.listViewFamilyDoor.Items.Add(item);
            }

        }
    }
}
