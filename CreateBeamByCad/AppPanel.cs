using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CreateBeamByCad
{
   public static class AppPanel
    {        
        public static frmCreateBeamCad myFormCreate;
        public static List<Element> listSelectLine;  
        public static void ShowCreateForm(UIApplication uiApp)
        {
            if (myFormCreate == null || myFormCreate.IsDisposed)
            {
                HandlerSelectLine hanlerSelectLine = new HandlerSelectLine();
                HandlerCreateBeam handler = new HandlerCreateBeam();
                ExternalEvent exEvent = ExternalEvent.Create(handler);
                ExternalEvent exEventSelectLine = ExternalEvent.Create(hanlerSelectLine);
                myFormCreate = new frmCreateBeamCad(exEvent, handler, exEventSelectLine, hanlerSelectLine);                             
                myFormCreate.Show();
            }
        }
       
    }
}
