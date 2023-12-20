using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation
{
    public partial class loading : Form
    {
        public loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2ProgressBar1.Value < guna2ProgressBar1.Maximum)
            {
                guna2ProgressBar1.Value += 10;
            }
            else
            {
                timer1.Stop();
                Login login = new Login();
                this.Hide();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }
    }
}
