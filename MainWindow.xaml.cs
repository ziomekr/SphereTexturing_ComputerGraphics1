using ComputerGraphics_ClippingFilling;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.IO;
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
        float OX = 0;
        float OY = 0;
        byte[] Texture;
        BitmapImage img;
        public MainWindow()
        {
            InitializeComponent();
            s = new Sphere(250, 50, 50);
            
            foreach(Vertex v in s.Vertices)
            {
                v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(0, 0, 0)) * v.P;
                v.Projected = TransformationMatrix.GetCameraMatrix(
                new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                v.Projected = TransformationMatrix.GetProjectionMatrix(180, 800, 600) * v.Projected;
                v.Projected = v.Projected / v.Projected.W;
                v.Projected.NormalizeToPixels();
            }
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);
            WriteableBitmap ToProcess = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, myPalette);
            byte[] Pixels = new byte[ToProcess.PixelHeight * ToProcess.PixelWidth * 3];

            SceneImage.Source = ToProcess;
            img = LoadImage("earth.jpg");
            Texture = new byte[img.PixelWidth * img.PixelHeight * img.Format.BitsPerPixel / 8];
            img.CopyPixels(Texture, img.PixelWidth * 4, 0);
        }

        private void LeftClick(object sender, MouseButtonEventArgs e)
        {
            OX += 0.1f;
            SceneCanvas.Children.Clear();
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);
            WriteableBitmap ToProcess = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, myPalette);
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
                        //drawLine(p[0], p[1], Colors.Red, SceneCanvas);
                        //drawLine(p[1], p[2], Colors.Red, SceneCanvas);
                        //drawLine(p[2], p[0], Colors.Red, SceneCanvas);
                        List<Vertex> v = new List<Vertex>();
                        v.Add(t.V1);
                        v.Add(t.V2);
                        v.Add(t.V3);
                        ScanlineTexture filler = new ScanlineTexture(v, Pixels, Texture, img.PixelWidth, img.PixelHeight);
                        filler.fillPolygon();
                    }

                }
              
            }
            ToProcess.WritePixels(new System.Windows.Int32Rect(0, 0, ToProcess.PixelWidth, ToProcess.PixelHeight), Pixels, ToProcess.BackBufferStride, 0);
            SceneImage.Source = ToProcess;
                foreach (Vertex v in s.Vertices)
                {
                    v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(OX,OY,0  )) * v.P;
                    v.Projected = TransformationMatrix.GetCameraMatrix(
                    new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                    v.Projected = TransformationMatrix.GetProjectionMatrix(180, 800, 600) * v.Projected;
                    v.Projected = v.Projected / v.Projected.W;
                v.Projected.NormalizeToPixels();
                }
            
                
            
        }
        private void RightClick(object sender, MouseButtonEventArgs e)
        {
            OY += 0.1f;
            SceneCanvas.Children.Clear();
            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(System.Windows.Media.Colors.Red);
            colors.Add(System.Windows.Media.Colors.Blue);
            colors.Add(System.Windows.Media.Colors.Green);
            BitmapPalette myPalette = new BitmapPalette(colors);
            WriteableBitmap ToProcess = new WriteableBitmap(800, 600, 96, 96, PixelFormats.Bgr32, myPalette);
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
                        //drawLine(p[0], p[1], Colors.Red, SceneCanvas);
                        //drawLine(p[1], p[2], Colors.Red, SceneCanvas);
                        //drawLine(p[2], p[0], Colors.Red, SceneCanvas);
                        List<Vertex> v = new List<Vertex>();
                        v.Add(t.V1);
                        v.Add(t.V2);
                        v.Add(t.V3);
                        ScanlineTexture filler = new ScanlineTexture(v, Pixels, Texture, img.PixelWidth, img.PixelHeight);
                        filler.fillPolygon();
                    }

                }

            }
            ToProcess.WritePixels(new System.Windows.Int32Rect(0, 0, ToProcess.PixelWidth, ToProcess.PixelHeight), Pixels, ToProcess.BackBufferStride, 0);
            SceneImage.Source = ToProcess;
            foreach (Vertex v in s.Vertices)
            {
                v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(OX, OY, 0)) * v.P;
                v.Projected = TransformationMatrix.GetCameraMatrix(
                new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                v.Projected = TransformationMatrix.GetProjectionMatrix(180, 800, 600) * v.Projected;
                v.Projected = v.Projected / v.Projected.W;
                v.Projected.NormalizeToPixels();
            }
            


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

        private BitmapImage LoadImage(string FileName)
        {
            MemoryStream ms = new MemoryStream();
            FileStream stream = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            ms.SetLength(stream.Length);
            stream.Read(ms.GetBuffer(), 0, (int)stream.Length);
            ms.Flush();
            stream.Close();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.StreamSource = ms;
            src.EndInit();
            return src;
        }
    }
}
