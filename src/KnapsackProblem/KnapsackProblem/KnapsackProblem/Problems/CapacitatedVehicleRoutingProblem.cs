namespace ProblemSolvers.Problems
{
    public class CapacitatedVehicleRoutingProblem
    {
        // Problem: https://en.wikipedia.org/wiki/Vehicle_routing_problem

        public const int TRUCK_CAPACITY = 20;

        // each index is a number of a city, the value is a capacity taken from the truck
        private readonly int[] CityAmountTakers = [10, 5, 12, 11];
    }
}
