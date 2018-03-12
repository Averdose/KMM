using KnnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    public class SimpleSolver : IKnn
    {
        private List<string> _classes;
        private List<Point> _trainSet;
        public void Classify(List<Point> testSet, int k)
        {
            for(int i = 0; i< testSet.Count; ++i)
            {
                for(int j =0; j < _trainSet.Count; ++j)
                {
                    _trainSet[j].DistanceToSample = _trainSet[j].CalculateDistance(testSet[i]);
                }
                List<Point> top = _trainSet.TopK(p => p.DistanceToSample, k);
                var votes = new Dictionary<string, double>();
                foreach (var c in _classes)
                {
                    votes.Add(c, 0);
                }
                for(int j =0; j< top.Count; ++j)
                {
                    votes[top[j].TrueLabel] += 1;
                }
                var max = votes.Max(n => n.Value);
                testSet[i].ResultLabel = votes.First(n => n.Value == max).Key;
            }
        }

        public void Train(List<Point> trainSet)
        {
            _classes = trainSet.Select(n => n.TrueLabel).Distinct().ToList();
            _trainSet = trainSet;
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
