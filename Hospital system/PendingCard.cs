using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_system
{
    public partial class PendingCard : UserControl
    {
        public PendingCard()
        {
            InitializeComponent();
        }

        public string PendingRequestID
        {
            get { return PRequestID.Text; }
            set { PRequestID.Text = value; }
        }
        public string PendingStatus
        {
            get { return PStatus.Text; }
            set { PStatus.Text = value; }
        }
        public void colorred()
        {
            PStatus.ForeColor = Color.Red;
        }
        public void colorgreen()
        {
            PStatus.ForeColor = Color.Green;
        }
    }
}
