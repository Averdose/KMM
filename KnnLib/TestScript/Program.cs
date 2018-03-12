using KNN;
using KnnLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestScript
{
    class Program
    {
        static RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        static byte[] byteArray = new byte[4];

        private static uint GetVal()
        {
            provider.GetBytes(byteArray);
            return BitConverter.ToUInt32(byteArray, 0);
        }
        public static List<Point> Randomize<Point>(List<Point> source)
        {
            return source.OrderBy(n => GetVal()).ToList();
        }

        static void Main(string[] args)
        {
            var scenarios = new List<Scenario>();
            Loader loader = null;
            List<Point> dataset = null;
            var ks = new List<int>();
            var trainingSizes = new List<int>();
            int iterations = 0;
            string currentArg = null;
            Console.WriteLine("Reading data...");
            try
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (i == 0)
                    {
                        loader = new Loader(args[i]);
                        dataset = loader.LoadPoints();
                    }
                    else if(i == 1)
                    {
                        if(!int.TryParse(args[i], out iterations) || iterations <= 0 || (iterations % 4 != 0 && iterations != 1))
                        {
                            Console.WriteLine("Invalid number of iterations");
                            return;
                        }
                    }
                    else if (args[i] == "t")
                    {
                        currentArg = args[i];
                    }
                    else if (args[i] == "k")
                    {
                        currentArg = args[i];
                    }
                    else if (currentArg == "t")
                    {
                        int t;
                        if (!int.TryParse(args[i], out t) || t >= dataset.Count - 1 || t <= 0)
                        {
                            Console.WriteLine("Invalid training size: {0}", t);
                            return;
                        }
                        trainingSizes.Add(t);
                    }
                    else if (currentArg == "k")
                    {
                        int k;
                        if (!int.TryParse(args[i], out k) || k <= 0)
                        {
                            Console.WriteLine("Invalid k: {0}", k);
                            return;
                        }
                        ks.Add(k);
                    }
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Invalid path");
                return;
            }
            if(ks.Count == 0 || trainingSizes.Count == 0)
            {
                Console.WriteLine("Not enough args, usage: ./program.exe dataset_path t 100 50 20 k 5 6 7");
                return;
            }

            foreach(var size in trainingSizes)
            {
                foreach(var k in ks)
                {
                    Console.WriteLine("Batch: size - {0}, k - {1}", size, k);
                    if(iterations > 1)
                    {
                        for(int j = 0; j < iterations / 4; j++)
                        {
                            Parallel.For(0, 4, new Action<int>( n =>
                            {
                                var p = Randomize(dataset);
                                var s = new Scenario(new MeanKnn(), loader, size, k);
                                s.Execute(p.Select(pt => new Point()
                                {
                                    X = pt.X,
                                    Y = pt.Y,
                                    DistanceToOrigin = pt.DistanceToOrigin,
                                    TrueLabel = pt.TrueLabel
                                }).ToList());
                                Console.WriteLine("Mean :" + s.Accuracy);
                                scenarios.Add(s);
                                s = new Scenario(new SimpleSolver(), loader, size, k);
                                s.Execute(p.Select(pt => new Point()
                                {
                                    X = pt.X,
                                    Y = pt.Y,
                                    DistanceToOrigin = pt.DistanceToOrigin,
                                    TrueLabel = pt.TrueLabel
                                }).ToList());
                                Console.WriteLine("Simple :" + s.Accuracy);
                                scenarios.Add(s);  
                            }));
                        }
                    }
                    else
                    {
                        var s = new Scenario(new MeanKnn(), loader, size, k);
                        s.Execute(dataset.Select(pt => new Point()
                        {
                            X = pt.X,
                            Y = pt.Y,
                            DistanceToOrigin = pt.DistanceToOrigin,
                            TrueLabel = pt.TrueLabel
                        }).ToList());
                        scenarios.Add(s);
                        s = new Scenario(new SimpleSolver(), loader, size, k);
                        s.Execute(dataset);
                        scenarios.Add(s);
                    }
                }
            }
            Console.WriteLine("Saving results...");
            var date = DateTime.Now;
            var resultWriter = new ResultWriter();
            resultWriter.WriteResults(scenarios, date);
            resultWriter.WriteResults(TotalScenario.GetTotalScenarios(scenarios), date);
            Console.WriteLine("Done.");
        }
    }
}
