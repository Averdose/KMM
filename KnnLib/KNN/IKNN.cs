using KnnLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    interface IKnn
    {
        void Train(List<Point> trainSet);
        /// <summary>
        /// Classifies each point in List of input points in the class problem by assigning them a result label
        /// </summary>
        /// <param name="problem">Problem class containg learging points and input points</param>
        /// <param name="k">number of neighbours to be checked</param>
        void Classify(List<Point> testSet, int k);
    }
}
