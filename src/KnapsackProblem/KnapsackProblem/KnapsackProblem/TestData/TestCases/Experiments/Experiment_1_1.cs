using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.DataLoaders;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.ProblemRunners;

namespace ProblemSolvers.TestData.TestCases.Experiments
{
    public class Experiment_1_1 : IExperiment
    {
        public void RunExperiment()
        {
            // The program is as follows:
            // create a problem with methods for Evaluation and for encoded data translation to problem's data
            // setup a solver for given problem
            var testDataPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "..\\..\\..\\TestData");

            // setup differently

            // setup problem
            IDataLoader<CVRProblem> dataLoader;
            dataLoader = new CVRPvrpDataLoader();

            var sourceFileI1 = testDataPath + "\\VRP\\Easy\\A-n32-k5.vrp";
            var sourceFileI2 = testDataPath + "\\VRP\\Easy\\A-n37-k6.vrp";
            var sourceFileI3 = testDataPath + "\\VRP\\Easy\\A-n39-k5.vrp";
            var sourceFileI4 = testDataPath + "\\VRP\\Easy\\A-n45-k6.vrp";
            var sourceFileI5 = testDataPath + "\\VRP\\Easy\\A-n48-k7.vrp";
            var sourceFileI6 = testDataPath + "\\VRP\\Hard\\A-n54-k7.vrp";
            var sourceFileI7 = testDataPath + "\\VRP\\Hard\\A-n60-k9.vrp";

            var cvrpI1 = dataLoader.LoadData(sourceFileI1);
            var cvrpI2 = dataLoader.LoadData(sourceFileI2);
            var cvrpI3 = dataLoader.LoadData(sourceFileI3);
            var cvrpI4 = dataLoader.LoadData(sourceFileI4);
            var cvrpI5 = dataLoader.LoadData(sourceFileI5);
            var cvrpI6 = dataLoader.LoadData(sourceFileI6);
            var cvrpI7 = dataLoader.LoadData(sourceFileI7);
            var maxFitnessCount = 1000;

            // setup algorithm generic data
            var GAdataCVRP = new GeneticAlgorithmGenericData(GenerationsAmount: 100, PopulationSize: 100, CrossoverProbability: 0.7, MutationProbability: 0.1, MaxFitnessComparisonCount: maxFitnessCount);
            // the same amount of Generations as for each genome in genetic algorithm per generation
            var RSdataCVRP = new RandomSearchGenericData(GenerationsAmount: GAdataCVRP.GenerationsAmount * GAdataCVRP.PopulationSize, maxFitnessCount);
            var SAdataCVRP = new SimulatedAnnealingGenericData(100, 1.0, 0.0001, 0.9, maxFitnessCount);

            // setup the solver
            var crossovererCVRP = new OrderedCrossoverer();
            var mutatorCVRP = new InvertedCombinationMutator();

            var runnerI1 = new CVRProblemRunner(cvrpI1, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI1, 10, false);
            var runnerI2 = new CVRProblemRunner(cvrpI2, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI2, 10, false);
            var runnerI3 = new CVRProblemRunner(cvrpI3, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI3, 10, false);
            var runnerI4 = new CVRProblemRunner(cvrpI4, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI4, 10, false);
            var runnerI5 = new CVRProblemRunner(cvrpI5, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI5, 10, false);
            var runnerI6 = new CVRProblemRunner(cvrpI6, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament,Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential,crossovererCVRP, mutatorCVRP, 5, sourceFileI6, 10, false);
            var runnerI7 = new CVRProblemRunner(cvrpI7, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, Solvers.SimulatedAnnealing.CVRPSimulatedAnnealingSolver.TemperatureChangeType.Exponential, crossovererCVRP, mutatorCVRP, 5, sourceFileI7, 10, false);
            runnerI1.RunProblem();
            runnerI2.RunProblem();
            runnerI3.RunProblem();
            runnerI4.RunProblem();
            runnerI5.RunProblem();
            runnerI6.RunProblem();
            runnerI7.RunProblem();
        }
    }
}
