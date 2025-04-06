using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.DataLoaders;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.ProblemRunners;

namespace ProblemSolvers.TestData.TestCases.Experiments
{
    public class Experiment_4 : IExperiment
    {
        public void RunExperiment()
        {
            // The program is as follows:
            // create a problem with methods for Evaluation and for encoded data translation to problem's data
            // setup a solver for given problem
            var testDataPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "..\\..\\..\\TestData");

            // setup differently
            // RouletteSelector - const, no stuff to change
            // TournamentSelector - TournamentContestants 
            TournamentSelector.TournamentContestants = 5;

            // setup problem
            IDataLoader<CVRProblem> dataLoader;

            var sourceFileEasyAn32k5 = testDataPath + "\\VRP\\Easy\\A-n32-k5.vrp";
            var sourceFileEasyAn60k9 = testDataPath + "\\VRP\\Hard\\A-n60-k9.vrp";
            dataLoader = new CVRPvrpDataLoader();
            var cvrpEasyOne = dataLoader.LoadData(sourceFileEasyAn32k5);
            var cvrpHardOne = dataLoader.LoadData(sourceFileEasyAn60k9);
            var maxFitnessCount = 1000;

            // setup algorithm generic data
            var GAdataCVRP = new GeneticAlgorithmGenericData(GenerationsAmount: 1000, PopulationSize: 5000, CrossoverProbability: 1.1, MutationProbability: 1.05, maxFitnessCount);
            // the same amount of Generations as for each genome in genetic algorithm per generation
            var RSdataCVRP = new RandomSearchGenericData(GenerationsAmount: GAdataCVRP.GenerationsAmount * GAdataCVRP.PopulationSize, maxFitnessCount);
            var SAdataCVRP = new SimulatedAnnealingGenericData(50, 1.0, 0.0001, 0.9, maxFitnessCount);

            // setup the solver
            var crossovererCVRP = new OrderedCrossoverer();
            var mutatorCVRP = new InvertedCombinationMutator();

            var runner = new CVRProblemRunner(cvrpEasyOne, GAdataCVRP, RSdataCVRP, SAdataCVRP,
                    SelectionType.Tournament, Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential, crossovererCVRP, mutatorCVRP, 5, sourceFileEasyAn32k5, 10, false);
            var runner2 = new CVRProblemRunner(cvrpHardOne, GAdataCVRP, RSdataCVRP, SAdataCVRP,
                    SelectionType.Tournament, Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential, crossovererCVRP, mutatorCVRP, 5, sourceFileEasyAn60k9, 10, false);
            runner.RunProblem();
            runner2.RunProblem();

        }
    }
}
