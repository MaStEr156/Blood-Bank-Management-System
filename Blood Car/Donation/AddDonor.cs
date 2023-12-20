using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donation
{

    public partial class AddDonor : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
        bool sidebarExpand;
        bool donorCollapse;
        public AddDonor()
        {
            InitializeComponent();
            String[] BloodGroup = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            foreach (string b in BloodGroup)
            {
                ComboBox1.Items.Add(b);
            }
        }
        private void Reset()
        {
            txt_Address.Text = "";
            txt_name.Text = "";
            txt_age.Text = "";
            txt_phone.Text = "";
            txt_snn.Text = "";
            ComboBox1.SelectedIndex = -1;
            rab_d_no.Checked = false;
            rab_d_yes.Checked = false;
            rdb_female.Checked = false;
            rdb_male.Checked = false;
            rdb_p_no.Checked = false;
            rdb_p_yes.Checked = false;
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }
        
        private void buttondonar_Click(object sender, EventArgs e)
        {
            donortimer.Start();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            AddDonor adddonor = new AddDonor();
            this.Hide();
            adddonor.FormClosed += (s, args) => this.Close();
            //adddonor.TopLevel = false;
            //containerpanal.Controls.Add(adddonor);
            //adddonor.BringToFront();
            adddonor.Show();
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            ViewDonor viewdonor = new ViewDonor();
            this.Hide();
            viewdonor.FormClosed += (s, args) => this.Close();
            //viewdonor.TopLevel = false;
            //containerpanal.Controls.Add(viewdonor);
            //viewdonor.BringToFront();
            viewdonor.Show();
        }
      
        private void button5_Click(object sender, EventArgs e)
        {
            Donate donate = new Donate();
            this.Hide();
            donate.FormClosed += (s, args) => this.Close();
            //donate.TopLevel = false;
            //containerpanal.Controls.Add(donate);
            //donate.BringToFront();
            donate.Show();
        }
     
        private void button8_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock();
            this.Hide();
            stock.FormClosed += (s, args) => this.Close();
            stock.Show();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            this.Hide();
            dashboard.FormClosed += (s, args) => this.Close();
            dashboard.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            string gender;
            string bdiabetes;
            string bpressure;

            if (rdb_male.Checked == true)
            {
                gender = "Male";
            }
            else
            {
                gender = "Female";
            }
            if (rdb_p_yes.Checked == true)
            {
                bpressure = "Yes";
            }
            else
            {
                bpressure = "No";
            }

            if (rab_d_yes.Checked == true)
            {
                bdiabetes = "Yes";
            }
            else
            {
                bdiabetes = "No";
            }
            if (txt_name.Text == "" || txt_age.Text == "" || txt_Address.Text == "" || txt_phone.Text == "" || txt_snn.Text == ""||ComboBox1.SelectedIndex==-1)
            {
                new msg(" Missing information").ShowDialog();
              
            }
            else
            {
                Cn.Open();
                SqlCommand checkID_inDonors = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Donors WHERE DonorID = '" + txt_snn.Text + "'", Cn);
                int count = Convert.ToInt32(checkID_inDonors.ExecuteScalar());
                if (count > 0)
                {
                    msg userfound = new msg("Donor is Already in System");
                    userfound.ShowDialog();
                    Cn.Close();
                    return;
                }
                Cn.Close();
                Cn.Open();
                SqlCommand cm = new SqlCommand("insert into Donors(DonorID, Name, Age, Phone, Address, BType, Gender, Diabetes, BPressure) values('" + txt_snn.Text + "','" + txt_name.Text + "','" + txt_age.Text + "','" + txt_phone.Text + "','" + txt_Address.Text + "','" + ComboBox1.SelectedItem.ToString() + "','" + gender + "','" + bdiabetes + "','" + bpressure + "')", Cn);
                cm.ExecuteNonQuery();
                new msg("Added Successfully").ShowDialog();
                Cn.Close();
                Reset();
            }
        }
        private void rdb_p_yes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_p_yes.Checked)
            {
                panel10.Visible = true;
            }
            else
            {
                panel10.Visible = false;
                rdb_p_yes_low.Checked = false;
                rdb_p_yes_high.Checked = false;
            }
        }
        private void donortimer_Tick(object sender, EventArgs e)
        {
            if (donorCollapse)
            {
                donorcontainer.Height += 10;
                if (donorcontainer.Height == donorcontainer.MaximumSize.Height)
                {
                    donorCollapse = false;
                    donortimer.Stop();
                }
            }
            else
            {
                donorcontainer.Height -= 10;
                if (donorcontainer.Height == donorcontainer.MinimumSize.Height)
                {
                    donorCollapse = true;
                    donortimer.Stop();
                }
            }
        }
       
        private void SideBarTimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                SideBar.Width -= 10;
                if (SideBar.Width == SideBar.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    SideBarTimer.Stop();
                }
            }
            else
            {
                SideBar.Width += 10;
                if (SideBar.Width == SideBar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    SideBarTimer.Stop();
                }
            }
        }
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

    }
}
