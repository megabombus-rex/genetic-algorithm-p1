using ProblemSolvers.CommonTypes;
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
        private readonly GeneticAlgorithmGenericData _geneticAlgorithmData;

        private readonly SelectionType _selectionTypeSelected = SelectionType.Roulette;
        private readonly CrossoverType _crossoverTypeSelected = CrossoverType.OnePoint;
        private readonly MutationType _mutationTypeSelected = MutationType.SingleBitInversion;

        private readonly RouletteSelector _populationSelector;
        private readonly BinaryCrossoverer _crossoverer;
        private readonly BinaryMutator _mutator;

        private int[] _populationFitnessScores;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

        private int _currentIteration;
        private BestKnapsackData _bestKnapsackData;

        public KnapsackGeneticSolver(KnapsackProblem knapsackProblem, SelectionType selectionType, CrossoverType crossoverType, MutationType mutationType, GeneticAlgorithmGenericData algorithmData)
        {
            _knapsackProblem = knapsackProblem;
            _geneticAlgorithmData = algorithmData;

            _selectionTypeSelected = selectionType;
            _crossoverTypeSelected = crossoverType;
            _mutationTypeSelected = mutationType;

            _populationSelector = new RouletteSelector();
            _crossoverer = new BinaryCrossoverer();
            _mutator = new BinaryMutator();

            _populationFitnessScores = new int[algorithmData.PopulationSize];
            _populationEncoded = new int[algorithmData.PopulationSize][];
            _populationEncodedNextGen = new int[algorithmData.PopulationSize][];
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
                    Console.WriteLine($"New best knapsack found. Iteration {_currentIteration}. Fitness = {_populationFitnessScores[i]}. Chromosome: {string.Join("", _populationEncoded[i])}.");
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
                    return _populationSelector.SelectParentIndexByRoulette(_populationFitnessScores, _sumOfFitness);
                default:
                    throw new NotImplementedException($"Selection type {selectionType} not implemented.");

            }
        }

        private int[] CrossoverTwoParents(int p1Index, int p2Index, CrossoverType crossoverType)
        {
            switch (crossoverType)
            {
                case CrossoverType.OnePoint:
                    return _crossoverer.OnePointCrossover(_populationEncoded[p1Index], _populationEncoded[p2Index]);
                default:
                    throw new NotImplementedException($"Crossover type {crossoverType} is not implemented.");
            }
        }

        private int[] MutateIndividual(int[] individual, MutationType mutationType)
        {
            switch (mutationType)
            {
                case (MutationType.SingleBitInversion):
                    return _mutator.SingleBitInversionMutation(individual);
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
            SumFitnessesOfPopulation();

            // do
            _currentIteration = 0;

            var rng = new Random();

            while (_currentIteration < _geneticAlgorithmData.GenerationsAmount)
            {
                Console.WriteLine($"Generation {_currentIteration}");

                // evaluate
                _sumOfFitness = 0;
                SetFitnessForPopulation();
                SumFitnessesOfPopulation();

                // index keeping the currently selected 'individual' from the next population
                int nextPopulationIndex = 0;
                while (nextPopulationIndex < POPULATION_SIZE)
                {
                    // proportional Selection
                    var parent1Index = SelectParentIndexForNextPopulation(_selectionTypeSelected);
                    var parent2Index = parent1Index;

                    // crossover or basic replication
                    if (rng.NextDouble() < CROSSOVER_PROBABILITY)
                    {
                        parent2Index = SelectParentIndexForNextPopulation(_selectionTypeSelected);

                        var crossoveredIndividual = CrossoverTwoParents(parent1Index, parent2Index, _crossoverTypeSelected);

                        for (int j = 0; j < _populationEncodedNextGen[nextPopulationIndex].Length; j++)
                        {
                            _populationEncodedNextGen[nextPopulationIndex][j] = crossoveredIndividual[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _populationEncodedNextGen[nextPopulationIndex].Length; j++)
                        {
                            _populationEncodedNextGen[nextPopulationIndex][j] = _populationEncoded[parent1Index][j];
                        }
                    }

                    // mutate
                    if (rng.NextDouble() < MUTATION_PROBABILITY)
                    {
                        MutateIndividual(_populationEncodedNextGen[nextPopulationIndex], _mutationTypeSelected);
                    }

                    nextPopulationIndex++;
                }


                // select best fit for the new population
                // crossover / create new population
                //DoCrossovers(CROSSOVER_TYPE_SELECTED);

                // mutate (maybe)

                //Console.WriteLine($"Current sum of fitnesses: {_sumOfFitness}");
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
