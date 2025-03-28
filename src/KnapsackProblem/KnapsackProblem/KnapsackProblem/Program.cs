using ProblemSolvers.TestData.TestCases.Experiment1;
using ProblemSolvers.TestData.TestCases.Experiment2;
using ProblemSolvers.TestData.TestCases.Experiment3;
using ProblemSolvers.TestData.TestCases.Experiment4;

public class Program
{
    private static void Main(string[] args)
    {
        //var experiment = new Experiment_1();
        //var experiment = new Experiment_1_1();
        //var experiment = new Experiment_1_2();
        //var experiment = new Experiment_2();
        //var experiment = new Experiment_3();
        var experiment = new Experiment_4();

        experiment.RunExperiment();
    }
}