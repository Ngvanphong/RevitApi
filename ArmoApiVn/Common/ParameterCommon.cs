using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmoApiVn.Common
{
    public static class ParameterCommon
    {
        public const string Door = Door3 + ";" + Door4 + ";" + Door5;
        //Not change
        public const string Door1 = "NameFamily";

        public const string Door2 = "Name";
        //Can change ( Must change class: "ValueDoorText", "FamilyElement")
        public const string Door3 = "Width";

        public const string Door4 = "Height";

        public const string Door5 = "Door_W";

        /// <summary>
        /// Finish parameter
        /// </summary>

        public const string TextTypeChose = "2.5mm Arial";

        public const string SectionStypeChose = "Building Section";

        public const string FamilyNotUser = "枠";

        public const string FamilyNotUser2 = "枠";
    }
}
