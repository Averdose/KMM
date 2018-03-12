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
            IKnn solver = new SimpleSolver();
            Problem problem = new Problem(solver);
            Loader loader = new Loader();
            int k = 20;

            if (args.Length == 4)
            {
                switch (args[0])
                {
                    case "1":
                        solver = new SimpleSolver();
                        break;
                    case "2":
                        solver = new MeanKnn();
                        break;
                    default:
                        Console.WriteLine("invalid key, default simple solver has been selected");
                        break;
                }
                try
                {
                    loader = new Loader(args[1]);
                    problem.LearningPoints = loader.LoadPoints();
                }
                catch
                {
                    Console.WriteLine("An exception occured. Does the specified file exist?");
                    return;
                }
                try
                {
                    loader.Path = args[2];
                    problem.InputPoints = loader.LoadPoints();
                }
                catch
                {
                    Console.WriteLine("An exception occured. Does the specified file exist?");
                    return;
                }
                try
                {
                    k = Convert.ToInt32(args[3]);

                }
                catch
                {
                    Console.WriteLine("Couldnt convert to an integer number a default number of 20 will be applied");
                }
            }
            else
            {
                Console.WriteLine("Select solver:");
                Console.WriteLine("1: SimpleSolver");
                Console.WriteLine("2: MeanKnn");

                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        solver = new SimpleSolver();
                        break;
                    case ConsoleKey.D2:
                        solver = new MeanKnn();
                        break;
                    default:
                        Console.WriteLine("invalid key, default simple solver has been selected");
                        break;
                }
                Console.WriteLine("Please specify the training set");
                try
                {
                    loader = new Loader(Console.ReadLine());
                    problem.LearningPoints = loader.LoadPoints();
                }
                catch
                {
                    Console.WriteLine("An exception occured. Does the specified file exist?");
                    return;
                }
                Console.WriteLine("Please specify the test set");
                try
                {
                    loader.Path = Console.ReadLine();
                    problem.InputPoints = loader.LoadPoints();
                }
                catch
                {
                    Console.WriteLine("An exception occured. Does the specified file exist?");
                    return;
                }
                Console.WriteLine("Specify k");
                try
                {
                    k = Convert.ToInt32(Console.ReadLine());

                }
                catch
                {
                    Console.WriteLine("Couldnt convert to an integer number a default number of 20 will be applied");
                }
            }
            
            problem.Solve(k);
            Drawer drawer = new Drawer(problem.InputPoints);
            drawer.DrawSolution();
            Console.WriteLine("A solution has been printed");
            
        }
    }
}
