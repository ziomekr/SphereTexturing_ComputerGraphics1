using SphereTexturing_ComputerGraphics1;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComputerGraphics_ClippingFilling
{
    internal class ScanlineFillVertexSort
    {
        private List<Edge> activeEdgeTable = new List<Edge>();
        private byte[] Pixels;
        private WrappedList polygonVertices;
        private List<int> sortedIndices;

        public ScanlineFillVertexSort(List<Point3D> points, byte[] pixels)
        {
            Pixels = pixels;
            polygonVertices = new WrappedList(points);
            List<Point3D> temp = points.OrderBy(p => p.Y).ToList();
            this.sortedIndices = new List<int>();
            foreach (Point3D p in temp)
            {
                sortedIndices.Add(points.IndexOf(p));
            }
        }

        public void drawLine(Point p1, Point p2)
        {
            for (int x = (int)p1.X; x < (int)p2.X; x++)
            {
                Pixels[x * 4 + (int)p1.Y * 600 * 4 + 2] = 255;
                Pixels[x * 4 + (int)p1.Y * 600 * 4 + 1] = 255;
                Pixels[x * 4 + (int)p1.Y * 600 * 4] = 255;
                Pixels[x * 4 + (int)p1.Y * 600 * 4 + 3] = 255;
            }
        }

        public void fillPolygon()
        {
            int k = 0;
            int i = sortedIndices[k];
            double y = polygonVertices[i].Y;
            double ymax = polygonVertices[sortedIndices[sortedIndices.Count - 1]].Y;
            while (y < ymax)
            {
                while (polygonVertices[i].Y == y)
                {
                    if (polygonVertices[i - 1].Y > polygonVertices[i].Y)
                    {
                        activeEdgeTable.Add(new Edge(polygonVertices[i], polygonVertices[i - 1]));
                    }
                    if (polygonVertices[i + 1].Y > polygonVertices[i].Y)
                    {
                        activeEdgeTable.Add(new Edge(polygonVertices[i], polygonVertices[i + 1]));
                    }
                    k += 1;
                    i = sortedIndices[k];
                }
                activeEdgeTable = activeEdgeTable.OrderBy(e => e.x).ToList();
                for (int eIdx = 0; eIdx < activeEdgeTable.Count; eIdx += 2)
                {
                    drawLine(new Point(activeEdgeTable[eIdx].x, (int)y), new Point(activeEdgeTable[eIdx + 1].x, (int)y));
                }
                y += 1;
                activeEdgeTable = activeEdgeTable.Where(e => (e.ymax != y)).ToList();
                foreach (Edge e in activeEdgeTable)
                {
                    e.x += e.invM;
                }
            }
        }

        private class WrappedList
        {
            private List<Point3D> array;

            public WrappedList(List<Point3D> _arr)
            {
                array = _arr;
            }

            public Point3D this[int position]
            {
                get
                {
                    if (position > -1 && position < array.Count)
                        return array[position];
                    else if (position < 0)
                        return array[array.Count + position];
                    else
                        return array[position - array.Count];
                }
            }
        }
    }
}