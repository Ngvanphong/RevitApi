using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace Lesson3
{
  
    public class ParameterRe 
    {
        UIApplication _uiApp;
        public ParameterRe(UIApplication uIApp)
        {
            _uiApp = uIApp;
        }
        //set parameter
        public bool SetValueParameter(Parameter parameter,string value)
        {
            bool status = false;
            using(Transaction t = new Transaction(_uiApp.ActiveUIDocument.Document,"Set value parameter"))
            {
                t.Start();
                if (IsNumeric(value))
                {
                    parameter.Set(value);
                }
                else
                {
                    parameter.SetValueString(value);
                }
            
                status = true;
                t.Commit();
            }
            return status;
        }

        static bool IsNumeric(string value)
        {
            try
            {
                char[] chars = value.ToCharArray();
                foreach (char c in chars)
                {
                    if (!char.IsNumber(c))
                        return false;
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }


    }
}
