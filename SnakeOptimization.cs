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
            double[] gbest = new double[T];
            double[] vbest = new double[T];

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
                            Xnewm[i][j] = Xrandm[j] + flag * c2 * Am * ((xmax[j] - xmin[j]) * random.NextDouble() + xmin[j]);
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
                            Xnewf[i][j] = Xrandf[j] + flag * c2 * Af * ((xmax[j] - xmin[j]) * random.NextDouble() + xmin[j]);
                        }
                    }
                }
                else
                {
                    // Exploitation phase (food exists)
                    if (Temp > treshold2)
                    {
                        // Hot
                        for (int i = 0; i < Nm; i++)
                        {
                            int flagid = (int)(2 * rnd.NextDouble());
                            double flag = vecflag[flagid];
                            for (int j = 0; j < dim; j++)
                            {
                                Xnewm[i][j] = Xfood[j] + flag * c3 * Temp * random.NextDouble() * (Xfood[j] - Xm[i][j]);
                            }
                        }

                        for (int i = 0; i < nf; i++)
                        {
                            int flagid = (int)(2 * random.NextDouble());
                            double flag = vecflag[flagid];
                            for (int j = 0; j < dim; j++)
                            {
                                Xnewf[i][j] = Xfood[j] + flag * c3 * Temp * random.NextDouble() * (Xfood[j] - Xf[i][j]);
                            }
                        }
                    }
                    else
                    {
                        if (random.NextDouble() > 0.6)
                        {
                            // Fight
                            for (int i = 0; i < nm; i++)
                            {
                                double fm = Math.Exp(-fbestv / (fitnessm[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    Xnewm[i][j] = Xm[i][j] + c3 * fm * random.NextDouble() * (Q * Xbestf[j] - Xm[i][j]);
                                }
                            }

                            for (int i = 0; i < nf; i++)
                            {
                                double ff = Math.Exp(-mbestv / (fitnessf[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    Xnewf[i][j] = Xf[i][j] + c3 * ff * random.NextDouble() * (Q * Xbestm[j] - Xf[i][j]);
                                }
                            }
                        }
                        else
                        {
                            // Mating
                            for (int i = 0; i < nm; i++)
                            {
                                double mm = Math.Exp(-fitnessf[i] / (fitnessm[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    Xnewm[i][j] = Xm[i][j] + c3 * mm * random.NextDouble() * (Q * Xf[i][j] - Xm[i][j]);
                                }
                            }

                            for (int i = 0; i < nf; i++)
                            {
                                double mf = Math.Exp(-fitnessm[i] / (fitnessf[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    Xnewf[i][j] = Xf[i][j] + c3 * mf * random.NextDouble() * (Q * Xm[i][j] - Xf[i][j]);
                                }
                            }

                            int flagid = (int)(2 * random.NextDouble());
                            double egg = vecflag[flagid];
                            if (egg == 1)
                            {
                                int mworstp = Array.IndexOf(fitnessm, fitnessm.Max());
                                int fworstp = Array.IndexOf(fitnessf, fitnessf.Max());
                                for (int i = 0; i < dim; i++)
                                {
                                    Xnewm[mworstp][i] = xmin[i] + random.NextDouble() * (xmax[i] - xmin[i]);
                                    Xnewf[fworstp][i] = xmin[i] + random.NextDouble() * (xmax[i] - xmin[i]);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < nm; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (Xnewm[i][j] > xmax[j])
                        {
                            Xnewm[i][j] = xmax[j];
                        }
                        if (Xnewm[i][j] < xmin[j])
                        {
                            Xnewm[i][j] = xmin[j];
                        }
                    }

                    double y = fobj(Xnewm[i]);
                    iFobj++;
                    if (y < fitnessm[i])
                    {
                        fitnessm[i] = y;
                        Xm[i] = Xnewm[i].ToArray();
                    }
                }

                double Mbestv = fitnessm.Min();
                int Mbestp = Array.IndexOf(fitnessm, Mbestv);

                for (int i = 0; i < nf; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (Xnewf[i][j] > xmax[j])
                        {
                            Xnewf[i][j] = xmax[j];
                        }
                        if (Xnewf[i][j] < xmin[j])
                        {
                            Xnewf[i][j] = xmin[j];
                        }
                    }

                    double y = fobj(Xnewf[i]);
                    iFobj++;
                    if (y < fitnessf[i])
                    {
                        fitnessf[i] = y;
                        Xf[i] = Xnewf[i].ToArray();
                    }
                }

                double Fbestv = fitnessf.Min();
                int Fbestp = Array.IndexOf(fitnessf, Fbestv);

                if (Mbestv < mbestv)
                {
                    Xbestm = Xm[Mbestp].ToArray();
                    mbestv = Mbestv;
                }

                if (Fbestv < fbestv)
                {
                    Xbestf = Xf[Fbestp].ToArray();
                    fbestv = Fbestv;
                }

                if (Mbestv < Fbestv)
                {
                    gbest[t] = Mbestv;
                    vbest[t] = Xm[Mbestp].ToArray();
                }
                else
                {
                    gbest[t] = Fbestv;
                    vbest[t] = Xf[Fbestp].ToArray();
                }

                if (mbestv < fbestv)
                {
                    Xfood = Xbestm.ToArray();
                }
                else
                {
                    Xfood = Xbestf.ToArray();
                }
            }
        }
    }
}
