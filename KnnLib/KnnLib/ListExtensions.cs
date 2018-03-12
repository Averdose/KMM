using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnnLib
{
    public static class ListExtensions
    {
        /// <summary>
        /// This function returns top K lowest selected values
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">list to process</param>
        /// <param name="selector">selector selecting member from enumerable</param>
        /// <param name="k">number of returned values</param>
        /// <returns></returns>
        public static List<T> TopK<T>(this List<T> enumerable, Func<T, double> selector, int k)
        {
            var points = new List<T>();
            var alreadyPicked = new HashSet<T>();
            for(int i = 0; i < k; i++)
            {
                double minDistance = double.MaxValue;
                int index = -1;
                for(int j = 0; j < enumerable.Count; j++)
                {
                    var val = selector(enumerable[j]);
                    if (val < minDistance && !alreadyPicked.Contains(enumerable[j]))
                    {
                        index = j;
                        minDistance = val;
                    }
                }
                points.Add(enumerable[index]);
                alreadyPicked.Add(enumerable[index]);
            }
            return points;
        }

        public static double Std<T>(this IEnumerable<T> enumerable, Func<T, double> selector, double avg)
        {
            var count = enumerable.Count();
            var total = enumerable.Select(selector)
                .Sum(n => Math.Pow(n - avg, 2));

            return Math.Sqrt((1 / (float)count) * total);
        }

        public static double Avg<T>(this IEnumerable<T> enumerable, Func<T, double> selector)
        {
            return enumerable.Select(selector)
                .Average();
        }
    }
}
