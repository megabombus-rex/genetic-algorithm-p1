using ProblemSolvers.CommonTypes.BestData;
using ProblemSolvers.Problems;

namespace ProblemSolvers.Solvers.Greedy
{
    // The implementation done by me, based off the problem calculation of fitness. Closest possible city -> optimal local solution.
    public class CVRPGreedySolver : ISolver<BestCVRPData>
    {
        private CVRProblem _problem;
        private BestCVRPData _bestCVRPData;

        public CVRPGreedySolver(CVRProblem problem)
        {
            _problem = problem;
            _bestCVRPData = new BestCVRPData(problem.CitiesCount);
        }

        public BestCVRPData FindOptimalSolution()
        {
            if (_problem.CitiesCount < 1)
            {
                Console.WriteLine("Empty city list, fitness = 0.");
                return _bestCVRPData;
            }

            int[] citiesSeenArray = new int[_problem.CitiesCount];

            // every city that has to be seen
            var citiesToSee = _problem.ProblemCities.ToList();

            // start from the depot, distance does not have to be calculated here
            var currentCapacity = _problem.TruckCapacity;
            
            // first city, will throw an error if citiesToSee is empty
            var currentCity = citiesToSee.OrderBy(x => x.DistanceToDepot).First();
            citiesSeenArray[0] = currentCity.Number;
            currentCapacity -= currentCity.ProduceDemand;
            var citiesSeen = 1;

            // we are after the first depot
            bool startFromDepot = false;

            // if one city, then done
            while (citiesSeen < _problem.CitiesCount)
            {
                // should only select the cities that do not exceed the capability
                var citiesPossible = citiesToSee.Where(x => x.ProduceDemand < currentCapacity);

                // no cities possible to see with current truck capacity
                if (citiesPossible.Count() < 1)
                {
                    // come back to the depot
                    currentCapacity = _problem.TruckCapacity;
                    startFromDepot = true;

                    // check again without incrementing the citiesSeen iterator
                    continue;
                }

                // go from the depot to the next closest city from the depot
                if (startFromDepot)
                {
                    // next closest city
                    currentCity = citiesPossible.OrderBy(x => x.DistanceToDepot).FirstOrDefault();

                    currentCapacity -= currentCity.ProduceDemand;
                    citiesSeenArray[citiesSeen] = currentCity.Number;
                    citiesSeen++;

                    // remove seen cities from the list of possible targets
                    citiesToSee = citiesToSee.Where(cityToSee => !citiesSeenArray.Contains(cityToSee.Number)).ToList();
                    startFromDepot = false;
                    continue;
                }

                // not starting from depot, currentCity - ok
                var closestDistance = double.MaxValue;
                // select only the cities that are possible to see with current capacity
                var possibleCityNrs = currentCity.DistancesToOtherCities.Keys.Where(nr => citiesPossible.Select(cp => cp.Number).Contains(nr));
                
                // no city with this nr is present
                var selectedNr = 0;

                // find the city closest to current city
                foreach (var cityNr in possibleCityNrs)
                {
                    if (currentCity.DistancesToOtherCities[cityNr] < closestDistance)
                    {
                        closestDistance = currentCity.DistancesToOtherCities[cityNr];
                        selectedNr = cityNr;
                    }
                }

                // we are now in the next city
                currentCity = citiesPossible.FirstOrDefault(x => x.Number == selectedNr);
                citiesSeenArray[citiesSeen] = selectedNr;

                // deduce the current truck capacity by the next city's demand
                currentCapacity -= currentCity!.ProduceDemand;

                // remove seen cities from the list of possible targets
                citiesToSee = citiesToSee.Where(cityToSee => !citiesSeenArray.Contains(cityToSee.Number)).ToList();
                citiesSeen++;
            }

            var distanceRan = _problem.CalculateFitness(citiesSeenArray);
            Console.WriteLine($"Greedy Algorithm:\nBest fitness occured for: [{string.Join("|", citiesSeenArray)}] with fitness score: {distanceRan}.");
            _bestCVRPData.UpdateBestCVRPData(0, distanceRan, citiesSeenArray);

            return _bestCVRPData;
        }
    }
}
