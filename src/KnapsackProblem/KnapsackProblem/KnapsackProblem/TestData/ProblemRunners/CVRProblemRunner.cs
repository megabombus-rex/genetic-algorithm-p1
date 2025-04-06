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
using System;
using System.Reflection;

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
            SelectionType selectionType, CVRPSimulatedAnnealingSolver.TemperatureChangeType temperatureChangeType, 
            CombinatoralCrossoverer crossoverer, CombinatoralMutator mutator, int tournamentContestantsAmount, 
            string problemTestCasePath, int iterationCount, bool isUsingEvaluations)
        {
            _problem = problem;

            _geneticSolver = new CVRPGeneticSolver(selectionType, crossoverer, mutator, GAData, problem, isUsingEvaluations);
            _greedySolver = new CVRPGreedySolver(problem);
            _randomSearchSolver = new CVRPRandomSearchSolver(problem, RSData, isUsingEvaluations);
            _annealingSolver = new CVRPSimulatedAnnealingSolver(problem, SAData, isUsingEvaluations, temperatureChangeType);

            _GAData = GAData;
            _RSData = RSData;
            _SAData = SAData;

            _problemTestCasePath = problemTestCasePath;
            _iterationCount = iterationCount;
            TournamentSelector.TournamentContestants = tournamentContestantsAmount;
        }

        public void RunProblem()
        {
            //var runTimeGreed = DateTime.UtcNow;
            //var resultCVRPGreed = _greedySolver.FindOptimalSolution();
            //var timespanGreed = DateTime.UtcNow - runTimeGreed;

            //var resGA = RunProblemGA();
            //var resRSA = RunProblemRSA();
            var resSA = RunProblemSA();

            // results writing
            //var genResults = ShowResults(resGA.mean, resGA.best, resGA.worst, resGA.stdDev, resGA.iterationCount, "Genetic Algorithm", resGA.runtimeMean, resGA.bestIndividual);
            //var ranResults = ShowResults(resRSA.mean, resRSA.best, resRSA.worst, resRSA.stdDev, resRSA.iterationCount, "Random Search Algorithm", resRSA.runtimeMean, resRSA.bestIndividual);
            var SAResults = ShowResults(resSA.mean, resSA.best, resSA.worst, resSA.stdDev, resSA.iterationCount, "Simulated Annealing Algorithm", resSA.runtimeMean, resSA.bestIndividual);
            //var greedResults = ShowResults(resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, resultCVRPGreed.Fitness, 0.0, 1, "Greedy Algorithm", timespanGreed, resultCVRPGreed.Genome);

            var now = DateTime.UtcNow;
            var date = (now.ToShortDateString() + now.ToShortTimeString()).Replace(':', '_').Replace(' ', '_');
            var fileWithoutExt = Path.GetFileNameWithoutExtension(_problemTestCasePath);
            var filePath = $"{Path.GetDirectoryName(_problemTestCasePath)}\\{fileWithoutExt}_results";
            (new DirectoryInfo(Path.GetFullPath(filePath))).Create();
            var filename = $"{filePath}\\results_{date}.txt";
            var gaData = $"Genetic algorithm data: Population size: {_GAData.PopulationSize}. Generations amount: {_GAData.GenerationsAmount}. Crossover probability {_GAData.CrossoverProbability}. Mutation probability {_GAData.MutationProbability}.";
            var ranData = $"Random Search algorithm data: Population size: {_RSData.GenerationsAmount}.";
            var saData = $"Simulated Annealing algorithm data: Initial temperature: {_SAData.InitialTemperature}. Minimal temperature: {_SAData.MinimalTemperature}. Iterations per T change: {_SAData.IterationsPerCoolingPeriod}. Alpha: {_SAData.Alpha}.";

            //var data = new List<string>() { gaData, ranData, saData, genResults, ranResults, SAResults, greedResults };
            //var data = new List<string>() { gaData, ranData, saData, genResults };
            var data = new List<string>() { saData, SAResults };

            SaveFile(filename, data, fileWithoutExt);
        }

        private (double mean, double best, double worst, double stdDev, int iterationCount, TimeSpan runtimeMean, int[] bestIndividual) RunProblemGA()
        {
            var geneticSolutions = new List<BestCVRPData>();
            var timespanGen = TimeSpan.Zero;


            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPGenetic = _geneticSolver.FindOptimalSolution();
                timespanGen += DateTime.UtcNow - runtime;
                geneticSolutions.Add(resultCVRPGenetic);
            }

            // mean, best, worst calculation
            var sumOfFitnessesGen = 0.0;
            var bestFitnessGen = double.MaxValue;
            var worstFitnessGen = 0.0;
            var index = 0;
            var indexGen = 0;

            foreach (var genSol in geneticSolutions)
            {
                sumOfFitnessesGen += genSol.Fitness;
                if (genSol.Fitness < bestFitnessGen)
                {
                    bestFitnessGen = genSol.Fitness;
                    indexGen = index;
                }
                if (genSol.Fitness > worstFitnessGen)
                {
                    worstFitnessGen = genSol.Fitness;
                }
                index++;
            }
            var meanFitnessGen = Math.Round(sumOfFitnessesGen / (double)geneticSolutions.Count);
            var meanTimespanGen = timespanGen / geneticSolutions.Count;

            // std deviation calculation
            var stdDevSumGen = 0.0;
            foreach (var genSol in geneticSolutions)
            {
                var root = genSol.Fitness - meanFitnessGen;
                stdDevSumGen += root * root;
            }
            var stdDevGen = Math.Round(Math.Sqrt(stdDevSumGen / geneticSolutions.Count));

            bestFitnessGen = Math.Round(bestFitnessGen);
            worstFitnessGen = Math.Round(worstFitnessGen);

            return (meanFitnessGen, bestFitnessGen, worstFitnessGen, stdDevGen, _iterationCount, meanTimespanGen, geneticSolutions[indexGen].Genome);
        }

        private (double mean, double best, double worst, double stdDev, int iterationCount, TimeSpan runtimeMean, int[] bestIndividual) RunProblemRSA()
        {
            var randomSolutions = new List<BestCVRPData>();
            var timespanRan = TimeSpan.Zero;
            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPRandom = _randomSearchSolver.FindOptimalSolution();
                timespanRan += DateTime.UtcNow - runtime;
                randomSolutions.Add(resultCVRPRandom);
            }

            // mean, best, worst calculation
            var sumOfFitnessesRan = 0.0;
            var bestFitnessRan = double.MaxValue;
            var worstFitnessRan = 0.0;
            var index = 0;
            var indexRan = 0;

            foreach (var ranSol in randomSolutions)
            {
                sumOfFitnessesRan += ranSol.Fitness;
                if (ranSol.Fitness < bestFitnessRan)
                {
                    bestFitnessRan = ranSol.Fitness;
                    indexRan = index;
                }
                if (ranSol.Fitness > worstFitnessRan)
                {
                    worstFitnessRan = ranSol.Fitness;
                }
                index++;
            }
            var meanFitnessRan = Math.Round(sumOfFitnessesRan / (double)randomSolutions.Count);
            var meanTimespanRan = timespanRan / randomSolutions.Count;

            // std deviation calculation
            var stdDevSumRan = 0.0;
            foreach (var ranSol in randomSolutions)
            {
                var root = ranSol.Fitness - meanFitnessRan;
                stdDevSumRan += root * root;
            }
            var stdDevRan = Math.Round(Math.Sqrt(stdDevSumRan / randomSolutions.Count));
            bestFitnessRan = Math.Round(bestFitnessRan);
            worstFitnessRan = Math.Round(worstFitnessRan);

            return (meanFitnessRan, bestFitnessRan, worstFitnessRan, stdDevRan, _iterationCount, meanTimespanRan, randomSolutions[indexRan].Genome);
        }

        private (double mean, double best, double worst, double stdDev, int iterationCount, TimeSpan runtimeMean, int[] bestIndividual) RunProblemSA()
        {
            var annealingSolutions = new List<BestCVRPDataSimulatedAnnealing>();

            var timespanSA = TimeSpan.Zero;


            for (int i = 0; i < _iterationCount; i++)
            {
                var runtime = DateTime.UtcNow;
                var resultCVRPAnnealing = _annealingSolver.FindOptimalSolution();
                timespanSA += DateTime.UtcNow - runtime;
                annealingSolutions.Add(resultCVRPAnnealing as BestCVRPDataSimulatedAnnealing);
            }

            // mean, best, worst calculation
            var sumOfFitnessesSA = 0.0;
            var bestFitnessSA = double.MaxValue;
            var worstFitnessSA = 0.0;
            var index = 0;
            var indexSA = 0;

            foreach (var saSol in annealingSolutions)
            {
                sumOfFitnessesSA += saSol.Fitness;
                if (saSol.Fitness < bestFitnessSA)
                {
                    bestFitnessSA = saSol.Fitness;
                    indexSA = index;
                }
                if (saSol.Fitness > worstFitnessSA)
                {
                    worstFitnessSA = saSol.Fitness;
                }
                index++;
            }
            var meanFitnessSA = Math.Round(sumOfFitnessesSA / (double)annealingSolutions.Count);
            var meanTimespanSA = timespanSA / annealingSolutions.Count;

            // std deviation calculation
            var stdDevSumSA = 0.0;
            foreach (var saSol in annealingSolutions)
            {
                var root = saSol.Fitness - meanFitnessSA;
                stdDevSumSA += root * root;
            }
            var stdDevSA = Math.Round(Math.Sqrt(stdDevSumSA / annealingSolutions.Count));


            bestFitnessSA = Math.Round(bestFitnessSA);
            worstFitnessSA = Math.Round(worstFitnessSA);

            return (meanFitnessSA, bestFitnessSA, worstFitnessSA, stdDevSA, _iterationCount, meanTimespanSA, annealingSolutions[indexSA].Genome);
        }


        private string ShowResults(double mean, double best, double worst, double stdDev, int iterationCount, string algorithmSelected, TimeSpan runtimeMean, int[] bestIndividual)
        {
            var message = algorithmSelected + $":\nMean fitness: {(int)mean}.\nBest fitness: {(int)best}.\nWorst fitness: {(int)worst}.\nStandard deviation: {(int)stdDev}.\nNumber of runs the algorithm had: {iterationCount}.\nThe algorithm runtime mean: {runtimeMean.TotalMilliseconds}ms.\nBest individual found: [{string.Join("|", bestIndividual)}]."; 
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
