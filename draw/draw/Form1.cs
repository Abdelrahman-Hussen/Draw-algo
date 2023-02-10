using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace draw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label5.Text = "Width : "+ pictureBox1.Width + " Height : " + pictureBox1.Height;     
        }

        #region Global variables

        int Shape;
        int EllipseScale = 0;
        int CheckMirror2D = 0;
        int ShearingAxis = 0;
        double xx, yy;

        struct Line
        {
            public int XStart;
            public int YStart;
            public int XEnd;
            public int YEnd;

            public Line(int x)
            {
                XStart = 0;
                YStart = 0;
                XEnd = 0;
                YEnd = 0;
            }
        }Line line;
        
        struct Circle
        {
            public int XCenter;
            public int YCenter;
            public int Reduis;

            public Circle(int x)
            {
                XCenter = 0;
                YCenter = 0;
                Reduis = 0;
            }
        }Circle circle;
        
        struct Ellipse
        {
            public int XCenter;
            public int YCenter;
            public int XReduis;
            public int YReduis;

            public Ellipse(int x)
            {
                XCenter = 0;
                YCenter = 0;
                XReduis = 0;
                YReduis = 0;
            }
        }Ellipse ellipse;

        
        Graphics GFXPKG;

        #endregion

        //Clear Botton
        private void button6_Click_1(object sender, EventArgs e)
        {
            GFXPKG.Clear(Color.White);
        }

        //********************** Shapes **********************\\

        #region DDA

        // DDA Botton
        private void button1_Click(object sender, EventArgs e)
        {
            label5.Text = "Width : " + pictureBox1.Width + " Height : " + pictureBox1.Height;

            int x0 = Convert.ToInt32(textBox1.Text);
            int y0 = Convert.ToInt32(textBox2.Text);
            int xend = Convert.ToInt32(textBox3.Text);
            int yend = Convert.ToInt32(textBox4.Text);

            Shape = 1;
            
            line.XStart = x0;
            line.YStart = y0;
            line.XEnd = xend;
            line.YEnd = yend;
            textBox8.Text = "";

            DrawDDAline(x0, xend, y0, yend);
        }
        
        // DDA Algo
        public void DrawDDAline(int x0, int xend, int y0, int yend)
        {
            GFXPKG = pictureBox1.CreateGraphics();
            GFXPKG.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            xx = x0;
            yy = y0;
            
            int dx = xend - x0,
                dy = yend - y0,
                steps, k;
            double xIncrement, yIncrement,
                x = x0,
                y = y0;

            if ((double)Math.Abs(dx) > (double)Math.Abs(dy))
                steps = Math.Abs(dx);
            else
                steps = Math.Abs(dy);
            xIncrement = (double)(dx) / (double)(steps);
            yIncrement = (double)(dy) / (double)(steps);

            setPixel(Math.Round(x), Math.Round(y));
            for (k = 0; k < steps; k++)
            {
                x += xIncrement;
                y += yIncrement;
                
                setPixel(Math.Round(x), Math.Round(y));
                xx = x;
                yy = y;
            }
        }

        #endregion

        #region Bresenham
        
        // Bresenham Button
        private void button2_Click(object sender, EventArgs e)
        {
            label5.Text = "Width : " + pictureBox1.Width + " Height : " + pictureBox1.Height;
            int x0 = Convert.ToInt32(textBox1.Text);
            int y0 = Convert.ToInt32(textBox2.Text);
            int xend = Convert.ToInt32(textBox3.Text);
            int yend = Convert.ToInt32(textBox4.Text);

            Shape = 2;
            
            line.XStart = x0;
            line.YStart = y0;
            line.XEnd = xend;
            line.YEnd = yend;
            textBox8.Text = "";

            DrawBraline01(x0, xend, y0, yend);           
        }

        // Bresenham Algo
        void DrawBraline01(int x0, int xend, int y0, int yend)
        {
            GFXPKG = pictureBox1.CreateGraphics();
            GFXPKG.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            xx = x0;
            yy = y0;

            int dx = Math.Abs(xend - x0),
                dy = Math.Abs(yend - y0);
            double x, y; 
            
            // Determine which Quad  
            double p ;
            int twoDy , twoDyMinusDx ;
            x = x0;
            y = y0;
            if (y < yend)
            {
                if(((dy / dx) >= 0 && (dy / dx) < 1) || ((dy / dx) <= 0 && (dy / dx) > -1))
                {
                    p = 2 * dy - dx;
                    twoDy = 2 * dy;
                    twoDyMinusDx = 2 * (dy - dx);
                    setPixel(x, y);
                    x = x0;
                    y = y0;
                    while (x < xend)
                    {
                        x++;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            y++;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                    while (x > xend)
                    {
                        x--;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            y++;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                }
                else
                {
                    dx = Math.Abs(yend - y0);
                    dy = Math.Abs(xend - x0);
                    p = 2 * dy - dx;
                    twoDy = 2 * dy;
                    twoDyMinusDx = 2 * (dy - dx);
                    setPixel(x, y);
                    x = x0;
                    y = y0;
                    
                        while (x <= xend && y < yend)
                        {
                            y++;

                            if (p < 0)
                                p += twoDy;
                            else
                            {
                                x++;
                                p += twoDyMinusDx;
                            }
                            setPixel(x, y);
                            xx = x;
                            yy = y;
                        }                  
                        while (x >= xend && y < yend)
                        {
                            y++;

                            if (p < 0)
                                p += twoDy;
                            else
                            {
                                x--;
                                p += twoDyMinusDx;
                            }
                            setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                    
                }
            }
            else
            {
                if (((dy / dx) >= 0 && (dy / dx) < 1) || ((dy / dx) <= 0 && (dy / dx) > -1))
                {
                    p = 2 * dy - dx;
                    twoDy = 2 * dy;
                    twoDyMinusDx = 2 * (dy - dx);
                    setPixel(x, y);
                    x = x0;
                    y = y0;
                    while (x < xend)
                    {
                        x++;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            y--;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                    while (x > xend)
                    {
                        x--;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            y--;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                }
                else
                {
                    dx = Math.Abs(yend - y0);
                    dy = Math.Abs(xend - x0);
                    p = 2 * dy - dx;
                    twoDy = 2 * dy;
                    twoDyMinusDx = 2 * (dy - dx);
                    setPixel(x, y);
                    x = x0;
                    y = y0;
                    while (x <= xend && y > yend)
                    {
                        y--;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            x++;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                    while (x >= xend && y > yend)
                    {
                        y--;

                        if (p < 0)
                            p += twoDy;
                        else
                        {
                            x--;
                            p += twoDyMinusDx;
                        }
                        setPixel(x, y);
                        xx = x;
                        yy = y;
                    }
                }
                
            }          
        }

        #endregion

        #region Points plot for both DDA and Bresenham
        public void setPixel(double x, double y)
        {
            GFXPKG.DrawLine(Pens.Black, (int)Math.Round(xx), -(int)Math.Round(yy), (int)Math.Round(x), -(int)Math.Round(y)); 
            textBox8.Text = textBox8.Text + "(" + x + "," + y + ")" + "\t";
        }
        #endregion

        #region Circle

        // Circle Botton
        private void button1_Click_1(object sender, EventArgs e)
        {
            label5.Text = "Width : " + pictureBox1.Width + " Height : " + pictureBox1.Height;
            int XCenter = Convert.ToInt32(textBox5.Text);
            int YCenter = Convert.ToInt32(textBox6.Text);
            int Reduis = Convert.ToInt32(textBox7.Text);

            Shape = 3;

            circle.XCenter = XCenter;
            circle.YCenter = YCenter;
            circle.Reduis = Reduis;
            
            textBox8.Text = "";
            DrawCircle(XCenter, YCenter, Reduis);

        }
        
        // Circle Algo
        public void DrawCircle(int Xcenter, int Ycenter, int reduis)
        {
            GFXPKG = pictureBox1.CreateGraphics();
            GFXPKG.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);

            

            int x = 0;
            int y = reduis;
            int p = 1 - reduis;

            xx = x;
            yy = y;

            CirclePlotPoints(Xcenter, Ycenter, x, y);

            while (x < y)
            {
                x++;
                if (p < 0)
                    p += 2 * x + 1;
                else
                {
                    y--;
                    p += 2 * (x - y) + 1;
                }
                CirclePlotPoints(Xcenter, Ycenter, x, y);
                xx = x;
                yy = y;
            }
           

        }
        
        // Circle Plot Pionts
        public void CirclePlotPoints(int xCenter,int Ycenter,int x ,int y)
        {
            int xpoint = xCenter + x;
            int ypoint = Ycenter + y;

            textBox8.Text = textBox8.Text + "(" +  xpoint + "," + ypoint+ ")" + "\t";
            GFXPKG.DrawLine(Pens.Black, xCenter + x, -(Ycenter + y), xCenter + (int)xx, -(Ycenter + (int)yy));
            GFXPKG.DrawLine(Pens.Black, xCenter + x, -(Ycenter - y), xCenter + (int)xx, -(Ycenter - (int)yy));
            GFXPKG.DrawLine(Pens.Black, xCenter - x, -(Ycenter + y), xCenter - (int)xx, -(Ycenter + (int)yy));
            GFXPKG.DrawLine(Pens.Black, xCenter - x, -(Ycenter - y), xCenter - (int)xx, -(Ycenter - (int)yy));
            GFXPKG.DrawLine(Pens.Black, xCenter + y, -(Ycenter + x), xCenter + (int)yy, -(Ycenter + (int)xx));
            GFXPKG.DrawLine(Pens.Black, xCenter + y, -(Ycenter - x), xCenter + (int)yy, -(Ycenter - (int)xx));
            GFXPKG.DrawLine(Pens.Black, xCenter - y, -(Ycenter + x), xCenter - (int)yy, -(Ycenter + (int)xx));
            GFXPKG.DrawLine(Pens.Black, xCenter - y, -(Ycenter - x), xCenter - (int)yy, -(Ycenter - (int)xx));
        }

        #endregion

        #region Ellipse

        //Ellipse Bottom
        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text = "Width : " + pictureBox1.Width + " Height : " + pictureBox1.Height;
            int xCenter = Convert.ToInt32(textBox11.Text);
            int yCenter = Convert.ToInt32(textBox10.Text);
            int reduisX = Convert.ToInt32(textBox9.Text);
            int reduisY = Convert.ToInt32(textBox12.Text);

            Shape = 4;

            ellipse.XCenter = xCenter;
            ellipse.YCenter = yCenter;
            ellipse.XReduis = reduisX;
            ellipse.YReduis = reduisY;

            textBox8.Text = "";
            DrawEllipse(xCenter, yCenter, reduisX, reduisY);
        }

        //Ellipse Algo
        public void DrawEllipse(int xCenter, int yCenter, int ReduisX, int ReduisY)
        {
            GFXPKG = pictureBox1.CreateGraphics();
            GFXPKG.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            double x = 0,
                   y = ReduisY;
            double p = Math.Pow(ReduisY, 2) - Math.Pow(ReduisX, 2) * ReduisY + (1 / 4) * Math.Pow(ReduisX, 2);
            xx = x;
            yy = y;
            while (2 * Math.Pow(ReduisY, 2) * x  < 2 * Math.Pow(ReduisX, 2) * y)
            {
                x++;
                if (p < 0)
                    p += 2 * Math.Pow(ReduisY, 2) * x + Math.Pow(ReduisY, 2);
                else
                {
                    y--;
                    p += 2 * Math.Pow(ReduisY, 2) * x - (2 * Math.Pow(ReduisX, 2) * y) + Math.Pow(ReduisY, 2);
                }
                textBox8.Text = textBox8.Text + "(" + x + "," + y + ")" + "\t";
                EllipsePlotpoint(xCenter, yCenter, x, y);
                xx = x;
                yy = y;
            }
            
            p = Math.Pow(ReduisY, 2) * Math.Pow((x + (1 / 2)), 2) + Math.Pow(ReduisX, 2) * Math.Pow((y - 1), 2) - Math.Pow(ReduisY, 2) * Math.Pow(ReduisX, 2);

            while (y > 0)
            {
                y--;
                if (p < 0)
                {
                    x++;
                    p += 2 * Math.Pow(ReduisY, 2) * x - 2 * Math.Pow(ReduisX, 2) * y + Math.Pow(ReduisX, 2);
                }
                else
                {
                    
                    p -= (2 * Math.Pow(ReduisX, 2) * y) + Math.Pow(ReduisX, 2);
                }
                   
                textBox8.Text = textBox8.Text + "(" + x + "," + y + ")" + "\t";
                EllipsePlotpoint(xCenter,yCenter, x, y);
                xx = x;
                yy = y;
            }
            
        }

        //Ellipse Plot points
        public void EllipsePlotpoint(int Xcenter,int Ycenter, double x, double y)
        {
            int xpoint = Xcenter + (int)x;
            int ypoint = Ycenter + (int)y;

            textBox8.Text = textBox8.Text + "(" + xpoint + "," + ypoint + ")" + "\t";
            GFXPKG.DrawLine(Pens.Black, Xcenter + Convert.ToInt32(x), -(Ycenter + Convert.ToInt32(y)), Xcenter + Convert.ToInt32(xx) , -(Ycenter + Convert.ToInt32(yy)));
            GFXPKG.DrawLine(Pens.Black, Xcenter + Convert.ToInt32(x), -(Ycenter - Convert.ToInt32(y)), Xcenter + Convert.ToInt32(xx) , -(Ycenter - Convert.ToInt32(yy)));
            GFXPKG.DrawLine(Pens.Black, Xcenter - Convert.ToInt32(x), -(Ycenter + Convert.ToInt32(y)), Xcenter - Convert.ToInt32(xx) , -(Ycenter + Convert.ToInt32(yy)));
            GFXPKG.DrawLine(Pens.Black, Xcenter - Convert.ToInt32(x), -(Ycenter - Convert.ToInt32(y)), Xcenter - Convert.ToInt32(xx) , -(Ycenter - Convert.ToInt32(yy)));
            
        }

        
        #endregion

        //********************** Tranformations **********************\\

        #region Translation

        //Translation Botton
        private void button3_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            GFXPKG.Clear(Color.White);
            Translation2D(Convert.ToInt32(textBox13.Text), Convert.ToInt32(textBox14.Text));
        }

        //Translation Function
        public void Translation2D(int x,int y)
        {
            switch (Shape)
            {
                case 1:
                    DrawDDAline(line.XStart + x, line.XEnd + x, line.YStart + y, line.YEnd + y);
                    break;
                case 2:
                    DrawBraline01(line.XStart + x, line.XEnd + x, line.YStart + y, line.YEnd + y);
                    break;
                case 3:
                    DrawCircle(circle.XCenter + x,circle.YCenter + y, circle.Reduis);
                    break;
                case 4:
                    DrawEllipse(ellipse.XCenter + x, ellipse.YCenter + y, ellipse.XReduis, ellipse.YReduis);
                    break;
                default:
                    label5.Text = "Draw Shape first";
                    break;
            }
        }
        #endregion

        #region Scale
        //Scale Botton
        private void button5_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            GFXPKG.Clear(Color.White);
            Scale2D(Convert.ToInt32( textBox15.Text));
        }
       
        //Scale Function
        public void Scale2D(int distance)
        {
            switch (Shape)
            {
                case 1:
                    if (line.XEnd > 0 && line.YEnd > 0)      // first quad
                        DrawDDAline(line.XStart - distance, line.XEnd + distance, line.YStart - distance, line.YEnd + distance);
                    else if (line.XEnd < 0 && line.YEnd < 0) // Third Quad
                    {
                        distance *= -1;
                        DrawDDAline(line.XStart - distance, line.XEnd + distance, line.YStart - distance, line.YEnd + distance);
                    }
                    else if (line.XEnd < 0 && line.YEnd > 0) // Second Quad
                        DrawDDAline(line.XStart + distance, line.XEnd - distance, line.YStart - distance, line.YEnd + distance);
                    else                                     // Fourth Quad
                        DrawDDAline(line.XStart - distance, line.XEnd + distance, line.YStart + distance, line.YEnd - distance);
                    break;
                case 2:
                    if (line.XEnd > 0 && line.YEnd > 0)      // first quad
                        DrawBraline01(line.XStart - distance, line.XEnd + distance, line.YStart - distance, line.YEnd + distance);
                    else if (line.XEnd < 0 && line.YEnd < 0) // Third Quad
                    {
                        distance *= -1;
                        DrawBraline01(line.XStart - distance, line.XEnd + distance, line.YStart - distance, line.YEnd + distance);
                    }
                    else if (line.XEnd < 0 && line.YEnd > 0) // Second Quad
                        DrawBraline01(line.XStart + distance, line.XEnd - distance, line.YStart - distance, line.YEnd + distance);
                    else                                     // Fourth Quad
                        DrawBraline01(line.XStart - distance, line.XEnd + distance, line.YStart + distance, line.YEnd - distance);
                    break;
                case 3:
                    DrawCircle(circle.XCenter , circle.YCenter , circle.Reduis + distance);
                    break;
                case 4:
                    if(EllipseScale == 0)
                        DrawEllipse(ellipse.XCenter, ellipse.YCenter, ellipse.XReduis + distance, ellipse.YReduis + distance);
                    else if(EllipseScale == 1)
                        DrawEllipse(ellipse.XCenter, ellipse.YCenter, ellipse.XReduis + distance, ellipse.YReduis);
                    else
                        DrawEllipse(ellipse.XCenter, ellipse.YCenter, ellipse.XReduis, ellipse.YReduis + distance);
                    break;
                default:
                    label5.Text = "Draw Shape first";
                    break;
            }
        }

        //X Radio botton
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EllipseScale = 1;
        }

        //Y Radio botton
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            EllipseScale = 2;
        }

        //Both Radio botton
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            EllipseScale = 0;
        }

        #endregion

        #region Reflaction

        //Reflaction Botton
        private void button7_Click(object sender, EventArgs e)
        {
            textBox8.Text = " ";
            GFXPKG.Clear(Color.White);
            Mirror2D();
        }

        //Reflaction Function
        public void Mirror2D()
        {
            switch (Shape)
            {
                case 1:
                    if(CheckMirror2D == 1)
                        DrawDDAline(-line.XStart, -line.XEnd, line.YStart, line.YEnd);
                    else if(CheckMirror2D == 2)
                        DrawDDAline(line.XStart, line.XEnd, -line.YStart, -line.YEnd);
                    else
                        DrawDDAline(-line.XStart, -line.XEnd, -line.YStart, -line.YEnd);
                    break;
                case 2:
                    if (CheckMirror2D == 1)
                        DrawBraline01(-line.XStart , -line.XEnd , line.YStart , line.YEnd );
                    else if (CheckMirror2D == 2)
                        DrawBraline01(line.XStart, line.XEnd, -line.YStart, -line.YEnd);
                    else
                        DrawBraline01(-line.XStart, -line.XEnd, -line.YStart, -line.YEnd);
                    break;
                case 3:
                    if (CheckMirror2D == 1)
                        DrawCircle(-circle.XCenter, circle.YCenter, circle.Reduis );
                    else if (CheckMirror2D == 2)
                        DrawCircle(circle.XCenter, -circle.YCenter, circle.Reduis);
                    else
                        DrawCircle(-circle.XCenter, -circle.YCenter, circle.Reduis);
                    break;
                case 4:
                    if (CheckMirror2D == 1)
                        DrawEllipse(-ellipse.XCenter, ellipse.YCenter, ellipse.XReduis , ellipse.YReduis);
                    else if(CheckMirror2D == 2)
                        DrawEllipse(ellipse.XCenter, -ellipse.YCenter, ellipse.XReduis, ellipse.YReduis);
                    else
                        DrawEllipse(-ellipse.XCenter, -ellipse.YCenter, ellipse.XReduis, ellipse.YReduis);
                    break;
                default:
                    label5.Text = "Draw Shape first";
                    break;
            }
        }

        //X Radio botton
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            CheckMirror2D = 2;
        }

        //Y Radio botton
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CheckMirror2D = 1;
        }

        //Both Radio botton
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            CheckMirror2D = 0;
        }

        #endregion

        #region Shearing

        //Shearing Botton
        private void button8_Click(object sender, EventArgs e)
        {
            textBox8.Text = "";
            GFXPKG.Clear(Color.White);
            Shearing2D(Convert.ToInt32(textBox16.Text));
        }

        //Shearing Algo
        public void Shearing2D(int x)
        {
            switch (Shape)
            {
                case 1:
                    if(ShearingAxis == 1)
                        DrawDDAline(line.XStart, line.XEnd + x, line.YStart, line.YEnd);
                    else if(ShearingAxis == 2)
                        DrawDDAline(line.XStart , line.XEnd , line.YStart , line.YEnd + x);
                    break;
                case 2:
                    if (ShearingAxis == 1)
                        DrawBraline01(line.XStart, line.XEnd + x, line.YStart, line.YEnd);
                    else if (ShearingAxis == 2)
                        DrawBraline01(line.XStart, line.XEnd, line.YStart, line.YEnd + x);
                    break;
                default:
                    label5.Text = "Draw Line first";
                    break;
            }
        }

        //X Radio botton
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            ShearingAxis = 1;
        }

        //Y Radio botton
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            ShearingAxis = 2;
        }

        #endregion

        //********************** Other **********************\\

        #region other

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
