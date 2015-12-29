using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace bt
{
    class Captcha
    {
        bool debug = false;
        bool dataLoaded = false;
        double chance = 1;
        bool[,] statsData; 
        int[] statsCount;
        Bitmap bmp = null;

        public Captcha(string dataSource) 
        {
            dataLoaded = loadData(dataSource);
        }

        public Captcha(string dataSource, Bitmap bmp)
        {
            dataLoaded = loadData(dataSource);
            loadBitmap(bmp);
        }

        private bool loadData(string file)
        {
            try
            {
                TextReader tr = new StreamReader(file);
                statsCount = new int[10];
                string [] indexes = tr.ReadLine().Split(' ');
                for (int x = 0; x < 10; x++)
                    statsCount[x] = Convert.ToInt32(indexes[x]);
            
                int statsSum;
                char[] row = new char[100]; 
                statsSum = statsCount.Sum();
                statsData = new bool[100, statsSum];
                for (int y = 0; y < statsSum; y++)
                {
                    row = tr.ReadLine().ToCharArray();
                    for (int x = 0; x < 100; x++)
                        statsData[x, y] = (row[x] == '1');
                }
                tr.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void loadBitmap(Bitmap _bmp)
        {
            bmp = _bmp;
        }

        public string solve()
        {
            makeBW();
            smoothen();
            isolateChars();
            return identifyChars();
        }

        private void makeBW()
        {
            Color color;
            float brightness;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 200; x++)
                {
                    color = bmp.GetPixel(x, y);
                    brightness = color.GetBrightness();
                    if (brightness > 0.1)
                        bmp.SetPixel(x, y, Color.White);
                    else
                        bmp.SetPixel(x, y, Color.Black);
                }
            }
            if (debug) bmp.Save("d:/bmp1bw.png", ImageFormat.Png);
        }

        private void smoothen()
        {
            Color color;
            for (int y = 1; y < 49; y++)
            {
                for (int x = 1; x < 199; x++)
                {
                    color = bmp.GetPixel(x, y);
                    if (bmp.GetPixel(x, y).ToArgb() == Color.White.ToArgb()
                        && ((bmp.GetPixel(x - 1, y).ToArgb() != Color.White.ToArgb() && bmp.GetPixel(x + 1, y).ToArgb() != Color.White.ToArgb())
                            || (bmp.GetPixel(x, y - 1).ToArgb() != Color.White.ToArgb() && bmp.GetPixel(x, y + 1).ToArgb() != Color.White.ToArgb())))
                    {
                        bmp.SetPixel(x, y, Color.Black);
                    }
                }
            }
            if (debug) bmp.Save("d:/bmp1smt.png", ImageFormat.Png);
        }

        private void isolateChars()
        {
            Color color;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 200; x++)
                {
                    color = bmp.GetPixel(x, y);
                    if (bmp.GetPixel(x, y).ToArgb() == Color.White.ToArgb())
                    {
                        if (isNoise(new Bitmap(bmp), new Point(x, y), 20))
                            fill(new Point(x, y), Color.Black);
                        else
                        {
                            Color c = bmp.GetPixel(x, y);
                            fill(new Point(x, y), Color.White);
                        }
                    }
                }
            }
            if (debug) bmp.Save("d:/bmp2isolated.png", ImageFormat.Png);
        }

        private string identifyChars()
        {
            int y = 25;
            string retVal = "";
            for (int x = 0; x < bmp.Width; x++)
            {
                if (bmp.GetPixel(x, y).ToArgb() == Color.White.ToArgb())
                {
                    fill(new Point(x, y), Color.Orange);
                    retVal += identify(Color.Orange).ToString();
                    fill(new Point(x, y), Color.Green);
                }
            }
            return retVal;
        }
        
        //identify number of color c
        private int identify(Color c) //, nn As Integer)
        {
            int xMax = 0, xMin = 200, yMax = 0, yMin = 50;
            for (int x = 0; x < 200; x++)
                for (int y = 0; y < 50; y++)
                    if (bmp.GetPixel(x, y).ToArgb() == c.ToArgb())
                    {
                        if (xMax < x) xMax = x;
                        if (xMin > x) xMin = x;
                        if (yMax < y) yMax = y;
                        if (yMin > y) yMin = y;
                    }
            int height = yMax - yMin, width = xMax - xMin;

            bool[,] map = bmpToMap(c, new Point(xMin, yMin), new Point(xMax, yMax));
            map = resizeMap(map, height);
            map = centerMap(map);
            bool[] mapData = mapToArray(map);
            double[] numberP = makePChart(mapData);

            //get best match
            int num = 0;
            for (int n = 0; n < 10; n++)
                if (numberP.Max() == numberP[n]) num = n;

            //Array.Sort(mapLikeness);
            //chance *= Math.Abs(mapLikeness[9] - mapLikeness[8]);

            return num;
        }


        //generate an array with probability for each number 0-9
        private double[] makePChart(bool[] mapData)
        {
            double[] numP = new double[10];
            for (int x = 0; x < 10; x++)
                numP[x] = 0;

            double f;
            int pos = 0;
            bool a, b;
            for (int n = 0; n < 10; n++)
            {
                for (int y = 0; y < statsCount[n]; y++)
                {
                    f = 0;
                    for (int x = 0; x < 100; x++)
                    {
                        a = statsData[x, pos];
                        b = mapData[x];
                        if (!(a ^ b)) f++;
                    }
                    f = f / 100;
                    if (numP[n] < f) numP[n] = f;
                    pos++;
                }
            }
            return numP;
        }


        private bool[,] bmpToMap(Color c, Point topLeft, Point bottomRight)
        {
            bool[,] map = new bool[40, 40];

            //input to matrix (40x40)
            int height = bottomRight.Y - topLeft.Y;
            int width = bottomRight.X - topLeft.X;
            if (width > height)
            {
                width = height;
                bottomRight.X = topLeft.X + width;
            }
            for (int y = topLeft.Y; y < bottomRight.Y; y++)
                for (int x = topLeft.X; (x < bottomRight.X); x++)
                    if (bmp.GetPixel(x, y).ToArgb() == c.ToArgb())
                        map[x - topLeft.X, y - topLeft.Y] = true;

            if (debug) drawMap(map, "map1new.txt");
            return map;
        }

        private bool[,] resizeMap(bool[,] map, int height)
        {
            bool[,] map2 = new bool[40, 40];

            double xyFactor = height / 40.0;
            int x2, y2;
            for (int y = 0; y < 40; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    x2 = Convert.ToInt32(Math.Floor(x * xyFactor));
                    y2 = Convert.ToInt32(Math.Floor(y * xyFactor));
                    map2[x, y] = map[x2, y2];
                }
            }
            if (debug) drawMap(map2, "map2resize.txt");
            return map2;
        }

        private bool[,] centerMap(bool[,] map)
        {
            int xMin = 40, xMax = 0;
            for (int y = 0; y < 40; y++)
                for (int x = 0; x < 40; x++)
                {
                    if (map[x, y] && xMax < x) xMax = x;
                    if (map[x, y] && xMin > x) xMin = x;
                }

            int width = xMax - xMin;
            int diff = Convert.ToInt32(Math.Floor((40 - width) / 2.0));
            bool[,] map2 = new bool[40, 40];
            for (int y = 0; y < 40; y++)
                for (int x = 0; x < (40 - diff); x++)
                    if (map[x, y]) map2[x + diff, y] = map[x, y];

            if (debug) drawMap(map2, "map3centered.txt");
            return map2;
        }

        private bool[] mapToArray(bool[,] map)
        {
            int s = 0;
            bool[] mapData = new bool[100];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    s = 0;
                    for (int y2 = 0; y2 < 4; y2++)
                        for (int x2 = 0; x2 < 4; x2++)
                            if (map[x * 4 + x2, y * 4 + y2]) s++;

                    mapData[y * 10 + x] = (s > 5);
                }
            }
            return mapData;
        }


        /*  
         *  input b/w image & white point
         *  colors area red, pass new instance to avoid!
         *  returns (area > threshold)
         */
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

        private void fill(Point p, Color c)
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

        //hjälpfunktion, skriver ut 40x40 matriserna från "identify"
        void drawMap(bool[,] map, string file)
        {

            FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int y = 0; y < 40; y++)
            {
                for (int x = 0; x < 40; x++)
                {
                    string s = (map[x, y]) ? "X" : "_";
                    sw.Write(s);
                }
                sw.Write(sw.NewLine);
            }
            sw.Close(); fs.Close();
        }










    }
}
