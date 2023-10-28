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

            // constant variables
            double[] vecflag = { 1, -1 };
            double treshold1 = 0.25;
            double treshold2 = 0.6;
            double c1 = 0.5;
            double c2 = 0.05;
            double c3 = 2;

            double[][] X = new double[N][];
            double[] fitness = new double[N];

            // initialize snake swarm and calculate fitness of each snake by objective function
            for (int i = 0; i < N; i++)
            {
                X[i] = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    X[i][j] = xmin[j] + rnd.NextDouble() * (xmax[j] - xmin[j]);
                }
                fitness[i] = funkcja(X[i]);
            }

            // Divide the swarm
            int Nm = N / 2;
            int Nf = N - Nm;
            double[][] Xm = X.Take(Nm).ToArray(); // males
            double[][] Xf = X.Skip(Nm).ToArray(); // females
            double[] male_fitness = fitness.Take(Nm).ToArray();
            double[] female_fitness = fitness.Skip(Nm).ToArray();

            // Get best male
            double bestMale_fitValue = male_fitness.Min(); // get minimum value from fitness array
            int bestMale_fitValue_index = Array.IndexOf(male_fitness, bestMale_fitValue); // get index of this element
            double[] bestMale = Xm[bestMale_fitValue_index].ToArray(); // find vector in the male matrix on this index

            // Get best female (same as male)
            double bestFemale_fitValue = female_fitness.Min();
            int bestFemale_fitValue_index = Array.IndexOf(female_fitness, bestFemale_fitValue);
            double[] bestFemale = Xf[bestFemale_fitValue_index].ToArray();


            // Get food position (Ffood)
            double bestSnake = fitness.Min();
            int bestSnake_index = Array.IndexOf(fitness, bestSnake);
            double[] food_position = X[bestSnake_index].ToArray();


           double[][] male_positions = new double[Nm][];
           double[][] female_positions = new double[Nf][];
           double[] gbest = new double[T];
           double[] vbest = new double[T];


            for (int t = 1; t <= T; t++)
            {
                // Calculate temperature
                double Temp = Math.Exp(-(double)t / T);
                // Calculate food quantity
                double Q = c1 * Math.Exp(((double)t - T) / T);
                if (Q > 1)
                {
                    Q = 1;
                }

                if (Q < treshold1)
                {
                    // Exploration phase (no food)
                    // Every snake searches for food and goes to random position

                    // For males
                    for (int i = 0; i < Nm; i++)
                    {
                        int randmid = (int)(Nm * rnd.NextDouble());
                        double[] randomMale_position = Xm[randmid].ToArray();
                        int flagid = (int)(2 * rnd.NextDouble());
                        double flag = vecflag[flagid];
                        double Am = Math.Exp(-male_fitness[randmid] / (male_fitness[i] + double.Epsilon));
                        for (int j = 0; j < dim; j++)
                        {
                            male_positions[i][j] = randomMale_position[j] + flag * c2 * Am * ((xmax[j] - xmin[j]) * rnd.NextDouble() + xmin[j]);
                        }
                    }

                    // For females
                    for (int i = 0; i < Nf; i++)
                    {
                        int randfid = (int)(Nf * rnd.NextDouble());
                        double[] randomFemale_position = Xf[randfid].ToArray();
                        int flagid = (int)(2 * rnd.NextDouble());
                        double flag = vecflag[flagid];
                        double Af = Math.Exp(-female_fitness[randfid] / (female_fitness[i] + double.Epsilon));
                        for (int j = 0; j < dim; j++)
                        {
                            female_positions[i][j] = randomFemale_position[j] + flag * c2 * Af * ((xmax[j] - xmin[j]) * rnd.NextDouble() + xmin[j]);
                        }
                    }
                }
                else
                {
                    // Exploitation phase (food exists)
                    if (Temp > treshold2)
                    {
                        // Hot
                        // Snakes go to the food

                        // For males
                        for (int i = 0; i < Nm; i++)
                        {
                            int flagid = (int)(2 * rnd.NextDouble());
                            double flag = vecflag[flagid];
                            for (int j = 0; j < dim; j++)
                            {
                                male_positions[i][j] = food_position[j] + flag * c3 * Temp * rnd.NextDouble() * (food_position[j] - Xm[i][j]);
                            }
                        }

                        // For females
                        for (int i = 0; i < Nf; i++)
                        {
                            int flagid = (int)(2 * rnd.NextDouble());
                            double flag = vecflag[flagid];
                            for (int j = 0; j < dim; j++)
                            {
                                female_positions[i][j] = food_position[j] + flag * c3 * Temp * rnd.NextDouble() * (food_position[j] - Xf[i][j]);
                            }
                        }
                    }
                    else
                    {
                        if (rnd.NextDouble() > 0.6)
                        {
                            // Fight

                            // For males
                            for (int i = 0; i < Nm; i++)
                            {
                                double fm = Math.Exp(-bestFemale_fitValue / (male_fitness[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    male_positions[i][j] = Xm[i][j] + c3 * fm * rnd.NextDouble() * (Q * bestFemale[j] - Xm[i][j]);
                                }
                            }

                            // For females
                            for (int i = 0; i < Nf; i++)
                            {
                                double ff = Math.Exp(-bestMale_fitValue / (female_fitness[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    female_positions[i][j] = Xf[i][j] + c3 * ff * rnd.NextDouble() * (Q * bestMale[j] - Xf[i][j]);
                                }
                            }
                        }
                        else
                        {
                            // Mating

                            // For males
                            for (int i = 0; i < Nm; i++)
                            {
                                double mm = Math.Exp(-female_fitness[i] / (male_fitness[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    male_positions[i][j] = Xm[i][j] + c3 * mm * rnd.NextDouble() * (Q * Xf[i][j] - Xm[i][j]);
                                }
                            }

                            // For females
                            for (int i = 0; i < Nf; i++)
                            {
                                double mf = Math.Exp(-male_fitness[i] / (female_fitness[i] + double.Epsilon));
                                for (int j = 0; j < dim; j++)
                                {
                                    female_positions[i][j] = Xf[i][j] + c3 * mf * rnd.NextDouble() * (Q * Xm[i][j] - Xf[i][j]);
                                }
                            }

                            // Randomize if egg hatches
                            int flagid = (int)(2 * rnd.NextDouble());
                            double egg = vecflag[flagid];

                            // Check if egg is there or not
                            if (egg == 1)
                            {
                                // Get worst male and female
                                int worstMale_fitValue_index = Array.IndexOf(male_fitness, male_fitness.Max());
                                int worstFemale_fitValue_index = Array.IndexOf(female_fitness, female_fitness.Max());
                                // Replace them
                                for (int i = 0; i < dim; i++)
                                {
                                    male_positions[worstMale_fitValue_index][i] = xmin[i] + rnd.NextDouble() * (xmax[i] - xmin[i]);
                                    female_positions[worstFemale_fitValue_index][i] = xmin[i] + rnd.NextDouble() * (xmax[i] - xmin[i]);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < Nm; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (male_positions[i][j] > xmax[j])
                        {
                            male_positions[i][j] = xmax[j];
                        }
                        if (male_positions[i][j] < xmin[j])
                        {
                            male_positions[i][j] = xmin[j];
                        }
                    }

                    double y = funkcja(male_positions[i]);
                    if (y < male_fitness[i])
                    {
                        male_fitness[i] = y;
                        Xm[i] = male_positions[i].ToArray();
                    }
                }

                double bestMatingMale_fitValue = male_fitness.Min();
                int bestMatingMale_fitValue_index = Array.IndexOf(male_fitness, bestMatingMale_fitValue);

                for (int i = 0; i < Nf; i++)
                {
                    for (int j = 0; j < dim; j++)
                    {
                        if (female_positions[i][j] > xmax[j])
                        {
                            female_positions[i][j] = xmax[j];
                        }
                        if (female_positions[i][j] < xmin[j])
                        {
                            female_positions[i][j] = xmin[j];
                        }
                    }

                    double y = funkcja(female_positions[i]);
                    if (y < female_fitness[i])
                    {
                        female_fitness[i] = y;
                        Xf[i] = female_positions[i].ToArray();
                    }
                }

                double bestMatingFemale_fitValue = female_fitness.Min();
                int bestMatingFemale_fitValue_index = Array.IndexOf(female_fitness, bestMatingFemale_fitValue);

                if (bestMatingMale_fitValue < bestMale_fitValue)
                {
                    bestMale = Xm[bestMatingMale_fitValue_index].ToArray();
                    bestMale_fitValue = bestMatingMale_fitValue;
                }

                if (bestMatingFemale_fitValue < bestFemale_fitValue)
                {
                    bestFemale = Xf[bestMatingFemale_fitValue_index].ToArray();
                    bestFemale_fitValue = bestMatingFemale_fitValue;
                }

                if (bestMatingMale_fitValue < bestMatingFemale_fitValue)
                {
                    gbest[t] = bestMatingMale_fitValue;
                }
                else
                {
                    gbest[t] = bestMatingFemale_fitValue;
                }

                if (bestMale_fitValue < bestFemale_fitValue)
                {
                    food_position = bestMale.ToArray();
                }
                else
                {
                    food_position = bestFemale.ToArray();
                }
            }
            return food_position; // best snake
        }
    }
}
