using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace MainProject.EntendForBeam
{

 public static class AppPanelExtend
    {
        public static fromExtendBeam myFormExtend;
        public static void ShowExtendForm(UIApplication uiApp)
        {
            if (myFormExtend == null || myFormExtend.IsDisposed)
            {
                ExtendBeamHandler handler = new ExtendBeamHandler();               
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                myFormExtend = new fromExtendBeam(exEvent, handler);
                myFormExtend.Show();
            }
        }


    }
}
