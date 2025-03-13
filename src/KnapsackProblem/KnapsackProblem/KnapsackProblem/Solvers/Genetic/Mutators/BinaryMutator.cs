namespace ProblemSolvers.Solvers.Genetic.Mutators
{
    public static class BinaryMutator
    {
        // viable for 01 representations
        public static int[] SingleBitInversionMutation(int[] individual)
        {
            var randomIndex = new Random().Next(0, individual.Length);
            individual[randomIndex] = individual[randomIndex] == 0 ? 1 : 0;

            return individual;
        }

        //
    }
}
