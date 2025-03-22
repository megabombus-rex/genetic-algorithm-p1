using ProblemSolvers.Problems;
using System.Numerics;

namespace ProblemSolvers.DataLoaders.CVRP
{
    public class CVRPvrpDataLoader : IDataLoader<CVRProblem>
    {
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

            if (!filePath.EndsWith(".vrp"))
            {
                throw new ArgumentException("Wrong file format.");
            }

            var citiesDict = new Dictionary<int, CVRProblem.City>();
            int capacity = 0;
            
            using (StreamReader sr = new StreamReader(filePath))
            {
                bool capacityRead = false;
                bool nodeCoordinatesRead = false;
                bool citiesDemandRead = false;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();

                    if (!capacityRead)
                    {
                        capacityRead = line.StartsWith("CAPA");
                        if (capacityRead)
                        {
                            var split = line.Split();
                            capacity = int.Parse(split[2]);
                        }
                        continue;
                    }

                    if (!nodeCoordinatesRead)
                    {
                        if (line.StartsWith("DEMAND"))
                        {
                            nodeCoordinatesRead = true;
                            continue;
                        }
                        if (line.StartsWith("NODE"))
                        {
                            continue;
                        }

                        var split = line.Split();

                        var cityNr = int.Parse(split[1]);
                        var cityX = float.Parse(split[2]);
                        var cityY = float.Parse(split[3]);

                        var city = new CVRProblem.City(cityNr, new Vector2(cityX, cityY), 0);

                        citiesDict.Add(cityNr, city);
                        continue;
                    }

                    // if we are here then the last line was "DEMAND_SECTION"
                    if (!citiesDemandRead)
                    {
                        if (line.StartsWith("DEPOT"))
                        {
                            citiesDemandRead = true;
                            continue;
                        }
                        var split = line.Split();

                        var cityNr = int.Parse(split[0]);
                        var cityDemand = int.Parse(split[1]);

                        citiesDict[cityNr].ProduceDemand = cityDemand;
                    }
                }

            }

            var cvrProblem = new CVRProblem(capacity, citiesDict.Values.ToArray());
            cvrProblem.CalculateDistancesAfterFileLoad();

            return cvrProblem;
        }
    }
}
