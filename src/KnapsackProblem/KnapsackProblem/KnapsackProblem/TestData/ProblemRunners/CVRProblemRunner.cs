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

            // mean, best, worst calculation
            var sumOfFitnessesGen = 0.0;
            var bestFitnessGen = double.MaxValue;
            var worstFitnessGen = 0.0;

            foreach (var genSol in geneticSolutions)
            {
                sumOfFitnessesGen += genSol.Fitness;
                if (genSol.Fitness < bestFitnessGen)
                {
                    bestFitnessGen = genSol.Fitness;
                }
                if (genSol.Fitness > worstFitnessGen)
                {
                    worstFitnessGen = genSol.Fitness;
                }
            }
            var meanFitnessGen = sumOfFitnessesGen / (double)geneticSolutions.Count;
            var meanTimespanGen = timespanGen / geneticSolutions.Count;

            // std deviation calculation
            var stdDevSumGen = 0.0;
            foreach (var genSol in geneticSolutions)
            {
                var root = genSol.Fitness - meanFitnessGen;
                stdDevSumGen += root * root;
            }
            var stdDevGen = Math.Sqrt(stdDevSumGen / geneticSolutions.Count);

            // mean, best, worst calculation
            var sumOfFitnessesRan = 0.0;
            var bestFitnessRan = double.MaxValue;
            var worstFitnessRan = 0.0;

            foreach (var ranSol in randomSolutions)
            {
                sumOfFitnessesRan += ranSol.Fitness;
                if (ranSol.Fitness < bestFitnessRan)
                {
                    bestFitnessRan = ranSol.Fitness;
                }
                if (ranSol.Fitness > worstFitnessRan)
                {
                    worstFitnessRan = ranSol.Fitness;
                }
            }
            var meanFitnessRan = sumOfFitnessesRan / (double)randomSolutions.Count;
            var meanTimespanRan = timespanRan / randomSolutions.Count;

            // std deviation calculation
            var stdDevSumRan = 0.0;
            foreach (var ranSol in randomSolutions)
            {
                var root = ranSol.Fitness - meanFitnessRan;
                stdDevSumRan += root * root;
            }
            var stdDevRan = Math.Sqrt(stdDevSumRan / randomSolutions.Count);

            // mean, best, worst calculation
            var sumOfFitnessesSA = 0.0;
            var bestFitnessSA = double.MaxValue;
            var worstFitnessSA = 0.0;

            foreach (var saSol in annealingSolutions)
            {
                sumOfFitnessesSA += saSol.Fitness;
                if (saSol.Fitness < bestFitnessSA)
                {
                    bestFitnessSA = saSol.Fitness;
                }
                if (saSol.Fitness > worstFitnessSA)
                {
                    worstFitnessSA = saSol.Fitness;
                }
            }
            var meanFitnessSA = sumOfFitnessesSA / (double)annealingSolutions.Count;
            var meanTimespanSA = timespanSA / annealingSolutions.Count;

            // std deviation calculation
            var stdDevSumSA = 0.0;
            foreach (var saSol in annealingSolutions)
            {
                var root = saSol.Fitness - meanFitnessSA;
                stdDevSumSA += root * root;
            }
            var stdDevSA = Math.Sqrt(stdDevSumSA / annealingSolutions.Count);

            // results writing
            var genResults = ShowResults(meanFitnessGen, bestFitnessGen, worstFitnessGen, stdDevGen, _iterationCount, "Genetic Algorithm", meanTimespanGen);
            var ranResults = ShowResults(meanFitnessRan, bestFitnessRan, worstFitnessRan, stdDevRan, _iterationCount, "Random Search Algorithm", meanTimespanRan);
            var SAResults = ShowResults(meanFitnessSA, bestFitnessSA, worstFitnessSA, stdDevSA, _iterationCount, "Simulated Annealing Algorithm", meanTimespanSA);
            var greedResults = ShowResults(resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, 0.0, 1, "Greedy Algorithm", timespanGreed);

            var now = DateTime.UtcNow;
            var date = (now.ToShortDateString() + now.ToShortTimeString()).Replace(':', '_').Replace(' ', '_');
            var fileWithoutExt = Path.GetFileNameWithoutExtension(_problemTestCasePath);
            var filePath = $"{Path.GetDirectoryName(_problemTestCasePath)}\\{fileWithoutExt}_results";
            (new DirectoryInfo(Path.GetFullPath(filePath))).Create();
            var filename = $"{filePath}\\results_{date}.txt";
            var gaData = $"Genetic algorithm data: Population size: {_GAData.PopulationSize}. Generations amount: {_GAData.GenerationsAmount}. Crossover probability {_GAData.CrossoverProbability}. Mutation probability {_GAData.MutationProbability}.";
            var ranData = $"Random Search algorithm data: Population size: {_RSData.GenerationsAmount}.";
            var saData = $"Simulated Annealing algorithm data: Initial temperature: {_SAData.InitialTemperature}. Minimal temperature: {_SAData.MinimalTemperature}. Iterations per T change: {_SAData.IterationsPerCoolingPeriod}. Alpha: {_SAData.Alpha}.";

            var data = new List<string>() { gaData, ranData, saData, genResults, ranResults, SAResults, greedResults };

            SaveFile(filename, data, fileWithoutExt);
        }

        private string ShowResults(double mean, double best, double worst, double stdDev, int iterationCount, string algorithmSelected, TimeSpan runtimeMean)
        {
            var message = algorithmSelected + $":\nMean fitness: {mean}.\nBest fitness: {best}.\nWorst fitness: {worst}.\nStandard deviation: {stdDev}.\nNumber of runs the algorithm had: {iterationCount}.\nThe algorithm runtime mean: {runtimeMean}";
            Console.WriteLine(message);
            return message;
        }

        private void SaveFile(string filename, List<string> linesOfData, string problemTitle)
        {
            using StreamWriter sw = new StreamWriter(filename, true);

            sw.WriteLine();
            sw.WriteLine($"Problem: {problemTitle}");

            foreach (var line in linesOfData)
            {
                sw.WriteLine(line);
            }

            return;
        }

    }
}
