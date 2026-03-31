using System;
using System.Collections.Generic;
using System.Linq;

public class RealEstateListing
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Location { get; set; }

    public RealEstateListing(int id, string title, string description, decimal price, string location)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        Location = location;
    }
}

public class RealEstateApp
{
    private List<RealEstateListing> listings = new List<RealEstateListing>();

    public void AddListing(RealEstateListing listing)
    {
        listings.Add(listing);
    }

    public void RemoveListing(int id)
    {
        var listing = listings.FirstOrDefault(l => l.Id == id);
        if (listing != null)
            listings.Remove(listing);
    }

    public void UpdateListing(RealEstateListing updatedListing)
    {
        var listing = listings.FirstOrDefault(l => l.Id == updatedListing.Id);
        if (listing != null)
        {
            listing.Title = updatedListing.Title;
            listing.Description = updatedListing.Description;
            listing.Price = updatedListing.Price;
            listing.Location = updatedListing.Location;
        }
    }

    public List<RealEstateListing> GetListings()
    {
        return new List<RealEstateListing>(listings);
    }

    public List<RealEstateListing> GetListingsByLocation(string location)
    {
        return listings
            .Where(l => l.Location.Equals(location, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<RealEstateListing> GetListingsByPriceRange(decimal minPrice, decimal maxPrice)
    {
        return listings
            .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
            .ToList();
    }
}

public class Program
{
    public static void Main()
    {
        RealEstateApp app = new RealEstateApp();

        Console.WriteLine("Enter number of listings:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter Id:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Price:");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Location:");
            string location = Console.ReadLine();

            app.AddListing(new RealEstateListing(id, title, description, price, location));
        }

        Console.WriteLine("Enter location to search:");
        string searchLocation = Console.ReadLine();
        var locationResults = app.GetListingsByLocation(searchLocation);

        Console.WriteLine("Listings in given location:");
        foreach (var listing in locationResults)
        {
            Console.WriteLine($"{listing.Id} - {listing.Title} - {listing.Price} - {listing.Location}");
        }

        Console.WriteLine("Enter minimum price:");
        decimal minPrice = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter maximum price:");
        decimal maxPrice = decimal.Parse(Console.ReadLine());

        var priceResults = app.GetListingsByPriceRange(minPrice, maxPrice);

        Console.WriteLine("Listings in given price range:");
        foreach (var listing in priceResults)
        {
            Console.WriteLine($"{listing.Id} - {listing.Title} - {listing.Price} - {listing.Location}");
        }
        
        Console.WriteLine("Enter Id to remove:");
        int removeId = int.Parse(Console.ReadLine());
        app.RemoveListing(removeId);

        Console.WriteLine("Total listings remaining: " + app.GetListings().Count);
    }
}