namespace SnakeOptimization
{
    public class TestFunction
    {
        public Funkcja Funkcja;
        public int Dim;
        public double[] Xmin;
        public double[] Xmax;
        
        //constructor
        public TestFunction(Funkcja _funkcja, int _dim, double[] _xmin, double[] _xmax)
        {
            Funkcja = _funkcja;
            Dim = _dim;
            Xmin = _xmin;
            Xmax = _xmax;
        }
        
    }
}