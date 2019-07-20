using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Cat
{
    public partial class Form1 : Form
    {
        Graphics gfx;
        public Form1()
        {
            InitializeComponent();
        }
        Random gen;
        private void Form1_Load(object sender, EventArgs e)
        {
            gen = new Random();
            gfx = CreateGraphics();
        }

        void Hair(int x, int y)
        {
            float numberOfPoints = 12.0f;
            float angle = 360 / numberOfPoints;
            int radius = 70;
            Point origin = new Point(x + radius / 2, y + radius / 2);
            gfx.DrawEllipse(Pens.Red, x, y, radius, radius);
            gfx.DrawLine(Pens.BlueViolet, x + radius, y + radius / 2, x, y + radius / 2);
            gfx.DrawLine(Pens.BlueViolet, x + radius / 2, y, x + radius / 2, y + radius);

            //start at 0 degrees and spin in a full circle
            for (int i = 0; i < 360; i++)
            {
                //to find the end point's X we have to multiply the radius by the Cos of the angle. To translate the lines to the center of the head, we need to add the origin's X
                double endX = (radius * Math.Cos(i)) + origin.X;
                //to find the end point's Y we have to multiply the radius by the Sin of the angle. To translate the lines to the center of the head, we need to add the origin's Y
                double endY = (radius * Math.Sin(i)) + origin.Y;

                gfx.DrawLine(Pens.Chocolate, origin.X, origin.Y, (float)endX, (float)endY);
            }
            //offset by 2 degrees to add highlights
            for (int i = 0; i < 360; i += 2)
            {
                double endX = (radius * Math.Cos(i)) + origin.X;
                double endY = (radius * Math.Sin(i)) + origin.Y;

                gfx.DrawLine(Pens.Peru, origin.X, origin.Y, (float)endX, (float)endY);
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            //*Background*//
            LinearGradientBrush skyBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, ClientSize.Height), Color.MidnightBlue, Color.OrangeRed);
            gfx.FillRectangle(skyBrush, 0, 0, ClientSize.Width, ClientSize.Height);
            Color StarColor = Color.White;
            for (int i = 0; i < 100; i++)
            {
                int y = gen.Next(1, ClientSize.Height + 1);

                StarColor = Color.FromArgb((int)(255 * ((ClientSize.Height - y) / (double)ClientSize.Height)), Color.White);

                gfx.DrawRectangle(new Pen(StarColor), new Rectangle(gen.Next(ClientSize.Width), y, 1, 1));
            }


            gfx.FillEllipse(Brushes.Goldenrod, ClientSize.Width / 3, ClientSize.Height / 3 + 30, 300, 300);

            LinearGradientBrush groundBrush = new LinearGradientBrush(new Rectangle(0, ClientSize.Height, ClientSize.Width, ClientSize.Height), Color.Yellow, Color.Sienna, LinearGradientMode.Vertical);
            gfx.FillRectangle(groundBrush, 0, ClientSize.Height - 50, ClientSize.Width, ClientSize.Height);


            //*Cat*//
            Hair(300, 275);

            //HEAD
            gfx.FillEllipse(Brushes.SandyBrown, 310, 285, 40, 40);
            gfx.FillEllipse(Brushes.SandyBrown, 320, 285, 40, 40);
            gfx.FillEllipse(Brushes.SaddleBrown, 315, 300, 40, 40);
            gfx.FillEllipse(Brushes.SandyBrown, 315, 295, 40, 40);
            GraphicsPath leftEar = new GraphicsPath();
            leftEar.AddArc(300, 280, 30, 30, 175, 100);
            gfx.FillPath(Brushes.SandyBrown, leftEar);
            leftEar.CloseFigure();
            leftEar.AddArc(300, 280, 10, 10, 175, 100);
            gfx.FillPath(Brushes.SaddleBrown, leftEar);
            //leftEye
            GraphicsPath leftEyePath = new GraphicsPath();
            leftEyePath.AddEllipse(321, 300, 7, 5);
            PathGradientBrush eyeBrush = new PathGradientBrush(leftEyePath);
            eyeBrush.CenterColor = Color.Gold;
            Color[] eyeColors = { Color.SaddleBrown};
            eyeBrush.SurroundColors = eyeColors;
            gfx.FillEllipse(Brushes.Black, 320, 300, 10, 6);
            gfx.FillPath(eyeBrush,leftEyePath);
            gfx.FillEllipse(Brushes.Black, 323, 301, 3, 3);

            //rightEye
            GraphicsPath  rightEyePath = new GraphicsPath();
            rightEyePath.AddEllipse(342, 300, 7, 5);
            eyeBrush = new PathGradientBrush(rightEyePath);
            eyeBrush.CenterColor = Color.Gold;
            eyeBrush.SurroundColors = eyeColors;
            gfx.FillEllipse(Brushes.Black, 340, 300, 10, 6);
            gfx.FillPath(eyeBrush, rightEyePath);
            gfx.FillEllipse(Brushes.Black, 344, 301, 3, 3);
            //329,316

            //nose + mouth
            gfx.DrawArc(Pens.SaddleBrown, 320, 310, 30, 20, 0, -180);
            gfx.FillPolygon(Brushes.Black, new Point[] { new Point(329, 316), new Point(341, 316), new Point(335, 320) });
            gfx.DrawLine(Pens.Black, new Point(335, 320), new Point(335, 330));
            gfx.DrawCurve(Pens.Black, new Point[] { new Point(335, 325),  new Point(340, 330), new Point(342, 330),new Point(345, 325) });
            gfx.DrawCurve(Pens.Black, new Point[] { new Point(335, 325), new Point(330, 330), new Point(328, 330), new Point(325, 325) });
            gfx.FillRectangle(Brushes.SaddleBrown, ClientSize.Width - 100, ClientSize.Height / 2, 100, ClientSize.Height - 100);

            //FILLING A PATH
            //Create graphics path object and add shape for path.
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddCurve(new Point[] { new Point(796, 224), new Point(518, 224), new Point(625, 312), new Point(599, 400), new Point(769, 400) });

            // Fill graphics path to screen.
            gfx.FillPath(Brushes.SaddleBrown, graphPath);

            LinearGradientBrush shadowBrush = new LinearGradientBrush(new Rectangle(599, 398, 200, 100), Color.Black, Color.Sienna, LinearGradientMode.ForwardDiagonal);
    GraphicsPath shadowPath = new GraphicsPath();
    shadowPath.AddCurve(new Point[] { new Point(800, 399), new Point(599, 398), new Point(671, 434), new Point(623, 449), new Point(800, 449) });
            gfx.FillPath(shadowBrush, shadowPath);

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
{
    this.Text = $"X:{e.X}, Y:{e.Y}";
}
    }
}
