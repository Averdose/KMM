using KnnLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    /// <summary>
    /// Class responsible from loading data from a given csv file into a Problem class
    /// </summary>
    class Loader
    {
        private Problem _problem;
        private string _path;
        private StreamReader _reader;

        public Loader(Problem problem, string path)
        {
            _problem = problem;
            _path = path;
            _reader = new StreamReader(path);
        }

        public void LoadInput()
        {
            bool firstLine = true;
            while (!_reader.EndOfStream)
            {
                var line = _reader.ReadLine();
                if (!firstLine)
                {
                    var values = line.Split(',');
                    Point point = new Point();
                    point.X = double.Parse(values[0], System.Globalization.CultureInfo.InvariantCulture);
                    point.Y = double.Parse(values[1], System.Globalization.CultureInfo.InvariantCulture);
                    point.TrueLabel = values[2];
                    point.DistanceToOrigin = Math.Sqrt(point.X * point.X + point.Y * point.Y);
                    _problem.InputPoints.Add(point);
                    _problem.InputPoints.OrderBy(p => p.DistanceToOrigin);
                }
                else
                {
                    firstLine = false;
                }
            }
        }
    }
}
