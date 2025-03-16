namespace ProblemSolvers.Solvers.Genetic.Crossoverers
{
    public class BinaryCrossoverer
    {
        public int[] OnePointCrossover(int[] parent1, int[] parent2)
        {
            var rng = new Random();

            if (parent1.Length != parent2.Length)
            {
                throw new InvalidDataException("Parents have incompatible chromosomes.");
            }

            // the point in which the "chromosome" is cut
            var pos = rng.Next(0, parent1.Length);
            var child = new int[parent1.Length];
            for (int j = 0; j < pos; j++)
            {
                child[j] = parent1[j];
            }

            for (int j = pos; j < parent1.Length; j++)
            {
                child[j] = parent2[j];
            }

            return child;
        }
    }
}
