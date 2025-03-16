namespace ProblemSolvers.Problems
{
    public class CapacitatedVehicleRoutingProblem
    {
        // Problem: https://en.wikipedia.org/wiki/Vehicle_routing_problem

        private readonly int _truckCapacity = 20;

        // each index is a number of a city, the value is a capacity taken from the truck
        private readonly int[] CityAmountTakers = [10, 5, 12, 11];

        // complete graph - from every city to every city (depot too)
        // between different cities there is constant 
        // every city will be entered once

        // [1, 2, 3, 4, 5] -> city 1, city 2, city 3, city 4, city 5

        public class City
        {
            public int ProduceDemand;
        }
    }
}
