using net1lab;
using System.Diagnostics;

public class TestRunner
{
    private readonly VectorMultiplier _multiplier;
    private readonly int _processorCountP;

    public TestRunner()
    {
        _multiplier = new VectorMultiplier();
        _processorCountP = Environment.ProcessorCount;
    }

    public void RunTest(int sizeN, double scalarK)
    {
        Console.WriteLine($"\nTesting for N = {sizeN:N0} elements:");

        //Setup
        double[] vectorA = Enumerable.Range(0, sizeN).Select(x => (double)x + 1.0).ToArray();

        //1. Single-threaded test (T1) 
        Stopwatch stopwatch = Stopwatch.StartNew();
        double[] resultSingle = _multiplier.SingleThreadedMultiply(vectorA, scalarK);
        stopwatch.Stop();
        long timeSingle = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"\t1-threaded time (T1): {timeSingle} ms");

        //2. Multi-threaded test (TP) 
        _multiplier.MultiThreadedMultiply(vectorA, scalarK); // Warm-up

        stopwatch.Restart();
        double[] resultMulti = _multiplier.MultiThreadedMultiply(vectorA, scalarK);
        stopwatch.Stop();
        long timeMulti = stopwatch.ElapsedMilliseconds;

        Console.WriteLine($"\tMulti-threaded time (TP): {timeMulti} ms (Using threads ~ {_processorCountP})");

        //3. Results Calculation and Display
        CalculateAndDisplayResults(timeSingle, timeMulti, resultSingle, resultMulti);
    }

    private void CalculateAndDisplayResults(
        long timeSingle,
        long timeMulti,
        double[] resultSingle,
        double[] resultMulti)
    {
        // Calculate Speedup (S)
        double speedupS = 0;
        if (timeMulti > 0 && timeSingle > 0)
        {
            speedupS = (double)timeSingle / timeMulti;
        }

        Console.WriteLine($"\tSpeedup (S = T1/TP): {speedupS:F2}x");

        // Calculate Efficiency (E)
        double efficiencyE = speedupS / _processorCountP;
        Console.WriteLine($"\tEfficiency (E = S/P): {efficiencyE:F2}");

        // Check calculation correctness
        bool isCorrect = resultSingle.Take(5).SequenceEqual(resultMulti.Take(5));
        Console.WriteLine($"\tCorrectness Check: {(isCorrect ? "Success" : "FAILURE!")}");
        Console.WriteLine(new string('-', 30));
    }
}

