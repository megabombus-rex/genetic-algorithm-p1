using System.Text;

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

        public virtual BestCVRPData Clone()
        {
            var clone = new BestCVRPData(Genome.Length);
            clone.Iteration = Iteration;
            clone.Fitness = Fitness;
            Array.Copy(Genome, clone.Genome, this.Genome.Length);
            return clone;
        }

        public void Clear()
        {
            for (int i = 0; i < Genome.Length; i++)
            {
                Genome[i] = 0;
            }
            Fitness = double.MaxValue;
            Iteration = 0;
        }

        public virtual void UpdateBestCVRPData(int iteration, double fitness, int[] genome)
        {
            Iteration = iteration;
            Fitness = fitness;
            //Array.Copy(genome, Genome, Genome.Length);
            Buffer.BlockCopy(genome, 0, Genome, 0, Genome.Length * sizeof(int));
        }

        public virtual void DisplayBestData(string algorithmUsed)
        {
            Console.WriteLine($"{algorithmUsed}:\nBest fitness occured in iteration {Iteration} for: [{string.Join("|", Genome)}] with fitness score: {Fitness}.");
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Iteration);
            sb.Append(',');
            sb.Append(Fitness);
            sb.Append(",");
            sb.Append('[' + string.Join("|", Genome) + ']');

            return sb.ToString();
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


        public override BestCVRPData Clone()
        {
            var clone = new BestCVRPDataSimulatedAnnealing(Genome.Length);
            clone.Temperature = Temperature;
            clone.Fitness = Fitness;
            clone.Iteration = Iteration;
            Array.Copy(Genome, clone.Genome, this.Genome.Length);

            return clone;
        }

        public override void DisplayBestData(string algorithmUsed)
        {
            Console.WriteLine($"{algorithmUsed}:\nBest fitness occured in iteration {Iteration} at temperature T={Temperature} for: [{string.Join("|", Genome)}] with fitness score: {Fitness}.");
        }
    }
}
