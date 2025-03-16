namespace ProblemSolvers.Solvers.Genetic.Mutators.CombinatoralMutators
{
    public class InvertedCombinationMutator : CombinatoralMutator
    {
        public override int[] MutateIndividual(int[] individual)
        {
            var rng = new Random();

            var positionOne = rng.Next(0, individual.Length);
            var positionTwo = rng.Next(0, individual.Length);

            var aux = individual[positionOne];
            individual[positionOne] = individual[positionTwo];
            individual[positionTwo] = aux;

            return individual;
        }
    }
}
