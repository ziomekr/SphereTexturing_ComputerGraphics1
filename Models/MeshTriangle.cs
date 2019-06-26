using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class MeshTriangle
    {
        public MeshTriangle(Vertex v1, Vertex v2, Vertex v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }

        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        public Vertex V3 { get; set; }
    }
}
