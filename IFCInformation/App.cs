#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using IFCInformation.Button;
#endregion

namespace IFCInformation
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            ButtonChangeColor buttonChangeClass = new ButtonChangeColor();
            buttonChangeClass.ButtonChageColor(a);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
