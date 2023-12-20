using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Donation
{
    public partial class ViewDonor : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        bool sidebarExpand;
        bool donorCollapse;
        public static string txtid;
        public ViewDonor()
        {
            InitializeComponent();
        }
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
        private void load()
        {
            string query = "Select DonorID ,Name,BType,LastDate,Age,Gender,Phone,Address,Diabetes,BPressure from Donors";
            
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DGV_donor.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }

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

        private void button5_Click_1(object sender, EventArgs e)
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

        private void guna2CircleButton2_Click_1(object sender, EventArgs e)
        {
            AddDonor adddonor = new AddDonor();
            this.Hide();
            adddonor.FormClosed += (s, args) => this.Close();
            //adddonor.TopLevel = false;
            //containerpanal.Controls.Add(adddonor);
            //adddonor.BringToFront();
            adddonor.Show();
        }

        private void guna2CircleButton1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DGV_donor_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txt_id.Text = DGV_donor.SelectedRows[0].Cells[0].Value.ToString();
        }


        private void delete_btn_Click_1(object sender, EventArgs e)
        {
            Cn.Open();
            SqlCommand check_stock = new SqlCommand("SELECT COUNT(*) FROM Stock WHERE DonorID = '" + txt_id.Text + "'", Cn);
            int checkStock = Convert.ToInt32(check_stock.ExecuteScalar());
            if (checkStock > 0)
            {

                new msg("Can't Delete Donor:\r\n Donor has existing blood in Stock").ShowDialog();
                Cn.Close();


            }
            else
            {
                
                if (MessageBox.Show("Are You Sure","Sure Message",MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                {
                    DataGridViewRow selectedRow = DGV_donor.SelectedRows[0];
                    int idToDelete = (int)Convert.ToInt64(selectedRow.Cells["DonorID"].Value);
                    SqlCommand cm = new SqlCommand("Delete from Donors where DonorID='" + txt_id.Text + "'", Cn);
                    cm.ExecuteNonQuery();
                    DGV_donor.Rows.Remove(selectedRow);
                    Cn.Close();

                }
                Cn.Close();

            }

        }

        private void edit_btn_Click(object sender, EventArgs e)
        {
            txtid = txt_id.Text;
            Edit edit = new Edit();
            this.Hide();
            edit.FormClosed += (s, args) => this.Close();
            edit.Show();


        }

        private void donortimer_Tick_1(object sender, EventArgs e)
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

        private void SideBarTimer_Tick_1(object sender, EventArgs e)
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

 
        private void ViewDonor_Load(object sender, EventArgs e)
        {
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
            DGV_donor.DataSource = dt;
            Cn.Close();
        }

        private void panel2_MouseDown_1(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel2_MouseUp_1(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        
    }
}
