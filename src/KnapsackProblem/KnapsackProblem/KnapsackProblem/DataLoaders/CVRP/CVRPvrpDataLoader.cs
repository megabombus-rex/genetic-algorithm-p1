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
                CVRProblem.City depotCity = new CVRProblem.City(0, new Vector2(0, 0), 0, new Vector2(0, 0));
                bool capacityRead = false;
                bool depotCoordinatesRead = false;
                bool depotRead = false;
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

                        if (!depotCoordinatesRead)
                        {
                            if (line.StartsWith(" 1"))
                            {
                                var splitLine = line.Split();
                                var depotX = float.Parse(splitLine[2]);
                                var depotY = float.Parse(splitLine[3]);
                                depotCity = new CVRProblem.City(0, new Vector2(depotX, depotY), 0, new Vector2(depotX, depotY));
                                depotCoordinatesRead = true;
                                continue;
                            }
                        }

                        var split = line.Split();

                        var cityNr = int.Parse(split[1]) - 1; // depot = 0 in this configuration
                        var cityX = float.Parse(split[2]);
                        var cityY = float.Parse(split[3]);

                        // depot should be known now
                        var city = new CVRProblem.City(cityNr, new Vector2(cityX, cityY), 0, depotCity.Position);

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
                        if (!depotRead)
                        {
                            if (line.StartsWith("1"))
                            {
                                depotRead = true;
                                continue;
                            }
                        }
                        var split = line.Split();

                        var cityNr = int.Parse(split[0]) - 1;
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
