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
            Loader loader = new Loader(problem, "data.simple.train.100.csv");
            loader.LoadInput();
            Console.WriteLine("{0},{1}:{2}", problem.InputPoints[0].X, problem.InputPoints[0].Y, problem.InputPoints[0].TrueLabel);
            Console.WriteLine("elements loaded: {0}", problem.InputPoints.Count);
        }
    }
}
