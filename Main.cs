using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    public class Main
    {

        public Main() {
            double[] range_1 = Enumerable.Repeat(-5.12, 5).ToArray();
            double[] range_2 = Enumerable.Repeat(5.12, 5).ToArray();

            SnakeOptimization snakeOptimization = new(80, 80, rastriginFunction, 5, range_1, range_2);
            var result = snakeOptimization.Solve();

            double[] array = result.Item1;
            double value = result.Item2;
            int number = result.Item3;

            Console.WriteLine("Best snake: {");
            foreach (double item in array)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("}");
            Console.WriteLine("Best snake fit value: " + value);
            Console.WriteLine("Number of objective function executions: " + number);
        }
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
