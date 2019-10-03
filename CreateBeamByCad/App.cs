#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CreateBeamByCad.Button;
#endregion

namespace CreateBeamByCad
{
    class App : IExternalApplication
    {
       
        public Result OnStartup(UIControlledApplication a)
        {
            CreateBeamButton createBeamClass = new CreateBeamButton();
            createBeamClass.CreateBeam(a);
            //CreateBeamAllButton createBeamAllClass = new CreateBeamAllButton();
            //createBeamAllClass.CreateButtonAll(a);          
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
           
            return Result.Succeeded;
        }
    }
}
