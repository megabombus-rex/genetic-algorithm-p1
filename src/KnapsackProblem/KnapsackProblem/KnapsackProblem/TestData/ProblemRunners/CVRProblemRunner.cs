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

        // solvers
        private CVRPGeneticSolver _geneticSolver;
        private CVRPGreedySolver _greedySolver;
        private CVRPRandomSearchSolver _randomSearchSolver;
        private CVRPSimulatedAnnealingSolver _annealingSolver;

        // generic data
        private GeneticAlgorithmGenericData _GAData;
        private RandomSearchGenericData _RSData;
        private SimulatedAnnealingGenericData _SAData;

        private string _problemTestCasePath;
        private int _iterationCount;

        public CVRProblemRunner(CVRProblem problem, 
            GeneticAlgorithmGenericData GAData, RandomSearchGenericData RSData, SimulatedAnnealingGenericData SAData,
            SelectionType selectionType, CombinatoralCrossoverer crossoverer, CombinatoralMutator mutator, int tournamentContestantsAmount, 
            string problemTestCasePath, int iterationCount)
        {
            _problem = problem;

            _geneticSolver = new CVRPGeneticSolver(selectionType, crossoverer, mutator, GAData, problem);
            _greedySolver = new CVRPGreedySolver(problem);
            _randomSearchSolver = new CVRPRandomSearchSolver(problem, RSData);
            _annealingSolver = new CVRPSimulatedAnnealingSolver(problem, SAData);

            _GAData = GAData;
            _RSData = RSData;
            _SAData = SAData;

            _problemTestCasePath = problemTestCasePath;
            _iterationCount = iterationCount;
            TournamentSelector.TournamentContestants = tournamentContestantsAmount;
        }

        public void RunProblem()
        {
            var runTimeGreed = DateTime.UtcNow;
            var resultCVRPGreed = _greedySolver.FindOptimalSolution();
            var timespanGreed = DateTime.UtcNow - runTimeGreed;

            var geneticSolutions = new List<BestCVRPData>();
            var randomSolutions = new List<BestCVRPData>();
            var annealingSolutions = new List<BestCVRPDataSimulatedAnnealing>();

            var timespanGen = TimeSpan.Zero;/* = new List<TimeSpan>();*/
            var timespanRan = TimeSpan.Zero;
            var timespanSA = TimeSpan.Zero;

            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPGenetic = _geneticSolver.FindOptimalSolution();    
                geneticSolutions.Add(resultCVRPGenetic);
                timespanGen += DateTime.UtcNow - runtime;
            }

            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPRandom = _randomSearchSolver.FindOptimalSolution();
                randomSolutions.Add(resultCVRPRandom);
                timespanRan += DateTime.UtcNow - runtime;
            }

            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPAnnealing = _annealingSolver.FindOptimalSolution();
                annealingSolutions.Add(resultCVRPAnnealing as BestCVRPDataSimulatedAnnealing);
                timespanSA += DateTime.UtcNow - runtime;
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
            var meanTimespanGen = timespanGen / geneticSolutions.Count;

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
            var meanTimespanRan = timespanRan / geneticSolutions.Count;

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
            var meanTimespanSA = timespanSA / geneticSolutions.Count;

            var genResults = ShowResults(meanFitnessGen, bestFitnessGen, _iterationCount, "Genetic Algorithm", meanTimespanGen);
            var ranResults = ShowResults(meanFitnessRan, bestFitnessRan, _iterationCount, "Random Search Algorithm", meanTimespanRan);
            var SAResults = ShowResults(meanFitnessSA, bestFitnessSA, _iterationCount, "Simulated Annealing Algorithm", meanTimespanSA);
            var greedResults = ShowResults(resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, 1, "Greedy Algorithm", timespanGreed);

            var now = DateTime.UtcNow;
            var date = (now.ToShortDateString() + now.ToShortTimeString()).Replace(':', '_').Replace(' ', '_');
            var filePath = $"{Path.GetDirectoryName(_problemTestCasePath)}\\{Path.GetFileNameWithoutExtension(_problemTestCasePath)}_results";
            (new DirectoryInfo(Path.GetFullPath(filePath))).Create();
            var filename = $"{filePath}\\results_{date}.txt";
            var gaData = $"Genetic algorithm data: Population size: {_GAData.PopulationSize}. Generations amount: {_GAData.GenerationsAmount}. Crossover probability {_GAData.CrossoverProbability}. Mutation probability {_GAData.MutationProbability}.";
            var ranData = $"Random Search algorithm data: Population size: {_RSData.GenerationsAmount}.";
            var saData = $"Simulated Annealing algorithm data: Initial temperature: {_SAData.InitialTemperature}. Minimal temperature: {_SAData.MinimalTemperature}. Iterations per T change: {_SAData.IterationsPerCoolingPeriod}. Alpha: {_SAData.Alpha}.";

            var data = new List<string>() { gaData, ranData, saData, genResults, ranResults, SAResults, greedResults };

            SaveFile(filename, data);
        }

        private string ShowResults(double mean, double best, int iterationCount, string algorithmSelected, TimeSpan runtimeMean)
        {
            var message = algorithmSelected + $":\nMean fitness: {mean}.\nBest fitness: {best}.\nNumber of runs the algorithm had: {iterationCount}.\nThe algorithm runtime mean: {runtimeMean}";
            Console.WriteLine(message);
            return message;
        }

        private void SaveFile(string filename, List<string> linesOfData)
        {
            using StreamWriter sw = new StreamWriter(filename, true);

            sw.WriteLine();

            foreach (var line in linesOfData)
            {
                sw.WriteLine(line);
            }

            return;
        }

    }
}
