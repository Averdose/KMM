using KnnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    class SimpleSolver : IKNN
    {
        public void Classify(Problem problem, int k)
        {
            Point[] neighbourPoints = new Point[k];
            for(int i = 0; i< problem.InputPoints.Count; ++i)
            {

            }
        }

        public void Train()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="teachers"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        private Point[] findNeighbours(Point p, List<Point> teachers, int k)
        {
            Point[] neighbours = new Point[k];
            for (int i =0; i < teachers.Count; i++)
            {
                double distance = p.CalculateDistance(teachers[i]);
                Point newPoint = new Point();
                newPoint.DistanceToSample = distance;
                if (neighbours.Length == k)
                {
                    if (neighbours[k - 1].DistanceToSample < distance)
                    {
                        neighbours[k - 1] = newPoint;
                    }
                }
                Array.Sort(neighbours, (p1, p2) => p1.DistanceToSample.CompareTo(p2.DistanceToSample));
            }
            return neighbours;
        }
    }
}
