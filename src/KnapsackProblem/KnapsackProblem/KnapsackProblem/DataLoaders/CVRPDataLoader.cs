using ProblemSolvers.Problems;

namespace ProblemSolvers.DataLoaders
{
    public class CVRPDataLoader : IDataLoader<CVRProblem>
    {
        public CVRPDataLoader()
        {

        }


        // data needed -> truck capacity, cities list
        // JSON data is ok, maybe implement different loaders for different inputType
        CVRProblem IDataLoader<CVRProblem>.LoadData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException($"Filepath is null. Problem {typeof(CVRProblem)} could not be loaded.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} does not exist.");
            }

            var cvrProblem = new CVRProblem();

            return new CVRProblem();
        }
    }
}
