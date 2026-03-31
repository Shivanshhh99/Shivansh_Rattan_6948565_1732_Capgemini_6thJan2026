using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public double Price { get; set; }
    public int Stock { get; set; }
}

class InventoryManager
{
    static void Main()
    {
        List<Book> inventory = new List<Book>();

        Console.WriteLine("Enter number of books:");
        int count = int.Parse(Console.ReadLine());

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Enter details for Book {i + 1}");
            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            inventory.Add(new Book { Title = title, Price = price, Stock = stock });
        }

        Console.Write("\nEnter price to find cheaper books: ");
        double targetPrice = double.Parse(Console.ReadLine());

        var cheapBooks = inventory.Where(b => b.Price < targetPrice);
        Console.WriteLine("\nBooks cheaper than target price:");
        foreach (var book in cheapBooks)
            Console.WriteLine($"{book.Title} - {book.Price}");

        Console.Write("\nEnter sale percentage increase: ");
        double percent = double.Parse(Console.ReadLine());

        inventory.ForEach(b => b.Price += b.Price * percent / 100);

        inventory.RemoveAll(b => b.Stock == 0);

        Console.WriteLine("\nFinal Inventory:");
        foreach (var book in inventory)
            Console.WriteLine($"{book.Title} | Price: {book.Price} | Stock: {book.Stock}");
    }
}
