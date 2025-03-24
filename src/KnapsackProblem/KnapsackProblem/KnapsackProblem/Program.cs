using ProblemSolvers.CommonTypes.GAEnums;
using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.DataLoaders;
using ProblemSolvers.DataLoaders.CVRP;
using ProblemSolvers.Problems;
using ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers;
using ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators;
using ProblemSolvers.Solvers.Genetic.Selectors;
using ProblemSolvers.TestData.ProblemRunners;
using ProblemSolvers.TestData.TestCases;

public class Program
{
    private static void Main(string[] args)
    {
        var experiment = new Experiment_1();
        //var experiment = new Experiment_2();
        //var experiment = new Experiment_3();

        experiment.RunExperiment();
    }
}