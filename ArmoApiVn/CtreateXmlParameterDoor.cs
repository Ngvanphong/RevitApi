using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.IO;
using System.Xml.Linq;
using Autodesk.Revit.ApplicationServices;
using System.Net;
using System.Runtime.Serialization.Json;



namespace ArmoApiVn
{
    public class CtreateXmlParameterDoor
    {

        public void CreateFileDoor(UIApplication uiApp, List<FamilyElement> listElementDoors)
        {
            Document doc = uiApp.ActiveUIDocument.Document;
            Application app = uiApp.Application;
            string name = doc.Title + ".xml";
            //String hostId = "192.168.3.8";
            //String rootFolder = "|";
            //ModelPath serverPath = ServerCentral.FindWSAPIModelPathOnServer(app, hostId, rootFolder, name);
            //String sourcePath = ModelPathUtils.ConvertModelPathToUserVisiblePath(serverPath);
            XmlTextWriter writer = new XmlTextWriter(name, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Table");
            foreach (var item in listElementDoors)
            {
                CreateNodeFile(item, writer);
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

        }
        public void SaveFileDoor(Document doc,FamilyElement listElementDoor)
        {
            string name = doc.Title + ".xml";
            string fullPath = Path.GetFullPath(name);
            var xmlDoc = XDocument.Load(fullPath);
            XElement elem = new XElement("Door",
            new XElement("Id", listElementDoor.ElementIdSection.ToString()),
            new XElement("NameFamily", listElementDoor.NameFamily),
            new XElement("NameTypeFamily", listElementDoor.NameTypeFamily),
            new XElement("Width", listElementDoor.Width),
            new XElement("Height", listElementDoor.Height),
            new XElement("Door_W", listElementDoor.Door_W)
         );
            xmlDoc.Element("Table").Add(elem);
            var xmltex = xmlDoc.Element("Table");
            xmlDoc.Save(name);

        }
        public void RemoveItemFileDoor(Document doc, FamilyElement listElementDoor)
        {
            string name = doc.Title + ".xml";
            string fullPath = Path.GetFullPath(name);
            var xmlDoc = XDocument.Load(fullPath);
            var doorLists = xmlDoc.Element("Table").Elements("Door");
            foreach(var item in doorLists)
            {
                if(item.Element("Id").Value== listElementDoor.ElementIdSection.ToString())
                {
                    item.Remove();
                    break;                   
                }
            }   
            xmlDoc.Save(name);
        }
        public void CreateNodeFile(FamilyElement element, XmlTextWriter writer)
        {
            writer.WriteStartElement("Door");
            writer.WriteStartElement("Id");
            writer.WriteString(element.ElementIdSection.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("NameFamily");
            writer.WriteString(element.NameFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("NameTypeFamily");
            writer.WriteString(element.NameTypeFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("Width");
            writer.WriteString(element.Width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(element.Height);
            writer.WriteEndElement();
            writer.WriteStartElement("Door_W");
            writer.WriteString(element.Door_W);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public void CreateFileTextDoor(Document doc, ValueDoorText valueText)
        {            
            string name = doc.Title + "valuetext.xml";
            string fullPath = Path.GetFullPath(name);
            try
            {
           
                var xmlDoc = XDocument.Load(fullPath);              
                XElement elem = new XElement("TextDoor",
                new XElement("Id", valueText.TextId.ToString()),
                new XElement("Name", valueText.Name),
                new XElement("NameFamily", valueText.NameFamily),
                new XElement("ElementId", valueText.ElementIdSection.ToString()),
                new XElement("Width", valueText.Width),
                new XElement("Height", valueText.Height),
                new XElement("Door_W", valueText.Door_W)
             );
                xmlDoc.Element("Table").Add(elem);
                var xmltex = xmlDoc.Element("Table");
                xmlDoc.Save(name);
            }
            catch (Exception ex)
            {
                XmlTextWriter writer = new XmlTextWriter(name, System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("Table");
                CreateNodeText(valueText, writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }

        }
        public void CreateNodeText(ValueDoorText element, XmlTextWriter writer)
        {
            writer.WriteStartElement("TextDoor");
            writer.WriteStartElement("Id");
            writer.WriteString(element.TextId.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Name");
            writer.WriteString(element.Name);
            writer.WriteEndElement();
            writer.WriteStartElement("NameFamily");
            writer.WriteString(element.NameFamily);
            writer.WriteEndElement();
            writer.WriteStartElement("ElementId");
            writer.WriteString(element.ElementIdSection.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Width");
            writer.WriteString(element.Width);
            writer.WriteEndElement();
            writer.WriteStartElement("Height");
            writer.WriteString(element.Height);
            writer.WriteEndElement();
            writer.WriteStartElement("Door_W");
            writer.WriteString(element.Door_W);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }
    public static class ServerCentral
    {
        public static ModelPath FindWSAPIModelPathOnServer(Application app, string hostId, string folderName, string fileName)
        {
            // Connect to host to find list of available models (the "/contents" flag)
            XmlDictionaryReader reader = GetResponse(app, hostId, folderName + "/contents");
            bool found = false;

            // Look for the target model name in top level folder
            List<String> folders = new List<String>();
            while (reader.Read())
            {
                // Save a list of subfolders, if found
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Folders")
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Folders")
                            break;

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
                        {
                            reader.Read();
                            folders.Add(reader.Value);
                        }
                    }
                }
                // Check for a matching model at this folder level
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Models")
                {
                    found = FindModelInServerResponseJson(reader, fileName);
                    if (found)
                        break;
                }
            }

            reader.Close();

            // Build the model path to match the found model on the server
            if (found)
            {
                // Server URLs use "|" for folder separation, Revit API uses "/"
                String folderNameFragment = folderName.Replace('|', '/');

                // Add trailing "/" if not present
                if (!folderNameFragment.EndsWith("/"))
                    folderNameFragment += "/";

                // Build server path
                ModelPath modelPath = new ServerPath(hostId, folderNameFragment + fileName);
                return modelPath;
            }
            else
            {
                // Try subfolders
                foreach (String folder in folders)
                {
                    ModelPath modelPath = FindWSAPIModelPathOnServer(app, hostId, folder, fileName);
                    if (modelPath != null)
                        return modelPath;
                }
            }

            return null;
        }

        // This string is different for each RevitServer version
        private static string s_revitServerVersion = "/RevitServerAdminRESTService2014/AdminRESTService.svc/";

        /// <summary>
        /// Connect to server to get list of available models and return server response
        /// </summary>
        private static XmlDictionaryReader GetResponse(Application app, string hostId, string info)
        {
            // Create request	
            WebRequest request = WebRequest.Create("http://" + hostId + s_revitServerVersion + info);
            request.Method = "GET";

            // Add the information the request needs

            request.Headers.Add("User-Name", app.Username);
            request.Headers.Add("User-Machine-Name", app.Username);
            request.Headers.Add("Operation-GUID", Guid.NewGuid().ToString());

            // Read the response
            XmlDictionaryReaderQuotas quotas =
                new XmlDictionaryReaderQuotas();
            XmlDictionaryReader jsonReader =
                JsonReaderWriterFactory.CreateJsonReader(request.GetResponse().GetResponseStream(), quotas);

            return jsonReader;
        }

        /// <summary>
        /// Read through server response to find particular model
        /// </summary>
        private static bool FindModelInServerResponseJson(XmlDictionaryReader reader, string fileName)
        {
            // Read through entries in this section
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Models")
                    break;

                if (reader.NodeType == XmlNodeType.Element && reader.Name == "Name")
                {
                    reader.Read();
                    String modelName = reader.Value;
                    if (modelName.Equals(fileName))
                    {
                        // Match found, stop looping and return
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
