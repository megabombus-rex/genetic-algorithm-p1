namespace ProblemSolvers.Extensions
{
    // https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
    public static class RandomExtensions
    {

        // Fisher-Yates shuffle algorithm
        public static void Shuffle<T> (this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        // https://stackoverflow.com/questions/1064901/random-number-between-2-double-numbers
        public static double RandomDouble(this Random rng, double minimum, double maximum)
        {
            return rng.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
