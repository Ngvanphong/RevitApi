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
    public partial class frmLoad : Form
    {
        public Action Worker { set; get; }
        public frmLoad(Action worker)
        {
            InitializeComponent();
            if (worker == null)
            {
                throw new ArgumentException();
            }
            Worker = worker;
            AppPanelAll.frmLoadProgess = this;
        }

        private void frmLoad_Load(object sender, EventArgs e)
        {
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Task.Factory.StartNew(Worker).ContinueWith(t => { this.Close(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
