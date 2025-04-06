using ProblemSolvers.DataLoaders;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.TestCases.Experiments;

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
        var experiment = new Experiment_6();

        experiment.RunExperiment();



        // The program is as follows:
        // create a problem with methods for Evaluation and for encoded data translation to problem's data
        // setup a solver for given problem
        //var testDataPath = string.Format("{0}\\{1}", Environment.CurrentDirectory, "..\\..\\..\\TestData");

        //// setup differently
        //// RouletteSelector - const, no stuff to change
        //// TournamentSelector - TournamentContestants 
        //TournamentSelector.TournamentContestants = 1;

        //// setup problem
        //IDataLoader<CVRProblem> dataLoader;

        //var sourceFileEasyAn32k5 = testDataPath + "\\VRP\\Easy\\A-n32-k5.vrp";
        //var sourceFileHardAn60k9 = testDataPath + "\\VRP\\Hard\\A-n60-k9.vrp";
        //dataLoader = new CVRPvrpDataLoader();
        //var cvrpEasyOne = dataLoader.LoadData(sourceFileEasyAn32k5);
        ////var cvrpHardOne = dataLoader.LoadData(sourceFileHardAn60k9);

        ////cvrpEasyOne.DisplayCityMatrix();

        ////var pop = new int[] { 32, 9, 51, 12, 56, 43, 8, 57, 37, 17, 27, 26, 15, 13, 29, 7, 59, 38, 18, 33, 52, 19, 35, 55, 39, 50, 47, 14, 41, 16, 20, 3, 11, 40, 46, 25, 23, 24, 6, 34, 48, 45, 58, 42, 5, 54, 10, 22, 36, 1, 2, 4, 21, 31, 28, 44, 49, 53, 30 };
        //var pop = new int[] { 18, 8, 9, 22, 15, 29, 10, 25, 5, 20, 28, 11, 4, 23, 2, 3, 6, 21, 31, 19, 17, 13, 7, 27, 24, 14, 26, 16, 30, 12, 1 };
        ////var pop = new int[] { 21, 31, 19, 17, 13, 7, 26, 12, 1, 16, 30, 27, 24, 29, 18, 8, 9, 22, 15, 10, 25, 5, 20, 14, 28, 11, 4, 23, 3, 2, 6 };

        ////var fitness = cvrpHardOne.CalculateFitness(pop);
        //var fitness = cvrpEasyOne.CalculateFitness(pop);

        //Console.WriteLine($"Fitness is {fitness}.");
    }
}