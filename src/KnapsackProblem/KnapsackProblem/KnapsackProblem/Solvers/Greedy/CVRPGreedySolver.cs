using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.Greedy
{
    public class CVRPGreedySolver : ISolver
    {
        private CVRProblem _problem;

        public CVRPGreedySolver(CVRProblem problem)
        {
            _problem = problem;
        }

        public void FindOptimalSolution()
        {
            int[] citiesArray = new int[_problem.CitiesCount];

            // every city
            var citiesToSee = _problem.ProblemCities.ToList();

            // start from the depot
            var currentCapacity = _problem.TruckCapacity;
            var currentCity = citiesToSee.OrderBy(x => x.DistanceToDepot).First();
            citiesArray[0] = currentCity.Number;
            currentCapacity -= currentCity.ProduceDemand;
            var citiesSeen = 1;


            while (citiesSeen < _problem.CitiesCount)
            {
                // should only select the cities that do not exceed the capability
                var citiesPossible = new List<CVRProblem.City>();

                foreach (var cityNumber in currentCity!.DistancesToOtherCities.Keys) 
                { 

                }


                if (currentCity != null)
                {
                    citiesArray[citiesSeen] = currentCity.Number;
                    citiesSeen++;
                    // change citiesToSee 
                    citiesToSee = citiesToSee.Where(cityToSee => citiesArray.Contains(cityToSee.Number)).ToList();
                }
            }
        }

        public void LoadInput(string data)
        {
            throw new NotImplementedException();
        }
    }
}
