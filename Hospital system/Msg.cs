using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_system
{
    public partial class Msg : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);
        public Msg(string msg = "ok")
        {
            InitializeComponent();
            label2.Text = msg;

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void guna2Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void guna2Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void guna2Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;

        }
    }
}
