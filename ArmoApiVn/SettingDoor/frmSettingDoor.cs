using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ArmoApiVn.SettingDoor
{
    public partial class frmSettingDoor : Form
    {
        public frmSettingDoor()
        {
            InitializeComponent();
        }

        private void btnSettingDoor_Click(object sender, EventArgs e)
        {
            var listDoorCheck = AppPenalSettingDoor.myFormSettingDoor.listViewFamilyDoor.CheckedItems;
            string name = AppPenalSettingDoor.nameDoorSetting;
            XmlTextWriter writer = new XmlTextWriter(name, System.Text.Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("Table");
            foreach (ListViewItem item in listDoorCheck)
            {
                writer.WriteStartElement("FamilyDoor");
                writer.WriteString(item.Text);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
            AppPenalSettingDoor.myFormSettingDoor.Hide();          
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
