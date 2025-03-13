using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators;
using ProblemSolvers.Solvers.Genetic.Selectors;

namespace ProblemSolvers.Solvers.Genetic
{
    public class KnapsackGeneticSolver : ISolver
    {
        private KnapsackProblem _knapsackProblem;

        public const int ITERATIONS = 500;
        public const int POPULATION_SIZE = 500;
        public const int N_POINTS_CROSSOVER = 4;
        public const double CROSSOVER_PROBABILITY = 0.7;
        public const double MUTATION_PROBABILITY = 0.001; // 0 <= MUTATION_PROBABILITY <= 1 

        public const CrossoverType CROSSOVER_TYPE_SELECTED = CrossoverType.OnePoint;
        public const MutationType MUTATION_TYPE_SELECTED = MutationType.SingleBitInversion;
        public const SelectionType SELECTION_TYPE_SELECTED = SelectionType.Roulette;


        private int[] _populationFitnessScores;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

        private int _currentIteration;
        private BestKnapsackData _bestKnapsackData;

        public KnapsackGeneticSolver(KnapsackProblem knapsackProblem)
        {
            _knapsackProblem = knapsackProblem;
            _populationFitnessScores = new int[POPULATION_SIZE];
            _populationEncoded = new int[POPULATION_SIZE][];
            _populationEncodedNextGen = new int[POPULATION_SIZE][];
            _bestKnapsackData = new BestKnapsackData(KnapsackProblem.POSSIBLE_ITEMS_COUNT);
            _currentIteration = 0;
            _sumOfFitness = 0;
        }

        private List<KnapsackProblem.Item> TranslateEncodedItemsToItemList(int[] encodedItems)
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

        private void CreateInitialPopulation()
        {
            var rng = new Random();


            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                _populationEncoded[i] = new int[KnapsackProblem.POSSIBLE_ITEMS_COUNT];
                _populationEncodedNextGen[i] = new int[KnapsackProblem.POSSIBLE_ITEMS_COUNT];
                for (int j = 0; j < KnapsackProblem.POSSIBLE_ITEMS_COUNT; j++)
                {
                    _populationEncoded[i][j] = rng.Next(0, 2);
                }
            }
        }

        private void SetFitnessForPopulation()
        {
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                _knapsackProblem.SetupProblem(TranslateEncodedItemsToItemList(_populationEncoded[i]));
                _populationFitnessScores[i] = _knapsackProblem.CalculateFitnessOfProblem();
            }

            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                if (_populationFitnessScores[i] > _bestKnapsackData.Fitness)
                {
                    _bestKnapsackData.UpdateBestKnapsack(_currentIteration, _populationFitnessScores[i], _populationEncoded[i]);
                }
            }
        }

        private void SumFitnessesOfPopulation()
        {
            // sum all of the fitness scores
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                _sumOfFitness += _populationFitnessScores[i];
            }
        }

        // not ok
        private int SelectParentIndexForNextPopulation(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.Roulette:
                    return RouletteSelector.SelectParentIndexByRoulette(_populationFitnessScores, _sumOfFitness);
                default:
                    throw new NotImplementedException($"Selection type {selectionType} not implemented.");

            }
        }

        private int[] CrossoverTwoParents(int p1Index, int p2Index, CrossoverType crossoverType)
        {
            switch (crossoverType)
            {
                case CrossoverType.OnePoint:
                    return BinaryCrossoverer.OnePointCrossover(_populationEncoded[p1Index], _populationEncoded[p2Index]);
                default:
                    throw new NotImplementedException($"Crossover type {crossoverType} is not implemented.");
            }
        }

        private int[] MutateIndividual(int[] individual, MutationType mutationType)
        {
            switch (mutationType)
            {
                case (MutationType.SingleBitInversion):
                    return BinaryMutator.SingleBitInversionMutation(individual);
                default:
                    throw new NotImplementedException($"Mutation {mutationType} not implemented.");
            }
        }

        private void SetNewPopulationAsCurrent()
        {
            for (int i = 0; i < POPULATION_SIZE - 1; i++)
            {
                _populationEncoded[i] = _populationEncodedNextGen[i];
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
            SetFitnessForPopulation();

            // do
            _currentIteration = 0;

            var rng = new Random();

            while (_currentIteration < ITERATIONS)
            {
                Console.WriteLine($"Generation {_currentIteration}");

                // evaluate
                _sumOfFitness = 0;
                SumFitnessesOfPopulation();

                // index keeping the currently selected 'individual' from the next population
                int nextPopulationIndex = 0;
                while (nextPopulationIndex < POPULATION_SIZE)
                {
                    // proportional Selection
                    var parent1Index = SelectParentIndexForNextPopulation(SELECTION_TYPE_SELECTED);
                    var parent2Index = parent1Index;

                    // crossover or basic replication
                    if (rng.NextDouble() < CROSSOVER_PROBABILITY)
                    {
                        parent2Index = SelectParentIndexForNextPopulation(SELECTION_TYPE_SELECTED);

                        var crossoveredIndividual = CrossoverTwoParents(parent1Index, parent2Index, CROSSOVER_TYPE_SELECTED);

                        for (int j = 0; j < _populationEncodedNextGen[_currentIteration].Length; j++)
                        {
                            _populationEncodedNextGen[_currentIteration][j] = crossoveredIndividual[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _populationEncodedNextGen[_currentIteration].Length; j++)
                        {
                            _populationEncodedNextGen[_currentIteration][j] = _populationEncoded[parent1Index][j];
                        }
                    }

                    // mutate
                    if (rng.NextDouble() < MUTATION_PROBABILITY)
                    {
                        MutateIndividual(_populationEncodedNextGen[_currentIteration], MUTATION_TYPE_SELECTED);
                    }

                    nextPopulationIndex++;
                }


                // select best fit for the new population
                // crossover / create new population
                //DoCrossovers(CROSSOVER_TYPE_SELECTED);

                // mutate (maybe)

                Console.WriteLine($"Current sum of fitnesses: {_sumOfFitness}");
                // while i < iterations
                SetNewPopulationAsCurrent();
                SetFitnessForPopulation();

                _currentIteration++;
            }

            Console.WriteLine($"Best fitness occured in iteration {_bestKnapsackData.Iteration} for: {string.Join("", _bestKnapsackData.Genome)} with fitness score: {_bestKnapsackData.Fitness}.");

            // also find best, and average 
        }
    }

    public class BestKnapsackData
    {
        public int Iteration;
        public long Fitness;
        public int[] Genome;

        public BestKnapsackData(int genomeSize)
        {
            Iteration = 0;
            Fitness = 0;
            Genome = new int[genomeSize];
        }

        public void UpdateBestKnapsack(int iteration, long fitness, int[] genome)
        {
            Iteration = iteration;
            Fitness = fitness;
            Array.ConstrainedCopy(genome, 0, Genome, 0, Genome.Length);
        }
    }
}
