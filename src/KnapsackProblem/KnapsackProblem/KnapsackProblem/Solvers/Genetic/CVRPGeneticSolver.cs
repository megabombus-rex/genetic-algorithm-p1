using ProblemSolvers.CommonTypes;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;

namespace ProblemSolvers.Solvers.Genetic
{
    public class CVRPGeneticSolver : ISolver
    {
        private readonly SelectionType _selectionType;
        private readonly CombinatoralCrossoverer _crossoverer;
        private readonly CombinatoralMutator _mutator;

        private readonly GeneticAlgorithmGenericData _algorithmData;

        CVRProblem _problem;

        private int[] _populationFitnessScores;
        private int[][] _populationEncoded; // |0|0|1|1|1|1|0| etc.
        private int[][] _populationEncodedNextGen; // |0|0|1|1|1|1|0| etc.
        private long _sumOfFitness;

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
            _populationFitnessScores = new int[data.PopulationSize];
            _sumOfFitness = 0;

            _currentIteration = 0;
            _bestCVRPData = new BestCVRPData(_problem.CitiesCount);
        }


        public void FindOptimalSolution()
        {
        }

        public void LoadInput(string data)
        {
            throw new NotImplementedException();
        }

        public class BestCVRPData
        {
            public int Iteration;
            public long Fitness;
            public int[] Genome;

            public BestCVRPData(int genomeSize)
            {
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
}
