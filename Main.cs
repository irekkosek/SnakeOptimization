using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    public class Main
    {
        public static double rastriginFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length; i++)
                sum += X[i] * X[i] - 10 * Math.Cos(2 * Math.PI * X[i]);

            return 10 * X.Length + sum;

        }

        public static double rosenbrockFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length - 1; i++)
                sum += 100 * Math.Pow(X[i + 1] - X[i] * X[i], 2) + Math.Pow(1 - X[i], 2);

            return sum;
        }

        public static double sphereFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length; i++)
                sum += X[i] * X[i];

            return sum;
        }

        public static double bealeFunction(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return Math.Pow(1.5 - x + x * y, 2) + Math.Pow(2.25 - x + x * y * y, 2) + Math.Pow(2.625 - x + x * y * y * y, 2);
        }

        public static double bukinFunctionN6(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return 100 * Math.Sqrt(Math.Abs(y - 0.01 * x * x)) + 0.01 * Math.Abs(x + 10);
        }


        public static double himmelblauFunctionN6(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return Math.Pow(x * x + y - 11, 2) + Math.Pow(x + y * y - 7, 2);
        }
    }
}
