﻿namespace KnapsackProblem.Solvers
{
    public class KnapsackGeneticSolver : ISolver
    {
        private KnapsackProblem _knapsackProblem;

        public const int ITERATIONS = 500;
        //public const int ITERATIONS = 10;
        public const int POPULATION_SIZE = 500;
        public const int N_POINTS_CROSSOVER = 4;
        public const double MINIMUM_CROSSOVER_PROBABILITY = 0.1;
        public const double MUTATION_PROBABILITY = 0.001; // 0 <= MUTATION_PROBABILITY <= 1 

        public const CrossoverType CROSSOVER_TYPE_SELECTED = CrossoverType.OnePoint;
        public const MutationType MUTATION_TYPE_SELECTED = MutationType.SingleBitInversion;


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
                _populationSelectionProbability[i] = (double)_populationFitnessScores[i] / (double)_sumOfFitness;
            }
        }

        // not ok
        private void ProportionalSelectionForNextGen()
        {
            //for (int i = 0; i < POPULATION; i++)
            //{
            //    var probabilityModifier = new Random().NextDouble();

            //    int k = 0;
            //    var currentProbability = _populationSelectionProbability[0];

            //    while (k < POPULATION - 1 && probabilityModifier < currentProbability)
            //    {
            //        k++;
            //        currentProbability = 0;
            //        for(int j = 0; j < k; j++)
            //        {
            //            currentProbability += _populationFitnessScores[j];
            //        }
            //        currentProbability = currentProbability / _sumOfFitness;
            //    }

            //    _populationEncodedNextGen[i] = _populationEncoded[k];
            //}


        }

        private void DoCrossovers(CrossoverType crossoverType)
        {
            switch (crossoverType) { 
                case (CrossoverType.OnePoint):
                    OnePointCrossover();
                    break;
                default:
                    throw new NotImplementedException($"Crossover type {crossoverType} is not implemented.");
            }
        }

        private void OnePointCrossover()
        {
            var rng = new Random();
            for (int i = 0; i < POPULATION_SIZE - 1; i++)
            {
                if (rng.NextDouble() <= MINIMUM_CROSSOVER_PROBABILITY)
                {
                    // the point in which the "chromosome" is cut
                    var pos = rng.Next(0, KnapsackProblem.POSSIBLE_ITEMS_COUNT);
                    var aux = new int[KnapsackProblem.POSSIBLE_ITEMS_COUNT];
                    for (int j = 0; j < pos; j++)
                    {
                        aux[j] = _populationEncodedNextGen[i][j];
                    }

                    for (int j = pos; j < KnapsackProblem.POSSIBLE_ITEMS_COUNT; j++)
                    {
                        aux[j] = _populationEncodedNextGen[i + 1][j];
                    }

                    _populationEncodedNextGen[i] = aux;
                    Console.WriteLine($"Crossover done for chromosomes {i} and {i + 1} at position {pos}.");
                    Console.WriteLine($"Previous population chromosome -> {string.Join("", _populationEncoded[i])}");
                    Console.WriteLine($"Next population chromosome -> {string.Join("", _populationEncodedNextGen[i])}");
                    continue;
                }
                _populationEncodedNextGen[i] = _populationEncoded[i];
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

            int i = 0;
            // do

            while (i < ITERATIONS)
            {
                Console.WriteLine($"Generation {i}");

                // evaluate
                _sumOfFitness = 0;
                SetFitnessForPopulation();
                CalculateSelectionProbabilityForCurrentPopulation();

                // proportional Selection
                ProportionalSelectionForNextGen();

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
