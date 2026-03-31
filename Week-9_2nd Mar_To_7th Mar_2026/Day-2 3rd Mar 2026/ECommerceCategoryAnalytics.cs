using System;
using System.Collections.Generic;
using System.Linq;

interface ICategory
{
    int Id { get; set; }
    string Name { get; set; }
    List<IProduct> Products { get; set; }
    void AddProduct(IProduct product);
}

interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
}

interface ICompany
{
    string GetTopCategoryNameByProductCount();
    List<IProduct> GetProductsBelongsToMultipleCategory();
    (string categoryName, decimal totalValue) GetTopCategoryBySumOfProductPrices();
    List<(ICategory category, decimal totalValue)> GetCategoriesWithSumOfTheProductPrices();
}

class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Category : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<IProduct> Products { get; set; }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
        Products = new List<IProduct>();
    }

    public void AddProduct(IProduct product)
    {
        Products.Add(product);
    }
}

class Company : ICompany
{
    private List<ICategory> categories = new List<ICategory>();

    public int Id { get; set; }
    public string Name { get; set; }

    public Company(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public void AddCategory(ICategory category)
    {
        categories.Add(category);
    }

    public string GetTopCategoryNameByProductCount()
    {
        return categories
            .OrderByDescending(c => c.Products.Count)
            .First().Name;
    }

    public List<IProduct> GetProductsBelongsToMultipleCategory()
    {
        return categories
            .SelectMany(c => c.Products)
            .GroupBy(p => p.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.First())
            .ToList();
    }

    public (string categoryName, decimal totalValue) GetTopCategoryBySumOfProductPrices()
    {
        var result = categories
            .Select(c => new
            {
                Category = c,
                Total = c.Products.Sum(p => p.Price)
            })
            .OrderByDescending(x => x.Total)
            .First();

        return (result.Category.Name, result.Total);
    }

    public List<(ICategory category, decimal totalValue)> GetCategoriesWithSumOfTheProductPrices()
    {
        return categories
            .Select(c => (c, c.Products.Sum(p => p.Price)))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        Company company = new Company(1, "Company 1");

        List<IProduct> products = new List<IProduct>();
        List<ICategory> categories = new List<ICategory>();

        Console.WriteLine("Enter number of products:");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter product {i + 1} details (Id Name Price):");
            var a = Console.ReadLine().Split();

            products.Add(new Product(
                Convert.ToInt32(a[0]),
                a[1],
                Convert.ToDecimal(a[2])
            ));
        }

        Console.WriteLine("Enter number of categories:");
        int m = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < m; i++)
        {
            Console.WriteLine($"Enter category {i + 1} details (Id Name):");
            var a = Console.ReadLine().Split();

            categories.Add(new Category(
                Convert.ToInt32(a[0]),
                a[1]
            ));
        }

        Console.WriteLine("Enter number of category-product mappings:");
        int p = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < p; i++)
        {
            Console.WriteLine($"Enter mapping {i + 1} (CategoryId ProductId):");
            var a = Console.ReadLine().Split();

            var c = categories.FirstOrDefault(x => x.Id == Convert.ToInt32(a[0]));
            var pr = products.FirstOrDefault(x => x.Id == Convert.ToInt32(a[1]));

            if (c != null && pr != null)
                c.AddProduct(pr);
        }

        foreach (var c in categories)
            company.AddCategory(c);

        var topCategory = company.GetTopCategoryNameByProductCount();
        var commonProducts = company.GetProductsBelongsToMultipleCategory();
        var mostValuableCategory = company.GetTopCategoryBySumOfProductPrices();
        var categoriesWithTotalPrices = company.GetCategoriesWithSumOfTheProductPrices();

        Console.WriteLine("Top category: " + topCategory);

        Console.WriteLine("Common products:");
        foreach (var item in commonProducts)
            Console.WriteLine(item.Name);

        Console.WriteLine("Most valuable category: " + mostValuableCategory.categoryName + " " + mostValuableCategory.totalValue);

        Console.WriteLine("Category price totals:");
        foreach (var item in categoriesWithTotalPrices)
            Console.WriteLine(item.category.Name + " " + item.totalValue);
    }
}