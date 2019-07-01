using SphereTexturing_ComputerGraphics1;
using System.Windows;

namespace ComputerGraphics_ClippingFilling
{
    internal class Edge
    {
        public Edge(Vertex v1, Vertex v2)
        {
            V1 = v1;
            V2 = v2;

            if (v1.Projected.Y > v2.Projected.Y)
                ymax = (int)v1.Projected.Y;
            else
                ymax = (int)v2.Projected.Y;
            x = v1.Projected.X;
            if (v1.Projected.X != v2.Projected.X && v2.Projected.Y != v1.Projected.Y)
            {
                invM = 1 / ((v2.Projected.Y - v1.Projected.Y) / (v2.Projected.X - v1.Projected.X));
            }
            else
                invM = 0;
        }

        public double invM { get; set; }
        public double x { get; set; }
        public int ymax { get; set; }
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
    }
}