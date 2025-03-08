namespace KnapsackProblem.Solvers
{
    public class GeneticSolver<T> : ISolver
    {
        public const int ITERATIONS = 500;
        public const int POPULATION = 100;
        public const int N_POINTS_CROSSOVER = 4;
        public const double MUTATION_PROBABILITY = 0.001; // 0 <= MUTATION_PROBABILITY <= 1 

        public const CrossoverType CROSSOVER_TYPE_SELECTED = CrossoverType.OnePoint;
        public const MutationType MUTATION_TYPE_SELECTED = MutationType.SingleBitInversion;

        protected int[]? populationFitnessScores;

        protected int[,]? populationEncoded; // |0|0|1|1|1|1|0| etc.

        public GeneticSolver()
        {

        }

        protected void CreateInitialPopulation(int n, int variablesAmount, int minVal, int maxVal)
        {
            populationFitnessScores = new int[n];
            populationEncoded = new int[n, variablesAmount];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < variablesAmount; j++)
                {
                    // this has to be determined by the dataset
                    var randVal = new Random().Next(minVal, maxVal + 1);
                    populationEncoded[j, i] = randVal;
                }
            }
        }

        protected void CalculateFitnessScore(Func<int> scoreFunction, int variablesAmount)
        {

        }

        protected void CrossoverParents(CrossoverType crossoverType, int parentAIndex, int parentBIndex)
        {
            switch (crossoverType)
            {
                case CrossoverType.OnePoint:

                    break;
                default:
                    Console.WriteLine("Not implemented.");
                    break;
            }
        }

        protected double CalculateCrossoverProbability(double evaluatedFitness, double fitnessSum)
        {
            if (fitnessSum < 0)
            {
                return 0.0;
            }

            return evaluatedFitness / fitnessSum;
        }

        // mutation probability should be out of the scope
        protected void Mutate(double mutationProbability, MutationType type)
        {
            var mutatorRng = new Random();

            if (!(mutatorRng.NextDouble() < mutationProbability))
            {
                return;
            }

            switch (type)
            {
                case MutationType.SingleBitInversion:

                    break;
                default:
                    Console.WriteLine("Not implemented.");
                    break;
            }
        }

        public int EvaluateFitness()
        {
            return 0;
        }

        public void FindOptimalSolution()
        {
            CreateInitialPopulation(POPULATION);

            var currentIteration = 0;

            while (currentIteration < ITERATIONS)
            {
                // proportional selection

                for (int i = 0; i < POPULATION; i++)
                {
                    var mutatorRng = new Random().NextDouble();


                    var k = 1;
                }

                //CrossoverParents(CROSSOVER_TYPE_SELECTED, )
            }
        }

        public void LoadInput(string data)
        {
        }

        public enum CrossoverType
        {
            OnePoint,
            NPoint,
            Segmented,
            Uniform,
            Shuffle
        }

        public enum MutationType
        {
            SingleBitInversion,
            BitWiseInversion,
            RandomSelection
        }
    }
}
