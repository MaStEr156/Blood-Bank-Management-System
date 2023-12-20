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

namespace Donation
{
    public partial class EditPatient : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        bool sidebarExpand;
        bool donorCollapse;
        bool patientCollapse;
        public EditPatient()
        {
            InitializeComponent();
            String[] BloodGroup = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            foreach (string b in BloodGroup)
            {
                ComboBox1.Items.Add(b);
            }
        }
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            Cn.Open();
            string query = "Select PatientID, Name, Age, Phone, Address, BType, LastDate, Gender from Patients where PatientID='" + txt_search.Text + "'";
            SqlCommand cm = new SqlCommand(query, Cn);
            SqlDataReader sdr = cm.ExecuteReader();
            if (sdr.HasRows == true)
            {
                sdr.Read();
                txt_name.Text = sdr["Name"].ToString();
                txt_phone.Text = sdr["Phone"].ToString();
                txt_Address.Text = sdr["Address"].ToString();
                txt_snn.Text = sdr["PatientID"].ToString();
                txt_age.Text = sdr["Age"].ToString();
                DateTimePicker1.Text = sdr["LastDate"].ToString();
                ComboBox1.SelectedItem = sdr["BType"].ToString();
                string gender = sdr["Gender"].ToString();
                if (gender == "Female" || gender == "female")
                {
                    rdb_female.Checked = true;
                }
                else
                {
                    rdb_male.Checked = true;
                }
              
            }

            Cn.Close();
        }
        private void Reset()
        {
            txt_search.Text = "";
            txt_Address.Text = "";
            txt_name.Text = "";
            txt_age.Text = "";
            txt_phone.Text = "";
            txt_snn.Text = "";
            ComboBox1.SelectedIndex = -1;
            rdb_female.Checked = false;
            rdb_male.Checked = false;
          
        }
        private void btn_edit_Click(object sender, EventArgs e)
        {
            Cn.Open();
            DateTime selectedDate = DateTimePicker1.Value;
            string id = txt_snn.Text;
            string name = txt_name.Text;
            string address = txt_Address.Text;
            string phone = txt_phone.Text;
            string age = txt_age.Text;
            string query = $"UPDATE Patients SET Name = '{name}', Phone='{phone}', Address='{address}', Age='{age}', LastDate='{selectedDate}' where PatientID='{id}'";
            SqlCommand cm = new SqlCommand(query, Cn);
            cm.ExecuteNonQuery();
            new msg("Data updated successfully.").ShowDialog();
            Cn.Close();
            Reset();

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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SideBarTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            this.Hide();
            home.FormClosed += (s, args) => this.Close();
            home.Show();
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

        private void buttonpatient_Click(object sender, EventArgs e)
        {
            patienttimer.Start();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddPatients addpatients = new AddPatients();
            this.Hide();
            addpatients.FormClosed += (s, args) => this.Close();
            addpatients.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ViewPatient viewpatient = new ViewPatient();
            this.Hide();
            viewpatient.FormClosed += (s, args) => this.Close();
            viewpatient.Show();
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

        private void button9_Click(object sender, EventArgs e)
        {
            Requests requests = new Requests();
            this.Hide();
            requests.FormClosed += (s, args) => this.Close();
            requests.Show();
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

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            AddPatients addpatients = new AddPatients();
            this.Hide();
            addpatients.FormClosed += (s, args) => this.Close();
            addpatients.Show();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void patienttimer_Tick(object sender, EventArgs e)
        {
            if (patientCollapse)
            {
                patientcontainer.Height += 10;
                if (patientcontainer.Height == patientcontainer.MaximumSize.Height)
                {
                    patientCollapse = false;
                    patienttimer.Stop();
                }
            }
            else
            {
                patientcontainer.Height -= 10;
                if (patientcontainer.Height == patientcontainer.MinimumSize.Height)
                {
                    patientCollapse = true;
                    patienttimer.Stop();
                }
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

        private void txt_age_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void EditPatient_Load(object sender, EventArgs e)
        {
            txt_search.Text = ViewPatient.txtid;

        }

       
    }
}
