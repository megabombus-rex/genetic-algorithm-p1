using Newtonsoft.Json;
using ProblemSolvers.CommonTypes.GenericData;
using System.Numerics;

namespace ProblemSolvers.Problems
{
    public class CVRProblem
    {
        // Problem: https://en.wikipedia.org/wiki/Vehicle_routing_problem

        public const PopulationFitnessType FitnessType = PopulationFitnessType.LessIsBetter;

        private int _truckCapacity = 20;

        // each index is a number of a city, the value is a capacity taken from the truck
        private City[] _problemCities;

        public int TruckCapacity {  get { return _truckCapacity; } }

        public City[] ProblemCities { get { return _problemCities; } }

        // genome size
        public int CitiesCount { get { return _problemCities.Length; } }

        // complete graph - from every city to every city (depot too)
        // every city will be entered once

        // [1, 2, 3, 4, 5] -> city 1, city 2, city 3, city 4, city 5
        public CVRProblem()
        {
            _truckCapacity = 20;
            _problemCities = Array.Empty<City>();
        }

        [JsonConstructor]
        public CVRProblem(int truckCapacity, City[] cities)
        {
            _truckCapacity = truckCapacity;
            _problemCities = cities;
        }

        public void SetupProblem(City[] cities, int truckCapacity)
        {
            _problemCities = new City[cities.Length];
            Array.Copy(cities, _problemCities, _problemCities.Length);
            Array.Sort(cities);

            foreach (City c in cities)
            {
                c.CalculateAndSetDistanceToOtherCities(cities);
            }

            _truckCapacity = truckCapacity;
        }

        public void CalculateDistancesAfterFileLoad()
        {
            foreach (City c in _problemCities)
            {
                c.CalculateAndSetDistanceToOtherCities(_problemCities);
            }
        }


        // at the moemnt higher fitness -> worse performance (distance = fitness)
        // maybe fitness => sumOfDistances / distanceRan

        // cities visited are the numbers of the cities in an array
        public double CalculateFitness(int[] citiesVisited)
        {
            // cities visited
            // |5|3|2|4|1|6| -> 5th number, 4th index
            // _problem cities
            // |1|2|3|4|5|6|

            var routes = new List<List<int>>();

            var distanceRan = 0.0;
            var currentCapacity = _truckCapacity;

            //Console.WriteLine($"Truck loaded: {currentCapacity}");

            var currentCity = _problemCities[citiesVisited[0] - 1];
            distanceRan += currentCity.DistanceToDepot;
            currentCapacity -= currentCity.ProduceDemand;
            //var currentRoute = new List<int>();

            // first city (i = 0) is for sure from depot
            for (int i = 1; i < citiesVisited.Length; i++)
            {
                // check if the next city can be ran to directly
                var previousCity = currentCity;
                currentCity = _problemCities[citiesVisited[i] - 1];
                if (currentCapacity >= currentCity.ProduceDemand)
                {
                    Console.WriteLine($"Current capacity: {currentCapacity}.\nCurrent demand: {currentCity.ProduceDemand}.\nGoing to city {currentCity.Number} for {previousCity.DistancesToOtherCities[currentCity.Number]} distance.");
                    currentCapacity -= currentCity.ProduceDemand;
                    distanceRan += Math.Round(previousCity.DistancesToOtherCities[currentCity.Number]);
                    //Console.WriteLine($"Distance ran {distanceRan}.");
                    continue;
                }
                //Console.WriteLine($"Capacity reached, {currentCity.ProduceDemand}, currently: {currentCapacity}.");

                // come back from the previous city
                Console.WriteLine($"Coming back to the depot for {previousCity.DistanceToDepot} distance.");
                distanceRan += Math.Round(previousCity.DistanceToDepot);

                // go to the next city from the depot
                Console.WriteLine($"Going to city {currentCity.Number} for {currentCity.DistanceToDepot} distance.\nCurrent demand: {currentCity.ProduceDemand}.");
                distanceRan += currentCity.DistanceToDepot;
                //Console.WriteLine($"Distance ran {distanceRan}.");
                currentCapacity = _truckCapacity - currentCity.ProduceDemand;
            }

            return distanceRan;
        }

        public class City : IComparable<City>
        {
            public int Number;
            public int ProduceDemand;
            public float DistanceToDepot;
            public Vector2 Position;
            // depot is not const, its the first city, maybe decrease every city's number so it won't affect everything
            private readonly Vector2 _depotConstPosition;

            // maybe round down to int
            public Dictionary<int, float> DistancesToOtherCities;

            [JsonConstructor]
            public City(int number, Vector2 position, int produceDemand, Vector2 depotPosition)
            {
                Number = number;
                ProduceDemand = produceDemand;
                Position = position;
                _depotConstPosition = depotPosition;

                DistanceToDepot = Vector2.Distance(Position, _depotConstPosition);
                DistancesToOtherCities = new Dictionary<int, float>();
            }

            // all of the other cities
            public void CalculateAndSetDistanceToOtherCities(City[] cities)
            {
                foreach (var city in cities.Where(x => x.Number != this.Number))
                {
                    DistancesToOtherCities.Add(city.Number, Vector2.Distance(this.Position, city.Position));
                    Console.WriteLine($"Distance from city nr {this.Number} to {city.Number} added.");
                }
            }

            public int CompareTo(City? other)
            {
                if (other == null) return 0;

                if (other.Number > this.Number)
                {
                    return -1;
                }
                if (other.Number < this.Number)
                {
                    return 1;
                }
                return 0;
            }
        }
    }
}
