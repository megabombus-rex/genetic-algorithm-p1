using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.RandomSearch
{
    // own implementation
    public class CVRPRandomSearchSolver : ISolver<BestCVRPData>
    {
        private CVRProblem _problem;
        private BestCVRPData _bestCVRPData;
        private RandomSearchGenericData _algorithmData;
        private int _evaluationCount;

        public CVRPRandomSearchSolver(CVRProblem problem, RandomSearchGenericData data)
        {
            _problem = problem;
            _bestCVRPData = new BestCVRPData(problem.CitiesCount);
            _algorithmData = data;
            _evaluationCount = 0;
        }

        public BestCVRPData FindOptimalSolution()
        {
            _bestCVRPData.Clear();

            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = 0.");
                return _bestCVRPData;
            }

            // create an initial genome
            int[] genome = new int[_problem.CitiesCount];

            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] = i + 1;
            }

            var rng = new Random();

            for (int generation = 0; generation < _algorithmData.GenerationsAmount; generation++)
            {
                rng.Shuffle(genome);
                var fitness = _problem.CalculateFitness(genome);
                _evaluationCount++;

                if (fitness < _bestCVRPData.Fitness)
                {
                    _bestCVRPData.UpdateBestCVRPData(generation, fitness, genome);
                }
            }

            Console.WriteLine($"Fitness evaluated {_evaluationCount} times.");
            //_bestCVRPData.DisplayBestData("Random Search");
            return _bestCVRPData.Clone();
        }
    }
}
