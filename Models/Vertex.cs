using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class Vertex
    {
        public Point3D P { get; set; }
        public Point3D Projected { get; set; }
        public MappingPoint TextureCoordinates { get; set; }
    }

    public class MappingPoint
    {
        public MappingPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }

    
}
