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
using System.IO;

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
        public void SetValueParameter(Parameter parameter,string value)
        {           
            using(Transaction t = new Transaction(_uiApp.ActiveUIDocument.Document,"Set value parameter"))
            {
                t.Start();
                try { parameter.Set(value); }
                catch(Exception ex) { };                     
                t.Commit();
            }
            
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


        //Create ShareParameter
       public void CreateParamerterRe(string groupName, string parameterName, BuiltInCategory category)
        {
            Application app = _uiApp.Application;
            Document doc = _uiApp.ActiveUIDocument.Document;
            DefinitionFile defitionFile = app.OpenSharedParameterFile();          
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"\Autodesk\ShareParameterArmo.txt");
            if (defitionFile == null)
            {
                StreamWriter stream;
                stream = new StreamWriter(path);
                stream.Close();
                app.SharedParametersFilename = path;                             
                defitionFile = app.OpenSharedParameterFile();
            }
            using(Transaction t = new Transaction(doc,"Create shareparameter"))
            {
                t.Start();
                //set paramneter
                SetNewParameterToInstance(defitionFile, category, groupName, parameterName);
                t.Commit();
            }  

        }

        //add parameter
        public bool SetNewParameterToInstance(DefinitionFile myDefitionfile, BuiltInCategory category,string groupName,string parameter)
        {
            try
            {
                DefinitionGroups myGroups = myDefitionfile.Groups;
                DefinitionGroup myGroup = null;
                Definition myDefination_ProductDate=null;
                foreach(var item in myGroups)
                {
                    if (item.Name == groupName)
                    {
                        myGroup = item;
                        myDefination_ProductDate = item.Definitions.get_Item(parameter);
                        if (myDefitionfile == null)
                        {
                            ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(parameter, ParameterType.Text);
                            myDefination_ProductDate = myGroup.Definitions.Create(option);
                        }
                       
                        break;
                    }
                }
                if (myGroup == null)
                {
                    myGroup = myGroups.Create(groupName);
                    ExternalDefinitionCreationOptions option = new ExternalDefinitionCreationOptions(parameter, ParameterType.Text);
                    myDefination_ProductDate = myGroup.Definitions.Create(option);
                }

                CategorySet categorySet = _uiApp.Application.Create.NewCategorySet();
                Category myCategory = _uiApp.ActiveUIDocument.Document.Settings.Categories.get_Item(category);
                categorySet.Insert(myCategory);
                InstanceBinding instantBinding = _uiApp.Application.Create.NewInstanceBinding(categorySet);
                BindingMap bindingMap = _uiApp.ActiveUIDocument.Document.ParameterBindings;

                bool instanceBindOk=bindingMap.Insert(myDefination_ProductDate, instantBinding,BuiltInParameterGroup.PG_TEXT);

                return instanceBindOk;
  
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        
    }
}
