namespace ProblemSolvers.Solvers.Genetic.Selectors
{
    public static class TournamentSelector
    {
        public static int TournamentContestants = 4;
        
        public static int SelectParentIndexByTournament(int[][] populationEncoded, int[] populationFitnessScores)
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
    }
}
