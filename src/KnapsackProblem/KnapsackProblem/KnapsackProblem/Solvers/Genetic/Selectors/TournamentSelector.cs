using ProblemSolvers.CommonTypes.GAEnums;

namespace ProblemSolvers.Solvers.Genetic.Selectors
{
    public static class TournamentSelector
    {
        public static int TournamentContestants = 4;


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
            var contestantGenomesIndexes = new int[TournamentContestants];

            // randomly select indexes from the population
            for (int i = 0; i < TournamentContestants; i++)
            {
                contestantGenomesIndexes[i] = rng.Next(0, populationEncoded.Length);
            }

            // find highest fitness of selected possible parents
            var highestFitnessIndex = contestantGenomesIndexes[0];
            for (int i = 1; i < TournamentContestants; i++)
            {
                if (populationFitnessScores[highestFitnessIndex] < populationFitnessScores[contestantGenomesIndexes[i]])
                {
                    highestFitnessIndex = contestantGenomesIndexes[i];
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
            var contestantGenomesIndexes = new int[TournamentContestants];

            // randomly select indexes from the population
            for (int i = 0; i < TournamentContestants; i++)
            {
                contestantGenomesIndexes[i] = rng.Next(0, populationEncoded.Length);
            }

            // find highest fitness of selected possible parents
            var lowestFitnessIndex = contestantGenomesIndexes[0];
            for (int i = 1; i > TournamentContestants; i++)
            {
                if (populationFitnessScores[lowestFitnessIndex] > populationFitnessScores[contestantGenomesIndexes[i]])
                {
                    lowestFitnessIndex = contestantGenomesIndexes[i];
                }
            }

            return lowestFitnessIndex;
        }


        public static int SelectParentIndexByTournamentLessIsBetter(int[][] populationEncoded, int[] populationFitnessScores)
        {
            // not implemented
            return 0;
        }
    }
}
