using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.UI;
namespace CreateBeamByCad
{
    public partial class frmCreateBeamAll : Form
    {
        private ExternalEvent _event;
        private HandlerCreateBeamAll _handler;       
        public frmCreateBeamAll(ExternalEvent mevent, HandlerCreateBeamAll handler)
        {
            InitializeComponent();
            _event = mevent;
            _handler = handler;
        }

        private void frmCreateBeamAll_Load(object sender, EventArgs e)
        {

        }

        private void btnCreateBeamAll_Click(object sender, EventArgs e)
        {
            _event.Raise();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
