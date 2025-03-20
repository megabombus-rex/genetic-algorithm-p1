using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.TabuSearch
{
    // https://en.wikipedia.org/wiki/Tabu_search
    public class CVRPTabuSearchSolver : ISolver
    {
        private CVRProblem _problem;
        private TabuSearchGenericData _algorithmData;

        public CVRPTabuSearchSolver(CVRProblem problem, TabuSearchGenericData data)
        {
            _problem = problem;
            _algorithmData = data;
        }

        public void FindOptimalSolution()
        {
            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = 0.");
                return;
            }

            // create an initial random genome
            int[] genome = new int[_problem.CitiesCount];
            var rng = new Random();

            for (int i = 0; i < genome.Length; i++)
            {
                genome[i] = i + 1;
            }

            rng.Shuffle(genome);

            var tabuGenomeList = new List<int[]>() { genome };

            var generation = 0;

            // main loop
            while (generation < _algorithmData.GenerationsAmount)
            {
                // swap closest, check if fitness is better, if not -> swap next from base, if yes, then the genome is optimal for the generation
                // repeat for next genomes
            }
        }
    }
}
