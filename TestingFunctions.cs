namespace SnakeOptimization
{
    public class TestingFunctions
    {
        //list of functions
        public static TestFunction[] Functions = new TestFunction[] //for now static, if more functions are needed dynamically,
        //for ex. for generation purposes, then consider
        //intiializing class as an object and adding more functions as needed
        {
            //TODO: add specific scenarios for each function (consider adding a generator function
            // to generate scenarios based on dimension number for each function)
            new(RastriginFunction, 5, Tools.Table(-5.12, 5), Tools.Table(5.12, 5)),
            new(RosenbrockFunction, 2, new double[]{-2.048, -2.048}, new double[]{2.048, 2.048}),
            new(SphereFunction, 2, new double[]{-5.12, -5.12}, new double[]{5.12, 5.12}),
            new(BealeFunction, 2, new double[]{-4.5, -4.5}, new double[]{4.5, 4.5}),
            new(BukinFunctionN6, 2, new double[]{-15, -3}, new double[]{-5, 3}),
            new(HimmelblauFunctionN6, 2, new double[]{-5, -5}, new double[]{5, 5}),
        };

        // methods
        public static double RastriginFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length; i++)
                sum += X[i] * X[i] - 10 * Math.Cos(2 * Math.PI * X[i]);

            return 10 * X.Length + sum;

        }

        public static double RosenbrockFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length - 1; i++)
                sum += 100 * Math.Pow(X[i + 1] - X[i] * X[i], 2) + Math.Pow(1 - X[i], 2);

            return sum;
        }

        public static double SphereFunction(params double[] X)
        {

            double sum = 0;
            for (int i = 0; i < X.Length; i++)
                sum += X[i] * X[i];

            return sum;
        }

        public static double BealeFunction(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return Math.Pow(1.5 - x + x * y, 2) + Math.Pow(2.25 - x + x * y * y, 2) + Math.Pow(2.625 - x + x * y * y * y, 2);
        }

        public static double BukinFunctionN6(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return 100 * Math.Sqrt(Math.Abs(y - 0.01 * x * x)) + 0.01 * Math.Abs(x + 10);
        }


        public static double HimmelblauFunctionN6(params double[] X)
        {
            double x = X[0];
            double y = X[1];

            return Math.Pow(x * x + y - 11, 2) + Math.Pow(x + y * y - 7, 2);
        }

        
    }
}