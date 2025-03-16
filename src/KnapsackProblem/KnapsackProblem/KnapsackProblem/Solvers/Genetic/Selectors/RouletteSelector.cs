namespace ProblemSolvers.Solvers.Genetic.Selectors
{
    public class RouletteSelector
    {
        public int SelectParentIndexByRoulette(int[] populationFitnessScores, long fitnessSum)
        {
            var randomVal = new Random().NextInt64(0, fitnessSum);
            long currentFitnessSum = 0;

            int i = 0;

            for (i = 0; i < populationFitnessScores.Length; i++)
            {
                currentFitnessSum += populationFitnessScores[i];
                if (currentFitnessSum > randomVal)
                {
                    return i;
                }
            }

            return i;
        }
    }
}
