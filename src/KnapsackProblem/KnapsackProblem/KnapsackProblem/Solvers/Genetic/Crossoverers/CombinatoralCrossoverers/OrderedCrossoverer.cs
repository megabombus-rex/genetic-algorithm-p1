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
            var queueOfLeftParentTwo = new Queue<int>();

            for (i = 0; i < parentOne.Length; i++)
            {
                child[i] = 0;
                if (!spanParentOne.Contains(parentTwo[i]))
                {
                    queueOfLeftParentTwo.Enqueue(parentTwo[i]);
                }
            }

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
                    child[i] = queueOfLeftParentTwo.Dequeue();
                }
                i++;
            }
            return child;
        }
    }
}
