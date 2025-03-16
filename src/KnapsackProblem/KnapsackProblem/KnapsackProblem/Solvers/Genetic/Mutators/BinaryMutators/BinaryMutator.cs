namespace ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators
{
    // viable for 01 representations
    public class BinaryMutator : IMutator
    {
        public virtual int[] MutateIndividual(int[] individual)
        {
            return individual;
        }
    }
}
