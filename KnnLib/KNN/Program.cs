using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    class Program
    {
        static void Main(string[] args)
        {
            Problem problem = new Problem();
            Loader loader = new Loader("data.simple.train.100.csv");
            problem.LearningPoints = loader.LoadPoints();
            Console.WriteLine("{0},{1}:{2}", problem.LearningPoints[0].X, problem.LearningPoints[0].Y, problem.LearningPoints[0].TrueLabel);
            Console.WriteLine("elements loaded: {0}", problem.LearningPoints.Count);
        }
    }
}
