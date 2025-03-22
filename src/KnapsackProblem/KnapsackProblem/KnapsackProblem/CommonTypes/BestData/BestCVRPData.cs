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

        public virtual void UpdateBestCVRPData(int iteration, double fitness, int[] genome)
        {
            Iteration = iteration;
            Fitness = fitness;
            Array.Copy(genome, Genome, Genome.Length);
        }

        public virtual void DisplayBestData(string algorithmUsed)
        {
            Console.WriteLine($"{algorithmUsed}:\nBest fitness occured in iteration {Iteration} for: [{string.Join("|", Genome)}] with fitness score: {Fitness}.");
        }
    }

    public class BestCVRPDataSimulatedAnnealing : BestCVRPData
    {
        public double Temperature;

        public BestCVRPDataSimulatedAnnealing(int genomeSize) : base(genomeSize)
        {
            Temperature = double.MaxValue;
        }

        public void UpdateBestCVRPData(int iteration, double fitness, int[] genome, double temperature)
        {
            base.UpdateBestCVRPData(iteration, fitness, genome);
            Temperature = temperature;
        }

        public override void DisplayBestData(string algorithmUsed)
        {
            Console.WriteLine($"{algorithmUsed}:\nBest fitness occured in iteration {Iteration} at temperature T={Temperature} for: [{string.Join("|", Genome)}] with fitness score: {Fitness}.");
        }
    }
}
