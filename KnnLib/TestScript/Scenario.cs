using KNN;
using KnnLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScript
{
    public class Scenario
    {
        private int _trainingSize;
        private int _k;
        private IKnn _knn;
        private Loader _loader;
        private int _classified;
        private int _datasetSize;
        private int _testSize;
        private float _accuracy;
        private TimeSpan _trainingTime;
        private TimeSpan _testTime;

        public string Path { get { return _loader.Path; } }
        public string Algorithm { get { return _knn.GetType().ToString(); } }
        public int K { get { return _k; } }
        public int DatasetSize { get { return _datasetSize; } }
        public int TrainingSize { get { return _trainingSize; } }
        public int Correct { get { return _classified; } }
        public int TestSize { get { return _testSize; } }
        public float Accuracy { get { return _accuracy; } }
        public string TrainingTime { get { return _trainingTime.ToString(@"ss\:fffffff"); } }
        public string TestTime { get { return _testTime.ToString(@"ss\:fffffff"); } }

        public Scenario(IKnn knn, Loader loader, int trainingSize, int k)
        {
            _knn = knn;
            _loader = loader;
            _trainingSize = trainingSize;
            _k = k;
        }

        public void Execute(List<Point> points)
        {
            var training = points.Take(_trainingSize).ToList();
            var test = points.Skip(_trainingSize).ToList();
            var trainWatch = Stopwatch.StartNew();
            _knn.Train(training);
            _trainingTime = trainWatch.Elapsed;
            _knn.Classify(test, _k);
            _testTime = trainWatch.Elapsed - _trainingTime;
            trainWatch.Stop();
            _classified = test.Count(n => n.ResultLabel == n.TrueLabel);
            _datasetSize = points.Count;
            _testSize = _datasetSize - _trainingSize;
            _accuracy = _classified / (float)_testSize;
        }
    }
}
