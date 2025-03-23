using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.Solvers.Greedy;
using ProblemSolvers.Solvers.RandomSearch;
using ProblemSolvers.Solvers.SimulatedAnnealing;

namespace ProblemSolvers.TestData.ProblemRunners
{
    public class CVRProblemRunner
    {
        private CVRProblem _problem;

        private CVRPGeneticSolver _geneticSolver;
        private CVRPGreedySolver _greedySolver;
        private CVRPRandomSearchSolver _randomSearchSolver;
        private CVRPSimulatedAnnealingSolver _annealingSolver;

        public CVRProblemRunner(CVRProblem problem, 
            GeneticAlgorithmGenericData GAData, RandomSearchGenericData RSData, SimulatedAnnealingGenericData SAData,
            SelectionType selectionType, CombinatoralCrossoverer crossoverer, CombinatoralMutator mutator, int tournamentContestantsAmount)
        {
            _problem = problem;

            _geneticSolver = new CVRPGeneticSolver(selectionType, crossoverer, mutator, GAData, problem);
            _greedySolver = new CVRPGreedySolver(problem);
            _randomSearchSolver = new CVRPRandomSearchSolver(problem, RSData);
            _annealingSolver = new CVRPSimulatedAnnealingSolver(problem, SAData);

            TournamentSelector.TournamentContestants = tournamentContestantsAmount;
        }

        public void RunProblem()
        {
            var resultCVRPGreed = _greedySolver.FindOptimalSolution();

            var geneticSolutions = new List<BestCVRPData>();
            var randomSolutions = new List<BestCVRPData>();
            var annealingSolutions = new List<BestCVRPDataSimulatedAnnealing>();

            int iterationCount = 10;

            for (int i = 0; i < iterationCount; i++)
            {
                var resultCVRPGenetic = _geneticSolver.FindOptimalSolution();
                var resultCVRPRandom = _randomSearchSolver.FindOptimalSolution();
                var resultCVRPAnnealing = _annealingSolver.FindOptimalSolution();

                geneticSolutions.Add(resultCVRPGenetic);
                randomSolutions.Add(resultCVRPRandom);
                annealingSolutions.Add(resultCVRPAnnealing as BestCVRPDataSimulatedAnnealing);
            }

            var sumOfFitnessesGen = 0.0;
            var bestFitnessGen = double.MaxValue;

            foreach (var genSol in geneticSolutions)
            {
                sumOfFitnessesGen += genSol.Fitness;
                if (genSol.Fitness < bestFitnessGen)
                {
                    bestFitnessGen = genSol.Fitness;
                }
            }
            var meanFitnessGen = sumOfFitnessesGen / (double)geneticSolutions.Count;

            var sumOfFitnessesRan = 0.0;
            var bestFitnessRan = double.MaxValue;

            foreach (var ranSol in randomSolutions)
            {
                sumOfFitnessesRan += ranSol.Fitness;
                if (ranSol.Fitness < bestFitnessRan)
                {
                    bestFitnessRan = ranSol.Fitness;
                }
            }
            var meanFitnessRan = sumOfFitnessesRan / (double)randomSolutions.Count;

            var sumOfFitnessesSA = 0.0;
            var bestFitnessSA = double.MaxValue;

            foreach (var saSol in annealingSolutions)
            {
                sumOfFitnessesSA += saSol.Fitness;
                if (saSol.Fitness < bestFitnessSA)
                {
                    bestFitnessSA = saSol.Fitness;
                }
            }
            var meanFitnessSA = sumOfFitnessesSA / (double)annealingSolutions.Count;

            ShowResults(meanFitnessGen, bestFitnessGen, iterationCount, "Genetic Algorithm");
            ShowResults(meanFitnessRan, bestFitnessRan, iterationCount, "Random Search Algorithm");
            ShowResults(meanFitnessSA, bestFitnessSA, iterationCount, "Simulated Annealing Algorithm");
            ShowResults(resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, 1, "Greedy Algorithm");
        }

        private void ShowResults(double mean, double best, int iterationCount, string algorithmSelected)
        {
            Console.WriteLine(algorithmSelected + $":\nMean fitness: {mean}.\nBest fitness: {best}.\n Number of runs the algorithm had: {iterationCount}.");
        }
    }
}
