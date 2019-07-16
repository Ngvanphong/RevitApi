using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreateBeamByCad
{
    public partial class frmCreateBeamCad : Form
    {
        private ExternalEvent _event;
        private ExternalEvent _eventSelectLine;
        private HandlerCreateBeam _handler;
        private HandlerSelectLine _handlerSelecline;
        public frmCreateBeamCad(ExternalEvent eventset,HandlerCreateBeam handler, ExternalEvent eventSelectLine, HandlerSelectLine handlerSelecline)
        {
            InitializeComponent();
            _event = eventset;
            _handler = handler;
            _eventSelectLine = eventSelectLine;
            _handlerSelecline = handlerSelecline;
        }
       
        private void frmCreateBeamCad_Load(object sender, EventArgs e)
        {
           
        }

        private void bntBeamCreating_Click(object sender, EventArgs e)
        {            
            _event.Raise();
            this.btnBeamCreating.Enabled = false;
        }

        private void btnSelectLine_Click(object sender, EventArgs e)
        {
            AppPanel.listSelectLine = null;
            _eventSelectLine.Raise();            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dropNameBeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void b_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioMiddle_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
   
}
