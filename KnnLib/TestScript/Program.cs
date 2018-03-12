using KNN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScript
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new Loader(@"../../../../data/data.simple.train.500.csv");
            var knn = new MeanKnn();
            var scenario = new Scenario(knn, loader, 250, 8);
            scenario.Execute();
            scenario.WriteToFile("result.txt");
        }
    }
}
