using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphereTexturing_ComputerGraphics1
{
    public static class TransformationMatrix
    {
        public static Matrix<double> GetRotationMatrix(Vector3D rotationVector)
        {
            Matrix<double> Rx = DenseMatrix.OfArray(new double[,]
            {
                { 1, 0, 0, 0 },
                { 0, Trig.Cos(rotationVector.X), -Trig.Sin(rotationVector.X), 0 },
                { 0, Trig.Sin(rotationVector.X), Trig.Cos(rotationVector.X), 0 },
                { 0, 0, 0, 1}
            });
            Matrix<double> Ry = DenseMatrix.OfArray(new double[,]
            {
                {Trig.Cos(rotationVector.Y), 0, Trig.Sin(rotationVector.Y), 0 },
                { 0, 1, 0, 0 },
                { -Trig.Sin(rotationVector.Y), 0, Trig.Cos(rotationVector.Y), 0 },
                { 0, 0, 0, 1 }
            }); Matrix<double> Rz = DenseMatrix.OfArray(new double[,]
             {
                { Trig.Cos(rotationVector.Z), -Trig.Sin(rotationVector.Z), 0, 0 },
                { Trig.Sin(rotationVector.Z), Trig.Cos(rotationVector.Z), 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
             });

            return Rz * Ry * Rx;
        }

        public static Matrix<double> GetTranslationMatrix(Point3D translation)
        {
            return DenseMatrix.OfArray(new double[,]
            {
                { 1, 0, 0, translation.X },
                { 0, 1, 0, translation.Y },
                { 0, 0, 1, translation.Z },
                { 0, 0, 0, 1} 
            });
        }

        public static Matrix<double> GetCameraMatrix(Camera camera)
        {
            Point3D cZ = (camera.Position - camera.Target) / (camera.Position - camera.Target).Magnitude();
            Point3D cX = camera.UpDirection.CrossProduct(cZ) / camera.UpDirection.CrossProduct(cZ).Magnitude();
            Point3D cY = cZ.CrossProduct(cX) / cZ.CrossProduct(cX).Magnitude();
            return DenseMatrix.OfArray(new double[,]
                {
                    { cX.X, cX.Y, cX.Z, cX*camera.Position},
                    { cY.X, cY.Y, cY.Z, cY*camera.Position},
                    { cZ.X, cZ.Y, cZ.Z, cZ*camera.Position},
                    { 0, 0, 0, 1}
                });
        }

        public static Matrix<double> GetProjectionMatrix(double angle, double screenWidth, double screenHeight)
        {
            return DenseMatrix.OfArray(new double[,]
            {
                { -(screenWidth/2)*Trig.Cot(angle/2), 0, screenWidth/2, 0},
                { 0, screenWidth/2 * Trig.Cot(angle/2), screenHeight/2, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 }
            });
        }
    }
}
