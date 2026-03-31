using System;
using System.Collections.Generic;
using System.Linq;

public interface IProduct
{
    string Name { get; set; }
    string Category { get; set; }
    int Stock { get; set; }
    int Price { get; set; }
}

public interface IInventory
{
    void AddProduct(IProduct product);
    void RemoveProduct(IProduct product);
    int CalculateTotalValue();
    List<IProduct> GetProductsByCategory(string category);
    List<IProduct> SearchProductsByName(string name);
    List<(string, int)> GetProductsByCategoryWithCount();
    List<(string, List<IProduct>)> GetAllProductsByCategory();
}

public class Product : IProduct
{
    public string Name { get; set; }
    public string Category { get; set; }
    public int Stock { get; set; }
    public int Price { get; set; }
}

public class Inventory : IInventory
{
    private List<IProduct> products = new List<IProduct>();

    public void AddProduct(IProduct product)
    {
        products.Add(product);
    }

    public void RemoveProduct(IProduct product)
    {
        products.Remove(product);
    }

    public int CalculateTotalValue()
    {
        return products.Sum(p => p.Stock * p.Price);
    }

    public List<IProduct> GetProductsByCategory(string category)
    {
        return products.Where(p => p.Category == category).ToList();
    }

    public List<IProduct> SearchProductsByName(string name)
    {
        return products.Where(p => p.Name.Contains(name)).ToList();
    }

    public List<(string, int)> GetProductsByCategoryWithCount()
    {
        return products
            .GroupBy(p => p.Category)
            .Select(g => (g.Key, g.Count()))
            .ToList();
    }

    public List<(string, List<IProduct>)> GetAllProductsByCategory()
    {
        return products
            .GroupBy(p => p.Category)
            .Select(g => (g.Key, g.ToList()))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        Inventory inventory = new Inventory();

        Console.WriteLine("Enter number of products:");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter Product {i + 1} details (Name Category Stock Price):");

            string[] input = Console.ReadLine().Split();

            Product p = new Product
            {
                Name = input[0],
                Category = input[1],
                Stock = Convert.ToInt32(input[2]),
                Price = Convert.ToInt32(input[3])
            };

            inventory.AddProduct(p);
        }

        Console.WriteLine("\nEnter category to display:");
        string category = Console.ReadLine();

        Console.WriteLine("\nProducts in Category:");
        foreach (var p in inventory.GetProductsByCategory(category))
            Console.WriteLine($"{p.Name} - {p.Category}");

        Console.WriteLine("\nEnter product name to search:");
        string search = Console.ReadLine();

        foreach (var p in inventory.SearchProductsByName(search))
            Console.WriteLine($"{p.Name} - {p.Category}");

        Console.WriteLine("\nTotal Value: $" + inventory.CalculateTotalValue());

        Console.WriteLine("\nCategory Count:");
        foreach (var c in inventory.GetProductsByCategoryWithCount())
            Console.WriteLine($"{c.Item1} : {c.Item2}");

        Console.WriteLine("\nProducts Grouped by Category:");
        foreach (var g in inventory.GetAllProductsByCategory())
        {
            Console.WriteLine(g.Item1);
            foreach (var p in g.Item2)
                Console.WriteLine($"Product Name:{p.Name} Price:{p.Price}");
        }

        Console.ReadLine();
    }
}