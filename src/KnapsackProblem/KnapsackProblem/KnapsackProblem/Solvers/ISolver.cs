namespace ProblemSolvers.Solvers
{
    public interface ISolver<T>
    {
        // the template is for the best fitness data
        T FindOptimalSolution();

    }
}
