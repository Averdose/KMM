using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KNN;
using KnnLib;

namespace TestScript
{
    public class TotalScenario
    {
        private int _k;
        string _algorithm;
        private int _trainingSize;
        private double _avgCorrect;
        private double _stdCorrect;
        private double _avgAccuracy;
        private double _stdAccuracy;


        public string Algorithm { get { return _algorithm; } }
        public int K { get { return _k; } }
        public int TrainingSize { get { return _trainingSize; } }
        public double AvgCorrect { get { return _avgCorrect; } }
        public double StdCorrect { get { return _stdCorrect; } }
        public double AvgAccuracy { get { return _avgAccuracy; } }
        public double StdAccuracy { get { return _stdAccuracy; } }

        public static List<TotalScenario> GetTotalScenarios(List<Scenario> scenarios)
        {
            var totalScenarios = new List<TotalScenario>();
            var ks = scenarios.Select(n => n.K).Distinct();
            var algorithms = scenarios.Select(n => n.Algorithm).Distinct();
            var trainingSizes = scenarios.Select(n => n.TrainingSize).Distinct();

            foreach(var k in ks)
            {
                foreach(var algorithm in algorithms)
                {
                    foreach(var trainingSize in trainingSizes)
                    {
                        var filtered = scenarios.Where(n => n.K == k && n.Algorithm == algorithm && n.TrainingSize == trainingSize);
                        var avgCorrect = filtered.Avg(n => n.Correct);
                        var avgAccuracy = filtered.Avg(n => n.Accuracy);
                        var stdCorrect = filtered.Std(n => n.Correct, avgCorrect);
                        var stdAccuracy = filtered.Std(n => n.Accuracy, avgAccuracy);
                        var scenario = new TotalScenario()
                        {
                            _trainingSize = trainingSize,
                            _k = k,
                            _algorithm = algorithm,
                            _avgCorrect = avgCorrect,
                            _stdCorrect = stdCorrect,
                            _avgAccuracy = avgAccuracy,
                            _stdAccuracy = stdAccuracy
                        };
                        totalScenarios.Add(scenario);
                    }
                }
            }
            return totalScenarios;
        }
    }
}
