using System;
using System.Collections.Generic;
using System.Linq;

class Car
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Price { get; set; }

    public Car(string brand, string model, int price)
    {
        Brand = brand;
        Model = model;
        Price = price;
    }
}

class CarManager
{
    public static Car MostExpensiveCar(List<Car> cars)
    {
        return cars.OrderByDescending(c => c.Price).First();
    }

    public static Car CheapestCar(List<Car> cars)
    {
        return cars.OrderBy(c => c.Price).First();
    }

    public static double AveragePriceOfCars(List<Car> cars)
    {
        return cars.Average(c => c.Price);
    }

    public static Dictionary<string, Car> MostExpensiveModelForEachBrand(List<Car> cars)
    {
        return cars
            .GroupBy(c => c.Brand)
            .ToDictionary(
                g => g.Key,
                g => g.OrderByDescending(c => c.Price).First()
            );
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of cars:");
        int n = Convert.ToInt32(Console.ReadLine());

        List<Car> cars = new List<Car>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter car {i + 1} details (Brand Model Price):");
            var data = Console.ReadLine().Split();

            cars.Add(new Car(
                data[0],
                data[1],
                Convert.ToInt32(data[2])
            ));
        }

        var mostExpensive = CarManager.MostExpensiveCar(cars);
        Console.WriteLine($"The most expensive car is {mostExpensive.Brand} {mostExpensive.Model}");

        var cheapest = CarManager.CheapestCar(cars);
        Console.WriteLine($"The cheapest car is {cheapest.Brand} {cheapest.Model}");

        double avg = CarManager.AveragePriceOfCars(cars);
        Console.WriteLine($"Average price of cars is {avg:F2}");

        var result = CarManager.MostExpensiveModelForEachBrand(cars);

        Console.WriteLine("Most expensive model for each brand:");

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Key} -> {item.Value.Model} {item.Value.Price}");
        }
    }
}