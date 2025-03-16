namespace ProblemSolvers.Solvers.Genetic.Crossoverers
{
    public interface ICrossoverer
    {
        int[] CrossoverParents(int[] parentOne, int[] parentTwo);
    }
}