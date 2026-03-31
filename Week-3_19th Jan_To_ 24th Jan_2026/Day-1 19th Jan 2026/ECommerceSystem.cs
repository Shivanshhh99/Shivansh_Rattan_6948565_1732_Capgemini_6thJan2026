using System;
using System.Collections.Generic;

class Product
{
    private int productId;
    private string name;
    protected double price;
    private int stock;

    public Product(int id, string name, double price, int stock)
    {
        productId = id;
        this.name = name;
        this.price = price;
        this.stock = stock;
    }

    public int GetProductId() => productId;
    public string GetName() => name;
    public double GetPrice() => price;
    public int GetStock() => stock;

    public bool ReduceStock(int qty)
    {
        if (stock >= qty)
        {
            stock -= qty;
            return true;
        }
        return false;
    }

    public virtual void DisplayProduct()
    {
        Console.WriteLine($"\nID: {productId}");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Price: {price}");
        Console.WriteLine($"Stock: {stock}");
    }
}

class Electronics : Product
{
    public Electronics(int id, string name, double price, int stock)
        : base(id, name, price, stock) { }

    public override void DisplayProduct()
    {
        base.DisplayProduct();
        Console.WriteLine("Category: Electronics");
    }
}

class Clothing : Product
{
    public Clothing(int id, string name, double price, int stock)
        : base(id, name, price, stock) { }

    public override void DisplayProduct()
    {
        base.DisplayProduct();
        Console.WriteLine("Category: Clothing");
    }
}

class Books : Product
{
    public Books(int id, string name, double price, int stock)
        : base(id, name, price, stock) { }

    public override void DisplayProduct()
    {
        base.DisplayProduct();
        Console.WriteLine("Category: Books");
    }
}

class CartItem
{
    public Product Product { get; }
    public int Quantity { get; }

    public CartItem(Product product, int qty)
    {
        Product = product;
        Quantity = qty;
    }

    public double GetItemTotal()
    {
        return Product.GetPrice() * Quantity;
    }
}

class Cart
{
    private List<CartItem> items = new List<CartItem>();

    public void AddToCart(Product product, int qty)
    {
        if (product.ReduceStock(qty))
        {
            items.Add(new CartItem(product, qty));
            Console.WriteLine("Product added to cart.");
        }
        else
        {
            Console.WriteLine("Insufficient stock.");
        }
    }

    public void ViewCart()
    {
        double total = 0;
        Console.WriteLine("\n--- Cart ---");

        foreach (var item in items)
        {
            Console.WriteLine($"{item.Product.GetName()} x {item.Quantity} = {item.GetItemTotal()}");
            total += item.GetItemTotal();
        }

        Console.WriteLine("Total Amount: " + total);
    }
}

// Main System
class ECommerceSystem
{
    static void Main()
    {
        List<Product> products = new List<Product>();
        Cart cart = new Cart();
        int choice;

        do
        {
            Console.WriteLine("\n--- E-Commerce Product Catalog ---");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. View All Products");
            Console.WriteLine("3. Filter by Category");
            Console.WriteLine("4. Add to Cart");
            Console.WriteLine("5. View Cart");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Price: ");
                    double price = double.Parse(Console.ReadLine());
                    Console.Write("Stock: ");
                    int stock = int.Parse(Console.ReadLine());

                    Console.WriteLine("Category: 1.Electronics 2.Clothing 3.Books");
                    int cat = int.Parse(Console.ReadLine());

                    if (cat == 1)
                        products.Add(new Electronics(id, name, price, stock));
                    else if (cat == 2)
                        products.Add(new Clothing(id, name, price, stock));
                    else
                        products.Add(new Books(id, name, price, stock));

                    Console.WriteLine("Product added.");
                    break;

                case 2:
                    foreach (var p in products)
                        p.DisplayProduct();
                    break;

                case 3:
                    Console.WriteLine("Select: 1.Electronics 2.Clothing 3.Books");
                    int filter = int.Parse(Console.ReadLine());

                    foreach (var p in products)
                    {
                        if ((filter == 1 && p is Electronics) ||
                            (filter == 2 && p is Clothing) ||
                            (filter == 3 && p is Books))
                            p.DisplayProduct();
                    }
                    break;

                case 4:
                    Console.Write("Product ID: ");
                    int pid = int.Parse(Console.ReadLine());
                    Console.Write("Quantity: ");
                    int qty = int.Parse(Console.ReadLine());

                    Product product = products.Find(p => p.GetProductId() == pid);
                    if (product != null)
                        cart.AddToCart(product, qty);
                    else
                        Console.WriteLine("Product not found.");
                    break;

                case 5:
                    cart.ViewCart();
                    break;

                case 6:
                    Console.WriteLine("Exiting...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }
}
