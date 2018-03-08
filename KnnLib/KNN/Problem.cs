using KnnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    class Problem
    {
        /// <summary>
        /// List of points to learn from
        /// </summary>
        public List<Point> InputPoints { get; set; }

        public Problem()
        {
            InputPoints = new List<Point>();
        }
    }
}
