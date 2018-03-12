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
    public class Loader
    {
        public string Path { get { return _path; }
            set {
                _path = value;
                _reader = new StreamReader(_path);
            } }

        private string _path;
        private StreamReader _reader;

        public Loader()
        {

        }
        public Loader(string path )
        {
            _path = path;
            _reader = new StreamReader(path);
        }

        public List<Point> LoadPoints()
        {
            List<Point> points = new List<Point>();
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
                    points.Add(point);
                    points.Sort((p1,p2) => p1.DistanceToOrigin.CompareTo(p2.DistanceToOrigin));
                }
                else
                {
                    firstLine = false;
                }
            }
            return points;
        }

        
    }
}
