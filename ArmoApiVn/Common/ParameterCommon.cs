using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArmoApiVn.Common
{
    public static class ParameterCommon
    {
        public static string Door = Door3+ ";" + Door4+ ";" + Door5;
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

    }
    public class GetProperites
    {
        public GetProperites()
        {

        }
        public static void UpdateProperties(string name)
        {
            try
            {
                string fullPath = Path.GetFullPath(name);
                var xmlDoc = XDocument.Load(fullPath);
                var xmlElement = xmlDoc.Element("Table").Elements("Properties");
                int index = 0;
                foreach(var element in xmlElement)
                {
                    if (index == 0)
                    {
                        ParameterCommon.Door3= element.Value; 
                    } else if (index == 1)
                    {
                        ParameterCommon.Door4 = element.Value;
                    }
                    else if (index == 2)
                    {
                        ParameterCommon.Door5 = element.Value;
                    }
                    index = index+1;
                }
                ParameterCommon.TextTypeChose=  xmlDoc.Element("Table").Elements("TextType").First().Value;
                ParameterCommon.SectionStypeChose = xmlDoc.Element("Table").Elements("SectionType").First().Value;
                ParameterCommon.Door = ParameterCommon.Door3 + ";" + ParameterCommon.Door4 + ";" + ParameterCommon.Door5;
            }
            catch
            {
                ParameterCommon.Door = ParameterCommon.Door3 + ";" + ParameterCommon.Door4 + ";" + ParameterCommon.Door5;
            }
            
        }
    }
    
}
