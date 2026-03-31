using System;
using System.Collections.Generic;

abstract class GroceryReceiptBase2
{
    public Dictionary<string, decimal> Prices { get; set; }
    public Dictionary<string, int> Discounts { get; set; }

    public GroceryReceiptBase2(Dictionary<string, decimal> prices, Dictionary<string, int> discounts)
    {
        Prices = prices;
        Discounts = discounts;
    }

    public abstract IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList);
}

class GroceryReceipt2 : GroceryReceiptBase2
{
    public GroceryReceipt2(Dictionary<string, decimal> prices,
                           Dictionary<string, int> discounts)
        : base(prices, discounts)
    {
    }

    public override IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList)
    {
        foreach (var item in shoppingList)
        {
            string fruit = item.Item1;
            int quantity = item.Item2;

            decimal price = Prices[fruit];
            decimal total = price * quantity;

            if (Discounts.ContainsKey(fruit))
            {
                int discount = Discounts[fruit];
                total = total - (total * discount / 100);
            }

            yield return (fruit, price, total);
        }
    }
}

class Program
{
    static void Main()
    {
        Dictionary<string, decimal> prices = new Dictionary<string, decimal>();
        Dictionary<string, int> discounts = new Dictionary<string, int>();
        List<Tuple<string, int>> shoppingList = new List<Tuple<string, int>>();

        Console.WriteLine("Enter number of fruits with prices:");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter fruit name and price (e.g., Apple 20):");
            var a = Console.ReadLine().Split();
            prices[a[0]] = Convert.ToDecimal(a[1]);
        }

        Console.WriteLine("Enter number of discounts:");
        int d = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < d; i++)
        {
            Console.WriteLine($"Enter fruit name and discount percentage (e.g., Orange 10):");
            var a = Console.ReadLine().Split();
            discounts[a[0]] = Convert.ToInt32(a[1]);
        }

        Console.WriteLine("Enter number of shopping items:");
        int s = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < s; i++)
        {
            Console.WriteLine($"Enter fruit name and quantity (e.g., Banana 2):");
            var a = Console.ReadLine().Split();
            shoppingList.Add(new Tuple<string, int>(a[0], Convert.ToInt32(a[1])));
        }

        var receipt = new GroceryReceipt2(prices, discounts);

        Console.WriteLine("\nReceipt:");
        foreach (var item in receipt.Calculate(shoppingList))
        {
            Console.WriteLine($"{item.fruit} {item.price} {item.total}");
        }
    }
}