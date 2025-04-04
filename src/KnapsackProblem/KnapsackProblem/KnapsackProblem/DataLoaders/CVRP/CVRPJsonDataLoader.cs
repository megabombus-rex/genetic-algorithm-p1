using Newtonsoft.Json;
using ProblemSolvers.Problems;

namespace ProblemSolvers.DataLoaders.CVRP
{
    [Obsolete]
    public class CVRPJsonDataLoader : IDataLoader<CVRProblem>
    {
        public CVRPJsonDataLoader()
        {
            throw new NotImplementedException("Json data format is not used anymore.");
        }


        // data needed -> truck capacity, cities list
        // JSON data is ok, maybe implement different loaders for different inputType
        public CVRProblem LoadData(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException($"Filepath is null. Problem {typeof(CVRProblem)} could not be loaded.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File {filePath} does not exist.");
            }

            if (!filePath.EndsWith(".json"))
            {
                throw new ArgumentException("Wrong file format.");
            }

            var cvrProblem = new CVRProblem();

            using (StreamReader sr = new StreamReader(filePath))
            {
                var json = sr.ReadToEnd();

                cvrProblem = JsonConvert.DeserializeObject<CVRProblem>(json);
            }

            cvrProblem.CalculateDistancesAfterFileLoad();

            return cvrProblem!;
        }
    }
}
