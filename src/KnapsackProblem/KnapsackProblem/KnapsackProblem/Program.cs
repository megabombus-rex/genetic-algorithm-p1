using ProblemSolvers.DataLoaders;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.TestCases.Experiment1;
using ProblemSolvers.TestData.TestCases.Experiment2;
using ProblemSolvers.TestData.TestCases.Experiment3;
using ProblemSolvers.TestData.TestCases.Experiment4;
using ProblemSolvers.TestData.TestCases.Experiment5;
using ProblemSolvers.TestData.TestCases.Experiment6;

public class Program
{
    private static void Main(string[] args)
    {
        //var experiment = new Experiment_1();
        //var experiment = new Experiment_1_1();
        //var experiment = new Experiment_1_2();
        //var experiment = new Experiment_2();
        //var experiment = new Experiment_3();
        //var experiment = new Experiment_4();
        //var experiment = new Experiment_5();
        //var experiment = new Experiment_6();

        //experiment.RunExperiment();



        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        var testDataPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "..\\..\\..\\TestData");

        // setup differently
        // RouletteSelector - const, no stuff to change
        // TournamentSelector - TournamentContestants 
        TournamentSelector.TournamentContestants = 1;

        // setup problem
        IDataLoader<CVRProblem> dataLoader;

        var sourceFileEasyAn32k5 = testDataPath + "\\VRP\\Easy\\A-n32-k5.vrp";
        var sourceFileHardAn60k9 = testDataPath + "\\VRP\\Hard\\A-n60-k9.vrp";
        dataLoader = new CVRPvrpDataLoader();
        //var cvrpEasyOne = dataLoader.LoadData(sourceFileEasyAn32k5);
        var cvrpHardOne = dataLoader.LoadData(sourceFileHardAn60k9);

        var pop = new int[] { 32, 9, 51, 12, 56, 43, 8, 57, 37, 17, 27, 26, 15, 13, 29, 7, 59, 38, 18, 33, 52, 19, 35, 55, 39, 50, 47, 14, 41, 16, 20, 3, 11, 40, 46, 25, 23, 24, 6, 34, 48, 45, 58, 42, 5, 54, 10, 22, 36, 1, 2, 4, 21, 31, 28, 44, 49, 53, 30 };

        var fitness = cvrpHardOne.CalculateFitness(pop);

        Console.WriteLine($"Fitness is {fitness}.");
    }
}