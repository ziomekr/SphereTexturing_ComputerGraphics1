using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class Sphere
    {
        public Sphere(int radius, int meridians, int parallels)
        {
            Radius = radius;
            Meridians = meridians;
            Parallels = parallels;
            Mesh = new MeshTriangle[2 * Meridians * Parallels];
            Vertices = GenerateVertices();
            GenerateMesh();
        }

        public int Radius { get; set; }
        public int Meridians { get; set; }
        public int Parallels { get; set; }
        public Vertex[] Vertices { get; set; }
        public MeshTriangle[] Mesh { get; set; }
        
        private Vertex[] GenerateVertices()
        {
            Vertex[] vertices = new Vertex[Meridians * Parallels + 2];
            vertices[0] = new Vertex { P = new Point3D(0, Radius, 0, 1), TextureCoordinates = new MappingPoint(1, 0.5) };
            for (int i = 0; i <= Parallels - 1; i++)
            {
                for (int j = 0; j <= Meridians - 1; j++)
                {
                    int x = (int)(Radius * Math.Cos(2 * Math.PI * j / Meridians) * Math.Sin( (i+1) * Math.PI / (Parallels + 1)));
                    int y = (int)(Radius * Math.Cos((i + 1) * Math.PI / (Parallels + 1)));
                    int z = (int)(Radius * Math.Sin(2 * Math.PI * j / Meridians) * Math.Sin((i + 1) * Math.PI / (Parallels + 1)));
                    vertices[i * Meridians + j + 1] = new Vertex { P = new Point3D(x, y, z , 1), TextureCoordinates = new MappingPoint((double)j / (Meridians - 1), (double)(i+1)/(Parallels+1)) };
                }
            }
            vertices[Meridians * Parallels + 1] = new Vertex { P = new Point3D(0, -Radius, 0, 1), TextureCoordinates = new MappingPoint(0, 0.5) };
            return vertices;
        }

        private void GenerateMesh()
        {
            GenerateLids(Vertices);
            GenerateRings(Vertices);
        }

        private void GenerateLids(Vertex[] vertices)
        {
            for (int i = 0; i <= Meridians - 2; i++)
            {
                Mesh[i]= new MeshTriangle(vertices[0], vertices[i + 2], vertices[i + 1]);
                Mesh[2*(Parallels - 1) * Meridians + Meridians + i]= new MeshTriangle(vertices[Meridians * Parallels + 1], vertices[Meridians * (Parallels - 1) + i + 1], vertices[Meridians * (Parallels - 1) + i + 2]);
            }
            Mesh[Meridians - 1] = new MeshTriangle(vertices[0], vertices[1], vertices[Meridians]);
            Mesh[2 * (Parallels - 1) * Meridians + 2 * Meridians - 1] = new MeshTriangle(vertices[Meridians * Parallels + 1], vertices[Meridians * Parallels], vertices[Meridians * (Parallels - 1) + 1]);
        }

        private void GenerateRings(Vertex[] vertices)
        {
            for (int i = 0; i <= Parallels - 2; i++)
            {
                for (int j = 1; j <= Meridians - 1; j++)
                {
                    Mesh[(2 * i + 1) * Meridians + j - 1] = new MeshTriangle(vertices[i * Meridians + j], vertices[i * Meridians + j + 1], vertices[(i + 1) * Meridians + j + 1]);
                    Mesh[(2 * i + 2) * Meridians + j - 1] = new MeshTriangle(vertices[i * Meridians + j], vertices[(i + 1) * Meridians + j + 1], vertices[(i + 1) * Meridians + j]);
                }
                Mesh[(2 * i + 1) * Meridians + Meridians - 1] = new MeshTriangle(vertices[(i + 1) * Meridians], vertices[i * Meridians + 1], vertices[(i + 1) * Meridians * 1]);
                Mesh[(2 * i + 2) * Meridians + Meridians - 1] = new MeshTriangle(vertices[(i + 1) * Meridians], vertices[(i + 1) * Meridians + 1], vertices[(i + 2) * Meridians]);
            }
        }
    }
}
