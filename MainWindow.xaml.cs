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
            s = new Sphere(150, 40, 40);
            
            foreach(Vertex v in s.Vertices)
            {
                v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(Math.PI, 0, 0)) * v.P;
                v.Projected = TransformationMatrix.GetCameraMatrix(
                new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 1, 0) }) * v.Projected;
                v.Projected = TransformationMatrix.GetProjectionMatrix(180, 600, 300) * v.Projected;
                v.Projected = v.Projected / v.Projected.W;
            }
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {

            SceneCanvas.Children.Clear();
            int k = 0;
                foreach (MeshTriangle t in s.Mesh)
                {
                    k++;
                    List<Point> p = new List<Point>();
                    p.Add(new Point((int)t.V1.Projected.X, (int)t.V1.Projected.Y));
                    p.Add(new Point((int)t.V2.Projected.X, (int)t.V2.Projected.Y));
                    p.Add(new Point((int)t.V3.Projected.X, (int)t.V3.Projected.Y));
                    drawLine(p[0], p[1], Colors.Black, SceneCanvas);
                    drawLine(p[1], p[2], Colors.Black, SceneCanvas);
                    drawLine(p[2], p[0], Colors.Black, SceneCanvas);
                   
                }
                foreach (Vertex v in s.Vertices)
                {
                    v.Projected = TransformationMatrix.GetRotationMatrix(new Vector3D(op,0,0  )) * v.P;
                    v.Projected = TransformationMatrix.GetCameraMatrix(
                    new Camera { Position = new Vector3D(0, 50, 300), Target = new Vector3D(0, 0, 0), UpDirection = new Vector3D(0, 11, 0) }) * v.Projected;
                    v.Projected = TransformationMatrix.GetProjectionMatrix(90, 600, 300) * v.Projected;
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
