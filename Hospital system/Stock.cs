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

namespace Hospital_system
{
    public partial class Stock : Form
    {
        SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
        bool drag = false;
        Point start_point = new Point(0, 0);
        public Stock(int HosID)
        {
            InitializeComponent();
            HosIDLbl.Text = HosID.ToString();
            load();
        }
        private void load()
        {
            string query = "select BType, BCount from HStock";
            SqlCommand cm = new SqlCommand(query, Cn);
            try
            {
                Cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cm);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView2.DataSource = dt;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
            finally { Cn.Close(); }

        }
        private void guna2Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void guna2Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void guna2Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void makeRequestBtn_Click(object sender, EventArgs e)
        {
            Requests requests = new Requests(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            requests.FormClosed += (s, args) => this.Close();
            requests.Show();

        }

        private void stockBtn_Click(object sender, EventArgs e)
        {
            
            Stock stock = new Stock(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            stock.FormClosed += (s, args) => this.Close();
            stock.Show();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Pending pending = new Pending(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            pending.FormClosed += (s, args) => this.Close();
            pending.Show();
        }
    }
}
