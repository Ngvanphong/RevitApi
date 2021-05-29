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

namespace MainProject.EntendForBeam
{
    public partial class fromExtendBeam : Form
    {
        private ExternalEvent _event;      
        private ExtendBeamHandler _handler;
        public fromExtendBeam(ExternalEvent mevent, ExtendBeamHandler handler)
        {            
            InitializeComponent();
            _event = mevent;
            _handler = handler;
        }

        private void fromExtendBeam_Load(object sender, EventArgs e)
        {

        }

        private void btnExtendBeam_Click(object sender, EventArgs e)
        {
            _event.Raise();
        }
    }
}
