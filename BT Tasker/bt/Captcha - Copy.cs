using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace bt
{
    class Captcha
    {
        Bitmap bmp = null;
        bool[,] statsData;
        int[] statsCount;

        public Captcha(Bitmap b)
        {
            bmp = b;
        }

        private int getRGBSum(Color c)
        {
            return c.R + c.G + c.B;
        }

        public string solve()
        {
            int x, y;
            Color pixel;
            //black&white
            for(y = 0; y < 50; y++)
            {
                for(x = 0; x < 200; x++)
                {
                    pixel = bmp.GetPixel(x, y);
                    if(getRGBSum(pixel) > 200)
                    {
                        bmp.SetPixel(x, y, Color.White);
                        //bt_pos(X, Y) = 1
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                        //bt_pos(X, Y) = 0
                    }
                }
            }

            //isolate numbers
            for(y = 0; y < 50; y++)
                for(x = 0; x < 200; x++)
                    if(bmp.GetPixel(x, y) == Color.White)
                        if(isNoise(x, y) fill(x, y, Color.Black);



            //identify numbers
            x = 0;
            y = 25;
            updatePos();
            string captcha = "";
            while(x < 200)
            {
                x++;
                pixel = bmp.GetPixel(x, y);
                if(bmp.GetPixel(x, y) == Color.White)
                {
                    fill(x, y, Color.Lime);
                    captcha += identify(x, y);
                    updatePos();
                    fill(x, y, Color.LightGray);
                }
            }

            return captcha;

        }

        private string identify(int x, int y)
        {
            int xMax, xMin, yMax, yMin, yc, xc, width;
            bool[,] map = new bool[40,40];
            bool[,] map4 = new bool[40,40];
            xMax = 0; xMin = 200;
            yMax = 0; yMin = 50;
            
            //get # size and pos
            for(y = 0; y < 50; y++)
            {  
                for(x = 0; x < 200; x++)
                {
                    if(bmp.GetPixel(x, y) == Color.Lime)
                    {
                        if(xMax < x) xMax = x;
                        if(xMin > x) xMin = x;
                        if(yMax < y) yMax = y;
                        if(yMin > y) yMin = y;
                    }
                }
            }

            //input to matrix (40x40)
                        
            xc = 0; yc = 0; width = 0;
            y = yMin;
            while(y++ < yMax && yc++ < 40)
            {
                x = xMin; xc = 0;
                while(x++ < xMax && xc++ < 40)
                {
                    map[xc,yc] = (bmp.GetPixel(x, y) == Color.Lime);
                }
                width = Math.Max(width, xc);
            }

            //resize to fit
            double xyFactor = yc / 40.0;
            for(x = 0; x < 40; x++)
                for(y = 0; y < 40; y++)
                    map4[x, y] = map[(int)(x*xyFactor), (int)(y*xyFactor)];

            //center
            for(x = 0; x < 40; x++)
            {
                for(y = 0; y < 40; y++)
                {
                    map[x, y] = false;
                    if(x > width && map4[x, y]) width = x;
                }
            }
            int xDiff = 40 - width;
            for(x = 0; x < 40; x++)
                for(y = 0; y < 40; y++)
                    if(map4[x, y]) map[x + xDiff / 2, y] = map4[x, y];


            //reduce 1600 bool to 100 int values
            int c, n, s, y2, x2;
            n = 10; c = 0;
            bool[] mapData = new bool[100];
            for (y = 0; y < 10; y++)
            {
                for (x = 0; x < 10; x++)
                {
                    s = 0;
                    for (y2 = 0; y2 < 4; y2++)
                        for (x2 = 0; x2 < 4; x2++)
                            if (map[x * 4 + x2, y * 4 + y2]) s++;

                    mapData[y * 10 + x] = (s > 5);
                }
            }

            //compare data
            double [] mapLikeness = new double[10];
            for (int x = 0; x <10; x++)
                mapLikeness[x] = 0;

            double f;
            int pos = 0;
            bool a, b;
            for (n = 0; n < 10; n++)
            {
                for (y = 0; y < statsCount[n]; y++)
                {
                    f = 0;
                    for (x = 0; x < 100; x++)
                    {
                        a = statsData[x, pos];
                        b = mapData[x];
                        //a = 16 - Math.Abs(a - b);
                        //f += a / 16;
                        if (!(a ^ b)) f++;
                    }
                    f = f / 100;
                    if (mapLikeness[n] < f) mapLikeness[n] = f;
                    pos++;
                }
            }

            //get best match
            int num = 0;
            for (n = 0; n <10; n++)
                if (mapLikeness.Max() == mapLikeness[n]) num = n;

            Array.Sort(mapLikeness);
            //chance *= Math.Abs(mapLikeness[9] - mapLikeness[8]);
    
            return num;
        }

        private bool isNoise(Bitmap bmp, Point p, int threshold)
        {
            Stack<Point> ptStack = new Stack<Point>();
            ptStack.Push(p);
            int count = 0;

            while (ptStack.Count > 0 && count < threshold)
            {
                p = ptStack.Pop();
                if (bmp.GetPixel(p.X, p.Y).ToArgb() == Color.White.ToArgb())
                {
                    count++;
                    bmp.SetPixel(p.X, p.Y, Color.Red);
                }

                if (p.X > 0)
                    if (bmp.GetPixel(p.X - 1, p.Y).ToArgb() == Color.White.ToArgb())
                        ptStack.Push(new Point(p.X - 1, p.Y));

                if (p.X < bmp.Width - 1) // - 2 ??
                    if (bmp.GetPixel(p.X + 1, p.Y).ToArgb() == Color.White.ToArgb())
                        ptStack.Push(new Point(p.X + 1, p.Y));

                if (p.Y > 0)
                    if (bmp.GetPixel(p.X, p.Y - 1).ToArgb() == Color.White.ToArgb())
                        ptStack.Push(new Point(p.X, p.Y - 1));

                if (p.Y < bmp.Height - 1) // - 2 ??
                    if (bmp.GetPixel(p.X, p.Y + 1).ToArgb() == Color.White.ToArgb())
                        ptStack.Push(new Point(p.X, p.Y + 1));

            }

            if (count == threshold)
                return false;
            else
                return true;
        }

        /*
        Private Sub btUpdatePos()
            For Y = 0 To 50
                For X = 0 To 200
                    PixCol = GetPixel(FMain.Picture1.hDC, X, Y)
                    If Not PixCol = 0 Then
                        bt_pos(X, Y) = 1
                    Else
                        bt_pos(X, Y) = 0
                    End If
                Next
            Next
        End Sub
        */
        private void fill(Bitmap bmp, Point p, Color c)
        {
            Stack<Point> ptStack = new Stack<Point>();
            ptStack.Push(p);
            Color color = bmp.GetPixel(p.X, p.Y);
            if (color.ToArgb() == c.ToArgb())
                return;

            while (ptStack.Count > 0)
            {
                p = ptStack.Pop();
                if (bmp.GetPixel(p.X, p.Y).ToArgb() == color.ToArgb())
                    bmp.SetPixel(p.X, p.Y, c);

                if (p.X > 0)
                    if (bmp.GetPixel(p.X - 1, p.Y).ToArgb() == color.ToArgb())
                        ptStack.Push(new Point(p.X - 1, p.Y));

                if (p.X < bmp.Width - 1) // - 2 ??
                    if (bmp.GetPixel(p.X + 1, p.Y).ToArgb() == color.ToArgb())
                        ptStack.Push(new Point(p.X + 1, p.Y));

                if (p.Y > 0)
                    if (bmp.GetPixel(p.X, p.Y - 1).ToArgb() == color.ToArgb())
                        ptStack.Push(new Point(p.X, p.Y - 1));

                if (p.Y < bmp.Height - 1) // - 2 ??
                    if (bmp.GetPixel(p.X, p.Y + 1).ToArgb() == color.ToArgb())
                        ptStack.Push(new Point(p.X, p.Y + 1));

            }
        }


    }
}
