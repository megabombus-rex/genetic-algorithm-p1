using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.TabuSearch
{
    // https://en.wikipedia.org/wiki/Tabu_search
    public class CVRPTabuSearchSolver : ISolver<BestCVRPData>
    {
        private CVRProblem _problem;
        private TabuSearchGenericData _algorithmData;
        private BestCVRPData _bestCVRPData;

        public CVRPTabuSearchSolver(CVRProblem problem, TabuSearchGenericData data)
        {
            _problem = problem;
            _algorithmData = data;
            _bestCVRPData = new BestCVRPData(problem.CitiesCount);
        }

        public BestCVRPData FindOptimalSolution()
        {
            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = 0.");
                return _bestCVRPData;
            }

            // create an initial random genome
            int[] genome = new int[_problem.CitiesCount];
            var rng = new Random();

            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] = i + 1;
            }
            rng.Shuffle(genome);

            var startingBestFitness = _problem.CalculateFitness(genome);
            _bestCVRPData.UpdateBestCVRPData(0, startingBestFitness, genome);

            var tabuGenomeList = new List<int[]>() { genome };
            var generation = 0;

            // main loop
            while (generation < _algorithmData.GenerationsAmount)
            {
                // swap closest, check if fitness is better, if not -> swap next from base, if yes, then the genome is optimal for the generation
                // repeat for next genomes

                // temp
                var bestNeigbour = new int[genome.Length];

                var bestNeighbourFitness = _problem.CalculateFitness(bestNeigbour);
                // less fitness is better
                if (bestNeighbourFitness < _bestCVRPData.Fitness)
                {
                    _bestCVRPData.UpdateBestCVRPData(generation, bestNeighbourFitness, bestNeigbour);
                }
            }

            return _bestCVRPData;
        }

        private List<int[]> GetNeigbours(int[] genome)
        {

            return new List<int[]> { genome };
        }
    }
}
