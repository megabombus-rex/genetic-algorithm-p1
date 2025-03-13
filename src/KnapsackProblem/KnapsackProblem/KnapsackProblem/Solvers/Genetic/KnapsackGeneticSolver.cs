﻿using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.Genetic
{
    public class KnapsackGeneticSolver : ISolver
    {
        private KnapsackProblem _knapsackProblem;

        public const int ITERATIONS = 500;
        public const int POPULATION_SIZE = 500;
        public const int N_POINTS_CROSSOVER = 4;
        public const double CROSSOVER_PROBABILITY = 0.4;
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

        public KnapsackGeneticSolver(double maxKgs)
        {
            _knapsackProblem = new KnapsackProblem(maxKgs);
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
                _knapsackProblem.SetupKnapsack(TranslateEncodedItemsToItemList(_populationEncoded[i]));
                _populationFitnessScores[i] = _knapsackProblem.EvaluateFitnessForKnapsack();
            }

            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                if (_populationFitnessScores[i] > _bestKnapsackData.Fitness)
                {
                    _bestKnapsackData.UpdateBestKnapsack(_currentIteration, _populationFitnessScores[i], _populationEncoded[i]);
                }
            }
        }

        private void CalculateSelectionProbabilityForCurrentPopulation()
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
            SetFitnessForPopulation();

            // do
            _currentIteration = 0;

            var rng = new Random();

            while (_currentIteration < ITERATIONS)
            {
                Console.WriteLine($"Generation {_currentIteration}");

                // evaluate
                _sumOfFitness = 0;
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
