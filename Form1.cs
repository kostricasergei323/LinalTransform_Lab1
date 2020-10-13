using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LinalTransform_Lab1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bitmap;
        Figure figure;
        Point gridCenter { get; set; } = new Point(512, 384);
        int polygonSideSize { get; set; }
        int diagonalRadius { get; set; }
        int axesRadius { get; set; }
        Matrix TempTransform { get; set; } = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Лабораторна робота №1";
            button1.Text = "Перетворити";
            button2.Text = "Зсув";
            button3.Text = "Поворот";
            button4.Text = "Скинути до початкових";
            button5.Text = "Застосувати";
            groupBox1.Text = "Величини:";
            label1.Text = "Сторона шестикутника:";
            label3.Text = "Радіус центрального кола та діагональних дуг:";
            label5.Text = "Радіус осьових дуг:";
            label7.Text = "dX=";
            label8.Text = "dY=";
            checkBox1.Text = "Автоматичне перетворення";
            checkBox2.Text = "Сітка";
            checkBox3.Text = "Осі координат";
            label2.Text = trackBar1.Value.ToString();
            label4.Text = trackBar2.Value.ToString();
            label6.Text = trackBar3.Value.ToString();
            this.MaximumSize = this.MinimumSize = this.Size;
            polygonSideSize = trackBar1.Value * 16;
            diagonalRadius = trackBar2.Value * 32;
            axesRadius = trackBar3.Value * 32;
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var affinValues = g.Transform.Elements;
            numericUpDown4.Value = (int)affinValues[0];
            numericUpDown5.Value = (int)affinValues[1];
            numericUpDown6.Value = (int)affinValues[2];
            numericUpDown7.Value = (int)affinValues[3];
            numericUpDown8.Value = (int)affinValues[4];
            numericUpDown9.Value = (int)affinValues[5];
            figure = new Figure(g, new Pen(Color.Black, 2f), gridCenter);
            button1.PerformClick();
        }

        private void DrawGrid()
        {
            Pen gridPen = new Pen(Color.LightGray, 0.05f);
            int verticalCount = pictureBox1.Width / 16;
            int horizontalCount = pictureBox1.Height / 16;

            // Vertical Lines
            for (int i = 0; i < verticalCount; i++)
            {
                g.DrawLine(gridPen,
                    new Point(i * 16, 0),
                    new Point(i * 16, pictureBox1.Height));
            }

            // Horizontal Lines
            for (int i = 0; i < horizontalCount; i++)
            {
                g.DrawLine(gridPen,
                    new Point(0, i * 16),
                    new Point(pictureBox1.Width, i * 16));
            }
        }

        private void DrawAxes()
        {
            //Длина стрелки
            const int ARR_LEN = 16;
            Pen axesPen = new Pen(Color.Gray, 2f);
            var f = new Font(Font.FontFamily, 15);
            f = new Font(f, FontStyle.Bold);
            Point startX = new Point(128, 384);
            Point endX = new Point(896, 384);
            Point startY = new Point(512, 720);
            Point endY = new Point(512, 48);

            //Ось X
            g.DrawLine(axesPen, startX, endX);
            //Стрелка
            g.DrawLines(axesPen, GetArrow(startX.X, startX.Y, endX.X, endX.Y, ARR_LEN));
            g.DrawString("X", f, Brushes.Black, new PointF(endX.X, endX.Y));

            //Ось Y
            g.DrawLine(axesPen, startY, endY);
            //Стрелка
            g.DrawLines(axesPen, GetArrow(startY.X, startY.Y, endY.X, endY.Y, ARR_LEN));
            g.DrawString("Y", f, Brushes.Black, new PointF(endY.X + 6, endY.Y));
        }

        //Вычисление стрелки оси
        private static PointF[] GetArrow(float x1, float y1, float x2, float y2, float len = 10, float width = 4)
        {
            PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - x1, y2 - y1);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * len;
            n.Y = y2 - v1.Y * len;
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar2.Maximum = trackBar1.Value > 4 ? trackBar1.Value - 3 : 1;
            trackBar3.Maximum = trackBar1.Value;
            if (trackBar2.Value > trackBar1.Value)
            {
                trackBar2.Value = trackBar2.Maximum;
            }

            if (trackBar3.Value > trackBar1.Value)
            {
                trackBar3.Value = trackBar3.Maximum;
            }

            label2.Text = trackBar1.Value.ToString();
            label4.Text = trackBar2.Value.ToString();
            label6.Text = trackBar3.Value.ToString();
            polygonSideSize = trackBar1.Value * 16;
            diagonalRadius = trackBar2.Value * 32;
            axesRadius = trackBar3.Value * 32;
            if (checkBox1.Checked)
            {
                button1.PerformClick();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label4.Text = trackBar2.Value.ToString();
            diagonalRadius = trackBar2.Value * 32;

            if (checkBox1.Checked)
            {
                button1.PerformClick();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label6.Text = trackBar3.Value.ToString();
            axesRadius = trackBar3.Value * 32;

            if (checkBox1.Checked)
            {
                button1.PerformClick();
            }
        }

        private void DrawFigure()
        {
            figure.DrawFigure(polygonSideSize, diagonalRadius, axesRadius);
            pictureBox1.Image = bitmap;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var tTransform = g.Transform.Clone();
            if (TempTransform == null)
            {
                TempTransform = g.Transform.Clone();
            }
            g.Clear(Color.White);
            g.ResetTransform();
            g.Transform = TempTransform;
            if (checkBox2.Checked)
            {
                DrawGrid();
            }
            if (checkBox3.Checked)
            {
                DrawAxes();
            }
            g.FillRectangle(Brushes.Red, new Rectangle(510, 382, 4, 4));
            g.Transform = tTransform;
            DrawFigure();
            if (gridCenter.X != 512 || gridCenter.Y != 384)
            {
                g.FillRectangle(Brushes.Blue, new Rectangle(gridCenter.X - 2, gridCenter.Y - 2, 4, 4));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tTransform = g.Transform.Clone();
            if (TempTransform == null)
            {
                TempTransform = g.Transform.Clone();
            }
            g.Clear(Color.White);
            g.ResetTransform();
            g.Transform = TempTransform;
            if (checkBox2.Checked)
            {
                DrawGrid();
            }
            if (checkBox3.Checked)
            {
                DrawAxes();
            }
            g.FillRectangle(Brushes.Red, new Rectangle(510, 382, 4, 4));
            g.Transform = tTransform;
            gridCenter = Figure.gridCenter = new Point(gridCenter.X + (int)numericUpDown1.Value * 16, gridCenter.Y - (int)numericUpDown2.Value * 16);
            DrawFigure();
            if (gridCenter.X != 512 || gridCenter.Y != 384)
            {
                g.FillRectangle(Brushes.Blue, new Rectangle(gridCenter.X - 2, gridCenter.Y - 2, 4, 4));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var tTransform = g.Transform.Clone();
            if (TempTransform == null)
            {
                TempTransform = g.Transform.Clone();
            }
            g.Clear(Color.White);
            g.ResetTransform();
            g.Transform = TempTransform;
            if (checkBox2.Checked)
            {
                DrawGrid();
            }
            if (checkBox3.Checked)
            {
                DrawAxes();
            }
            g.FillRectangle(Brushes.Red, new Rectangle(510, 382, 4, 4));
            g.Transform = tTransform;
            g.RotateTransform((int)numericUpDown3.Value);
            DrawFigure();
            if (gridCenter.X != 512 || gridCenter.Y != 384)
            {
                g.FillRectangle(Brushes.Blue, new Rectangle(gridCenter.X - 2, gridCenter.Y - 2, 4, 4));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = checkBox3.Checked = true;
            trackBar1.Value = 12;
            label2.Text = trackBar1.Value.ToString();
            trackBar2.Maximum = 9;
            trackBar2.Value = 5;
            label4.Text = trackBar2.Value.ToString();
            trackBar3.Maximum = 12;
            trackBar3.Value = 3;
            label6.Text = trackBar3.Value.ToString();
            gridCenter = Figure.gridCenter = new Point(512, 384);
            polygonSideSize = trackBar1.Value * 16;
            diagonalRadius = trackBar2.Value * 32;
            axesRadius = trackBar3.Value * 32;
            numericUpDown1.Value = numericUpDown2.Value = numericUpDown3.Value = numericUpDown5.Value = numericUpDown6.Value = numericUpDown8.Value = numericUpDown9.Value = 0;
            numericUpDown4.Value = numericUpDown7.Value = 1;
            TempTransform = null;
            g.ResetTransform();
            button1.PerformClick();
            g.FillRectangle(Brushes.Red, new Rectangle(510, 382, 4, 4));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            TempTransform = null;
            g.ResetTransform();
            g.Transform = new Matrix(
                (float)numericUpDown4.Value, (float)numericUpDown5.Value,
                (float)numericUpDown6.Value, (float)numericUpDown7.Value,
                (float)numericUpDown8.Value, (float)numericUpDown9.Value);
            if (checkBox2.Checked)
            {
                DrawGrid();
            }
            if (checkBox3.Checked)
            {
                DrawAxes();
            }
            g.FillRectangle(Brushes.Red, new Rectangle(510, 382, 4, 4));
            DrawFigure();
            if (gridCenter.X != 512 || gridCenter.Y != 384)
            {
                g.FillRectangle(Brushes.Blue, new Rectangle(gridCenter.X - 2, gridCenter.Y - 2, 4, 4));
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            button1.PerformClick();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}