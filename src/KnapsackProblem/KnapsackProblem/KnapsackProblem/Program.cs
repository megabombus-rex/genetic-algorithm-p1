// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using ProblemSolvers.CommonTypes;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;
using ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;

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
        var knapsackGeneticSolver = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Roulette, CrossoverType.OnePoint, mutator, GAdataKnapsack);
        
        var knapsackGeneticSolver2 = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Tournament, CrossoverType.OnePoint, mutator, GAdataKnapsack);
        
        // solve the problem
        knapsackGeneticSolver.FindOptimalSolution();

        Console.WriteLine("\nSTOP STOP\n");

        knapsackGeneticSolver2.FindOptimalSolution();
        
        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        
    }
}