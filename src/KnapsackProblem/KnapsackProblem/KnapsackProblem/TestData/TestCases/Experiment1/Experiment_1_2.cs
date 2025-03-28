using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.DataLoaders;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.ProblemRunners;

namespace ProblemSolvers.TestData.TestCases.Experiment1
{
    // the best results
    public class Experiment_1_2 : IExperiment
    {
        public void RunExperiment()
        {
            var expStart = DateTime.UtcNow; 

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

            // setup algorithm generic data - the best
            var GAdataCVRP = new GeneticAlgorithmGenericData(GenerationsAmount: 500, PopulationSize: 5000, CrossoverProbability: 0.9, MutationProbability: 0.1);
            // the same amount of Generations as for each genome in genetic algorithm per generation - it does not matter for the RS
            var RSdataCVRP = new RandomSearchGenericData(GenerationsAmount: GAdataCVRP.GenerationsAmount * GAdataCVRP.PopulationSize);
            var SAdataCVRP = new SimulatedAnnealingGenericData(100, 1.0, 0.0001, 0.9);

            // setup the solver
            var crossovererCVRP = new OrderedCrossoverer();
            var mutatorCVRP = new InvertedCombinationMutator();

            var runnerI1 = new CVRProblemRunner(cvrpI1, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI1, 10);
            var runnerI2 = new CVRProblemRunner(cvrpI2, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI2, 10);
            var runnerI3 = new CVRProblemRunner(cvrpI3, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI3, 10);
            var runnerI4 = new CVRProblemRunner(cvrpI4, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI4, 10);
            var runnerI5 = new CVRProblemRunner(cvrpI5, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI5, 10);
            var runnerI6 = new CVRProblemRunner(cvrpI6, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI6, 10);
            var runnerI7 = new CVRProblemRunner(cvrpI7, GAdataCVRP, RSdataCVRP, SAdataCVRP, SelectionType.Tournament, crossovererCVRP, mutatorCVRP, 10, sourceFileI7, 10);
            var time = DateTime.UtcNow;
            runnerI1.RunProblem();
            var runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 1 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI2.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 2 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI3.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 3 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI4.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 4 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI5.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 5 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI6.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 6 ran for: {runtime.TotalMilliseconds}");

            time = DateTime.UtcNow;
            runnerI7.RunProblem();
            runtime = DateTime.UtcNow - time;
            Console.WriteLine($"Runner 7 ran for: {runtime.TotalMilliseconds}");

            Console.WriteLine($"Experiment 1.2 ran for {(DateTime.UtcNow - expStart).TotalMilliseconds}");
        }
    }
}
