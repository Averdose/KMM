using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnnLib;

namespace KNN
{
    class MeanKnn : IKnn
    {
        private Dictionary<string, Point> _means;
        private Dictionary<string, double> _counts;
        private List<Point> _trainSet;
        private double _maxDistance;
        private List<string> _classes;
        /// <summary>
        /// assigns labels to a set of test points
        /// </summary>
        /// <param name="testSet">set of points for testing</param>
        /// <param name="k">number of neighbours</param>
        public void Classify(List<Point> testSet, int k)
        {
            for (int i = 0; i < testSet.Count; i++)
            {
                for (int j = 0; j < _trainSet.Count; j++)
                {
                    _trainSet[j].DistanceToSample = _trainSet[j].CalculateDistance(testSet[i]);
                }
                var votes = new Dictionary<string, double>();
                foreach (var c in _classes)
                {
                    votes.Add(c, 0);
                }
                var top = _trainSet.TopK(n => n.DistanceToSample, k);
                for (int j = 0; j < top.Count; j++)
                {
                    votes[top[j].TrueLabel] += _maxDistance / top[j].DistanceToMean;
                }
                var max = votes.Max(n => n.Value);
                testSet[i].ResultLabel = votes.First(n => n.Value == max).Key;
            }
        }
        /// <summary>
        /// Trains the knn classifier by finding the centre point for each label
        /// </summary>
        /// <param name="trainSet">set of training points</param>
        public void Train(List<Point> trainSet)
        {
            _classes = trainSet.Select(n => n.TrueLabel).Distinct().ToList();
            _means = new Dictionary<string, Point>();
            _counts = new Dictionary<string, double>();
            foreach (var c in _classes)
            {
                _means.Add(c, new Point());
                _counts.Add(c, 0);
            }
            for (int i = 0; i < trainSet.Count; i++)
            {
                _means[trainSet[i].TrueLabel].X += trainSet[i].X;
                _means[trainSet[i].TrueLabel].Y += trainSet[i].Y;
                _counts[trainSet[i].TrueLabel]++;
            }
            foreach (var c in _classes)
            {
                _means[c].X /= _counts[c];
                _means[c].Y /= _counts[c];
            }
            for (int i = 0; i < trainSet.Count; i++)
            {
                trainSet[i].DistanceToMean = trainSet[i].CalculateDistance(_means[trainSet[i].TrueLabel]);
            }
            _maxDistance = trainSet.Max(n => n.DistanceToMean);
            _trainSet = trainSet;
        }
    }
}
