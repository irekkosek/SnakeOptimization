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
            double[] vecflag = { 1, -1 };
            double treshold1 = 0.25;
            double treshold2 = 0.6;
            double c1 = 0.5;
            double c2 = 0.05;
            double c3 = 2;

            double[][] X = new double[N][];
            double[] fitness = new double[N];
            double bestv;
            int bestp;

            for (int i = 0; i < N; i++)
            {
                X[i] = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    X[i][j] = xmin[j] + rnd.NextDouble() * (xmax[j] - xmin[j]);
                }
                fitness[i] = funkcja(X[i]);
            }

            bestv = fitness.Min();
            bestp = Array.IndexOf(fitness, bestv);
            XBest = X[bestp].ToArray();

            int Nm = N / 2;
            int Nf = N - Nm;
            double[][] Xm = X.Take(Nm).ToArray();
            double[][] Xf = X.Skip(Nm).ToArray();
            double[] fitnessm = fitness.Take(Nm).ToArray();
            double[] fitnessf = fitness.Skip(Nm).ToArray();

            double mbestv = fitnessm.Min();
            int mbestp = Array.IndexOf(fitnessm, mbestv);
            double[] Xbestm = Xm[mbestp].ToArray();

            double fbestv = fitnessf.Min();
            int fbestp = Array.IndexOf(fitnessf, fbestv);
            double[] Xbestf = Xf[fbestp].ToArray();

            double[][] Xnewm = new double[Nm][];
            double[][] Xnewf = new double[Nf][];
            double[] fbest = new double[T]; //jak sie zmieniała wartość funkcji celu
            double[] xbest = new double[T]; //jak się zmieniały x

            for (int t = 1; t <= T; t++)
            {
                double Temp = Math.Exp(-(double)t / T);
                double Q = c1 * Math.Exp(((double)t - T) / T);
                if (Q > 1)
                {
                    Q = 1;
                }

                if (Q < treshold1)
                {
                    // Exploration phase (no food)
                    for (int i = 0; i < Nm; i++)
                    {
                        int randmid = (int)(Nm * rnd.NextDouble());
                        double[] Xrandm = Xm[randmid].ToArray();
                        int flagid = (int)(2 * rnd.NextDouble());
                        double flag = vecflag[flagid];
                        double Am = Math.Exp(-fitnessm[randmid] / (fitnessm[i] + double.Epsilon));
                        for (int j = 0; j < dim; j++)
                        {
                            Xnewm[i][j] = Xrandm[j] + flag * c2 * Am * ((xmax[j] - xmin[j]) * rnd.NextDouble() + xmin[j]);
                        }
                    }

                    for (int i = 0; i < Nf; i++)
                    {
                        int randfid = (int)(Nf * rnd.NextDouble());
                        double[] Xrandf = Xf[randfid].ToArray();
                        int flagid = (int)(2 * rnd.NextDouble());
                        double flag = vecflag[flagid];
                        double Af = Math.Exp(-fitnessf[randfid] / (fitnessf[i] + double.Epsilon));
                        for (int j = 0; j < dim; j++)
                        {
                            Xnewf[i][j] = Xrandf[j] + flag * c2 * Af * ((xmax[j] - xmin[j]) * rnd.NextDouble() + xmin[j]);
                        }
                    }
                }
            }
        }
    }
}
