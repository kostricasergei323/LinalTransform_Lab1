using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinalTransform_Lab1
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bitmap;
        Figure figure;
        int polygonSideSize { get; set; }
        int diagonalRadius { get; set; }
        int axesRadius { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Лабораторна робота №1";
            button1.Text = "Перетворити";
            groupBox1.Text = "Величини:";
            label1.Text = "Сторона шестикутника:";
            label3.Text = "Радіус центрального кола та діагональних дуг:";
            label5.Text = "Радіус осьових дуг:";
            checkBox1.Text = "Автоматичне перетворення";
            label2.Text = label4.Text = label6.Text = trackBar1.Value.ToString();
            polygonSideSize = trackBar1.Value * 16;
            diagonalRadius = trackBar2.Value * 32;
            axesRadius = trackBar3.Value * 32;
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            figure = new Figure(g, new Pen(Color.Black, 2f), new Point(512, 384));
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

        }
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar2.Maximum = trackBar1.Value > 4 ? trackBar1.Value - 3 : 1;
            trackBar3.Maximum = trackBar1.Value;
            if(trackBar2.Value > trackBar1.Value)
            {
                trackBar2.Value = trackBar2.Maximum;
            }

            if(trackBar3.Value > trackBar1.Value)
            {
                trackBar3.Value = trackBar3.Maximum;
            }

            label2.Text = trackBar1.Value.ToString();
            label4.Text = trackBar2.Value.ToString();
            label6.Text = trackBar3.Value.ToString();
            polygonSideSize = trackBar1.Value * 16;
            diagonalRadius = trackBar2.Value * 32;
            axesRadius = trackBar3.Value * 32;
            if(checkBox1.Checked)
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
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            g.ResetTransform();
            DrawGrid();
            DrawAxes();
            figure.DrawFigure(polygonSideSize, diagonalRadius, axesRadius);
            pictureBox1.Image = bitmap;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.PerformClick();
        }
    }
}