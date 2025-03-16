namespace ProblemSolvers.Solvers.Genetic.Mutators.BinaryMutators
{
    public class SingleBitInversionMutator : BinaryMutator
    {
        public override int[] MutateIndividual(int[] individual)
        {
            var randomIndex = new Random().Next(0, individual.Length);
            individual[randomIndex] = individual[randomIndex] == 0 ? 1 : 0;

            return individual;
        }
    }
}
