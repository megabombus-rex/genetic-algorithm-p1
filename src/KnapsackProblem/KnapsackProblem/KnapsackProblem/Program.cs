// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using ProblemSolvers.Solvers.Genetic;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dum dum dummmmm.");

        // Genetic algorithm

        // setup problem with problem solver
        var knapsackGeneticSolver = new KnapsackGeneticSolver(7.5);
        
        // solve the problem
        knapsackGeneticSolver.FindOptimalSolution();
        
        
    }
}