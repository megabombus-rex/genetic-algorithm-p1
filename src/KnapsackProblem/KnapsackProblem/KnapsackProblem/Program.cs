// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using ProblemSolvers.CommonTypes;
using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dum dum dummmmm.");

        // Genetic algorithm

        // setup problem with problem solver
        var knapsackProbem = new KnapsackProblem(7.5);
        var GAdataKnapsack = new GeneticAlgorithmGenericData(1000, 500, 0.7, 0.01);
        
        var knapsackGeneticSolver = new KnapsackGeneticSolver(knapsackProbem, SelectionType.Roulette, CrossoverType.OnePoint, MutationType.SingleBitInversion, GAdataKnapsack);
        
        // solve the problem
        knapsackGeneticSolver.FindOptimalSolution();
        
        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        
    }
}