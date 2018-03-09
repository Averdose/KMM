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
            MeanKnn solver = new MeanKnn();
            Problem problem = new Problem(solver);
            Loader loader = new Loader("data.simple.train.100.csv");
            problem.LearningPoints = loader.LoadPoints();
            loader.Path = "data.simple.train.1000.csv";
            problem.InputPoints = loader.LoadPoints();
            problem.Solve(20);
            Drawer drawer = new Drawer(problem.InputPoints);
            drawer.DrawSolution();
            /*
            for(int i =0; i< problem.InputPoints.Count; i++)
            {
                Console.WriteLine("{0},{1} : our label {2} : true label{3}", problem.InputPoints[i].X, problem.InputPoints[i].Y, problem.InputPoints[i].ResultLabel, problem.InputPoints[i].TrueLabel);
            }
            */
        }
    }
}
