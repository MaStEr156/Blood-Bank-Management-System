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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        bool drag = false;
        Point start_point = new Point(0, 0);
        private string HashPassword(string password)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(bytes);
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        private void InsertEmployeeData(string EmpID, string userEmail, string password, string phone)
        {
            // Use your actual connection string
            SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");



            Cn.Open();

            // Use parameterized query to prevent SQL injection
            string insertQuery = "INSERT INTO HosUsers (HosName, Email, Password, Phone) VALUES (@HosName, @Email, @Password, @Phone)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, Cn))
            {
                cmd.Parameters.AddWithValue("@HosName", EmpID);
                cmd.Parameters.AddWithValue("@Email", userEmail);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Password", HashPassword(password));

                // Execute the query
                cmd.ExecuteNonQuery();
            }

        }
        private void RegBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(EmpID.Text) ||
                string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(passoword.Text) ||
                string.IsNullOrWhiteSpace(rePassoword.Text))
            {
                new Msg("* Registration failed \r\nFields are empty").ShowDialog();

            }
            else if (passoword.Text != rePassoword.Text)
            {
                new Msg("* Registration failed \r\nThe Passwords do not match").ShowDialog();
            }
            else
            {
                // Validation passed, proceed with database insertion
                InsertEmployeeData(EmpID.Text, email.Text, passoword.Text, phoneTxt.Text);

                // Clear the input fields
                EmpID.Text = "";
                email.Text = "";
                passoword.Text = "";
                rePassoword.Text = "";
                new Msg(" Success\r\nRegistration successful").ShowDialog();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                passoword.PasswordChar = '\0';
                rePassoword.PasswordChar = '\0';
            }
            else
            {
                passoword.PasswordChar = '*';
                rePassoword.PasswordChar = '*';
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
