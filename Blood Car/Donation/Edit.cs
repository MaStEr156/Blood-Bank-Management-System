using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation
{ 
    public partial class Edit : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        bool sidebarExpand;
        bool donorCollapse;

        public Edit()
        {
            InitializeComponent();
            String[] BloodGroup = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            foreach (string b in BloodGroup)
            {
                ComboBox1.Items.Add(b);
            }
           
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();

        }

        private void buttondonar_Click(object sender, EventArgs e)
        {
            donortimer.Start();

        }

        private void button4_Click(object sender, EventArgs e)
        {

            AddDonor adddonor = new AddDonor();
            this.Hide();
            adddonor.FormClosed += (s, args) => this.Close();
            //adddonor.TopLevel = false;
            //containerpanal.Controls.Add(adddonor);
            //adddonor.BringToFront();
            adddonor.Show();
        }

        private void button3_Click(object sender, EventArgs e)
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
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            Cn.Open();
            string query = "Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Donors where DonorID='" + txt_Search.Text+"'";
            SqlCommand cm = new SqlCommand(query, Cn);
            SqlDataReader sdr= cm.ExecuteReader();
            if (sdr.HasRows == true)
            {
                sdr.Read();
                txt_name.Text = sdr["Name"].ToString();
                txt_phone.Text = sdr["Phone"].ToString();
                txt_Address.Text = sdr["Address"].ToString();
                txt_snn.Text = sdr["DonorID"].ToString();
                txt_age.Text = sdr["Age"].ToString();
                ComboBox1.SelectedItem = sdr["BType"].ToString() ;
                string gender = sdr["Gender"].ToString();
                if (gender == "Female" || gender == "female")
                {
                    rdb_female.Checked = true;
                }
                else
                {
                    rdb_male.Checked = true;
                }
                string diabetes = sdr["Diabetes"].ToString();
                if (diabetes == "yes" || diabetes == "Yes")
                {
                    rab_d_yes.Checked = true;
                }
                else
                {
                    rab_d_no.Checked = true;
                }
                string pressure = sdr["BPressure"].ToString();
                if (pressure == "yes" || pressure == "Yes")
                {
                    rdb_p_yes.Checked = true;
                }
                else
                {
                    rdb_p_no.Checked = true;
                }

            }
           
            Cn.Close();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            AddDonor adddonor = new AddDonor();
            this.Hide();
            adddonor.FormClosed += (s, args) => this.Close();
            
            adddonor.Show();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void txt_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
            }
        }
        private void Reset()
        {
            txt_Search.Text = "";
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
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Cn.Open();
            string id=txt_snn.Text;
            string name=txt_name.Text;
            string address=txt_Address.Text;
            string phone=txt_phone.Text;
            string age=txt_age.Text;
            string bdiabetes;
            string bpressure;
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
            string query = $"UPDATE Donors SET Name = '{name}', Phone='{phone}', Address='{address}', Age='{age}', Diabetes='{bdiabetes}', BPressure='{bpressure}' where DonorID='{id}'";
            SqlCommand cm = new SqlCommand(query, Cn);
            cm.ExecuteNonQuery();
            new msg("Data updated successfully.").ShowDialog();
            Cn.Close();
            Reset();
           
        }

        private void txt_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            txt_Search.Text = ViewDonor.txtid;
        }
    }
}
