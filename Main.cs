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

        //N = {10, 20, 40, 80}, I = {5, 10, 20, 40, 60, 80}.
        public static int[] N = { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
        public static int[] I = { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
        //retries
        public static int n = 10;
        public static void TestSnakeOptimization()
        {
            TestRunner.RunTests();
        }
        
    }
}
