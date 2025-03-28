using ProblemSolvers.CommonTypes.GenericData;

namespace ProblemSolvers.Solvers.Genetic.Selectors
{
    public static class TournamentSelector
    {
        private static int[] _contestantGenomesIndexes;
        private static int _tournamentContestants;

        static TournamentSelector()
        {
            _contestantGenomesIndexes = Array.Empty<int>();
        }

        public static int TournamentContestants { 
            get 
            { 
                return _tournamentContestants;  
            } 
            set 
            {
                _tournamentContestants = value;
                _contestantGenomesIndexes = new int[_tournamentContestants];
            } 
        }


        // different array types for numbers only
        public static int SelectParentIndexByTournament(int[][] populationEncoded, dynamic populationFitnessScores, PopulationFitnessType fitnessType)
        {
            switch (fitnessType)
            {
                case PopulationFitnessType.MoreIsBetter:
                    return SelectParentIndexByTournamentMoreIsBetter(populationEncoded, populationFitnessScores);
                case PopulationFitnessType.LessIsBetter:
                    return SelectParentIndexByTournamentLessIsBetter(populationEncoded, populationFitnessScores);
                default:
                    return 0;
            }
        }

        public static int SelectParentIndexByTournamentMoreIsBetter(int[][] populationEncoded, int[] populationFitnessScores)
        {
            if (populationFitnessScores.Length < TournamentContestants)
            {
                throw new ArgumentException("Too many contestants. Not enough population.");
            }

            var rng = new Random();
            //_contestantGenomesIndexes = new int[TournamentContestants];

            // randomly select indexes from the population
            for (int i = 0; i < TournamentContestants; i++)
            {
                _contestantGenomesIndexes[i] = rng.Next(0, populationEncoded.Length);
            }

            // find highest fitness of selected possible parents
            var highestFitnessIndex = _contestantGenomesIndexes[0];
            for (int i = 1; i < TournamentContestants; i++)
            {
                if (populationFitnessScores[highestFitnessIndex] < populationFitnessScores[_contestantGenomesIndexes[i]])
                {
                    highestFitnessIndex = _contestantGenomesIndexes[i];
                }
            }

            return highestFitnessIndex;
        }

        public static int SelectParentIndexByTournamentMoreIsBetter(int[][] populationEncoded, double[] populationFitnessScores)
        {
            // not implemented
            return 0;
        }

        public static int SelectParentIndexByTournamentLessIsBetter(int[][] populationEncoded, double[] populationFitnessScores)
        {
            if (populationFitnessScores.Length < TournamentContestants)
            {
                throw new ArgumentException("Too many contestants. Not enough population.");
            }

            var rng = new Random();
            //_contestantGenomesIndexes = new int[TournamentContestants];

            // randomly select indexes from the population
            for (int i = 0; i < TournamentContestants; i++)
            {
                _contestantGenomesIndexes[i] = rng.Next(0, populationEncoded.Length);
            }

            // find highest fitness of selected possible parents
            var lowestFitnessIndex = _contestantGenomesIndexes[0];
            //Console.WriteLine($"Contestant {contestantGenomesIndexes[0]} fitness: {populationFitnessScores[lowestFitnessIndex]}");

            for (int i = 1; i < TournamentContestants; i++)
            {
                //Console.WriteLine($"Contestant {contestantGenomesIndexes[i]} fitness: {populationFitnessScores[lowestFitnessIndex]}");
                if (populationFitnessScores[lowestFitnessIndex] > populationFitnessScores[_contestantGenomesIndexes[i]])
                {
                    lowestFitnessIndex = _contestantGenomesIndexes[i];
                }
            }

            return lowestFitnessIndex;
        }

    }
}
