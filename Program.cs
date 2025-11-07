public class Program
{
    private static void Main(string[] args)
    {
        // Array of vector sizes to test
        int[] vectorSizesN = { 100000, 1000000, 10000000, 100000000 };
        double scalarK = 5.5;

        // Initialization
        TestRunner runner = new TestRunner();

        Console.WriteLine($"Laboratory Work No.1. Task 5a: Vector-Scalar Multiplication (K = {scalarK})");
        Console.WriteLine($"Logical CPU Cores (P): {Environment.ProcessorCount}");
        Console.WriteLine(new string('=', 50));

        // 2. Execute Tests
        foreach (int sizeN in vectorSizesN)
        {
            runner.RunTest(sizeN, scalarK);
        }
    }
}



