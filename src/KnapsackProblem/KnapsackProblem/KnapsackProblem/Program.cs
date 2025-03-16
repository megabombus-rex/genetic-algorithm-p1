// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using ProblemSolvers.CommonTypes;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;
using ProblemSolvers.Solvers.Genetic.Selectors;
using static ProblemSolvers.Problems.KnapsackProblem;

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
        var knapsackPossibleItems = new Item[]
        {
            new Item() { Value = 1500, WeightInKg = 1.5 }, // laptop
            new Item() { Value = 50, WeightInKg = 0.3 }, // mug
            new Item() { Value = 100, WeightInKg = 0.1 }, // sunglasses
            new Item() { Value = 50, WeightInKg = 0.2 }, // mousepad
            new Item() { Value = 200, WeightInKg = 0.4 }, // drawing tablet
            new Item() { Value = 150, WeightInKg = 0.2 }, // mouse
            new Item() { Value = 200, WeightInKg = 0.6 }, // speaker
            new Item() { Value = 1500, WeightInKg = 2 }, // console
            new Item() { Value = 50, WeightInKg = 0.4 }, // notepad
            new Item() { Value = 100, WeightInKg = 0.1 }, // keys
            new Item() { Value = 20, WeightInKg = 0.5 }, // bread
            new Item() { Value = 700, WeightInKg = 0.3 }, // earbuds
            new Item() { Value = 100, WeightInKg = 0.2 }, // pills 
            new Item() { Value = 500, WeightInKg = 1.5 }, // vase
            new Item() { Value = 500, WeightInKg = 10 }, // dirt bag
        };
        var knapsackProbem = new KnapsackProblem(7.5, knapsackPossibleItems);
        
        // setup genetic algorithm generic data
        var GAdataKnapsack = new GeneticAlgorithmGenericData(1000, 500, 0.7, 0.01);
        
        // setup the solver
        var knapsackGeneticSolver = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Roulette, CrossoverType.OnePoint, MutationType.SingleBitInversion, GAdataKnapsack);
        var knapsackGeneticSolver2 = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Tournament, CrossoverType.OnePoint, MutationType.SingleBitInversion, GAdataKnapsack);
        
        // solve the problem
        knapsackGeneticSolver.FindOptimalSolution();

        Console.WriteLine("\nSTOP STOP\n");

        knapsackGeneticSolver2.FindOptimalSolution();
        
        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        
    }
}