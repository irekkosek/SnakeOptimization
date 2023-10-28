namespace SnakeOptimization
{
    public class Tools{
        /// <summary>
        /// Creates an array of size size and fills it with value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static double[] Table(double value, int size)
        {
            double[] array = new double[size];
            for (int i = 0; i < size; i++)
                array[i] = value;
            return array;
        }
    }
}