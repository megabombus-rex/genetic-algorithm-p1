namespace ProblemSolvers.CommonTypes.GenericData
{
    public record RandomSearchGenericData(int GenerationsAmount, int MaxFitnessComparisonCount)
        : GenericAlgorithmData(MaxFitnessComparisonCount);
}
