using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform.Windows;


namespace dvizhpoints
{
    public partial class Form1 : Form
    {
        double x = 0, y = 0;

        double ax = Math.Cos(2.0 * Math.PI / 16.0);
        double ay = Math.Sin(2.0 * Math.PI / 16.0);

        double bx = 0.0;
        double by = 0.0;

        double[,] array = new double[4, 2] { { -1.0, -1.0 }, { 1.0, -1.0 }, { 1, 0 }, { -1.0, 1.0 } };

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE);//для отображения кадра 
                                                                        //будет использоваться двойная буферизация, убирает мерцание монитора
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            Gl.glMatrixMode(Gl.GL_PROJECTION);//запускаем матрицу ее проекцию
            Gl.glLoadIdentity();//задает единичную матрицу проекции
            Glu.gluOrtho2D(-4.0, 4.0, -4.0, 4.0);//строим проекцию
            Gl.glMatrixMode(Gl.GL_MODELVIEW);//перенос на вектор, поворот
            Gl.glLoadIdentity();
            //clear();
            //DrawAxes();
            //DrawVector(2.5, 2.5);

        }

        public void clear()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);

        }
        public void DrawAxes()
        {
            
            Gl.glLineWidth(1.0f);
            //DrawVector(ax, ay);

            Gl.glBegin(Gl.GL_LINES);
            //Gl.glColor3f(1.0f, 1.0f, 1.0f);
            Gl.glVertex2d(-4.0, 0.0);
            Gl.glVertex2d(4.0, 0.0);
            Gl.glVertex2d(0, 4.0);
            Gl.glVertex2d(0, -4.0);
            Gl.glEnd();
        }
        public void DrawVector(double x, double y)
        {
            double alfa = 0;//угол нашего вектора
            alfa = Math.Acos(x/Math.Sqrt(x * x + y * y));
            if (y < 0)
                alfa = -alfa;
            double p1x = Math.Cos(alfa + Math.PI / 18);
            double p1y = Math.Sin(alfa + Math.PI / 18);
            double p2x = Math.Cos(alfa - Math.PI / 18);
            double p2y = Math.Sin(alfa - Math.PI / 18);

            Gl.glLineWidth(2.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(0, 0);
            Gl.glVertex2d(x, y);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(x - p1x * 0.2, y - p1y * 0.2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(x - p2x * 0.2, y - p2y * 0.2);
            Gl.glEnd();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            Draw();
        }

        public void Draw()
        {
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            Gl.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            
            ax = Math.Cos(2.0 * Math.PI / 8.0);
            ay = Math.Sin(2.0 * Math.PI / 8.0);
            
            Gl.glColor3f(1.0f, 1.0f, 1.0f);
            DrawAxes();

            ////vector a i pryamaiya cherez a
            //Gl.glColor3f(0.03f, 0.7f, 0.7f);
            //DrawVector(ax, ay);
            //DrawPryamaya(ax, ay);


            ////vector k kursoru
            //Gl.glColor3f(0.4f, 0.0f, 1.0f);
            //DrawVector(x, y);

            ////proektsiya
            //DrawProjection(ax, ay);

            //for (int i = 0; i < array.GetLength(0); i++)
            //{
            //    Gl.glColor3f(0.03f, 0.7f, 0.7f);
            //    DrawVector(array[i, 0], array[i, 1]);
            //    DrawProjection(array[i, 0], array[i, 1]);
            //}

            //Gl.glColor3f(0f, 1f, 0f);
            //Gl.glBegin(Gl.GL_LINE_LOOP);
            //for (int i = 0; i < array.GetLength(0); i++)
            //{
            //    Gl.glVertex2d(array[i, 0], array[i, 1]);
            //}
            //Gl.glEnd();

            //pLine(1, 1, 0, 1);
            Gl.glColor3f(0.4f, 0.0f, 1.0f);
            edVect(trackBar1.Value);

            Gl.glFlush();
            AnT.Invalidate();
        }


        public void DrawPryamaya(double A, double B)
        {
            double ex = A / Math.Sqrt(A * A + B * B);
            double ey = B / Math.Sqrt(A * A + B * B);

            Gl.glLineWidth(1.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(6.0 * ex, 6.0 * ey);
            Gl.glVertex2d(-6.0 * ex, -6.0 * ey);
            Gl.glEnd();
        }

        public void DrawProjection(double A, double B)
        {
            //proektsiya
            double ex = A / Math.Sqrt(A * A + B * B);
            double ey = B / Math.Sqrt(A * A + B * B);

            bx = ex * (ex * x + ey * y);
            by = ey * (ex * x + ey * y);

            if (ex * x + ey * y <= 0.0) Gl.glColor3f(1.0f, 0.0f, 0.0f);
            else Gl.glColor3f(1.0f, 1.0f, 0.0f);

            Gl.glPointSize(4.0f);
            Gl.glBegin(Gl.GL_POINTS);
            Gl.glVertex2d(bx, by);
            Gl.glEnd();

            Gl.glLineWidth(1.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(bx, by);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(bx, by);
            Gl.glVertex2d(bx - (x - bx), by - (y - by));
            Gl.glEnd();
        }

        public void pLine(double v1, double v2, double u1, double u2) {

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(v1, v2);
            Gl.glVertex2d(u1, u2);
            Gl.glEnd();
        }

        public void edVect(double alpha) {

            double alfa = 0;//угол нашего вектора
            x = Math.Cos(alpha);
            y = Math.Sin(alpha);
            alfa = Math.Acos(x / Math.Sqrt(x * x + y * y));
            if (y < 0)
                alfa = -alfa;
            double p1x = Math.Cos(alfa + Math.PI / 18);
            double p1y = Math.Sin(alfa + Math.PI / 18);
            double p2x = Math.Cos(alfa - Math.PI / 18);
            double p2y = Math.Sin(alfa - Math.PI / 18);

            Gl.glLineWidth(2.0f);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(0, 0);
            Gl.glVertex2d(x, y);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(x - p1x * 0.2, y - p1y * 0.2);
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex2d(x, y);
            Gl.glVertex2d(x - p2x * 0.2, y - p2y * 0.2);
            Gl.glEnd();

        }



        private void AnT_MouseMove_1(object sender, MouseEventArgs e)
        {
            //x = -4.0 + ((double)e.X / (double)AnT.Width) * 8.0;
            //y = -4.0 + (((double)AnT.Height - (double)e.Y) / (double)AnT.Height) * 8.0;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void AnT_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
