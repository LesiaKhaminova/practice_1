
namespace net1lab
{
    public class VectorMultiplier
    {
        public double[] SingleThreadedMultiply(double[] vector, double scalarK) 
        {
            int N = vector.Length;
            double[] resultVector = new double[N];

            for (int i = 0; i < N; i++)
            {
                resultVector[i] = vector[i] * scalarK;
            }

            return resultVector;
        }

        public double[] MultiThreadedMultiply(double[] vector, double scalarK)
        {
            int N = vector.Length;
            double[] resultVector = new double[N];

            Parallel.For(0, N, i =>
            {
                resultVector[i] = vector[i] * scalarK;
            });

            return resultVector;
        }
    }
}


