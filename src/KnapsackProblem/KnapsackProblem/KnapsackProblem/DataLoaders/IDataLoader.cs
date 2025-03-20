namespace ProblemSolvers.DataLoaders
{
    public interface IDataLoader<T>
    {
        T LoadData(string filePath);
    }
}
