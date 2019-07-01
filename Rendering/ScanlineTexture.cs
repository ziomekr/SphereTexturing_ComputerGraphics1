using SphereTexturing_ComputerGraphics1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ComputerGraphics_ClippingFilling
{
    internal class ScanlineTexture
    {
        private List<Edge> activeEdgeTable = new List<Edge>();
        private byte[] Pixels;
        private WrappedList polygonVertices;
        private List<int> sortedIndices;
        private byte[] Texture;
        private int textureHeight;
        private int textureWidth;
        public ScanlineTexture(List<Vertex> vertices, byte[] pixels, byte[] texture, int tWidth, int tHeight)
        {
            Pixels = pixels;
            Texture = texture;
            textureHeight = tHeight;
            textureWidth = tWidth;
            polygonVertices = new WrappedList(vertices);
            List<Vertex> temp = vertices.OrderBy(v => v.Projected.Y).ToList();
            this.sortedIndices = new List<int>();
            foreach (Vertex v in temp)
            {
                sortedIndices.Add(vertices.IndexOf(v));
            }
        }

        public void drawScanline(Point p1, Point p2)
        {
            Vertex v1 = activeEdgeTable[0].V1;
            Vertex v2 = activeEdgeTable[0].V2;

            Vertex v3 = activeEdgeTable[0].V1;
            Vertex v4 = activeEdgeTable[0].V2;

            for (int x = (int)p1.X; x < (int)p2.X; x++)
            {
                double t = Math.Abs((p1.Y - v1.Projected.Y) / (v2.Projected.Y - v1.Projected.Y));
                double u;
                if (v1.Projected.Z != v2.Projected.Z && t != 0)
                {
                    u = ((1 / (((v2.Projected.Z - v1.Projected.Z) * t) + v1.Projected.Z)) - 1 / v1.Projected.Z) / ((1 / v2.Projected.Z) - (1 / v1.Projected.Z));
                }
                else
                {
                    u = t;
                }
                MappingPoint m1 = u * (v2.TextureCoordinates - v1.TextureCoordinates) + v1.TextureCoordinates;

                t = Math.Abs((p1.Y - v3.Projected.Y) / (v4.Projected.Y - v3.Projected.Y));
             
                if (v3.Projected.Z != v4.Projected.Z && t != 0)
                {
                    u = ((1 / (((v4.Projected.Z - v3.Projected.Z) * t) + v3.Projected.Z)) - 1 / v3.Projected.Z) / ((1 / v4.Projected.Z) - (1 / v3.Projected.Z));
                }
                else
                {
                    u = t;
                }
                MappingPoint m2 = u * (v2.TextureCoordinates - v1.TextureCoordinates) + v1.TextureCoordinates;
                int textureIdx = (int)(((m1.X + m2.X)/2 * textureWidth + (m1.Y + m2.Y)/2 * textureHeight * textureWidth) * 4);
                Pixels[x * 4 + (int)p1.Y * 800 * 4 + 2] = Texture[textureIdx + 3];
                Pixels[x * 4 + (int)p1.Y * 800 * 4 + 1] = Texture[textureIdx + 2];
                Pixels[x * 4 + (int)p1.Y * 800 * 4] = Texture[textureIdx + 1];
            }
        }

        public void fillPolygon()
        {
            int k = 0;
            int i = sortedIndices[k];
            double y = polygonVertices[i].Projected.Y;
            double ymax = polygonVertices[sortedIndices[sortedIndices.Count - 1]].Projected.Y;
            while (y < ymax)
            {
                while (polygonVertices[i].Projected.Y == y)
                {
                    if (polygonVertices[i - 1].Projected.Y > polygonVertices[i].Projected.Y)
                    {
                        activeEdgeTable.Add(new Edge(polygonVertices[i], polygonVertices[i - 1]));
                    }
                    if (polygonVertices[i + 1].Projected.Y > polygonVertices[i].Projected.Y)
                    {
                        activeEdgeTable.Add(new Edge(polygonVertices[i], polygonVertices[i + 1]));
                    }
                    k += 1;
                    i = sortedIndices[k];
                }
                activeEdgeTable = activeEdgeTable.OrderBy(e => e.x).ToList();
                for (int eIdx = 0; eIdx < activeEdgeTable.Count; eIdx += 2)
                {
                    drawScanline(new Point(activeEdgeTable[eIdx].x, (int)y), new Point(activeEdgeTable[eIdx + 1].x, (int)y));
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
            private List<Vertex> array;

            public WrappedList(List<Vertex> _arr)
            {
                array = _arr;
            }

            public Vertex this[int position]
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