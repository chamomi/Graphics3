using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphics3
{
    public partial class Filling : Form
    {
        private int x1, y1;
        List<Point> P = new List<Point>();
        List<int> indices = new List<int>();
        List<Edge> AET = new List<Edge>();
        Bitmap img;

        public Filling()
        {
            InitializeComponent();
            x1 = y1 = -1;
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
        }

        private void Draw(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    int x = ((MouseEventArgs)e).X;
                    int y = ((MouseEventArgs)e).Y;

                    P.Add(new Point(x, y));

                    if (x1 == -1 || y1 == -1)
                    {
                        x1 = x; y1 = y;
                    }
                    else
                    {
                        DDA(x, pictureBox1.Height - y, x1, pictureBox1.Height - y1);
                        pictureBox1.Image = img;

                        x1 = x;
                        y1 = y;
                    }
                    break;

                case MouseButtons.Right:
                    int xl = P[0].X;
                    int yl = P[0].Y;

                    DDA(xl, pictureBox1.Height - yl, x1, pictureBox1.Height - y1);
                    pictureBox1.Image = img;

                    x1 = -1;
                    y1 = -1;

                    break;
            }
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create indices
            int high = img.Height;
            while (indices.Count != P.Count)
            {
                int i0 = 0;
                while (P[i0].Y >= high) i0++;
                int max = P[i0].Y;
                int imax = i0;
                foreach (var p in P)
                {
                    if ((p.Y > max) && (p.Y < high))
                    {
                        max = p.Y;
                        imax = P.IndexOf(p);
                    }
                }
                indices.Add(imax);
                high = max;
            }

            int k = 0;
            int i = indices[k];
            int ymin = P[indices[0]].Y;
            int ymax = P[indices[P.Count - 1]].Y;
            int y = ymin;
            while (y > ymax)
            {
                while (P[i].Y == y)
                {
                    if ((i > 0) && (P[i - 1].Y < P[i].Y))
                        AET.Add(new Edge(P[i], P[i - 1]));
                    if ((i == 0) && (P[P.Count - 1].Y < P[i].Y))
                        AET.Add(new Edge(P[i], P[P.Count - 1]));
                    if (P[(i + 1)%P.Count].Y < P[i].Y)
                        AET.Add(new Edge(P[i], P[(i + 1)%P.Count]));

                    k++;
                    i = indices[k];
                }

                AET = AET.OrderBy(o => o.xmin).ToList();

                for (int a = 0; a < AET.Count; a += 2)
                {
                    double xcur = AET[a].xmin - 1;
                    while (xcur <= AET[a + 1].xmin +2)
                    {
                        img.SetPixel((int)xcur, y, Color.Aquamarine);
                        xcur++;
                    }
                }
                pictureBox1.Image = img;
                y--;

                foreach (var el in AET.ToList())
                    if (el.ymax >= y) AET.Remove(el);

                for (int a = 0; a < AET.Count; a++)
                    AET[a].xmin += AET[a].incr;
            }
        }

        private void DDA(int x1, int y1, int x2, int y2)
        {
            float step;
            float diffy = Math.Abs(y2 - y1);
            float diffx = Math.Abs(x2 - x1);

            if (diffx >= diffy)
                step = diffx;
            else
                step = diffy;

            diffx = diffx / step;
            diffy = diffy / step;

            float x = x1;
            float y = y1;
            int i = 1;

            while (i <= step)
            {
                putPixel((int)Math.Round(x), (int)Math.Round(y), Color.Black);

                if (y2 - y1 >= 0) y += diffy;
                else y -= diffy;

                if (x2 - x1 >= 0) x += diffx;
                else x -= diffx;

                i++;
            }
        }

        private void putPixel(int x, int y, Color color)
        {
            if (x < 0) x = 0;
            if (x >= img.Width) x = img.Width - 1;
            if (y < 0) y = 0;
            if (y >= img.Height) y = img.Height - 1;

            img.SetPixel(x, img.Height - 1 - y, color);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;

            P.Clear();
            indices.Clear();
            AET.Clear();
            x1 = y1 = -1;
        }
    }
}
