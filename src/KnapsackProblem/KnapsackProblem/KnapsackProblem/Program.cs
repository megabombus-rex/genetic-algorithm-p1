using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.DataLoaders;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;
using ProblemSolvers.Solvers.Genetic.Crossoverers.BinaryCrossoverers;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.Solvers.Greedy;
using ProblemSolvers.Solvers.RandomSearch;
using ProblemSolvers.Solvers.SimulatedAnnealing;

public class Program
{
    private static void Main(string[] args)
    {
        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        var testDataPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "..\\..\\..\\TestData");


        // setup differently
        // RouletteSelector - const, no stuff to change
        // TournamentSelector - TournamentContestants 
        TournamentSelector.TournamentContestants = 5;

        // Genetic algorithm
        // setup problem
        IDataLoader<CVRProblem> dataLoader;

        //var jsonDataLoader = new CVRPJsonDataLoader();
        //var cvrpOne = jsonDataLoader.LoadData(testDataPath + "\\CVRProblem_T2.json");
        dataLoader = new CVRPvrpDataLoader();
        var cvrpOne = dataLoader.LoadData(testDataPath + "\\VRP\\Easy\\A-n32-k5.vrp");

        // setup algorithm generic data
        var GAdataCVRP = new GeneticAlgorithmGenericData(GenerationsAmount: 100, PopulationSize: 100, CrossoverProbability: 0.7, MutationProbability: 0.1);
        
        // the same amount of Generations as for each genome in genetic algorithm per generation
        var RSdataCVRP = new RandomSearchGenericData(GenerationsAmount: GAdataCVRP.GenerationsAmount * GAdataCVRP.PopulationSize);
        
        var SAdataCVRP = new SimulatedAnnealingGenericData(100, 1.0, 0.0001, 0.9);

        // setup the solver
        var crossovererCVRP = new OrderedCrossoverer();
        var mutatorCVRP = new InvertedCombinationMutator();


        var cvrpGeneticSolver = new CVRPGeneticSolver(SelectionType.Roulette, crossovererCVRP, mutatorCVRP, GAdataCVRP, cvrpOne);
        var cvrpGreedySolver = new CVRPGreedySolver(cvrpOne);
        var cvrpRandomSolver = new CVRPRandomSearchSolver(cvrpOne, RSdataCVRP);
        var cvrpAnnealingSolver = new CVRPSimulatedAnnealingSolver(cvrpOne, SAdataCVRP);

        var restultCVRPGenetic = cvrpGeneticSolver.FindOptimalSolution();
        var restultCVRPGreed = cvrpGreedySolver.FindOptimalSolution();
        var restultCVRPRandom = cvrpRandomSolver.FindOptimalSolution();
        var restultCVRPAnnealing = cvrpAnnealingSolver.FindOptimalSolution();
    }
}