using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class BackFaceCulling
    {
        public static bool IsFrontFace(MeshTriangle mesh)
        {
            if (((mesh.V2.Projected.X - mesh.V1.Projected.X) * (mesh.V3.Projected.Y - mesh.V1.Projected.Y) 
                - (mesh.V3.Projected.X - mesh.V1.Projected.X) * (mesh.V2.Projected.Y - mesh.V1.Projected.Y)) > 0)
                return true;
            return false;
        }
    }
}
