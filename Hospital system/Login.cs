﻿using Microsoft.Win32;
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
    public partial class Login : Form
    {
        private const string connectionString = "Data Source=MASTER;Initial Catalog=Blood_Bank;Integrated Security=True";

        bool drag = false;
        Point start_point = new Point(0, 0);
        public Login()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.PasswordChar = '\0';

            }
            else
            {
                password.PasswordChar = '*';

            }
        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(password.Text))
            {
                new Msg("* Login failed \r\nFields are empty").ShowDialog();
                return;
            }

            if (AuthenticateUser(Email.Text, password.Text))
            {
                // Proceed with further actions after successful login
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                SqlCommand cmd_getID = new SqlCommand("SELECT HosID FROM HosUsers WHERE Email=@Email AND Password=@Password", connection);
                cmd_getID.Parameters.AddWithValue("@Email", Email.Text);
                cmd_getID.Parameters.AddWithValue("@Password", HashPassword(password.Text));

                int HosID = Convert.ToInt32(cmd_getID.ExecuteScalar());

                connection.Close();
                Requests home = new Requests(HosID);
                this.Hide();
                home.FormClosed += (s, args) => this.Close();
                home.Show();
            }
            else
            {
                new Msg("* Login failed \r\nInvalid credentials").ShowDialog();

            }

        }
        private bool AuthenticateUser(string Email, string enteredPassword)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Use parameterized query to prevent SQL injection
                string query = "SELECT COUNT(*) FROM HosUsers WHERE Email=@Email AND Password=@Password";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(enteredPassword));

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.FormClosed += (s, args) => this.Close();
            register.Show();
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
