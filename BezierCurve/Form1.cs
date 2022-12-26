using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BezierCurve
{
    public partial class Form1 : Form
    {
        Point[] Q = new Point[5];
        int clickcount = 0;
        bool mouse = false;
        Control[] ct = new Control[5];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 255));
            Pen p = new Pen(Color.FromArgb(255, 255, 0, 255));
            if (clickcount == 4)
            {

                Point p1 = Q[0];   // Start point
                Point c1 = Q[1];   // First control point
                Point c2 = Q[2];  // Second control point
                Point p2 = Q[3];  // Endpoint

                //e.Graphics.DrawBezier(pen, p1, c1, c2, p2);
                for (double t = 0.00; t <= 1.0; t += 0.001)
                {
                    double x = (-t * t * t + 3 * t * t - 3 * t + 1) * p1.X
                            + (3 * t * t * t - 6 * t * t + 3 * t) * c1.X
                            + (-3 * t * t * t + 3 * t * t) * c2.X
                            + (t * t * t) * p2.X;
                    double y = (-t * t * t + 3 * t * t - 3 * t + 1) * p1.Y
                            + (3 * t * t * t - 6 * t * t + 3 * t) * c1.Y
                            + (-3 * t * t * t + 3 * t * t) * c2.Y
                            + (t * t * t) * p2.Y;
                    e.Graphics.FillRectangle(Brushes.Red, Convert.ToInt32(x), Convert.ToInt32(y), 2, 2);
                }
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (clickcount < 4)
            {
                Control cr = new Control();
                Q[clickcount] = new Point(e.X, e.Y);
                ct[clickcount] = cr;
                clickcount += 1;
                this.Controls.Add(cr);
                cr.Location = new Point(e.X - 5, e.Y - 5);
                cr.Size = new Size(8, 8);
                cr.BackColor = Color.Black;
                cr.MouseDown += Cr_Down;
                cr.MouseMove += Cr_Move;
                cr.MouseUp += Cr_Up;
            }
            if (clickcount == 4)
            {
                this.Refresh();
            }
        }

        void Cr_Move(object sender, MouseEventArgs e)
        {
            if (mouse == true)
            {
                Control cr = sender as Control;
                int x = e.Location.X;
                int y = e.Location.Y;
                cr.Location = new Point(cr.Location.X + x, cr.Location.Y + y);
                if (cr == ct[0])
                {
                    Q[0].X = Q[0].X + x;
                    Q[0].Y = Q[0].Y + y;
                    this.Refresh();
                }
                if (cr == ct[1])
                {
                    Q[1].X = Q[1].X + x;
                    Q[1].Y = Q[1].Y + y;
                    this.Refresh();
                }
                if (cr == ct[2])
                {
                    Q[2].X = Q[2].X + x;
                    Q[2].Y = Q[2].Y + y;
                    this.Refresh();
                }
                if (cr == ct[3])
                {
                    Q[3].X = Q[3].X + x;
                    Q[3].Y = Q[3].Y + y;
                    this.Refresh();
                }
            }
        }
        void Cr_Down(object sender, MouseEventArgs e)
        {
            mouse = true;
        }
        void Cr_Up(object sender, MouseEventArgs e)
        {
            mouse = false;
            this.Refresh();
        }
    }
}
