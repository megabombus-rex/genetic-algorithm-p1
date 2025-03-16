namespace ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers
{
    public class CombinatoralCrossoverer : ICrossoverer
    {
        public virtual int[] CrossoverParents(int[] parentOne, int[] parentTwo)
        {
            return (int[])parentOne.Clone();
        }
    }
}
