using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation
{
    public partial class Donate : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);

        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");

        bool sidebarExpand;
        bool donarCollapse;
        bool patientCollapse;
        public Donate()
        {
            InitializeComponent();
            String[] BloodGroup = { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            foreach (string b in BloodGroup)
            {
                Combo_Btype.Items.Add(b);
            }
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
            donartimer.Start();

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

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = DGV.SelectedRows[0].Cells[0].Value.ToString();
            Combo_Btype.SelectedItem = DGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btn_donate_Click(object sender, EventArgs e)
        {
            if (txt_id.Text == "" || Combo_Btype.SelectedIndex == -1)
            {
                new msg(" Missing information").ShowDialog();
            }
            else
            {

                Cn.Open();
                SqlCommand cmd_getLastDate = new SqlCommand("SELECT LastDate FROM Donors WHERE DonorID = '" + txt_id.Text + "'", Cn);
                if (cmd_getLastDate.ExecuteScalar() != DBNull.Value)
                {
                    DateTime lastDonationDate = (DateTime)cmd_getLastDate.ExecuteScalar();

                    // Check if the last donation was more than 6 months ago
                    if (lastDonationDate != null && (DateTime.Now - lastDonationDate).TotalDays < 180)
                    {
                        // Display a message box and exit the method
                        msg cantDonate = new msg("Cannot donate.\r\n Last donation was less than 6 months ago.");
                        cantDonate.ShowDialog();
                        Cn.Close();
                        return;
                    }
                }

                DateTime selectedDate = DateTimePicker1.Value;
                SqlCommand cm = new SqlCommand("insert into Stock(  DonorID, BType,BDate) values('" + txt_id.Text + "','" + Combo_Btype.SelectedItem.ToString() + "','" + selectedDate + "')", Cn);
                SqlCommand cmd_updateDonorDate = new SqlCommand("UPDATE Donors SET LastDate = '" + selectedDate + "'" +
                    "WHERE DonorID = '" + txt_id.Text + "'", Cn);
                cm.ExecuteNonQuery();
                cmd_updateDonorDate.ExecuteNonQuery();
                new msg("Donated Successfully").ShowDialog();
                Cn.Close();
                load();

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

        private void donartimer_Tick(object sender, EventArgs e)
        {

            if (donarCollapse)
            {
                donarcontainer.Height += 10;
                if (donarcontainer.Height == donarcontainer.MaximumSize.Height)
                {
                    donarCollapse = false;
                    donartimer.Stop();

                }
            }
            else
            {
                donarcontainer.Height -= 10;
                if (donarcontainer.Height == donarcontainer.MinimumSize.Height)
                {
                    donarCollapse = true;
                    donartimer.Stop();

                }
            }
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

        private void load()
        {
            DateTimePicker1.Value = DateTime.Now;
            string query = "select BType  ,count(*) Quantity from stock  group by BType";
            SqlCommand cm = new SqlCommand(query, Cn);
            try  
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DGV_stock.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }

        }
        private void Donate_Load(object sender, EventArgs e)
        {
            string query = "Select DonorID ,Name,BType,LastDate,Age,Gender,Diabetes,BPressure from Donors";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DGV.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            load();
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            string query = "Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Donors where Name like '%" + txt_search.Text + "%' or DonorID='" + txt_search.Text + "'";
            Cn.Open();
            SqlCommand cm = new SqlCommand(query, Cn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.SelectCommand = cm;
            dt.Clear();
            adapter.Fill(dt);
            DGV.DataSource = dt;
            Cn.Close();
        }
    }
}
