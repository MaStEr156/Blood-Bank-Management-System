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
    public partial class Pending : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);

        SqlConnection con = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
        public Pending(int HosID)
        {
            InitializeComponent();
            HosIDLbl.Text = HosID.ToString();
            load();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Pending pending = new Pending(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            pending.FormClosed += (s, args) => this.Close();
            pending.Show();
        }
        private void load()
        {
            flowLayoutPanel1.Controls.Clear();
            con.Open();

            SqlCommand cmd_getPending = new SqlCommand("SELECT ReqID FROM PendingRequests WHERE HosID = @HosID ", con);
            cmd_getPending.Parameters.AddWithValue("@HosID", Convert.ToInt32(HosIDLbl.Text));
            SqlDataReader reader = cmd_getPending.ExecuteReader();
            while (reader.Read())
            {
                PendingCard card = new PendingCard();
               
                    card.PendingStatus = "Pending";
                    card.PendingRequestID = reader["ReqID"].ToString();
                    card.colorred();
                flowLayoutPanel1.Controls.Add(card);
            }
            con.Close();
            con.Open();
            SqlCommand cmd_getAcc = new SqlCommand("SELECT ReqID FROM AccRequests WHERE HosID = @HosID", con);
            cmd_getAcc.Parameters.AddWithValue("@HosID", Convert.ToInt32(HosIDLbl.Text));
            SqlDataReader reader2 = cmd_getAcc.ExecuteReader();
            while (reader2.Read())
            {
                PendingCard card = new PendingCard();

                card.PendingStatus = "Accepted";
                card.PendingRequestID = reader2["ReqID"].ToString();
                card.colorgreen();
                flowLayoutPanel1.Controls.Add(card);
            }

            con.Close();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void stockBtn_Click(object sender, EventArgs e)
        {
            Stock stock = new Stock(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            stock.FormClosed += (s, args) => this.Close();
            stock.Show();
        }

        private void makeRequestBtn_Click(object sender, EventArgs e)
        {
            Requests requests = new Requests(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            requests.FormClosed += (s, args) => this.Close();
            requests.Show();
        }
    }
}
