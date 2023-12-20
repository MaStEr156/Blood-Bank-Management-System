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
    public partial class Requests : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public Requests(int HosID)
        {
            InitializeComponent();
            HosIDLbl.Text = HosID.ToString();
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

        private void guna2Button2_Click(object sender, EventArgs e)
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string email = "", phone = "", hosname = "";
            int HosID = Convert.ToInt32(HosIDLbl.Text);
            double z;
 
                DateTime dt = DateTime.Now;
                DateTime sub = reqdate.Value;
                SqlConnection con = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");
                con.Open();

            SqlCommand cmd_get_HosName = new SqlCommand("SELECT HosName FROM HosUsers WHERE HosID=@HosID", con);
            SqlCommand cmd_get_HosEmail = new SqlCommand("SELECT Email FROM HosUsers WHERE HosID=@HosID", con);
            SqlCommand cmd_get_HosPhone = new SqlCommand("SELECT Phone FROM HosUsers WHERE HosID=@HosID", con);

            cmd_get_HosName.Parameters.AddWithValue("@HosID", HosID);
            cmd_get_HosEmail.Parameters.AddWithValue("@HosID", HosID);
            cmd_get_HosPhone.Parameters.AddWithValue("@HosID", HosID);

            hosname = cmd_get_HosEmail.ExecuteScalar().ToString();
            email = cmd_get_HosEmail.ExecuteScalar().ToString();
            phone = cmd_get_HosPhone.ExecuteScalar().ToString();



            string query = "INSERT INTO Requests (HosName, HosID, SubDate, ReqDate, Note, Phone, Email, APos, ANeg, BPos, BNeg, OPos, ONeg, ABPos, ABNeg) " +
                   "VALUES (@HosName, @HosID, @SubDate, @ReqDate, @Note, @Phone, @Email, @APos, @ANeg, @BPos, @BNeg, @OPos, @ONeg, @ABPos, @ABNeg); " +
                   "SELECT SCOPE_IDENTITY();";
                SqlCommand cmd_scope = new SqlCommand(query, con);
                cmd_scope.Parameters.AddWithValue("@HosName", hosname);
                cmd_scope.Parameters.AddWithValue("@HosID", HosID);
                cmd_scope.Parameters.AddWithValue("@SubDate", sub);
                cmd_scope.Parameters.AddWithValue("@ReqDate", dt);
                cmd_scope.Parameters.AddWithValue("@Note", note.Text);
                cmd_scope.Parameters.AddWithValue("@Phone", phone);
                cmd_scope.Parameters.AddWithValue("@Email", email);
                cmd_scope.Parameters.AddWithValue("@APos", apos.Value);
                cmd_scope.Parameters.AddWithValue("@ANeg", aneg.Value);
                cmd_scope.Parameters.AddWithValue("@BPos", bpos.Value);
                cmd_scope.Parameters.AddWithValue("@BNeg", bneg.Value);
                cmd_scope.Parameters.AddWithValue("@OPos", opos.Value);
                cmd_scope.Parameters.AddWithValue("@ONeg", oneg.Value);
                cmd_scope.Parameters.AddWithValue("@ABPos", abpos.Value);
                cmd_scope.Parameters.AddWithValue("@ABNeg", abneg.Value);

                int reqId = Convert.ToInt32(cmd_scope.ExecuteScalar());

                string insertPendingQuery = "INSERT INTO PendingRequests (ReqID, HosID) VALUES (@ReqID, @HosID)";
                SqlCommand add_to_pending = new SqlCommand(insertPendingQuery, con);
                add_to_pending.Parameters.AddWithValue("@ReqID", reqId);
                add_to_pending.Parameters.AddWithValue("@HosID", HosID);
                add_to_pending.ExecuteNonQuery();

                con.Close();
                new Msg("Request Sent Successfully!").ShowDialog();
                guna2CheckBox1.Checked = false;
                guna2CheckBox2.Checked = false;
                guna2CheckBox3.Checked = false;
                guna2CheckBox4.Checked = false;
                guna2CheckBox5.Checked = false;
                guna2CheckBox6.Checked = false;
                guna2CheckBox7.Checked = false;
                guna2CheckBox8.Checked = false;

                oneg.Value = 0;
                abpos.Value = 0;
                abneg.Value = 0;
                opos.Value = 0;
                apos.Value = 0;
                aneg.Value = 0;
                bpos.Value = 0;
                bneg.Value = 0;

                note.Text = null;

        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                oneg.Enabled = true;
            }
            else
            {
                oneg.Enabled = false;
            }
        }

        private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox2.Checked)
            {
                opos.Enabled = true;
            }
            else
            {
                opos.Enabled = false;
            }
        }

        private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
        {

            if (guna2CheckBox3.Checked)
            {
                abpos.Enabled = true;
            }
            else
            {
                abpos.Enabled = false;
            }
        }

        private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox4.Checked)
            {
                abneg.Enabled = true;
            }
            else
            {
                abneg.Enabled = false;
            }
        }

        private void guna2CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox5.Checked)
            {
                bpos.Enabled = true;
            }
            else
            {
                bpos.Enabled = false;
            }
        }

        private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox6.Checked)
            {
                bneg.Enabled = true;
            }
            else
            {
                bneg.Enabled = false;
            }
        }

        private void guna2CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox7.Checked)
            {
                apos.Enabled = true;
            }
            else
            {
                apos.Enabled = false;
            }
        }

        private void guna2CheckBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (guna2CheckBox8.Checked)
            {
                aneg.Enabled = true;
            }
            else
            {
                aneg.Enabled = false;
            }

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Pending pending = new Pending(Convert.ToInt32(HosIDLbl.Text));
            this.Hide();
            pending.FormClosed += (s, args) => this.Close();
            pending.Show();
        }

        private void Requests_Load(object sender, EventArgs e)
        {
            reqdate.Value = DateTime.Now;
        }
    }
}
