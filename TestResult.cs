using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    public class TestResult
    {
        public string AlgorithmName { get; set; }
        public string TestFunctionName { get; set; }
        public int NumberOfParameters { get; set; }
        public int NumberOfIterations { get; set; }
        public int PopulationSize { get; set; }
        public double[] FoundMinimum { get; set; }
        public double StdDevParameters { get; set; }
        public double ObjectiveValue { get; set; }
        public double StdDevObjectiveValue { get; set; }
    }
}
