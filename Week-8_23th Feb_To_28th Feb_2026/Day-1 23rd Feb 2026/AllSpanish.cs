namespace AllSpanish
{
    class Dish
    {
        public string dishName { get; set; }
    }

    class DishTest
    {
        public static List<Dish> AddYummyToName(List<Dish> list, string word)
        {
            return list.Select(d => new Dish
            {
                dishName = d.dishName + word
            }).ToList();
        }

        public static long Count(List<Dish> list, string word)
        {
            return list.Count(d => d.dishName.Contains(word));
        }
    }

    class Program
    {
        static void Main()
        {
            List<Dish> dishes = new List<Dish>
        {
            new Dish{dishName="Taco"},
            new Dish{dishName="Rice"}
        };

            var updated = DishTest.AddYummyToName(dishes, "Yummy");
            Console.WriteLine(updated[0].dishName);

            Console.WriteLine(DishTest.Count(updated, "Yummy"));
        }
    }
}