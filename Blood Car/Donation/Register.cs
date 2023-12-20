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
    public partial class Register : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public Register()
        {
            InitializeComponent();
        }
        private string HashPassword(string password)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password);
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                byte[] hashedBytes = hash.ComputeHash(bytes);
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        private void InsertEmployeeData(string EmpID, string userEmail, string password)
        {
            // Use your actual connection string
            SqlConnection Cn = new SqlConnection("Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True");

          
                Cn.Open();

                // Use parameterized query to prevent SQL injection
                string insertQuery = "INSERT INTO Employee (EmpID, Email, Password) VALUES (@EmpID, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, Cn))
                {
                    cmd.Parameters.AddWithValue("@EmpID", EmpID);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(password));

                    // Execute the query
                    cmd.ExecuteNonQuery();
                }
            
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();

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

        private void RegBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(EmpID.Text) ||
                string.IsNullOrWhiteSpace(email.Text) ||
                string.IsNullOrWhiteSpace(passoword.Text) ||
                string.IsNullOrWhiteSpace(rePassoword.Text))
            {
                new msg("* Registration failed \r\nFields are empty").ShowDialog();

            }
            else if (passoword.Text != rePassoword.Text)
            {
                new msg("* Registration failed \r\nThe Passwords do not match").ShowDialog();
            }
            else
            {
                // Validation passed, proceed with database insertion
                InsertEmployeeData(EmpID.Text, email.Text, passoword.Text);

                // Clear the input fields
                EmpID.Text = "";
                email.Text = "";
                passoword.Text = "";
                rePassoword.Text = "";
                new msg(" Success\r\nRegistration successful").ShowDialog();
            }
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
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

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }
    }
}
