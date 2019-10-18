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
            try
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
                var listPara = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.CheckedItems;
                foreach (ListViewItem para in listPara)
                {
                    writer.WriteStartElement("Properties");
                    writer.WriteString(para.Text);
                    writer.WriteEndElement();
                }
                var typeSection = AppPenalSettingDoor.myFormSettingDoor.comboBoxSectionType.SelectedItem.ToString();
                var typeNote = AppPenalSettingDoor.myFormSettingDoor.comboBoxTextNoteType.SelectedItem.ToString();
                writer.WriteStartElement("SectionType");
                writer.WriteString(typeSection);
                writer.WriteEndElement();

                writer.WriteStartElement("TextType");
                writer.WriteString(typeNote);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
                AppPenalSettingDoor.myFormSettingDoor.Hide();
            }
            catch
            {
                MessageBox.Show("You must input properties");
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnDownProperties_Click(object sender, EventArgs e)
        {
            if (AppPenalSettingDoor.index == 1)
            {
                var listSelect = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.SelectedItems;
                AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Clear();
                foreach (ListViewItem item in listSelect)
                {
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Add(item);
                }
            }
        }

        private void btnTopParameter_Click(object sender, EventArgs e)
        {
            if (AppPenalSettingDoor.index == 1)
            {
                var listSelect = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.CheckedItems;
                List<ListViewItem> listViewItem = new List<ListViewItem>();
                foreach (ListViewItem item in listSelect)
                {
                    listViewItem.Add(item);
                }
                AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Clear();
                foreach (ListViewItem item in listViewItem)
                {
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Add(item);
                    item.Checked = false;
                }
                AppPenalSettingDoor.index = 2;
            }
            var selectMove = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.CheckedItems;
            foreach(ListViewItem item in selectMove)
            {
                if (item.Index > 0)
                {
                    int nowIndex = item.Index - 1;
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.RemoveAt(item.Index);
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Insert(nowIndex, item);
                }
            }
        }

        private void btnDownProperties_Click_1(object sender, EventArgs e)
        {
            if (AppPenalSettingDoor.index == 1)
            {
                var listSelect = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.CheckedItems;
                List<ListViewItem> listViewItem = new List<ListViewItem>();
                foreach (ListViewItem item in listSelect)
                {
                    listViewItem.Add(item);
                }
                AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Clear();
                foreach (ListViewItem item in listViewItem)
                {
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Add(item);
                    item.Checked = false;
                }
                AppPenalSettingDoor.index = 2;
            }

            var selectMove = AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.CheckedItems;
            foreach (ListViewItem item in selectMove)
            {
                if (item.Index < AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Count-1)
                {
                    int nowIndex = item.Index + 1;
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.RemoveAt(item.Index);
                    AppPenalSettingDoor.myFormSettingDoor.listViewChooseParameter.Items.Insert(nowIndex, item);
                }
            }


        }

        private void frmSettingDoor_Load(object sender, EventArgs e)
        {

        }
    }
}
