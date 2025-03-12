using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.Genetic
{
    public class KnapsackGeneticSolver : ISolver
    {
        private KnapsackProblem _knapsackProblem;

        public const int ITERATIONS = 500;
        //public const int ITERATIONS = 10;
        public const int POPULATION_SIZE = 500;
        public const int N_POINTS_CROSSOVER = 4;
        public const double CROSSOVER_PROBABILITY = 0.4;
        public const double MUTATION_PROBABILITY = 0.001; // 0 <= MUTATION_PROBABILITY <= 1 

        public const CrossoverType CROSSOVER_TYPE_SELECTED = CrossoverType.OnePoint;
        public const MutationType MUTATION_TYPE_SELECTED = MutationType.SingleBitInversion;
        public const SelectionType SELECTION_TYPE_SELECTED = SelectionType.Roulette;


        private int[] _populationFitnessScores;
        private double[] _populationSelectionProbability;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

        public KnapsackGeneticSolver(double maxKgs)
        {
            _knapsackProblem = new KnapsackProblem(maxKgs);
            _populationFitnessScores = new int[POPULATION_SIZE];
            _populationSelectionProbability = new double[POPULATION_SIZE];
            _populationEncoded = new int[POPULATION_SIZE][];
            _populationEncodedNextGen = new int[POPULATION_SIZE][];
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
                _knapsackProblem.SetupKnapsack(TranslateEncodedItemsToItemList(_populationEncoded[i]));
                _populationFitnessScores[i] = _knapsackProblem.EvaluateFitnessForKnapsack();
            }
        }

        private void CalculateSelectionProbabilityForCurrentPopulation()
        {
            // sum all of the fitness scores
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                _sumOfFitness += _populationFitnessScores[i];
            }


            // the best fitness has the biggest probability of being selected
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                _populationSelectionProbability[i] = _populationFitnessScores[i] / (double)_sumOfFitness;
            }
        }

        // not ok
        private int SelectParentIndexForNextPopulation(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.Roulette:
                    return SelectParentIndexByRoulette(_populationFitnessScores, _sumOfFitness);
                default:
                    throw new NotImplementedException($"Selection type {selectionType} not implemented.");

            }
        }

        private int SelectParentIndexByRoulette(int[] populationFitnessScores, long fitnessSum)
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


        private int[] CrossoverTwoParents(int p1Index, int p2Index, CrossoverType crossoverType)
        {
            switch (crossoverType)
            {
                case CrossoverType.OnePoint:
                    return OnePointCrossover(_populationEncoded[p1Index], _populationEncoded[p2Index]);
                default:
                    throw new NotImplementedException($"Crossover type {crossoverType} is not implemented.");
            }
        }

        private int[] OnePointCrossover(int[] parent1, int[] parent2)
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

        private int[] MutateIndividual(int[] individual, MutationType mutationType)
        {
            switch (mutationType)
            {
                case (MutationType.SingleBitInversion):
                    return SingleBitInversionMutation(individual);
                default:
                    throw new NotImplementedException($"Mutation {mutationType} not implemented.");
            }
        }


        // viable for 01 representations
        private int[] SingleBitInversionMutation(int[] individual)
        {
            var randomIndex = new Random().Next(0, individual.Length);
            individual[randomIndex] = individual[randomIndex] == 0 ? 1 : 0;

            return individual;
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

            int i = 0;
            // do

            var rng = new Random();

            while (i < ITERATIONS)
            {
                Console.WriteLine($"Generation {i}");

                // evaluate
                _sumOfFitness = 0;
                SetFitnessForPopulation();
                CalculateSelectionProbabilityForCurrentPopulation();

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

                        for (int j = 0; j < _populationEncodedNextGen[i].Length; j++)
                        {
                            _populationEncodedNextGen[i][j] = crossoveredIndividual[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < _populationEncodedNextGen[i].Length; j++)
                        {
                            _populationEncodedNextGen[i][j] = _populationEncoded[parent1Index][j];
                        }
                    }

                    // mutate
                    if (rng.NextDouble() < MUTATION_PROBABILITY)
                    {
                        MutateIndividual(_populationEncodedNextGen[i], MUTATION_TYPE_SELECTED);
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

                i++;
            }


            // also find best, and average 
        }
    }
}
