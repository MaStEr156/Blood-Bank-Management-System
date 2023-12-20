using Guna.UI2.WinForms;
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
    public partial class Stock : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
        bool sidebarExpand;
        bool donorCollapse;
        bool patientCollapse;
        public Stock()
        {
            InitializeComponent();

            load();
        }
        private void load()
        {
            string query = "select BType  ,count(*) Quantity from stock  group by BType";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            btn_back.Visible = false;
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

        private void buttondonar_Click(object sender, EventArgs e)
        {
            donortimer.Start();
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
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string query = "select  StockID  ,BType ,BDate ,DonorID ,BSource  from Stock where BType ='" + "A+" + "'";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource from Stock where BType ='" + "A-" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource  from Stock where BType ='" + "AB+" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource from Stock where BType ='" + "AB-" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource  from Stock where BType ='" + "B+" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource  from Stock where BType ='" + "B-" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource  from Stock where BType ='" + "O+" + "' group by StockID , BType,BDate ,DonorID ,BSource";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            string query = "select  StockID , BType ,BDate ,DonorID ,BSource from Stock where BType ='" + "O-" + "' ";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }
            guna2Button1.Visible = false;
            guna2Button4.Visible = false;
            guna2Button5.Visible = false;
            guna2Button6.Visible = false;
            guna2Button7.Visible = false;
            guna2Button8.Visible = false;
            guna2Button9.Visible = false;
            guna2Button3.Visible = false;
            btn_back.Visible = true;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            load();
            guna2Button1.Visible = true;
            guna2Button4.Visible = true;
            guna2Button5.Visible = true;
            guna2Button6.Visible = true;
            guna2Button7.Visible = true;
            guna2Button8.Visible = true;
            guna2Button9.Visible = true;
            guna2Button3.Visible = true;
        }

        private void Stock_Load(object sender, EventArgs e)
        {

        }
    }
}
