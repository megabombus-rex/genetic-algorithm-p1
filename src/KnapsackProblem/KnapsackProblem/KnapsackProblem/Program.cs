// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using ProblemSolvers.CommonTypes;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;
using ProblemSolvers.Solvers.Genetic.Crossoverers.BinaryCrossoverers;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using System.Numerics;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dum dum dummmmm.");

        // setup differently
        // RouletteSelector - const, no stuff to change
        // TournamentSelector - TournamentContestants 
        TournamentSelector.TournamentContestants = 4;

        // Genetic algorithm
        // setup problem
        var knapsackPossibleItems = new KnapsackProblem.Item[]
        {
            new KnapsackProblem.Item() { Value = 1500, WeightInKg = 1.5 }, // laptop
            new KnapsackProblem.Item() { Value = 50, WeightInKg = 0.3 }, // mug
            new KnapsackProblem.Item() { Value = 100, WeightInKg = 0.1 }, // sunglasses
            new KnapsackProblem.Item() { Value = 50, WeightInKg = 0.2 }, // mousepad
            new KnapsackProblem.Item() { Value = 200, WeightInKg = 0.4 }, // drawing tablet
            new KnapsackProblem.Item() { Value = 150, WeightInKg = 0.2 }, // mouse
            new KnapsackProblem.Item() { Value = 200, WeightInKg = 0.6 }, // speaker
            new KnapsackProblem.Item() { Value = 1500, WeightInKg = 2 }, // console
            new KnapsackProblem.Item() { Value = 50, WeightInKg = 0.4 }, // notepad
            new KnapsackProblem.Item() { Value = 100, WeightInKg = 0.1 }, // keys
            new KnapsackProblem.Item() { Value = 20, WeightInKg = 0.5 }, // bread
            new KnapsackProblem.Item() { Value = 700, WeightInKg = 0.3 }, // earbuds
            new KnapsackProblem.Item() { Value = 100, WeightInKg = 0.2 }, // pills 
            new KnapsackProblem.Item() { Value = 500, WeightInKg = 1.5 }, // vase
            new KnapsackProblem.Item() { Value = 500, WeightInKg = 10 }, // dirt bag
        };
        var knapsackProbem = new KnapsackProblem(7.5, knapsackPossibleItems);
        
        // setup genetic algorithm generic data
        var GAdataKnapsack = new GeneticAlgorithmGenericData(GenerationsAmount: 1000, PopulationSize: 500, CrossoverProbability: 0.7, MutationProbability: 0.01);

        // setup the solver
        var mutator = new SingleBitInversionMutator();
        var crossoverer = new OnePointCrossoverer();
        var knapsackGeneticSolver = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Roulette, crossoverer, mutator, GAdataKnapsack);
        
        var knapsackGeneticSolver2 = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Tournament, crossoverer, mutator, GAdataKnapsack);
        
        // solve the problem
        //knapsackGeneticSolver.FindOptimalSolution();

        Console.WriteLine("\nSTOP STOP\n");

        //knapsackGeneticSolver2.FindOptimalSolution();

        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem

        //var crossovererOrdered = new OrderedCrossoverer();

        //int[] parentOne = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //int[] parentTwo = { 5, 7, 4, 9, 1, 3, 6, 12, 8, 11, 10, 2 };

        //var child = crossovererOrdered.CrossoverParents(parentOne, parentTwo);

        //Console.WriteLine("Parent A: " + string.Join(",", parentOne));
        //Console.WriteLine("Parent B: " + string.Join(",", parentTwo));
        //Console.WriteLine("Child: " + string.Join(",", child));

        // setup the cVR Problem
        CVRProblem cvrpOne = new CVRProblem();
        var cvrpOneCities = new CVRProblem.City[]
        {
            new CVRProblem.City(1, new Vector2(15.0f, 12.0f), 5),
            new CVRProblem.City(2, new Vector2(10.0f, 1.0f), 10),
            new CVRProblem.City(3, new Vector2(-5.0f, 4.0f), 15),
            new CVRProblem.City(4, new Vector2(-4.0f, 2.0f), 13),
            new CVRProblem.City(5, new Vector2(-3.0f, 13.0f), 11),
            new CVRProblem.City(6, new Vector2(1.5f, 5.0f), 3),
            new CVRProblem.City(7, new Vector2(0.0f, 6.0f), 4),
            new CVRProblem.City(8, new Vector2(45.0f, 2.0f), 1),
            new CVRProblem.City(9, new Vector2(0.0f, 11.0f), 4),
            new CVRProblem.City(10, new Vector2(0.0f, 32.0f), 4),
        };

        var cvrpOneTruckCapacity = 25;
        cvrpOne.SetupProblem(cvrpOneCities, cvrpOneTruckCapacity);

        var GAdataCVRP = new GeneticAlgorithmGenericData(GenerationsAmount: 1000, PopulationSize: 500, CrossoverProbability: 0.7, MutationProbability: 0.01);

        var crossovererCVRP = new OrderedCrossoverer();
        var mutatorCVRP = new InvertedCombinationMutator();

        var cvrpGeneticSolver = new CVRPGeneticSolver(SelectionType.Roulette, crossovererCVRP, mutatorCVRP, GAdataCVRP, cvrpOne);

        cvrpGeneticSolver.FindOptimalSolution();

        // the int array should be provided by the solver
        //Console.WriteLine(cvrpOne.CalculateFitness(new int[] { 3, 2, 1 }));

    }
}