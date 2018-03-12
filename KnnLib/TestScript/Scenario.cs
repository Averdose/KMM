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

        private TimeSpan _trainingTime;
        private TimeSpan _testTime;

        public Scenario(IKnn knn, Loader loader, int trainingSize, int k)
        {
            _knn = knn;
            _loader = loader;
            _trainingSize = trainingSize;
            _k = k;
        }

        public void Execute()
        {
            var points = _loader.LoadPoints();
            if(points == null || points.Count - 1 <= _trainingSize)
            {
                return;
            }
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
        }

        public void WriteToFile(string path)
        {
            var testSize = _datasetSize - _trainingSize;
            Console.WriteLine("Dataset: {0}", _loader.Path);
            Console.WriteLine("Dataset size: {0}", _datasetSize);
            Console.WriteLine("Knn type: {0}", _knn.GetType());
            Console.WriteLine("K param: {0}", _k);
            Console.WriteLine("Training size: {0}", _trainingSize);
            Console.WriteLine("Test size: {0}", testSize);
            Console.WriteLine("Result: {0}/{1} ({2})", _classified, testSize,
                _classified / (float)testSize);
            Console.WriteLine("Training time: {0}", _trainingTime);
            Console.WriteLine("Test time: {0}", _testTime);   
        }
    }
}
