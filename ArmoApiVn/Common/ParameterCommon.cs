using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmoApiVn.Common
{
    public static class ParameterCommon
    {
        public static string Door = Door3 + ";" + Door4 + ";" + Door5;
        //Not change
        public const string Door1 = "NameFamily";

        public const string Door2 = "Name";

        //Can change ( Must change class: "ValueDoorText", "FamilyElement")
        public static string Door3 = "Width";

        public static string Door4 = "Height";

        public static string Door5 = "Door_W";

        /// <summary>
        /// Finish parameter
        /// </summary>

        public static string TextTypeChose = "2.5mm Arial";

        public static string SectionStypeChose = "Building Section";

        private static List<string> GetNamePara()
        {
            List<string> listString = new List<string>();
            return listString;
        }

    }
    
}
