namespace ProblemSolvers.Problems
{
    public class KnapsackProblem
    {
        public int PossibleItemCount { get { return _possibleItems.Length; } }

        private Knapsack _knapsack;
        private readonly Item[] _possibleItems = [
                new Item() { Value = 1500, WeightInKg = 1.5 }, // laptop
                new Item() { Value = 50, WeightInKg = 0.3 }, // mug
                new Item() { Value = 100, WeightInKg = 0.1 }, // sunglasses
                new Item() { Value = 50, WeightInKg = 0.2 }, // mousepad
                new Item() { Value = 200, WeightInKg = 0.4 }, // drawing tablet
                new Item() { Value = 150, WeightInKg = 0.2 }, // mouse
                new Item() { Value = 200, WeightInKg = 0.6 }, // speaker
                new Item() { Value = 1500, WeightInKg = 2 }, // console
                new Item() { Value = 50, WeightInKg = 0.4 }, // notepad
                new Item() { Value = 100, WeightInKg = 0.1 }, // keys
                new Item() { Value = 20, WeightInKg = 0.5 }, // bread
                new Item() { Value = 700, WeightInKg = 0.3 }, // earbuds
                new Item() { Value = 100, WeightInKg = 0.2 }, // pills 
                new Item() { Value = 500, WeightInKg = 1.5 }, // vase
                new Item() { Value = 500, WeightInKg = 10 }, // dirt bag
            ];

        // this will return a useless item when index is out of bounds
        public Item GetItemFromPossibleItems(int index)
        {
            if (index < 0 || index > _possibleItems.Length)
            {
                return new Item() { Value = 0, WeightInKg = int.MaxValue };
            }

            return _possibleItems[index];
        }

        public void SetupProblem(List<Item> decodedData)
        {
            _knapsack.EmptyKnapsack();

            foreach (Item item in decodedData)
            {
                _knapsack.AddItem(item);
            }
        }

        public int CalculateFitnessOfProblem()
        {
            if (_knapsack.IsOverweight)
            {
                return 0;
            }

            return _knapsack.CurrentValue;
        }

        public KnapsackProblem(double maxWeightInKgs, Item[] itemSet)
        {
            _knapsack = new Knapsack(maxWeightInKgs);
            _possibleItems = itemSet;
            PossibleItemCount = itemSet.Length;
        }

        public struct Item
        {
            public int Value;
            public double WeightInKg;
        }

        public class Knapsack
        {
            public List<Item> Items;
            public double MaxWeightInKg;
            public double CurrentWeight;
            public int CurrentValue;

            public void AddItem(Item item)
            {
                Items.Add(item);
                CurrentWeight += item.WeightInKg;
                CurrentValue += item.Value;
            }

            public void RemoveItem(Item item)
            {
                Items.Remove(item);
                CurrentWeight -= item.WeightInKg;
                CurrentValue -= item.Value;
            }

            public void EmptyKnapsack()
            {
                Items.Clear();
                CurrentWeight = 0;
                CurrentValue = 0;
            }

            public bool IsOverweight
            {
                get { return CurrentWeight > MaxWeightInKg; }
            }

            public Knapsack(double maxWeightInKg)
            {
                Items = new List<Item>();
                CurrentWeight = 0;
                CurrentValue = 0;
                MaxWeightInKg = maxWeightInKg;
            }
        }
    }
}
