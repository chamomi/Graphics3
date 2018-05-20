using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphics3
{
    public partial class Form1 : Form
    {
        private int x1, y1, x2, y2;
        private int cx1, cy1, cx2, cy2;
        private Bitmap img;
        private bool dda, midpoint, xline, xcircle, copy, cs, vs;
        bool ongo = false;

        public Form1()
        {
            InitializeComponent();
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
            x1 = x2 = y1 = y2 = -1;
            cx1 = cx2 = cy1 = cy2 = -1;
            dda = true;
            midpoint = xline = xcircle = copy = cs = vs = false;
        }

        private void Draw(object sender, EventArgs e)
        {
            int x = ((MouseEventArgs)e).X;
            int y = ((MouseEventArgs)e).Y;

            if (ongo) return;

            if (x1 == -1 || y1 == -1)
            {
                x1 = x; y1 = y;
            }
            else
            {
                x2 = x; y2 = y;
                //Trace.WriteLine("line " + x1 + " " + x2 + " " + y1 + " " + y2);
                if (dda)
                    DDA(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
                else if (midpoint)
                    MidpointCircle(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
                else if (xline)
                    XLine(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
                else if (xcircle)
                    XCircle(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
                else if (copy)
                    PixelCopying(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
                else if ((cs) && (!ongo))
                {
                    MessageBox.Show("Mark target rectangle");
                    ongo = true;
                }

                if (!cs)
                    x1 = x2 = y1 = y2 = -1;
            }
            pictureBox1.Image = img;
        }

        private void mdown(object sender, MouseEventArgs e)
        {
            if ((cs) && (x1 != -1) && (x2 != -1))
            {
                int x = ((MouseEventArgs)e).X;
                int y = ((MouseEventArgs)e).Y;
                cx1 = x;
                cy1 = y;
            }
        }

        private void mup(object sender, MouseEventArgs e)
        {
            if ((cs) && (x1 != -1) && (x2 != -1) && (cx1 != -1))
            {
                int x = ((MouseEventArgs)e).X;
                int y = ((MouseEventArgs)e).Y;
                int tmp;
                cx2 = x;
                cy2 = y;

                if (cx2 < cx1)
                { tmp = cx2; cx2 = cx1; cx1 = tmp; }
                if (cy2 < cy1)
                { tmp = cy2; cy2 = cy1; cy1 = tmp; }
                RectangleF rect = new RectangleF(cx1, cy1, cx2 - cx1, cy2 - cy1);

                DDA(cx1, pictureBox1.Height - cy1, cx2, pictureBox1.Height - cy1);//top line
                DDA(cx1, pictureBox1.Height - cy2, cx2, pictureBox1.Height - cy2);//bottom line
                DDA(cx1, pictureBox1.Height - cy1, cx1, pictureBox1.Height - cy2);//left
                DDA(cx2, pictureBox1.Height - cy1, cx2, pictureBox1.Height - cy2);//right

                if (cs)
                    CohenSutherland(x1, y1, x2, y2, rect);

                pictureBox1.Image = img;
                x1 = x2 = y1 = y2 = -1;
                cx1 = cx2 = cy1 = cy2 = -1;
                ongo = false;
            }
        }


        #region Lab3
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

        private void MidpointCircle(int x1, int y1, int x2, int y2)
        {
            int diffx = (int)Math.Abs(x2 - x1);
            int diffy = (int)Math.Abs(y2 - y1);
            int R = (int)Math.Sqrt(diffx * diffx + diffy * diffy);

            int d = 1 - R;
            int x = 0;
            int y = R;
            putPixel(x1 + x, y1 + y, Color.Black);
            putPixel(x1 + y, y1 + x, Color.Black);
            putPixel(x1 - y, y1 + x, Color.Black);
            putPixel(x1 - x, y1 + y, Color.Black);
            putPixel(x1 - x, y1 - y, Color.Black);
            putPixel(x1 - y, y1 - x, Color.Black);
            putPixel(x1 + y, y1 - x, Color.Black);
            putPixel(x1 + x, y1 - y, Color.Black);
            while (y > x)
            {
                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += 2 * x - 2 * y + 5;
                    --y;
                }
                ++x;
                putPixel(x1 + x, y1 + y, Color.Black);
                putPixel(x1 + y, y1 + x, Color.Black);
                putPixel(x1 - y, y1 + x, Color.Black);
                putPixel(x1 - x, y1 + y, Color.Black);
                putPixel(x1 - x, y1 - y, Color.Black);
                putPixel(x1 - y, y1 - x, Color.Black);
                putPixel(x1 + y, y1 - x, Color.Black);
                putPixel(x1 + x, y1 - y, Color.Black);
            }
        }

        private void XLine(int x1, int y1, int x2, int y2)
        {
            Color color = Color.Black;
            Color backcolor = Color.FromArgb(color.R + 175 > 255 ? 255 : color.R + 175,
                                             color.G + 175 > 255 ? 255 : color.G + 175,
                                             color.B + 175 > 255 ? 255 : color.B + 175);
            float diffy = y2 - y1, diffx = x2 - x1;
            float m = diffy / diffx;

            if (Math.Abs(diffx) > Math.Abs(diffy))
            {
                if (x1 > x2)
                {
                    int tmp = x1; x1 = x2; x2 = tmp;
                    tmp = y1; y1 = y2; y2 = tmp;
                }
                float y = y1;
                for (int x = x1; x <= x2; ++x)
                {
                    Color c1 = Color.FromArgb((byte)(color.R * (1 - (y - (int)y)) + backcolor.R * (y - (int)y)),
                                              (byte)(color.G * (1 - (y - (int)y)) + backcolor.G * (y - (int)y)),
                                              (byte)(color.B * (1 - (y - (int)y)) + backcolor.B * (y - (int)y)));
                    Color c2 = Color.FromArgb((byte)(color.R * ((y - (int)y)) + backcolor.R * (1 - (y - (int)y))),
                                              (byte)(color.G * ((y - (int)y)) + backcolor.G * (1 - (y - (int)y))),
                                              (byte)(color.B * ((y - (int)y)) + backcolor.B * (1 - (y - (int)y))));

                    if (c1.R + c1.G + c1.B > c2.R + c2.G + c2.B)
                    {
                        putPixel(x, (int)Math.Floor(y), c1);
                        putPixel(x, (int)Math.Floor(y) + 1, c2);
                    }
                    else
                    {
                        putPixel(x, (int)Math.Floor(y) + 1, c2);
                        putPixel(x, (int)Math.Floor(y), c1);
                    }
                    y += m;
                    pictureBox1.Image = img;
                }
            }
            else
            {
                if (y1 > y2)
                {
                    int tmp = x1; x1 = x2; x2 = tmp;
                    tmp = y1; y1 = y2; y2 = tmp;
                }
                float x = x1;
                m = diffx / diffy;
                for (int y = y1; y < y2; ++y)
                {
                    Color c1 = Color.FromArgb((byte)(color.R * (1 - (x - (int)x)) + backcolor.R * (x - (int)x)),
                                              (byte)(color.G * (1 - (x - (int)x)) + backcolor.G * (x - (int)x)),
                                              (byte)(color.B * (1 - (x - (int)x)) + backcolor.B * (x - (int)x)));
                    Color c2 = Color.FromArgb((byte)(color.R * ((x - (int)x)) + backcolor.R * (1 - (x - (int)x))),
                                              (byte)(color.G * ((x - (int)x)) + backcolor.G * (1 - (x - (int)x))),
                                              (byte)(color.B * ((x - (int)x)) + backcolor.B * (1 - (x - (int)x))));
                    if (c1.R + c1.G + c1.B > c2.R + c2.G + c2.B)
                    {
                        putPixel((int)Math.Floor(x), y, c1);
                        putPixel((int)Math.Floor(x) + 1, y, c2);
                    }
                    else
                    {
                        putPixel((int)Math.Floor(x) + 1, y, c2);
                        putPixel((int)Math.Floor(x), y, c1);
                    }
                    x += m;
                }
            }
        }

        void XCircle(int x1, int y1, int x2, int y2)
        {
            Color color = Color.Black;

            Color backcolor = Color.FromArgb(color.R + 100 > 175 ? 175 : color.R + 100,
                                              color.G + 100 > 175 ? 175 : color.G + 100,
                                              color.B + 100 > 175 ? 175 : color.B + 100);
            int dx = (int)Math.Abs(x2 - x1);
            int dy = (int)Math.Abs(y2 - y1);
            int R = (int)Math.Sqrt(dx * dx + dy * dy);

            double x = R;
            int y = 0;

            float T = diam(R, y);
            Color c1 = Color.FromArgb((byte)(color.R * (1 - T) + backcolor.R * T),
                                      (byte)(color.G * (1 - T) + backcolor.G * T),
                                      (byte)(color.B * (1 - T) + backcolor.B * T));
            Color c2 = Color.FromArgb((byte)(color.R * (T) + backcolor.R * (1 - T)),
                                      (byte)(color.G * (T) + backcolor.G * (1 - T)),
                                      (byte)(color.B * (T) + backcolor.B * (1 - T)));

            putPixel(x1 + (int)x, y1 + y, c1);
            putPixel(x1 - y, y1 + (int)x, c1);
            putPixel(x1 - (int)x, y1 - y, c1);
            putPixel(x1 + y, y1 - (int)x, c1);

            for (y = 0; y < (int)x; y++)
            {
                T = diam(R, y);
                c1 = Color.FromArgb((byte)(color.R * (1 - T) + backcolor.R * T),
                                    (byte)(color.G * (1 - T) + backcolor.G * T),
                                    (byte)(color.B * (1 - T) + backcolor.B * T));
                c2 = Color.FromArgb((byte)(color.R * (T) + backcolor.R * (1 - T)),
                                    (byte)(color.G * (T) + backcolor.G * (1 - T)),
                                    (byte)(color.B * (T) + backcolor.B * (1 - T)));

                x = Math.Sqrt(R * R - y * y);
                int f = (int)x % 1;
                int xleft = (int)(x - f);

                //1st
                putPixel(x1 + xleft, y1 + y, c1);
                putPixel(x1 + xleft + 1, y1 + y, c2);
                //2nd
                putPixel(x1 + y, y1 + xleft, c1);
                putPixel(x1 + y, y1 + xleft + 1, c2);
                //3rd
                putPixel(x1 - y, y1 + xleft, c1);
                putPixel(x1 - y, y1 + xleft + 1, c2);
                //4th
                putPixel(x1 - xleft, y1 + y, c1);
                putPixel(x1 - xleft + 1, y1 + y, c2);
                //5th
                putPixel(x1 - xleft, y1 - y, c1);
                putPixel(x1 - xleft + 1, y1 - y, c2);
                //6th
                putPixel(x1 - y, y1 - xleft, c1);
                putPixel(x1 - y, y1 - xleft + 1, c2);
                //7th
                putPixel(x1 + y, y1 - xleft, c1);
                putPixel(x1 + y, y1 - xleft + 1, c2);
                //8th
                putPixel(x1 + xleft, y1 - y, c1);
                putPixel(x1 + xleft + 1, y1 - y, c2);
            }
        }

        private float diam(int R, int y)
        {
            float x = (float)Math.Sqrt(R * R - y * y);
            return (float)Math.Ceiling(x) - x;
        }


        private void PixelCopying(int x1, int y1, int x2, int y2)
        {
            //int thick = Int32.Parse(Thick.Text);
            int thick = 1;
            if (thick >= 1)
            {
                DDA(x1, y1, x2, y2);
                if (thick >= 3)
                {
                    int dx = x2 - x1, dy = y2 - y1;
                    if (Math.Abs(dx) > Math.Abs(dy))
                    {
                        DDA(x1, y1 + 1, x2, y2 + 1);
                        DDA(x1, y1 - 1, x2, y2 - 1);
                        if (thick >= 5)
                        {
                            DDA(x1, y1 + 2, x2, y2 + 2);
                            DDA(x1, y1 - 2, x2, y2 - 2);
                        }
                    }
                    else
                    {
                        DDA(x1 + 1, y1, x2 + 1, y2);
                        DDA(x1 - 1, y1, x2 - 1, y2);
                        if (thick >= 5)
                        {
                            DDA(x1 + 2, y1, x2 + 2, y2);
                            DDA(x1 - 2, y1, x2 - 2, y2);
                        }
                    }
                }
            }
        }

        private void putPixel(int x, int y, Color color)
        {
            int thick = Int32.Parse(Thick.Text);
            if (x < 0) x = 0;
            if (x >= img.Width) x = img.Width - 1;
            if (y < 0) y = 0;
            if (y >= img.Height) y = img.Height - 1;

            img.SetPixel(x, img.Height - 1 - y, color);
            if (thick >= 3)
            {
                if (img.Height - 1 - y + 1 < img.Height)
                    img.SetPixel(x, img.Height - 1 - y + 1, color); // up
                if (img.Height - 1 - y - 1 >= 0)
                    img.SetPixel(x, img.Height - 1 - y - 1, color); //down
                if (x - 1 >= 0)
                    img.SetPixel(x - 1, img.Height - 1 - y, color);  //left
                if (x + 1 < img.Width)
                    img.SetPixel(x + 1, img.Height - 1 - y, color);  //right
            }

            if (thick >= 5)
            {
                if (img.Height - 1 - y + 2 < img.Height)
                    img.SetPixel(x, img.Height - 1 - y + 2, color); // up2
                if (img.Height - 1 - y - 2 >= 0)
                    img.SetPixel(x, img.Height - 1 - y - 2, color); //down2
                if (img.Height - 1 - y + 1 < img.Height && x - 1 >= 0)
                    img.SetPixel(x - 1, img.Height - 1 - y + 1, color); // up left
                if (img.Height - 1 - y + 1 < img.Height && x + 1 < img.Width)
                    img.SetPixel(x + 1, img.Height - 1 - y + 1, color); // up right
                if (x - 2 >= 0)
                    img.SetPixel(x - 2, img.Height - 1 - y, color);  //left2
                if (x + 2 < img.Width)
                    img.SetPixel(x + 2, img.Height - 1 - y, color);  //right2
                if (img.Height - 1 - y - 1 >= 0 && x - 1 >= 0)
                    img.SetPixel(x - 1, img.Height - 1 - y - 1, color); //down left
                if (img.Height - 1 - y - 1 >= 0 && x + 1 < img.Width)
                    img.SetPixel(x + 1, img.Height - 1 - y - 1, color);  //down right
            }
        }
        #endregion

        #region Lab4


        enum Outcodes
        {
            LEFT = 1,
            RIGHT = 2,
            BOTTOM = 4,
            TOP = 8
        };

        byte ComputeOutcode(int x, int y, RectangleF clip)
        {
            byte outcode = 0;
            if (x > clip.Right) outcode |= (byte)Outcodes.RIGHT;
            else if (x < clip.Left) outcode |= (byte)Outcodes.LEFT;
            if (y > clip.Bottom) outcode |= (byte)Outcodes.TOP;
            else if (y < clip.Top) outcode |= (byte)Outcodes.BOTTOM;

            return outcode;
        }
        void CohenSutherland(int x1, int y1, int x2, int y2, RectangleF clip)
        {
            bool accept = false, done = false;
            byte outcode1 = ComputeOutcode(x1, y1, clip);
            byte outcode2 = ComputeOutcode(x2, y2, clip);
            do
            {
                if ((outcode1 | outcode2) == 0)
                { //trivially accepted
                    accept = true;
                    done = true;
                }
                else if ((outcode1 & outcode2) != 0)
                { //trivially rejected
                    accept = false;
                    done = true;
                }
                else
                { //subdivide
                    byte outcodeOut = (outcode1 != 0) ? outcode1 : outcode2;
                    int x = 0;
                    int y = 0;
                    if ((outcodeOut & (byte)Outcodes.BOTTOM) != 0)
                    {
                        x = x1 + (x2 - x1) * ((int)clip.Top - y1) / (y2 - y1);
                        y = (int)clip.Top;
                    }
                    else if ((outcodeOut & (byte)Outcodes.TOP) != 0)
                    {
                        x = x1 + (x2 - x1) * ((int)clip.Bottom - y1) / (y2 - y1);
                        y = (int)clip.Bottom;
                    }
                    else if ((outcodeOut & (byte)Outcodes.RIGHT) != 0)
                    {
                        x = x1 + (x2 - x1) * ((int)clip.Right - y1) / (y2 - y1);
                        y = (int)clip.Right;
                    }
                    else if ((outcodeOut & (byte)Outcodes.LEFT) != 0)
                    {
                        x = x1 + (x2 - x1) * ((int)clip.Left - y1) / (y2 - y1);
                        y = (int)clip.Left;
                    }


                    if (outcodeOut == outcode1)
                    {
                        x1 = x;
                        y1 = y;
                        outcode1 = ComputeOutcode(x1, y1, clip);
                    }
                    else
                    {
                        x2 = x;
                        y2 = y;
                        outcode2 = ComputeOutcode(x2, y2, clip);
                    }
                }
            } while (!done);

            if (accept)
                DDA(x1, pictureBox1.Height - y1, x2, pictureBox1.Height - y2);
        }




        #endregion

        //menu
        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dda = true;
            midpoint = xline = xcircle = copy = cs = vs = false;
        }

        private void midpointCircleAdditionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            midpoint = true;
            dda = xline = xcircle = copy = cs = vs = false;
        }

        private void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xline = true;
            dda = midpoint = xcircle = copy = cs = vs = false;
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xcircle = true;
            dda = midpoint = xline = copy = cs = vs = false;
        }

        private void thickLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copy = true;
            dda = midpoint = xline = xcircle = cs = vs = false;
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
        }

        private void cohenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cs = true;
            dda = midpoint = xline = xcircle = copy = vs = false;
        }

        private void vertexSortingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vs = true;
            dda = midpoint = xline = xcircle = copy = cs = false;
        }
    }
}
