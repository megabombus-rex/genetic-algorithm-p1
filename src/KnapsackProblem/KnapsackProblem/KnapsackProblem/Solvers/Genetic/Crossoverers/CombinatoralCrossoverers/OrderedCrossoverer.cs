namespace ProblemSolvers.Solvers.Genetic.Crossoverers.CombinatoralCrossoverers
{
    public class OrderedCrossoverer : CombinatoralCrossoverer
    {
        public override int[] CrossoverParents(int[] parentOne, int[] parentTwo)
        {
            var rng = new Random();

            var rIndex = rng.Next(0, parentOne.Length);
            var rLenght = 0;
            try
            {
                rLenght = rng.Next(1, parentOne.Length - rIndex);
            }
            catch (Exception) 
            {
                Console.WriteLine("Exception at finding ok length. Returning a clone.");
                return base.CrossoverParents(parentOne, parentTwo);
            }

            var lIndex = rIndex + rLenght;
            var child = new int[parentOne.Length];

            int i = 0;

            Span<int> spanParentOne = parentOne.AsSpan(rIndex, rLenght);
            Span<int> spanParentTwo = parentTwo.AsSpan();

            // take the part from parent one
            for (i = rIndex; i < lIndex; i++)
            {
                child[i] = parentOne[i];
            }

            i = 0;

            // fill the rest from parent without the repeating keys


            while (i < parentOne.Length)
            {
                if (i < rIndex || i >= lIndex)
                {
                    for (int j = 0; j < parentOne.Length; j++)
                    {
                        if (spanParentOne.Contains(spanParentTwo[j]) || child.Contains(spanParentTwo[j]))
                        {
                            continue;
                        };
                        child[i] = spanParentTwo[j];
                    }
                    i++;
                }
                i++;
            }
            return child;
        }
    }
}
