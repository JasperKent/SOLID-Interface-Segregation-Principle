﻿using System;
using System.Drawing;
using System.Windows.Forms;
using TurtleLogic;

namespace TurtleDisplay
{
    public partial class TurtleForm : Form, ICanvas
    {
        private IShapeDrawer _turtle;

        public TurtleForm(IShapeDrawer turtle)
        {
            InitializeComponent();

            _turtle = turtle;
            _turtle.Canvas = this;
        }

        public void DrawSegment(double x0, double y0, double x1, double y1)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)delegate () { DrawSegment(x0, y0, x1, y1); });
            else
                using (var g = pnlMain.CreateGraphics())
                {
                    g.DrawLine(new Pen (Color.DarkBlue,2), ToLocal(x0, y0), ToLocal(x1, y1));
                }

            Point ToLocal(double x, double y)
            {
                int dimension = Math.Min(pnlMain.Width, pnlMain.Height);

                return new Point((int)(dimension * x) + (pnlMain.Width - dimension)/2, (int)(dimension * y) + (pnlMain.Height - dimension) / 2);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void lineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _turtle.DrawShape(new Line());
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var g = pnlMain.CreateGraphics())
            {
                g.Clear(pnlMain.BackColor);
            }
        }

        private async void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _turtle.DrawShape(new Square (0.3));
        }

        private async void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _turtle.DrawShape(new Circle(0.3));
        }

        private async void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _turtle.DrawShape(new Triangle(0.3));
        }
    }
}
