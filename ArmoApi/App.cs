#region Namespaces
using System;
using System.Collections.Generic;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media;
using ArmoApi.Button;
#endregion

namespace ArmoApi
{
    class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            CreateSectionForDoorButton createSectionClass = new CreateSectionForDoorButton();
            createSectionClass.CreateSection(application);
            CreateTextForDoorButton createTextClass = new CreateTextForDoorButton();
            createTextClass.CreateText(application);
            UpdateSectionForDoorButton updateDoorClass = new UpdateSectionForDoorButton();
            updateDoorClass.UpdateDoorSection(application);
            return Result.Succeeded;
        }

       
    }
}
