using ComputerGraphics_ClippingFilling;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SphereTexturing_ComputerGraphics1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Sphere s;
        float op=0;
        public MainWindow()
        {
            InitializeComponent();
            s = new Sphere(150, 10, 10);
            
            foreach(Vertex v in s.Vertices)
            {
                v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(Math.PI, 0, 0)) * v.P;
                v.Projected = TransformationMatrix.GetCameraMatrix(
                new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                v.Projected = TransformationMatrix.GetProjectionMatrix(180, 600, 300) * v.Projected;
                v.Projected = v.Projected / v.Projected.W;
            }
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);
            WriteableBitmap ToProcess = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, myPalette);
            byte[] Pixels = new byte[ToProcess.PixelHeight * ToProcess.PixelWidth * 3];

            for (int i = 0; i < Pixels.Length; i++)
            {
                Pixels[i] = 0;
                
            }
            //ToProcess.WritePixels(new System.Windows.Int32Rect(0, 0, ToProcess.PixelWidth, ToProcess.PixelHeight), Pixels, ToProcess.BackBufferStride, 0);
            SceneImage.Source = ToProcess;
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            //SceneCanvas.Children.Clear();
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);
            WriteableBitmap ToProcess = new WriteableBitmap(600, 300, 96, 96, PixelFormats.Bgr32, myPalette);
            byte[] Pixels = new byte[ToProcess.PixelHeight * ToProcess.PixelWidth * ToProcess.Format.BitsPerPixel / 8];
            foreach (MeshTriangle t in s.Mesh)
            {
                if (t != null)
                {
                    if (BackFaceCulling.IsFrontFace(t))
                    {
                        //List<Point> p = new List<Point>();
                        //p.Add(new Point((int)t.V1.Projected.X, (int)t.V1.Projected.Y));
                        //p.Add(new Point((int)t.V2.Projected.X, (int)t.V2.Projected.Y));
                        //p.Add(new Point((int)t.V3.Projected.X, (int)t.V3.Projected.Y));
                        //drawLine(p[0], p[1], Colors.Black, SceneCanvas);
                        //drawLine(p[1], p[2], Colors.Black, SceneCanvas);
                        //drawLine(p[2], p[0], Colors.Black, SceneCanvas);
                        List<Point3D> p = new List<Point3D>();
                        p.Add(t.V1.Projected);
                        p.Add(t.V2.Projected);
                        p.Add(t.V3.Projected);
                        ScanlineFillVertexSort filler = new ScanlineFillVertexSort(p, Pixels);
                        filler.fillPolygon();
                    }

                }
            }
            ToProcess.WritePixels(new System.Windows.Int32Rect(0, 0, ToProcess.PixelWidth, ToProcess.PixelHeight), Pixels, ToProcess.BackBufferStride, 0);
            SceneImage.Source = ToProcess;
                foreach (Vertex v in s.Vertices)
                {
                    v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(op,0,0  )) * v.P;
                    v.Projected = TransformationMatrix.GetCameraMatrix(
                    new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                    v.Projected = TransformationMatrix.GetProjectionMatrix(180, 600, 300) * v.Projected;
                    v.Projected = v.Projected / v.Projected.W;
                
                }
            op+=0.05f;
                
            
        }
        private void drawLine(Point p1, Point p2, Color color, Canvas Canvas)
        {
            Line l = new Line();
            l.X1 = p1.X;
            l.Y1 = p1.Y;
            l.X2 = p2.X;
            l.Y2 = p2.Y;
            SolidColorBrush brush = new SolidColorBrush(color);
            l.StrokeThickness = 1;
            l.Stroke = brush;
            Canvas.Children.Add(l);
        }
    }
}
