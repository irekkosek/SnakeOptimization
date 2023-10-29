using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    //TODO: rename class name to more appropriate one
    public class Main
    { 
        // properties
        public string Raport { get; set; }

        // constructor
        public Main()
        {
            Raport = "";
        }

        //N = {10, 20, 40, 80}, I = {5, 10, 20, 40, 60, 80}.
        public static int[] N = { 10, 20, 40, 80 };
        public static int[] I = { 5, 10, 20, 40, 60, 80 };
        //retries
        public static int n = 10;
        public static void TestSnakeOptimization()
        {
            Main main = new Main();

            for (int j = 0; j < TestingFunctions.Functions.Length; j++)
            {
                for (int k = 0; k < N.Length; k++)
                {
                    for (int l = 0; l < I.Length; l++)
                    {
                       SnakeOptimization snakeOptimization = new SnakeOptimization(
                            _N: N[k],
                            _T: I[l],
                            _funkcja: TestingFunctions.Functions[j].Funkcja,
                            _dim: TestingFunctions.Functions[j].Dim,
                            _xmin: TestingFunctions.Functions[j].Xmin,
                            _xmax: TestingFunctions.Functions[j].Xmax
                        );
                        for (int i = 0; i < n; i++)
                        //fragment z instrukcji:
                        //Algorytmy maetaheurystyczne należą do grupy probabilistycznych, czyli po każdym uruchomieniu
                        //takiego algorytmu otrzymamy różne wyniki. W przypadku, gdy wyniki te różnią się nieznacznie,
                        //możemy powiedzieć, że algorytm działa stabilnie. Stąd w trakcie testów należy uruchomić algorytm
                        //n razy (np. przyjmijmy n = 10) dla ustalonych ustawień. Na podstawie dziesięciu powtórzeń, jako
                        //wynik należy wybrać najlepsze rozwiązanie (pod względem wartości funkcji celu). Potrzeba również wyznaczyć wskaźnik zwany współczynnikiem zmienności. Wskaźnik ten wyznaczamy dla każdej
                        //zmiennej wektora rozwiązań, jak również dla wartości funkcji celu.
                        {   double[] food_position; double bestFitValue; int iFobj;
                            (food_position, bestFitValue, iFobj) = snakeOptimization.Solve(); //TODO: add {Xfood, fval, gbest, vbest, iFobj}
                            //to returned values
                            //TODO: add logic that will gather all the data from each run and then
                            //decide which one is the best and the worst (based on fval)
                            //data can be easily gathered using method inside the SnakeOptimization class that gathers needed information:
                            //result=snakeOptimization.Result()
                        } 
                            //main.Raport+=result
                            //TODO: add raport generation based on table from the assignment (see https://platforma.polsl.pl/rms/pluginfile.php/235440/mod_resource/content/1/Heurystyki___instrukcja_do_test%C3%B3w.pdf )
                            //this can be easily done using formatted strings
                            //would be nice to export to .csv after that or other format of choice 
                    }
                }
            }
            TestRunner.RunTests();
        }
        
    }
}
