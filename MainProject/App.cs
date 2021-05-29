#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MainProject.Button;
#endregion

namespace MainProject
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            ExtendBeamButton extendbtClass = new ExtendBeamButton();
            extendbtClass.ExtendButton(a);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
