namespace ProblemSolvers.Solvers.Genetic.Crossoverers.BinaryCrossoverers
{
    public class BinaryCrossoverer : ICrossoverer
    {
        public virtual int[] CrossoverParents(int[] parentOne, int[] parentTwo)
        {
            return (int[])parentOne.Clone();
        }
    }
}
