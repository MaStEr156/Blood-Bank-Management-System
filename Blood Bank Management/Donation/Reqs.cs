using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donation
{
    public partial class Reqs : UserControl
    {
        public event EventHandler RequestInfoButtonClick;

        private void viewBtn_Click(object sender, EventArgs e)
        {
            RequestInfoButtonClick?.Invoke(this, EventArgs.Empty);
        }
        public Reqs()
        {
            InitializeComponent();
        } 
        public string HospitalName
        {
            get { return HosNameLbl.Text; }
            set { HosNameLbl.Text = value; }
        }

        public string RequestId
        {
            get { return ReqIDLbl.Text; }
            set { ReqIDLbl.Text = value; }
        }

        public string Date
        {
            get { return ReqDateLbl.Text; }
            set { ReqDateLbl.Text = value; }
        }
        public string Phone
        {
            get { return phoneLbl.Text; }
            set { phoneLbl.Text = value; }
        }

    }
}
