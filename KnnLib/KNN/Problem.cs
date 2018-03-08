using KnnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    /// <summary>
    /// class responsible for calculating classes for new input points
    /// </summary>
    class Problem
    {
        /// <summary>
        /// List of points to learn from
        /// </summary>
        public List<Point> LearningPoints { get; set; }
        /// <summary>
        /// List of points to have a class assigned to them
        /// </summary>
        public List<Point> InputPoints { get; set; }

        private IKNN _solver;

        public Problem(IKNN solver)
        {
            LearningPoints = new List<Point>();
            InputPoints = new List<Point>();
            _solver = solver;
        }

        public void Solve()
        {
            _solver.Train();
            _solver.Classify();
        }


    }
}
