namespace ProblemSolvers.Solvers.Genetic.Mutators
{
    public interface IMutator
    {
        int[] MutateIndividual(int[] individual);
    }
}
