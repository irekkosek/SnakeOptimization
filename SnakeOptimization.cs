using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    delegate double Funkcja(params double[] x);

    class SnakeOptimization : IOptimizationAlgorithm
    {
        private int N { get; set; }
        private int T { get; set; }
        private Funkcja funkcja { get; set; }
        private int dim { get; set; }
        private double[] xmin { get; set; }
        private double[] xmax { get; set; }

        public string Name { get; set; }
        public double[] XBest { get; set; }
        public double FBest { get; set; }
        public int NumberOfEvaluationFitnessFunction { get; set; }

        public SnakeOptimization(int _N, int _T, Funkcja _funkcja, int _dim, double[] _xmin, double[] _xmax)
        {
            this.N = _N;
            this.T = _T;
            this.funkcja = _funkcja;
            this.dim = _dim;
            this.xmin = _xmin;
            this.xmax = _xmax;

            this.XBest = new double[dim];
        }

        public double[] Solve()
        {
            Random rnd = new Random();

            double[,] X = new double[N, dim];
            double[] fitness = new double[N];

            for(int i = 0; i < N; i++)
            {
                for(int j = 0; j < dim; j++)
                    X[i, j] = xmin[j] + rnd.NextDouble() * (xmax[j] - xmin[j]);
            }

            for(int i = 0; i < N; i++)
            {
                for
                fitness[i] = funkcja(X[i,All]])
            }
        }
    }
}
