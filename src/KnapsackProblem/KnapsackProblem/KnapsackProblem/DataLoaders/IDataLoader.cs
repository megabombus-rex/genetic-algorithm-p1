namespace ProblemSolvers.DataLoaders
{
    public interface IDataLoader<T>
    {
        public T LoadData(string filePath);
    }
}
