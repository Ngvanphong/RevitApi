using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
namespace CreateBeamByCad
{
   public static class AppPanelAll
    {
        public static frmCreateBeamAll formCreateBeamAll;
        public static InformationBeam informationBeam;
        public static frmLoad frmLoadProgess;
        public static void ShowCreateForm(UIApplication uiApp)
        {
            if (formCreateBeamAll == null || formCreateBeamAll.IsDisposed)
            {                
                HandlerCreateBeamAll handler = new HandlerCreateBeamAll();
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                formCreateBeamAll = new frmCreateBeamAll(exEvent, handler);
                formCreateBeamAll.Show();
            }  
        }
        
    }
}
