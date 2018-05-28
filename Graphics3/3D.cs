using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TCD.Mathematics;

namespace Graphics3
{
    public partial class _3D : Form
    {
        Bitmap img;
        public _3D()
        {
            InitializeComponent();
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
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

        private void drawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int size = Int32.Parse(toolStripTextBox1.Text) / 2;
            //Trace.WriteLine(size);
            List<Point3D> points = new List<Point3D>();
            points.Add(new Point3D(-size, -size, -size));
            points.Add(new Point3D(-size, size, -size));
            points.Add(new Point3D(-size, size, size));
            points.Add(new Point3D(-size, -size, size));

            points.Add(new Point3D(size, -size, -size));
            points.Add(new Point3D(size, size, -size));
            points.Add(new Point3D(size, size, size));
            points.Add(new Point3D(size, -size, size));
            Trace.WriteLine("added");

            DDA(project(points[0]).X, pictureBox1.Image.Height - project(points[0]).Y, project(points[1]).X, pictureBox1.Image.Height - project(points[1]).Y);
            DDA(project(points[1]).X, pictureBox1.Image.Height - project(points[1]).Y, project(points[2]).X, pictureBox1.Image.Height - project(points[2]).Y);
            DDA(project(points[2]).X, pictureBox1.Image.Height - project(points[2]).Y, project(points[3]).X, pictureBox1.Image.Height - project(points[3]).Y);
            DDA(project(points[3]).X, pictureBox1.Image.Height - project(points[3]).Y, project(points[0]).X, pictureBox1.Image.Height - project(points[0]).Y);

            Trace.WriteLine("1");
            DDA(project(points[4]).X, pictureBox1.Image.Height - project(points[4]).Y, project(points[5]).X, pictureBox1.Image.Height - project(points[5]).Y);
            DDA(project(points[5]).X, pictureBox1.Image.Height - project(points[5]).Y, project(points[6]).X, pictureBox1.Image.Height - project(points[6]).Y);
            DDA(project(points[6]).X, pictureBox1.Image.Height - project(points[6]).Y, project(points[7]).X, pictureBox1.Image.Height - project(points[7]).Y);
            DDA(project(points[7]).X, pictureBox1.Image.Height - project(points[7]).Y, project(points[4]).X, pictureBox1.Image.Height - project(points[4]).Y);

            Trace.WriteLine("2");
            DDA(project(points[0]).X, pictureBox1.Image.Height - project(points[0]).Y, project(points[4]).X, pictureBox1.Image.Height - project(points[4]).Y);
            DDA(project(points[1]).X, pictureBox1.Image.Height - project(points[1]).Y, project(points[5]).X, pictureBox1.Image.Height - project(points[5]).Y);
            DDA(project(points[2]).X, pictureBox1.Image.Height - project(points[2]).Y, project(points[6]).X, pictureBox1.Image.Height - project(points[6]).Y);
            DDA(project(points[3]).X, pictureBox1.Image.Height - project(points[3]).Y, project(points[7]).X, pictureBox1.Image.Height - project(points[7]).Y);

            Trace.WriteLine("3");
            pictureBox1.Image = img;
        }

        private Point project(Point3D v)
        {
            int d = 50;
            //Trace.WriteLine((int)(v.X * d / v.Z + 100));
            //Trace.WriteLine((int)(v.Y * d / v.Z + 100));
            return new Point((int)(v.X * d / v.Z + 100), (int)(v.Y * d / v.Z + 100));
        }

    }
}
