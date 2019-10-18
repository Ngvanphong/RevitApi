using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmoApiVn.SettingDoor
{
    public static class AppPenalSettingDoor
    {
        public static frmSettingDoor myFormSettingDoor;
        public static string nameDoorSetting = string.Empty;
        public static int index;
        public static void ShowSettingDoor()
        {
            index = 1;
            myFormSettingDoor = new frmSettingDoor();
            myFormSettingDoor.Show();
        }
    }
}
