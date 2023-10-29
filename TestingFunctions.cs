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
            new(RastriginFunction, 2, Tools.Table(-5.12, 2), Tools.Table(5.12, 2)),
            new(RastriginFunction, 3, Tools.Table(-5.12, 3), Tools.Table(5.12, 3)),
            new(RastriginFunction, 4, Tools.Table(-5.12, 4), Tools.Table(5.12, 4)),
            new(RastriginFunction, 5, Tools.Table(-5.12, 5), Tools.Table(5.12, 5)),
            new(RastriginFunction, 6, Tools.Table(-5.12, 6), Tools.Table(5.12, 6)),
            new(RastriginFunction, 7, Tools.Table(-5.12, 7), Tools.Table(5.12, 7)),
            new(RastriginFunction, 8, Tools.Table(-5.12, 8), Tools.Table(5.12, 8)),
            new(RastriginFunction, 9, Tools.Table(-5.12, 9), Tools.Table(5.12, 9)),
            new(RastriginFunction, 10, Tools.Table(-5.12, 10), Tools.Table(5.12, 10)),
            new(RosenbrockFunction, 2, Tools.Table(-10, 2), Tools.Table(10, 2)),
            new(RosenbrockFunction, 3, Tools.Table(-10, 3), Tools.Table(10, 3)),
            new(RosenbrockFunction, 4, Tools.Table(-10, 4), Tools.Table(10, 4)),
            new(RosenbrockFunction, 5, Tools.Table(-10, 5), Tools.Table(10, 5)),
            new(RosenbrockFunction, 6, Tools.Table(-10, 6), Tools.Table(10, 6)),
            new(RosenbrockFunction, 7, Tools.Table(-10, 7), Tools.Table(10, 7)),
            new(RosenbrockFunction, 8, Tools.Table(-10, 8), Tools.Table(10, 8)),
            new(RosenbrockFunction, 9, Tools.Table(-10, 9), Tools.Table(10, 9)),
            new(RosenbrockFunction, 10, Tools.Table(-10, 10), Tools.Table(10, 10)),
            new(SphereFunction, 2, Tools.Table(-10, 2), Tools.Table(10, 2)),
            new(SphereFunction, 3, Tools.Table(-10, 3), Tools.Table(10, 3)),
            new(SphereFunction, 4, Tools.Table(-10, 4), Tools.Table(10, 4)),
            new(SphereFunction, 5, Tools.Table(-10, 5), Tools.Table(10, 5)),
            new(SphereFunction, 6, Tools.Table(-10, 6), Tools.Table(10, 6)),
            new(SphereFunction, 7, Tools.Table(-10, 7), Tools.Table(10, 7)),
            new(SphereFunction, 8, Tools.Table(-10, 8), Tools.Table(10, 8)),
            new(SphereFunction, 9, Tools.Table(-10, 9), Tools.Table(10, 9)),
            new(SphereFunction, 10, Tools.Table(-10, 10), Tools.Table(10, 10)),
            new(BealeFunction, 2, Tools.Table(-4.5, 2), Tools.Table(4.5, 2)),
            new(BealeFunction, 3, Tools.Table(-4.5, 3), Tools.Table(4.5, 3)),
            new(BealeFunction, 4, Tools.Table(-4.5, 4), Tools.Table(4.5, 4)),
            new(BealeFunction, 5, Tools.Table(-4.5, 5), Tools.Table(4.5, 5)),
            new(BealeFunction, 6, Tools.Table(-4.5, 6), Tools.Table(4.5, 6)),
            new(BealeFunction, 7, Tools.Table(-4.5, 7), Tools.Table(4.5, 7)),
            new(BealeFunction, 8, Tools.Table(-4.5, 8), Tools.Table(4.5, 8)),
            new(BealeFunction, 9, Tools.Table(-4.5, 9), Tools.Table(4.5, 9)),
            new(BealeFunction, 10, Tools.Table(-4.5, 10), Tools.Table(4.5, 10)),
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