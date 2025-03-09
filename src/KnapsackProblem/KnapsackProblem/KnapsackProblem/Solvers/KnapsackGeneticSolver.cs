namespace KnapsackProblem.Solvers
{
    public class KnapsackGeneticSolver : ISolver
    {
        private KnapsackProblem _knapsackProblem;


        public const int ITERATIONS = 500;
        public const int POPULATION = 100;
        public const int N_POINTS_CROSSOVER = 4;
        public const double MINIMUM_CROSSOVER_PROBABILITY = 0.1;
        public const double MUTATION_PROBABILITY = 0.001; // 0 <= MUTATION_PROBABILITY <= 1 

        public const CrossoverType CROSSOVER_TYPE_SELECTED = CrossoverType.OnePoint;
        public const MutationType MUTATION_TYPE_SELECTED = MutationType.SingleBitInversion;


        private int[] _populationFitnessScores;
        private double[] _populationSelectionProbability;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

        public KnapsackGeneticSolver(double maxKgs)
        {
            _knapsackProblem = new KnapsackProblem(maxKgs);
            _populationFitnessScores = new int[POPULATION];
            _populationSelectionProbability = new double[POPULATION];
            _populationEncoded = new int[POPULATION][];
            _sumOfFitness = 0;
        }

        public List<KnapsackProblem.Item> TranslateEncodedItemsToItemList(int[] encodedItems)
        {
            var list = new List<KnapsackProblem.Item>();

            for (int itemIndex = 0; itemIndex < encodedItems.Length; itemIndex++)
            {
                if (encodedItems[itemIndex] == 0)
                {
                    continue;
                }
                list.Add(_knapsackProblem.GetItemFromPossibleItems(itemIndex));
            }

            return list;
        }

        public void CreateInitialPopulation()
        {
            _knapsackProblem.SetupProblem();

            var rng = new Random();

            _populationFitnessScores = new int[POPULATION];
            _populationEncoded = new int[POPULATION][];

            for (int i = 0; i < POPULATION; i++)
            {
                _populationEncoded[i] = new int[KnapsackProblem.POSSIBLE_ITEMS_COUNT];
                for (int j = 0; j < KnapsackProblem.POSSIBLE_ITEMS_COUNT; j++)
                {
                    _populationEncoded[i][j] = rng.Next(0, 2);
                }
            }
        }

        public void SetFitnessForPopulation()
        {
            for (int i = 0; i < POPULATION; i++)
            {
                _knapsackProblem.SetupKnapsack(TranslateEncodedItemsToItemList(_populationEncoded[i]));
                _populationFitnessScores[i] = _knapsackProblem.EvaluateFitnessForKnapsack();
            }
        }

        public void CalculateSelectionProbabilityForCurrentPopulation()
        {
            // sum all of the fitness scores
            for (int i = 0; i < POPULATION; i++)
            {
                _sumOfFitness += _populationFitnessScores[i];
            }


            // the best fitness has the biggest probability of being selected
            for (int i = 0; i < POPULATION;i++)
            {
                _populationSelectionProbability[i] = (int)((long)_populationFitnessScores[i] / _sumOfFitness);
            }
        }

        public void LoadInput(string data)
        {
            throw new NotImplementedException();
        }

        public void FindOptimalSolution()
        {
            // populate
            CreateInitialPopulation();

            int i = 1;
            // do
            do
            {
                // evaluate
                _sumOfFitness = 0;
                SetFitnessForPopulation();
                CalculateSelectionProbabilityForCurrentPopulation();

                // proportionalSelection

                // select best fit for the new population
                // crossover / create new population

                // mutate (maybe)


                // while i < iterations
                i++;
            } 
            while (i < ITERATIONS);


            // also find best, and average 
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
