using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeOptimization
{
    public class TestRunner
    {
        public static void RunTests()
        {
            string reportFilePath = "raport.csv";

            List<TestResult> testResults = new List<TestResult>();
            List<TestResult> allTestResults = new List<TestResult>();
            foreach (var testFunction in TestingFunctions.Functions)
            {
                foreach (var nValue in Main.N)
                {
                    foreach (var tValue in Main.I)
                    {
                        SnakeOptimization snakeOptimization = new SnakeOptimization(
                            _N: nValue,
                            _T: tValue,
                            _funkcja: testFunction.Funkcja,
                            _dim: testFunction.Dim,
                            _xmin: testFunction.Xmin,
                            _xmax: testFunction.Xmax
                        );
                        List<TestResult> LocalIterationsResults = new List<TestResult>();
                        double topBestFitValue=0 ;
                        int topBestFitValueIndex=0;
                        double worstBestFitValue=0;
                        int worstBestFitValueIndex=0;
                        bool first_pass = true;
                        for (int i = 0; i < Main.n; i++)
                        {
                            double[] food_position;
                            double bestFitValue;
                            int iFobj;
                            //masure execution time of snakeOptimization.Solve() and print it to stdout
                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            (food_position, bestFitValue, iFobj) = snakeOptimization.Solve(); //TODO: add {Xfood, fval, gbest, vbest, iFobj}
                            watch.Stop();
                            var elapsedMs = watch.ElapsedMilliseconds;
                            //to returned values
                            //if topBestfitValue unusigned then assign it to bestFitValue
                            if (first_pass)
                            {
                                topBestFitValue = bestFitValue;
                                topBestFitValueIndex = i;
                                worstBestFitValue = bestFitValue;
                                worstBestFitValueIndex = i;
                                first_pass = false;
                            }
                            TestResult testResult = new TestResult
                            {
                                AlgorithmName = "SnakeOptimization",
                                TestFunctionName = testFunction.Funkcja.Method.Name,
                                NumberOfParameters = testFunction.Dim,
                                NumberOfIterations = tValue, //or iFobj?
                                PopulationSize = nValue,
                                FoundMinimum = food_position,
                                FoundMinimumString = $"[{String.Join("; ",food_position)}]",
                                ObjectiveValue = bestFitValue, //wartość funkcji celu
                                CoeffOfVarObjectiveValue = 0,
                                NumberOfFobjEvaluation = iFobj,
                                ExecutionTime = elapsedMs.ToString()
                            };


                            // wynik na liście
                            LocalIterationsResults.Add(testResult);
                            allTestResults.Add(testResult);
                            if (bestFitValue > topBestFitValue)
                            {
                                topBestFitValue = bestFitValue;
                                topBestFitValueIndex = i;
                            }
                            if (bestFitValue < worstBestFitValue)
                            {
                                worstBestFitValue = bestFitValue;
                                worstBestFitValueIndex = i;
                            }


                            //log formatted result to stdout
                            Console.WriteLine($"TestFunctionName: {testResult.TestFunctionName}, NumberOfParameters: {testResult.NumberOfParameters}, NumberOfIterations: {testResult.NumberOfIterations}, PopulationSize: {testResult.PopulationSize}, FoundMinimum: {testResult.FoundMinimumString}, CoeffOfVarParameters: {testResult.CoeffOfVarParameters}, ObjectiveValue: {testResult.ObjectiveValue}, CoeffOfVarObjectiveValue: {testResult.CoeffOfVarObjectiveValue}");
                            Console.WriteLine($"Execution time of single function call: {elapsedMs} ms");
                        }   

                            // calculate CoeffOfVarObjectiveValue and CoeffOfVarParameters for topBestFitValueIndex and worstBestFitValueIndex
                            double coefVarObjectiveValue, stdDevObjectiveValue;
                            (coefVarObjectiveValue, stdDevObjectiveValue)=Tools.CoefOfVariation(LocalIterationsResults.Select(x => x.ObjectiveValue).ToArray());

                            LocalIterationsResults[topBestFitValueIndex].CoeffOfVarObjectiveValue = coefVarObjectiveValue;
                            LocalIterationsResults[topBestFitValueIndex].StdDevObjectiveValue = stdDevObjectiveValue;
                            
                            LocalIterationsResults[worstBestFitValueIndex].CoeffOfVarObjectiveValue = coefVarObjectiveValue;
                            LocalIterationsResults[worstBestFitValueIndex].StdDevObjectiveValue = stdDevObjectiveValue;
                            
                            var CoefOfVarParameters = new List<double>();
                            var stdDevParameters = new List<double>();
                            for (int i = 0; i < LocalIterationsResults[0].FoundMinimum.Length; i++)
                            {  
                                double coefOfVar, stdDev;
                                (coefOfVar, stdDev)=Tools.CoefOfVariation(LocalIterationsResults.Select(x => x.FoundMinimum[i]).ToArray());
                                CoefOfVarParameters.Add(coefOfVar);
                                stdDevParameters.Add(stdDev);
                            }

                            LocalIterationsResults[topBestFitValueIndex].CoeffOfVarParameters = $"[{String.Join("; ",CoefOfVarParameters)}]";
                            LocalIterationsResults[topBestFitValueIndex].StdDevParameters = $"[{String.Join("; ",stdDevParameters)}]";

                            LocalIterationsResults[worstBestFitValueIndex].CoeffOfVarParameters = $"[{String.Join(", ",CoefOfVarParameters)}]";
                            LocalIterationsResults[worstBestFitValueIndex].StdDevParameters = $"[{String.Join("; ",stdDevParameters)}]";

                            //log CoeffOfVarParameters to stdout
                            // Console.WriteLine($"CoeffOfVarParameters:[{String.Join(", ",CoefOfVarParameters)}]");
                            // assign best and worst results to testResult
                            testResults.Add(LocalIterationsResults[topBestFitValueIndex]);
                            testResults.Add(LocalIterationsResults[worstBestFitValueIndex]);
                    }
                }
            }

            // Zapisz CSV
            var config = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            using (var writer = new StreamWriter(reportFilePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(testResults); 
                // based on table from the assignment (see https://platforma.polsl.pl/rms/pluginfile.php/235440/mod_resource/content/1/Heurystyki___instrukcja_do_test%C3%B3w.pdf)
            }
            using (var writer = new StreamWriter("all"+reportFilePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(allTestResults); 
            }
        }
    }
}

