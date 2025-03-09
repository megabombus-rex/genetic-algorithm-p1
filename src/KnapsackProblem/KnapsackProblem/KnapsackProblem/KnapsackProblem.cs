namespace KnapsackProblem
{
    public class KnapsackProblem
    {
        public const int POSSIBLE_ITEMS_COUNT = 15;

        private Knapsack _knapsack;
        private Item[] _possibleItems;

        public void SetupProblem()
        {
            _possibleItems =
            [
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

            //var item1 = new Item() { Value = 1500, WeightInKg = 1.5 }; // laptop
            //var item2 = new Item() { Value = 50, WeightInKg = 0.3 }; // mug
            //var item3 = new Item() { Value = 100, WeightInKg = 0.1 }; // sunglasses
            //var item4 = new Item() { Value = 50, WeightInKg = 0.2 }; // mousepad
            //var item5 = new Item() { Value = 200, WeightInKg = 0.4 }; // drawing tablet
            //var item6 = new Item() { Value = 150, WeightInKg = 0.2 }; // mouse
            //var item7 = new Item() { Value = 200, WeightInKg = 0.6 }; // speaker
            //var item8 = new Item() { Value = 1500, WeightInKg = 2 }; // console
            //var item9 = new Item() { Value = 50, WeightInKg = 0.4 }; // notepad
            //var item10 = new Item() { Value = 100, WeightInKg = 0.1 }; // keys
            //var item11 = new Item() { Value = 20, WeightInKg = 0.5 }; // bread
            //var item12 = new Item() { Value = 700, WeightInKg = 0.3 }; // earbuds
            //var item13 = new Item() { Value = 100, WeightInKg = 0.2 }; // pills 
            //var item14 = new Item() { Value = 500, WeightInKg = 1.5 }; // vase
            //var item15 = new Item() { Value = 500, WeightInKg = 10 }; // dirt bag
            //var item16 = new Item() { Value = 1, WeightInKg = 1 };
            //var item17 = new Item() { Value = 1, WeightInKg = 1 };
            //var item18 = new Item() { Value = 1, WeightInKg = 1 };
            //var item19 = new Item() { Value = 1, WeightInKg = 1 };
            //var item20 = new Item() { Value = 1, WeightInKg = 1 };
        }

        // this will return a useless item when index is out of bounds
        public Item GetItemFromPossibleItems(int index)
        {
            if (index < 0 || index > _possibleItems.Length)
            {
                return new Item() { Value = 0, WeightInKg = int.MaxValue };
            }

            return _possibleItems[index];
        }

        public void SetupKnapsack(List<Item> items)
        {
            _knapsack.EmptyKnapsack();

            foreach (Item item in items)
            {
                _knapsack.AddItem(item);
            }
        }

        public int EvaluateFitnessForKnapsack()
        {
            if (_knapsack.IsOverweight)
            {
                return 0;
            }

            return _knapsack.CurrentValue;
        }

        public KnapsackProblem(double maxWeightInKgs)
        {
            _knapsack = new Knapsack(maxWeightInKgs);
            _possibleItems = new Item[POSSIBLE_ITEMS_COUNT];
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
