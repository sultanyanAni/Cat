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

        void Hair(int x, int y, int start = 0, int sweep = 360)
        {
            int radius = 60;
            Point origin = new Point(x + radius / 2, y + radius / 2);


            //start at 0 degrees and spin in a full circle
            for (int i = start; i < sweep; i++)
            {
                //to find the end point's X we have to multiply the radius by the Cos of the angle. To translate the lines to the center of the head, we need to add the origin's X
                double endX = (radius * Math.Cos(i)) + origin.X;
                //to find the end point's Y we have to multiply the radius by the Sin of the angle. To translate the lines to the center of the head, we need to add the origin's Y
                double endY = (radius * Math.Sin(i)) + origin.Y;

                gfx.DrawLine(Pens.Chocolate, origin.X, origin.Y, (float)endX, (float)endY);
            }
            //offset by 2 degrees to add highlights
            for (int i = start; i < sweep; i += 2)
            {
                double endX = (radius * Math.Cos(i)) + origin.X;
                double endY = (radius * Math.Sin(i)) + origin.Y;

                gfx.DrawLine(Pens.Peru, origin.X, origin.Y, (float)endX, (float)endY);
            }

        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            #region Background
            LinearGradientBrush skyBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, ClientSize.Height), Color.MidnightBlue, Color.OrangeRed);
            gfx.FillRectangle(skyBrush, 0, 0, ClientSize.Width, ClientSize.Height);

            //drawing stars (lost opacity as they get closer to horizon)
            Color StarColor = Color.White;
            for (int i = 0; i < 100; i++)
            {
                int y = gen.Next(1, ClientSize.Height + 1);

                StarColor = Color.FromArgb((int)(255 * ((ClientSize.Height - y) / (double)ClientSize.Height)), Color.White);

                gfx.DrawRectangle(new Pen(StarColor), new Rectangle(gen.Next(ClientSize.Width), y, 1, 1));
            }

            //sun
            gfx.FillEllipse(Brushes.Goldenrod, ClientSize.Width / 3, ClientSize.Height / 3 + 30, 300, 300);

            //ground
            LinearGradientBrush groundBrush = new LinearGradientBrush(new Rectangle(0, ClientSize.Height, ClientSize.Width, ClientSize.Height), Color.Yellow, Color.Sienna, LinearGradientMode.Vertical);
            gfx.FillRectangle(groundBrush, 0, ClientSize.Height - 50, ClientSize.Width, ClientSize.Height);

            //drawing Cliff + Shadow
            gfx.FillRectangle(Brushes.SaddleBrown, ClientSize.Width - 100, ClientSize.Height / 2, 100, ClientSize.Height - 100);
            GraphicsPath cliffPath = new GraphicsPath();
            cliffPath.AddCurve(new Point[] { new Point(796, 224), new Point(518, 224), new Point(625, 312), new Point(599, 400), new Point(769, 400) });
            gfx.FillPath(Brushes.SaddleBrown, cliffPath);
            LinearGradientBrush shadowBrush = new LinearGradientBrush(new Rectangle(599, 398, 200, 100), Color.Black, Color.Sienna, LinearGradientMode.ForwardDiagonal);
            GraphicsPath shadowPath = new GraphicsPath();
            shadowPath.AddCurve(new Point[] { new Point(800, 399), new Point(599, 398), new Point(671, 434), new Point(623, 449), new Point(800, 449) });
            gfx.FillPath(shadowBrush, shadowPath);
            #endregion

            //*Cat*//

            #region Body
            
            Point[] backPoints =
            {

               new Point(365,343),
                new Point(373,353),
                new Point(375,363),
                new Point(376,370),
                new Point(377,373),
                new Point(380,383),
                new Point(383,393),
                new Point(390,403),
                new Point(320,403),
                new Point(317,343)
            };
            GraphicsPath back = new GraphicsPath();
            back.AddCurve(backPoints);
            gfx.FillPath(Brushes.SandyBrown, back);
          
            #region Head

            //Hair
            Hair(305, 275);
            //top of head   
            gfx.FillEllipse(Brushes.SandyBrown, 310, 285, 40, 40);
            gfx.FillEllipse(Brushes.SandyBrown, 320, 285, 40, 40);
            //nose + mouth
            gfx.FillEllipse(Brushes.SandyBrown, 315, 295, 40, 40);
            gfx.DrawArc(Pens.SaddleBrown, 320, 310, 30, 20, 0, -180);
            gfx.FillPolygon(Brushes.Black, new Point[] { new Point(329, 316), new Point(341, 316), new Point(335, 320) });
            gfx.DrawLine(Pens.Black, new Point(335, 320), new Point(335, 330));
            gfx.DrawCurve(Pens.Black, new Point[] { new Point(335, 325), new Point(340, 330), new Point(342, 330), new Point(345, 325) });
            gfx.DrawCurve(Pens.Black, new Point[] { new Point(335, 325), new Point(330, 330), new Point(328, 330), new Point(325, 325) });

            //leftEar
            int leftX = 305;
            int leftY = 280;
            GraphicsPath leftEar = new GraphicsPath();
            leftEar.AddArc(leftX, leftY, 30, 30, 170, 100);
            gfx.FillPath(Brushes.SandyBrown, leftEar);
            GraphicsPath innerLeftEar = new GraphicsPath();
            innerLeftEar.AddArc(leftX + 3, leftY + 3, 20, 20, 170, 100);
            gfx.FillPath(Brushes.SaddleBrown, innerLeftEar);

            //rightEar
            int rightX = 335;
            int rightY = 280;
            GraphicsPath rightEar = new GraphicsPath();
            rightEar.AddArc(rightX, rightY, 30, 30, 270, 100);
            gfx.FillPath(Brushes.SandyBrown, rightEar);
            GraphicsPath innerRightEar = new GraphicsPath();
            innerRightEar.AddArc(rightX + 7, rightY + 3, 20, 20, 270, 100);
            gfx.FillPath(Brushes.SaddleBrown, innerRightEar);

            //leftEye
            GraphicsPath leftEyePath = new GraphicsPath();
            leftEyePath.AddEllipse(321, 300, 7, 5);
            PathGradientBrush eyeBrush = new PathGradientBrush(leftEyePath);
            eyeBrush.CenterColor = Color.Gold;
            Color[] eyeColors = { Color.SaddleBrown };
            eyeBrush.SurroundColors = eyeColors;
            gfx.FillEllipse(Brushes.Black, 320, 300, 10, 6);
            gfx.FillPath(eyeBrush, leftEyePath);
            gfx.FillEllipse(Brushes.Black, 323, 301, 3, 3);

            //rightEye
            GraphicsPath rightEyePath = new GraphicsPath();
            rightEyePath.AddEllipse(342, 300, 7, 5);
            eyeBrush = new PathGradientBrush(rightEyePath);
            eyeBrush.CenterColor = Color.Gold;
            eyeBrush.SurroundColors = eyeColors;
            gfx.FillEllipse(Brushes.Black, 340, 300, 10, 6);
            gfx.FillPath(eyeBrush, rightEyePath);
            gfx.FillEllipse(Brushes.Black, 344, 301, 3, 3);

            #endregion

            //front right leg
            gfx.DrawLine(Pens.SaddleBrown, 315, 360, 315, 403);
            gfx.DrawLine(Pens.SaddleBrown, 325, 360, 325, 403);
            GraphicsPath rightFoot = new GraphicsPath();
            rightFoot.AddArc(305, 400, 40, 10, 270, -180);
            gfx.FillPath(Brushes.SandyBrown, rightFoot);

            //front left leg
            gfx.DrawLine(Pens.SaddleBrown, 330, 360, 330, 403);
            gfx.DrawLine(Pens.SaddleBrown, 340, 360, 340, 403);
            GraphicsPath leftFoot = new GraphicsPath();
            leftFoot.AddArc(325, 400, 40, 13, 200, -180);
            gfx.FillPath(Brushes.SandyBrown, leftFoot);
            gfx.DrawPath(Pens.SaddleBrown, leftFoot);

            //backleg
            GraphicsPath leg = new GraphicsPath();
            leg.AddArc(345, 375, 50, 70, 180, 175);
            gfx.FillPath(Brushes.SandyBrown, leg);
            leg = new GraphicsPath();
            leg.AddArc(345, 375, 50, 60, 280, -100);
            gfx.DrawPath(Pens.SaddleBrown, leg);
            GraphicsPath backFoot = new GraphicsPath();
            backFoot.AddArc(335, 405, 122, 10, 270, -180);
            gfx.FillPath(Brushes.SandyBrown, backFoot);
            backFoot = new GraphicsPath();
            backFoot.AddArc(335, 405, 80, 10, 270, -180);
            gfx.DrawPath(Pens.SaddleBrown, backFoot);

            //Tail
            Point[] tailPoints =
            {
                new Point(390, 405),
                new Point(400, 405),
                new Point(410, 410),
                new Point(412, 415),
                new Point(410, 420),
                new Point(405, 425),
                new Point(400,430),
                new Point(395,432),
                new Point(390,435),
                new Point(385,435)
        

            };
            gfx.DrawCurve(new Pen(Brushes.SandyBrown, 10), tailPoints);

                //tip of tail
            GraphicsPath tail = new GraphicsPath();
            tail.AddArc(380,431,10,10,275,-180);
            gfx.FillPath(Brushes.SandyBrown, tail);
         
            gfx.FillEllipse(Brushes.Chocolate, 375, 430, 15, 15);
            gfx.FillPolygon(Brushes.Chocolate, new Point[] { new Point(380, 430), new Point(360, 440), new Point(380, 445) });
            #endregion



        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = $"X:{e.X}, Y:{e.Y}";
        }
    }
}
