namespace ProblemSolvers.Solvers.Genetic.Crossoverers.BinaryCrossoverers
{
    public class OnePointCrossoverer : BinaryCrossoverer
    {
        public override int[] CrossoverParents(int[] parentOne, int[] parentTwo)
        {
            var rng = new Random();

            if (parentOne.Length != parentTwo.Length)
            {
                throw new InvalidDataException("Parents have incompatible chromosomes.");
            }

            // the point in which the "chromosome" is cut
            var pos = rng.Next(0, parentOne.Length);
            var child = new int[parentOne.Length];
            for (int j = 0; j < pos; j++)
            {
                child[j] = parentOne[j];
            }

            for (int j = pos; j < parentOne.Length; j++)
            {
                child[j] = parentTwo[j];
            }

            return child;
        }
    }
}
