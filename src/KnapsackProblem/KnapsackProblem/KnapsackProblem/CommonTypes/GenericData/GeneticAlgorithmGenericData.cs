namespace ProblemSolvers.CommonTypes.GenericData
{
    public record GeneticAlgorithmGenericData(int GenerationsAmount, int PopulationSize, double CrossoverProbability, double MutationProbability, int MaxFitnessComparisonCount) 
        : GenericAlgorithmData(MaxFitnessComparisonCount);
}