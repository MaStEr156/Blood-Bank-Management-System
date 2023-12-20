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
    public partial class Requests : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        bool sidebarExpand;
        bool donorCollapse;
        bool patientCollapse;
        SqlConnection con = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");

        public Requests()
        {
            InitializeComponent();
            load();
        }
        private void load()
        {
            flowLayoutPanel1.Controls.Clear();
            tabControl1.SelectedIndex = 0;
            tabControl1.Appearance = TabAppearance.Normal;
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);

            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT HosName, ReqID, SubDate, Phone FROM Requests", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Reqs container = new Reqs();
                container.HospitalName = reader["HosName"].ToString();
                container.RequestId = reader["ReqID"].ToString();
                container.Date = reader["SubDate"].ToString();
                container.Phone = reader["Phone"].ToString();

                container.RequestInfoButtonClick += HandleRequestInfoButtonClick;

                flowLayoutPanel1.Controls.Add(container);
            }

            con.Close();
        }
        private void HandleRequestInfoButtonClick(object sender, EventArgs e)
        {
            // Handle the event when the button in the user control is clicked
            Reqs clickedControl = sender as Reqs;

            // Switch to the other tab and display the information
            tabControl1.SelectedIndex = 1;

            // Access the information from the clicked user control
            string hospitalName = clickedControl.HospitalName;
            string requestId = clickedControl.RequestId;
            string date = clickedControl.Date;
            string phone = clickedControl.Phone;
            HosNameLbl2.Text = hospitalName;
            ReqIDLbl2.Text = requestId;
            ReqDateLbl2.Text = date;
            phoneLbl2.Text = phone;

            con.Open();
            // Get Request Data
            SqlCommand cmd_APos = new SqlCommand("SELECT APos FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_ANeg = new SqlCommand("SELECT ANeg FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_BPos = new SqlCommand("SELECT Bpos FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_BNeg = new SqlCommand("SELECT BNeg FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_OPos = new SqlCommand("SELECT Opos FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_ONeg = new SqlCommand("SELECT ONeg FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_ABPos = new SqlCommand("SELECT ABPos FROM Requests WHERE ReqID ='" + requestId + "'", con);
            SqlCommand cmd_ABNeg = new SqlCommand("SELECT ABNeg FROM Requests WHERE ReqID ='" + requestId + "'", con);

            SqlCommand cmd_email = new SqlCommand("SELECT Email FROM Requests WHERE ReqID ='" + requestId + "'", con);


            APosLbl.Text = cmd_APos.ExecuteScalar().ToString();
            ANegLbl.Text = cmd_ANeg.ExecuteScalar().ToString();
            BPosLbl.Text = cmd_BPos.ExecuteScalar().ToString();
            BNegLbl.Text = cmd_BNeg.ExecuteScalar().ToString();
            OPosLbl.Text = cmd_OPos.ExecuteScalar().ToString();
            ONegLbl.Text = cmd_ONeg.ExecuteScalar().ToString();
            ABPosLbl.Text = cmd_ABPos.ExecuteScalar().ToString();
            ABNegLbl.Text = cmd_ABNeg.ExecuteScalar().ToString();
            emailLbl2.Text = cmd_email.ExecuteScalar().ToString();

            con.Close();

            APosUpDown.Enabled = true;
            ANegUpDown.Enabled = true;
            BPosUpDown.Enabled = true;
            BNegUpDown.Enabled = true;
            OPosUpDown.Enabled = true;
            ONegUpDown.Enabled = true;
            ABPosUpDown.Enabled = true;
            ABNegUpDown.Enabled = true;

            APosUpDown.Value = 0;
            ANegUpDown.Value = 0;
            BPosUpDown.Value = 0;
            BNegUpDown.Value = 0;
            OPosUpDown.Value = 0;
            ONegUpDown.Value = 0;
            ABPosUpDown.Value = 0;
            ABNegUpDown.Value = 0;

            APosUpDown.Maximum = Convert.ToInt32(APosLbl.Text);
            ANegUpDown.Maximum = Convert.ToInt32(ANegLbl.Text);
            BPosUpDown.Maximum = Convert.ToInt32(BPosLbl.Text);
            BNegUpDown.Maximum = Convert.ToInt32(BNegLbl.Text);
            OPosUpDown.Maximum = Convert.ToInt32(OPosLbl.Text);
            ONegUpDown.Maximum = Convert.ToInt32(ONegLbl.Text);
            ABPosUpDown.Maximum = Convert.ToInt32(ABPosLbl.Text);
            ABNegUpDown.Maximum = Convert.ToInt32(ABNegLbl.Text);

            if (APosLbl.Text == "0")
                APosUpDown.Enabled = false;
            if (ANegLbl.Text == "0")
                ANegUpDown.Enabled = false;
            if (BPosLbl.Text == "0")
                BPosUpDown.Enabled = false;
            if (BNegLbl.Text == "0")
                BNegUpDown.Enabled = false;
            if (OPosLbl.Text == "0")
                OPosUpDown.Enabled = false;
            if (ONegLbl.Text == "0")
                ONegUpDown.Enabled = false;
            if (ABPosLbl.Text == "0")
                ABPosUpDown.Enabled = false;
            if (ABNegLbl.Text == "0")
                ABNegUpDown.Enabled = false;

        }
        private void submitBtn_Click(object sender, EventArgs e)
        {
            int APosCnt = Convert.ToInt32(APosUpDown.Value);
            int ANegCnt = Convert.ToInt32(ANegUpDown.Value);
            int BPosCnt = Convert.ToInt32(BPosUpDown.Value);
            int BNegCnt = Convert.ToInt32(BNegUpDown.Value);
            int OPosCnt = Convert.ToInt32(OPosUpDown.Value);
            int ONegCnt = Convert.ToInt32(ONegUpDown.Value);
            int ABPosCnt = Convert.ToInt32(ABPosUpDown.Value);
            int ABNegCnt = Convert.ToInt32(ABNegUpDown.Value);

            int requestId = Convert.ToInt32(ReqIDLbl2.Text);
            con.Open();

            SqlCommand cmd_APos_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'A+';", con);
            SqlCommand cmd_ANeg_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'A-';", con);
            SqlCommand cmd_BPos_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'B+';", con);
            SqlCommand cmd_BNeg_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'B-';", con);
            SqlCommand cmd_OPos_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'O+';", con);
            SqlCommand cmd_ONeg_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'O-';", con);
            SqlCommand cmd_ABPos_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'AB+';", con);
            SqlCommand cmd_ABNeg_Count = new SqlCommand("SELECT COUNT(*) AS CountOfRows FROM Stock WHERE BType = 'AB-';", con);

            int APos_StockCount = Convert.ToInt32(cmd_APos_Count.ExecuteScalar());
            int ANeg_StockCount = Convert.ToInt32(cmd_ANeg_Count.ExecuteScalar());
            int BPos_StockCount = Convert.ToInt32(cmd_BPos_Count.ExecuteScalar());
            int BNeg_StockCount = Convert.ToInt32(cmd_BNeg_Count.ExecuteScalar());
            int OPos_StockCount = Convert.ToInt32(cmd_OPos_Count.ExecuteScalar());
            int ONeg_StockCount = Convert.ToInt32(cmd_ONeg_Count.ExecuteScalar());
            int ABPos_StockCount = Convert.ToInt32(cmd_ABPos_Count.ExecuteScalar());
            int ABNeg_StockCount = Convert.ToInt32(cmd_ABNeg_Count.ExecuteScalar());

            con.Close();

            if (APos_StockCount < APosCnt || ANeg_StockCount < ANegCnt || BPos_StockCount < BPosCnt || BNeg_StockCount < BNegCnt ||
                 OPos_StockCount < OPosCnt || ONeg_StockCount < ONegCnt || ABPos_StockCount < ABPosCnt || ABNeg_StockCount < ABNegCnt)
                new msg("Insufficient Stock").ShowDialog();

            else
            {
                con.Open();

                SqlCommand cmd_APosUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + APosCnt + "' WHERE BType = 'A+'", con);
                SqlCommand cmd_ANegUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + ANegCnt + "' WHERE BType = 'A-'", con);
                SqlCommand cmd_BPosUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + BPosCnt + "' WHERE BType = 'B+'", con);
                SqlCommand cmd_BNegUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + BNegCnt + "' WHERE BType = 'B-'", con);
                SqlCommand cmd_OPosUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + OPosCnt + "' WHERE BType = 'O+'", con);
                SqlCommand cmd_ONegUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + ONegCnt + "' WHERE BType = 'O-'", con);
                SqlCommand cmd_ABPosUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + ABPosCnt + "' WHERE BType = 'AB+'", con);
                SqlCommand cmd_ABNegUpdate = new SqlCommand("UPDATE HStock SET BCount = BCount + '" + ABNegCnt + "' WHERE BType = 'AB-'", con);

                SqlCommand cmd_UpdateStock_APos = new SqlCommand("WITH CTE AS(SELECT TOP " + APosCnt + " * FROM STOCK WHERE BType = 'A+' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_ANeg = new SqlCommand("WITH CTE AS(SELECT TOP " + ANegCnt + " * FROM STOCK WHERE BType = 'A-' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_BPos = new SqlCommand("WITH CTE AS(SELECT TOP " + BPosCnt + " * FROM STOCK WHERE BType = 'B+' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_BNeg = new SqlCommand("WITH CTE AS(SELECT TOP " + BNegCnt + " * FROM STOCK WHERE BType = 'B-' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_OPos = new SqlCommand("WITH CTE AS(SELECT TOP " + OPosCnt + " * FROM STOCK WHERE BType = 'O+' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_ONeg = new SqlCommand("WITH CTE AS(SELECT TOP " + ONegCnt + " * FROM STOCK WHERE BType = 'O-' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_ABPos = new SqlCommand("WITH CTE AS(SELECT TOP " + ABPosCnt + " * FROM STOCK WHERE BType = 'AB+' ORDER BY BDate) DELETE FROM CTE", con);
                SqlCommand cmd_UpdateStock_ABNeg = new SqlCommand("WITH CTE AS(SELECT TOP " + ABNegCnt + " * FROM STOCK WHERE BType = 'AB-' ORDER BY BDate) DELETE FROM CTE", con);

                SqlCommand cmd_updateRequestStatus = new SqlCommand("INSERT INTO AccRequests(ReqID, HosID) SELECT ReqID, HosID FROM PendingRequests WHERE ReqID = @RequestID", con);
                SqlCommand cmd_deleteFromPending = new SqlCommand("DELETE FROM PendingRequests WHERE ReqID = @RequestID;", con);


                SqlCommand cmd_DeleteRequest = new SqlCommand("DELETE FROM Requests WHERE ReqID = '" + requestId + "'", con);


                cmd_updateRequestStatus.Parameters.AddWithValue("@RequestID", requestId);
                cmd_deleteFromPending.Parameters.AddWithValue("@RequestID", requestId);

                cmd_APosUpdate.ExecuteNonQuery();
                cmd_ANegUpdate.ExecuteNonQuery();
                cmd_BPosUpdate.ExecuteNonQuery();
                cmd_BNegUpdate.ExecuteNonQuery();
                cmd_OPosUpdate.ExecuteNonQuery();
                cmd_ONegUpdate.ExecuteNonQuery();
                cmd_ABPosUpdate.ExecuteNonQuery();
                cmd_ABNegUpdate.ExecuteNonQuery();

                cmd_UpdateStock_APos.ExecuteNonQuery();
                cmd_UpdateStock_ANeg.ExecuteNonQuery();
                cmd_UpdateStock_BPos.ExecuteNonQuery();
                cmd_UpdateStock_BNeg.ExecuteNonQuery();
                cmd_UpdateStock_OPos.ExecuteNonQuery();
                cmd_UpdateStock_ONeg.ExecuteNonQuery();
                cmd_UpdateStock_ABPos.ExecuteNonQuery();
                cmd_UpdateStock_ABNeg.ExecuteNonQuery();

                cmd_updateRequestStatus.ExecuteNonQuery();
                cmd_deleteFromPending.ExecuteNonQuery();

                cmd_DeleteRequest.ExecuteNonQuery();


                con.Close();

                load();

                new msg("Items Requested Have Been Sent!").ShowDialog();
            }
        }

        private void openNote_Click(object sender, EventArgs e)
        {
            int requestId = Convert.ToInt32(ReqIDLbl2.Text);
            con.Open();

            SqlCommand cmd_note = new SqlCommand("SELECT Note FROM Requests WHERE ReqID ='" + requestId + "'", con);
            string note = cmd_note.ExecuteScalar().ToString();
            new msg(note).ShowDialog();
            con.Close();
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }



        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void flowLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void buttonpatient_Click(object sender, EventArgs e)
        {
            patienttimer.Start();
        }

        private void ReqList_MouseDown(object sender, MouseEventArgs e)
        {

            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void ReqList_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void ReqList_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
