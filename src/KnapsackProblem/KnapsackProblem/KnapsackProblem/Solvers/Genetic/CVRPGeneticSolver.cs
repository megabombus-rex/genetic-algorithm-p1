using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;

namespace ProblemSolvers.Solvers.Genetic
{
    // own implementation based off KnapsackGeneticSolver
    public class CVRPGeneticSolver : ISolver<BestCVRPData>
    {
        private readonly SelectionType _selectionType;
        private readonly CombinatoralCrossoverer _crossoverer;
        private readonly CombinatoralMutator _mutator;

        private readonly GeneticAlgorithmGenericData _algorithmData;

        private CVRProblem _problem;

        private double[] _populationFitnessScores;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private double _sumOfFitness;

        private int _currentIteration;
        private BestCVRPData _bestCVRPData;

        public CVRPGeneticSolver(SelectionType selectionType, CombinatoralCrossoverer crossoverer, CombinatoralMutator mutator, 
            GeneticAlgorithmGenericData data, CVRProblem problem)
        {
            _selectionType = selectionType;
            _crossoverer = crossoverer;
            _mutator = mutator;

            _algorithmData = data;
            _problem = problem;

            _populationEncoded = new int[data.PopulationSize][];
            _populationEncodedNextGen = new int[data.PopulationSize][];
            _populationFitnessScores = new double[data.PopulationSize];
            _sumOfFitness = 0;

            _currentIteration = 0;
            _bestCVRPData = new BestCVRPData(_problem.CitiesCount);
        }


        public BestCVRPData FindOptimalSolution()
        {

            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = 0.");
                return _bestCVRPData;
            }

            // Create population
            CreateInitialPopulation();

            // evaluate first population
            EvaluatePopulation();

            _currentIteration = 0;

            var rng = new Random();

            while (_currentIteration < _algorithmData.GenerationsAmount)
            {
                Console.WriteLine($"Generation {_currentIteration}.");

                // select parents
                // index keeping the currently selected 'individual' from the next population
                int nextPopulationIndex = 0;
                while (nextPopulationIndex < _algorithmData.PopulationSize)
                {

                    // proportional Selection
                    var parent1Index = SelectParentIndexForNextPopulation(_selectionType);
                    var parent2Index = parent1Index;

                    // crossover or basic replication
                    if (rng.NextDouble() < _algorithmData.CrossoverProbability)
                    {
                        parent2Index = SelectParentIndexForNextPopulation(_selectionType);

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
                    if (rng.NextDouble() < _algorithmData.MutationProbability)
                    {
                        _mutator.MutateIndividual(_populationEncodedNextGen[nextPopulationIndex]);
                    }

                    nextPopulationIndex++;
                }
                
                // replace population
                ReplacePopulation();

                // evaluate new population
                _sumOfFitness = 0;
                EvaluatePopulation();
                _currentIteration++;
            }

            Console.WriteLine($"Genetic Algorithm:\nBest fitness occured in iteration {_bestCVRPData.Iteration} for: [{string.Join("|", _bestCVRPData.Genome)}] with fitness score: {_bestCVRPData.Fitness}.");
            return _bestCVRPData;
        }

        private void CreateInitialPopulation()
        {
            int[] initialCombination = new int[_problem.CitiesCount];

            var rng = new Random();

            for (int i = 0; i < _problem.CitiesCount; i++)
            {
                initialCombination[i] = i + 1;
            }

            for (int i = 0; i < _populationEncoded.Length; i++)
            {
                _populationEncoded[i] = new int[_problem.CitiesCount];
                Array.Copy(initialCombination, _populationEncoded[i], _problem.CitiesCount);
                rng.Shuffle<int>(_populationEncoded[i]);

                _populationEncodedNextGen[i] = new int[_problem.CitiesCount];
            }
        }

        private void EvaluatePopulation()
        {
            var bestFitnessPerPopulation = 0.0;

            for (int i = 0; i < _populationEncoded.Length; i++)
            {
                var calculatedFitness = _problem.CalculateFitness(_populationEncoded[i]);
                _populationFitnessScores[i] = calculatedFitness;

                // less is better
                if (calculatedFitness < _bestCVRPData.Fitness)
                {
                    _bestCVRPData.UpdateBestCVRPData(_currentIteration, calculatedFitness, _populationEncoded[i]);
                }

                if (calculatedFitness > bestFitnessPerPopulation)
                {
                    bestFitnessPerPopulation = calculatedFitness;
                }

                _sumOfFitness += calculatedFitness;
            }

            Console.WriteLine($"Best fitness in generation {_currentIteration} is {bestFitnessPerPopulation}.");
        }

        private int SelectParentIndexForNextPopulation(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.Roulette:
                    return RouletteSelector.SelectParentIndexByRoulette(_populationFitnessScores, _sumOfFitness, CVRProblem.FitnessType);
                case SelectionType.Tournament:
                    return TournamentSelector.SelectParentIndexByTournament(_populationEncoded, _populationFitnessScores, CVRProblem.FitnessType);
                default:
                    throw new NotImplementedException($"Selection type {selectionType} not implemented.");

            }
        }

        private void ReplacePopulation()
        {
            for (int i = 0; i < _populationEncoded.Length; i++)
            {
                _populationEncodedNextGen[i] = _populationEncoded[i];
            }
        }

    }
}
