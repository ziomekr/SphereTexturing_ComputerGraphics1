using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public class Point3D
    {
        public Point3D() { }
        public Point3D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double W { get; set; }

        public Point3D CrossProduct(Point3D other)
        {
            return new Point3D { X = this.Y * other.Z - this.Z * other.Y, Y = this.Z * other.X - this.X * other.Z, Z = this.X * other.Y - this.Y * other.X, W = 0 };
        }

        public double Magnitude()
        {
            return Math.Sqrt(Math.Pow(this.X, 2) + Math.Pow(this.Y, 2) + Math.Pow(this.Z, 2) + Math.Pow(this.W, 2));
        }

        public static double operator* (Point3D a, Point3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Point3D operator- (Point3D a, Point3D b)
        {
            return new Point3D { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z, W = a.W - b.W };
        }

        public static Point3D operator/ (Point3D a, double b)
        {
            return new Point3D { X = a.X / b, Y = a.Y / b, Z = a.Z / b, W = a.W / b };
        }

        public static Point3D operator* (Matrix<double> a, Point3D b)
        {
            Vector<double> vector = Vector<double>.Build.DenseOfArray(new double[] { b.X, b.Y, b.Z, b.W });
            vector = a * vector;
            return new Point3D { X = vector[0], Y = vector[1], Z = vector[2], W = vector[3] };

        }

    }
}
