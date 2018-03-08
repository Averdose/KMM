using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnnLib
{
    //jak czegoś ci brakuje to dodaj
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        /// <summary>
        /// Distance to current test sample
        /// </summary>
        public double DistanceToSample { get; set; }
        /// <summary>
        /// Distance to centre of given class
        /// </summary>
        public double DistanceToMean { get; set; }
        /// <summary>
        /// True class of point read from input file
        /// </summary>
        public string TrueLabel { get; set; }
        /// <summary>
        /// Output class given by the algorithm
        /// </summary>
        public string ResultLabel { get; set; }

        /// <summary>
        /// Distance to the origin
        /// </summary>
        public double DistanceToOrigin { get; set; }

        public double CalculateDistance(Point p)
        {
            return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2));
        }
    }
}
