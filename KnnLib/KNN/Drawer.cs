using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using KnnLib;



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
            for (int i = 0; i < _points.Count; i++)
            {
                if (_points[i].ResultLabel == "1")
                {
                    _bmp.SetPixel(Normalize(_points[i].X), HEIGHT - 1 - Normalize(_points[i].Y), Color.Red);
                }
                else
                {
                    _bmp.SetPixel(Normalize(_points[i].X), HEIGHT -1 - Normalize(_points[i].Y), Color.Green);
                }
            }
            string tmp = GenerateName();
            _bmp.Save(GenerateName());

        }

        private int Normalize(double number)
        {
            int intNumber = Convert.ToInt32((number +1) * 0.5 * WIDTH);
            if (intNumber == WIDTH)
            {
                intNumber -= 1;
            }
            return intNumber;
        }
        private string GenerateName()
        {
            DateTime date = new DateTime();
            return date.ToLongDateString() + ".jpg";
        }

    }
}
