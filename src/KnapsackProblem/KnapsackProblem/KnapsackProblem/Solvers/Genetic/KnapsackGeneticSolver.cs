using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.BinaryCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;

namespace ProblemSolvers.Solvers.Genetic
{
    //  My own implementation based off the information gathered on video by Kie: https://www.youtube.com/watch?v=uQj5UNhCPuo&ab_channel=KieCodes
    //  The information was gathered from Genetic Algorithms: Theory and Applications, Lecture Notes, Third Edition—Winter 2003/2004 by Ulrich Bodenhofer
    public class KnapsackGeneticSolver : ISolver<BestKnapsackData>
    {
        private KnapsackProblem _knapsackProblem;
        private readonly GeneticAlgorithmGenericData _geneticAlgorithmData;

        private readonly SelectionType _selectionTypeSelected = SelectionType.Roulette;

        private readonly BinaryCrossoverer _crossoverer;
        private readonly BinaryMutator _mutator; // or a binary mutator

        private int[] _populationFitnessScores;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

        private int _currentIteration;
        private BestKnapsackData _bestKnapsackData;

        public KnapsackGeneticSolver(KnapsackProblem knapsackProblem, SelectionType selectionType, BinaryCrossoverer crossoverer, BinaryMutator mutator, GeneticAlgorithmGenericData algorithmData)
        {
            _knapsackProblem = knapsackProblem;
            _geneticAlgorithmData = algorithmData;

            _selectionTypeSelected = selectionType;

            _crossoverer = crossoverer;
            _mutator = mutator;

            _populationFitnessScores = new int[algorithmData.PopulationSize];
            _populationEncoded = new int[algorithmData.PopulationSize][];
            _populationEncodedNextGen = new int[algorithmData.PopulationSize][];
            _bestKnapsackData = new BestKnapsackData(_knapsackProblem.PossibleItemCount);
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

            for (int i = 0; i < _geneticAlgorithmData.PopulationSize; i++)
            {
                _populationEncoded[i] = new int[_knapsackProblem.PossibleItemCount];
                _populationEncodedNextGen[i] = new int[_knapsackProblem.PossibleItemCount];
                for (int j = 0; j < _knapsackProblem.PossibleItemCount; j++)
                {
                    _populationEncoded[i][j] = rng.Next(0, 2);
                }
            }
        }

        private void SetFitnessForPopulation()
        {
            for (int i = 0; i < _geneticAlgorithmData.PopulationSize; i++)
            {
                _knapsackProblem.SetupProblem(TranslateEncodedItemsToItemList(_populationEncoded[i]));
                _populationFitnessScores[i] = _knapsackProblem.CalculateFitnessOfProblem();
            }

            for (int i = 0; i < _geneticAlgorithmData.PopulationSize; i++)
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
            for (int i = 0; i < _geneticAlgorithmData.PopulationSize; i++)
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
                    return RouletteSelector.SelectParentIndexByRoulette(_populationFitnessScores, _sumOfFitness, KnapsackProblem.FitnessType);
                case SelectionType.Tournament:
                    return TournamentSelector.SelectParentIndexByTournament(_populationEncoded, _populationFitnessScores, KnapsackProblem.FitnessType);
                default:
                    throw new NotImplementedException($"Selection type {selectionType} not implemented.");

            }
        }

        private void SetNewPopulationAsCurrent()
        {
            for (int i = 0; i < _geneticAlgorithmData.PopulationSize - 1; i++)
            {
                _populationEncoded[i] = _populationEncodedNextGen[i];
            }
        }

        public void LoadInput(string data)
        {
            throw new NotImplementedException();
        }

        public BestKnapsackData FindOptimalSolution()
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
                while (nextPopulationIndex < _geneticAlgorithmData.PopulationSize)
                {
                    // proportional Selection
                    var parent1Index = SelectParentIndexForNextPopulation(_selectionTypeSelected);
                    var parent2Index = parent1Index;

                    // crossover or basic replication
                    if (rng.NextDouble() < _geneticAlgorithmData.CrossoverProbability)
                    {
                        parent2Index = SelectParentIndexForNextPopulation(_selectionTypeSelected);

                        var crossoveredIndividual = _crossoverer.CrossoverParents(_populationEncoded[parent1Index], _populationEncoded[parent2Index]);

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
                    if (rng.NextDouble() < _geneticAlgorithmData.MutationProbability)
                    {
                        _mutator.MutateIndividual(_populationEncodedNextGen[nextPopulationIndex]);
                    }

                    nextPopulationIndex++;
                }

                //Console.WriteLine($"Current sum of fitnesses: {_sumOfFitness}");
                // while i < iterations
                SetNewPopulationAsCurrent();
                SetFitnessForPopulation();

                _currentIteration++;
            }

            Console.WriteLine($"Genetic Algorithm:\nBest fitness occured in iteration {_bestKnapsackData.Iteration} for: {string.Join("", _bestKnapsackData.Genome)} with fitness score: {_bestKnapsackData.Fitness}.");
            return _bestKnapsackData;
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
