using System;
using System.Drawing;

namespace LinalTransform_Lab1
{
    public class Figure
    {
        public Graphics g { get; set; }
        public Pen figurePen { get; set; }
        public static Point gridCenter { get; set; }

        public Figure(Graphics graphics, Pen pen, Point center)
        {
            g = graphics;
            figurePen = pen;
            gridCenter = center;
        }

        public void DrawFigure(int polygonSize, int diagonalCircleSize, int horizontalCircleSize)
        {
            DrawCentralPolygon(polygonSize);
            DrawCenterAndDiagonalArcs(diagonalCircleSize, polygonSize);
            DrawHorizontalArcs(horizontalCircleSize, polygonSize);
        }
        private void DrawCentralPolygon(int polygonSize)
        {
            PointF[] points =
            {
                new PointF(gridCenter.X, gridCenter.Y + polygonSize),
                new PointF(gridCenter.X - (float)Math.Sqrt(3) * polygonSize / 2, gridCenter.Y + polygonSize / 2),
                new PointF(gridCenter.X - (float)Math.Sqrt(3) * polygonSize / 2, gridCenter.Y - polygonSize / 2),
                new PointF(gridCenter.X, gridCenter.Y - polygonSize),
                new PointF(gridCenter.X + (float)Math.Sqrt(3) * polygonSize / 2, gridCenter.Y - polygonSize / 2),
                new PointF(gridCenter.X + (float)Math.Sqrt(3) * polygonSize / 2, gridCenter.Y + polygonSize / 2),
            };
            g.DrawPolygon(figurePen, points);
        }

        private void DrawCenterAndDiagonalArcs(int circleSize, int polygonSize)
        {
            // Central circle
            g.DrawEllipse(figurePen, new Rectangle(gridCenter.X-circleSize / 2, gridCenter.Y - circleSize / 2, circleSize, circleSize));

            // Left diagonal arc
            g.DrawArc(figurePen, new RectangleF(gridCenter.X - (float)Math.Sqrt(3) * polygonSize / 2 - circleSize / 2, 
                gridCenter.Y + polygonSize / 2 - circleSize / 2,
                circleSize, circleSize),30,240);

            // Right diagonal arc
            g.DrawArc(figurePen, new RectangleF(gridCenter.X + (float)Math.Sqrt(3) * polygonSize / 2 - circleSize / 2,
                gridCenter.Y + polygonSize / 2 - circleSize / 2,
                circleSize, circleSize),-90,240);
        }

        private void DrawHorizontalArcs(int circleSize, int polygonSize)
        {
            // Up horizontal arc
            g.DrawArc(figurePen, new RectangleF(gridCenter.X - circleSize / 2,
                gridCenter.Y - polygonSize - circleSize / 2,
                circleSize, circleSize), 150, 240);

            // Down horizontal arc
            g.DrawArc(figurePen, new RectangleF(gridCenter.X - circleSize / 2,
                gridCenter.Y + polygonSize - circleSize / 2,
                circleSize, circleSize), -30, 240);
        }

    }
}
