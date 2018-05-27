using System.Drawing;

namespace Graphics3
{
    class Edge
    {
        public int ymax;
        public double xmin;
        public double incr;
        public Edge next;


        public Edge()
        { }

        public Edge(Point p1, Point p2)
        {
            if (p1.Y < p2.Y)
            {
                ymax = p1.Y;
                xmin = p2.X;
                incr = -(double)(p1.X - p2.X) / (p1.Y - p2.Y);
            }
            else
            {
                ymax = p2.Y;
                xmin = p1.X;
                incr = -(double)(p1.X - p2.X) / (p1.Y - p2.Y);
            }
        }
    }
}
