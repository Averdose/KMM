﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using KnnLib;
using System.Diagnostics;
using System.Globalization;

namespace KNN
{
    class Drawer
    {
        private Bitmap _bmp;
        private List<KnnLib.Point> _points;
        private const int WIDTH = 200;
        private const int HEIGHT = 200;


        public Drawer(List<KnnLib.Point> points)
        {
            _points = points;

        }

        public void DrawSolution()
        {
            _bmp = new Bitmap(WIDTH, HEIGHT);
            for(int i =0; i< WIDTH; i++)
            {
                for(int j =0; j< HEIGHT; j++)
                {
                    _bmp.SetPixel(i, j, Color.White);
                }
            }
            double maksX = Math.Abs(_points.Min(p =>p.X));
            double maksY = Math.Abs(_points.Min(p => p.Y));

            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].ResultLabel == "1")
                {
                    double temp = HEIGHT - 1 - Normalize(_points[i].Y, maksY);
                    _bmp.SetPixel(Normalize(_points[i].X, maksX), HEIGHT - 1 - Normalize(_points[i].Y, maksY), Color.Black);
                }
                else if(_points[i].ResultLabel == "2")
                {
                    _bmp.SetPixel(Normalize(_points[i].X, maksX), HEIGHT -1 - Normalize(_points[i].Y, maksY), Color.Red);
                }
                else if(_points[i].ResultLabel == "3")
                {
                    _bmp.SetPixel(Normalize(_points[i].X, maksX), HEIGHT - 1 - Normalize(_points[i].Y, maksY), Color.Green);
                }
                else
                {
                    _bmp.SetPixel(Normalize(_points[i].X, maksX), HEIGHT - 1 - Normalize(_points[i].Y, maksY), Color.Blue);
                }
            }
            string fileName = GenerateName();
            _bmp.Save(fileName);
            ProcessStartInfo startInfo = new ProcessStartInfo(fileName);
            Process.Start(startInfo);
        }

        private int Normalize(double number, double maks)
        {
            int intNumber = Convert.ToInt32((number + maks) * (1/(maks*2)) * WIDTH);
            if (intNumber == WIDTH)
            {
                intNumber -= 1;
            }
            return intNumber;
        }
        private string GenerateName()
        {
            DateTime date = DateTime.Now;
            return date.ToString("yyyy-MMM-dd__HH-mm-ss_", CultureInfo.InvariantCulture) + ".png";
        }

    }
}
