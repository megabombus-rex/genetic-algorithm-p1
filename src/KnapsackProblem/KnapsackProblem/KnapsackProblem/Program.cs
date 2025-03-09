// Knapsack problem is an optimization problem. Search for the most value taken for full backpack.
// Eg. laptop (1000, 1.5kg), keys (300, 0.05kg), wallet (500, 0.2kg), cup (100, 0.5kg) etc. and backpack has storage of x kgs.
using KnapsackProblem.Solvers;

public class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Dum dum dummmmm.");

        // Genetic algorithm

        // setup problem
        var knapsackGeneticSolver = new KnapsackGeneticSolver(5.0);

        
        // explain the issue





        
        // this has to be mapped
        //var knapsackEvaluator = new KnapsackProblem.KnapsackProblem(3.51);

        //var item1 = new KnapsackProblem.KnapsackProblem.Item() { Value = 100, WeightInKg = 1};
        //var item2 = new KnapsackProblem.KnapsackProblem.Item() { Value = 500, WeightInKg = 1.5};
        //var item3 = new KnapsackProblem.KnapsackProblem.Item() { Value = 100, WeightInKg = 2};
        //var item4 = new KnapsackProblem.KnapsackProblem.Item() { Value = 400, WeightInKg = 0.5};

        //var itemsTooMuch = new List<KnapsackProblem.KnapsackProblem.Item>() { item1, item2, item3, item4 };
        //knapsackEvaluator.SetupKnapsack(itemsTooMuch);
        //var overFitness = knapsackEvaluator.EvaluateFitnessForKnapsack();

        //var itemsOk = new List<KnapsackProblem.KnapsackProblem.Item>() { item1, item3, item4 };
        //knapsackEvaluator.SetupKnapsack(itemsOk);
        //var okFitness = knapsackEvaluator.EvaluateFitnessForKnapsack();

        //Console.WriteLine(overFitness);
        //Console.WriteLine(okFitness);

        // items should be thrown in an array
    }
}