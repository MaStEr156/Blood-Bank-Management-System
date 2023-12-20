using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging.Design;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (ProgressBar1.Value < ProgressBar1.Maximum)
            {
                ProgressBar1.Value += 4;
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
