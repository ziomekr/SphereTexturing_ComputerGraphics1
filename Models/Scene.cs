using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class Scene
    {
        private Camera Camera;
        private Sphere Sphere;
        private Point3D Position;
        public Scene()
        {
            Camera = new Camera { Position = new Vector3D(0, 0, 100), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) };
            Sphere = new Sphere(200, 30, 30);
            Position = new Point3D(0, 0, 0, 0);
        }
    }
}
