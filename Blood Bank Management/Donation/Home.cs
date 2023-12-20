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
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void donor_img_Click(object sender, EventArgs e)
        {
            ViewDonor viewDonor = new ViewDonor();
            this.Hide();
            viewDonor.FormClosed += (s, args) => this.Close();
            viewDonor.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ViewDonor viewDonor = new ViewDonor();
            this.Hide();
            viewDonor.FormClosed += (s, args) => this.Close();
            viewDonor.Show();
        }

        private void patient_img_Click(object sender, EventArgs e)
        {
            ViewPatient viewpatient = new ViewPatient();
            this.Hide();
            viewpatient.FormClosed += (s, args) => this.Close();
            viewpatient.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ViewPatient viewpatient = new ViewPatient();
            this.Hide();
            viewpatient.FormClosed += (s, args) => this.Close();
            viewpatient.Show();
        }

        private void request_img_Click(object sender, EventArgs e)
        {
            Requests requests = new Requests();
            this.Hide();
            requests.FormClosed += (s, args) => this.Close();
            requests.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Requests requests = new Requests();
            this.Hide();
            requests.FormClosed += (s, args) => this.Close();
            requests.Show();
        }

        private void stock_img_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            this.Hide();
            stock.FormClosed += (s, args) => this.Close();
            stock.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            this.Hide();
            stock.FormClosed += (s, args) => this.Close();
            stock.Show();
        }

        private void dash_img_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Hide();
            dashboard.FormClosed += (s, args) => this.Close();
            dashboard.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Hide();
            dashboard.FormClosed += (s, args) => this.Close();
            dashboard.Show();
        }
        bool drag = false;
        Point start_point = new Point(0, 0);
        private void Home_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void Home_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void Home_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }
    }
}
