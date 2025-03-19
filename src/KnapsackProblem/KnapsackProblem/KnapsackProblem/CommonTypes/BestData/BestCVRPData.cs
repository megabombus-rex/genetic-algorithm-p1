namespace ProblemSolvers.CommonTypes.BestData
{
    public class BestCVRPData
    {
        public int Iteration;
        public double Fitness;
        public int[] Genome;

        public BestCVRPData(int genomeSize)
        {
            Genome = new int[genomeSize];
            Fitness = double.MaxValue;
        }

        public void UpdateBestCVRPData(int iteration, double fitness, int[] genome)
        {
            Iteration = iteration;
            Fitness = fitness;
            Array.ConstrainedCopy(genome, 0, Genome, 0, Genome.Length);
        }
    }
}
