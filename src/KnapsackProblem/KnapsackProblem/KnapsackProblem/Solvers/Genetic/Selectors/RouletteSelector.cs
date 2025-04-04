﻿using ProblemSolvers.CommonTypes.GenericData;
using ProblemSolvers.Extensions;

namespace ProblemSolvers.Solvers.Genetic.Selectors
{
    public static class RouletteSelector
    {
        // different array types for numbers only
        public static int SelectParentIndexByRoulette(dynamic populationFitnessScores, dynamic fitnessSum, PopulationFitnessType fitnessType)
        {
            switch (fitnessType)
            {
                case PopulationFitnessType.MoreIsBetter:
                    return SelectParentIndexMoreIsBetter(populationFitnessScores, fitnessSum);
                case PopulationFitnessType.LessIsBetter:
                    return SelectParentIndexLessIsBetter(populationFitnessScores);
                default:
                    return 0;
            }
        }

        private static int SelectParentIndexLessIsBetter(double[] populationFitnessScores) 
        {
            int i;
            var invertedSum = 0.0;

            // bigger fitness -> lesser inversion
            // 5, 10, 20 -> 0.2 + 0.1 + 0.05 = 0.35
            for (i = 0; i < populationFitnessScores.Length; i++)
            {
                invertedSum += (1.0 / populationFitnessScores[i]);
            }

            var randomVal = new Random().RandomDouble(0.0, invertedSum);
            double currentFitnessSum = 0.0;

            for (i = 0; i < populationFitnessScores.Length; i++)
            {
                currentFitnessSum += (1.0 / populationFitnessScores[i]);
                if (currentFitnessSum > randomVal)
                {
                    return i;
                }
            }

            return i;
        }

        private static int SelectParentIndexLessIsBetter(int[] populationFitnessScores)
        {
            // not implemented
            return 0;
        }

        private static int SelectParentIndexMoreIsBetter(int[] populationFitnessScores, long fitnessSum)
        {
            var randomVal = new Random().NextInt64(0, fitnessSum);
            long currentFitnessSum = 0;

            int i;

            for (i = 0; i < populationFitnessScores.Length; i++)
            {
                currentFitnessSum += populationFitnessScores[i];
                if (currentFitnessSum > randomVal)
                {
                    return i;
                }
            }

            return i;
        }

    }
}
