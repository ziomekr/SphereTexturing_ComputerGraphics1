using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class Camera
    {
        public Point3D Position { get; set; }
        public Point3D Target { get; set; }
        public Vector3D UpDirection { get; set; }
    }
}
