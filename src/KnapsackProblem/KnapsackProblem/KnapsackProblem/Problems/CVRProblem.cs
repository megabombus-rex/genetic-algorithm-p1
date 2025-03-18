using ProblemSolvers.CommonTypes.GAEnums;
using System.Numerics;

namespace ProblemSolvers.Problems
{
    public class CVRProblem
    {
        // Problem: https://en.wikipedia.org/wiki/Vehicle_routing_problem

        public const GoodPopulationFitness FitnessType = GoodPopulationFitness.LessIsBetter;

        private int _truckCapacity = 20;

        // each index is a number of a city, the value is a capacity taken from the truck
        private City[] _problemCities;

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


        // at the moemnt higher fitness -> worse performance (distance = fitness)
        // maybe fitness => sumOfDistances / distanceRan

        // cities visited are the numbers of the cities in an array
        public double CalculateFitness(int[] citiesVisited)
        {
            // cities visited
            // |5|3|2|4|1|6| -> 5th number, 4th index
            // _problem cities
            // |1|2|3|4|5|6|

            var distanceRan = 0.0;
            var currentCapacity = _truckCapacity;

            //Console.WriteLine($"Truck loaded: {currentCapacity}");

            var currentCity = _problemCities[citiesVisited[0] - 1];
            distanceRan += currentCity.DistanceToDepot;
            currentCapacity -= currentCity.ProduceDemand;


            // first city (i = 0) is for sure from depot
            for (int i = 1; i < citiesVisited.Length; i++)
            {
                // check if the next city can be ran to directly
                var previousCity = currentCity;
                currentCity = _problemCities[citiesVisited[i] - 1];
                if (currentCapacity >= currentCity.ProduceDemand)
                {
                    currentCapacity -= currentCity.ProduceDemand;
                    distanceRan += previousCity.DistancesToOtherCities[currentCity.Number];
                    //Console.WriteLine($"Distance ran {distanceRan}.");
                    continue;
                }
                //Console.WriteLine($"Capacity reached, {currentCity.ProduceDemand}, currently: {currentCapacity}.");

                // come back from the previous city
                distanceRan += previousCity.DistanceToDepot;

                // go to the next city from the depot
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
            private readonly Vector2 _depotConstPosition;

            // maybe round down to int
            public Dictionary<int, float> DistancesToOtherCities;

            public City(int number, Vector2 position, int produceDemand)
            {
                Number = number;
                ProduceDemand = produceDemand;
                Position = position;
                _depotConstPosition = new Vector2(0.0f, 0.0f);

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
