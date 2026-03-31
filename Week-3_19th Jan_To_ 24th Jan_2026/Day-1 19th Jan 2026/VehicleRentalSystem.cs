using System;
using System.Collections.Generic;

class Vehicle
{
    private int vehicleId;
    private string model;
    protected double ratePerDay;
    private bool isAvailable = true;

    public Vehicle(int id, string model, double rate)
    {
        vehicleId = id;
        this.model = model;
        ratePerDay = rate;
    }

    public int GetVehicleId() => vehicleId;
    public string GetModel() => model;
    public bool IsAvailable() => isAvailable;

    public void Rent()
    {
        isAvailable = false;
    }

    public void Return()
    {
        isAvailable = true;
    }

    public virtual double CalculateRent(int days)
    {
        return ratePerDay * days;
    }

    public virtual void DisplayVehicle()
    {
        Console.WriteLine($"\nID: {vehicleId}");
        Console.WriteLine($"Model: {model}");
        Console.WriteLine($"Rate/Day: {ratePerDay}");
        Console.WriteLine($"Available: {isAvailable}");
    }
}

class Car : Vehicle
{
    public Car(int id, string model, double rate) : base(id, model, rate) { }

    public override double CalculateRent(int days)
    {
        return base.CalculateRent(days) + 500; // service charge
    }

    public override void DisplayVehicle()
    {
        base.DisplayVehicle();
        Console.WriteLine("Type: Car");
    }
}

class Bike : Vehicle
{
    public Bike(int id, string model, double rate) : base(id, model, rate) { }

    public override double CalculateRent(int days)
    {
        return base.CalculateRent(days);
    }

    public override void DisplayVehicle()
    {
        base.DisplayVehicle();
        Console.WriteLine("Type: Bike");
    }
}

class Truck : Vehicle
{
    public Truck(int id, string model, double rate) : base(id, model, rate) { }

    public override double CalculateRent(int days)
    {
        return base.CalculateRent(days) + 1000; // heavy vehicle charge
    }

    public override void DisplayVehicle()
    {
        base.DisplayVehicle();
        Console.WriteLine("Type: Truck");
    }
}

class Customer
{
    private int customerId;
    private string name;

    public Customer(int id, string name)
    {
        customerId = id;
        this.name = name;
    }

    public int GetCustomerId() => customerId;
    public string GetName() => name;
}

class RentalTransaction
{
    private Vehicle vehicle;
    private Customer customer;
    private int rentalDays;
    private double totalCost;

    public RentalTransaction(Vehicle v, Customer c, int days)
    {
        vehicle = v;
        customer = c;
        rentalDays = days;
        totalCost = v.CalculateRent(days);
    }

    public void DisplayTransaction()
    {
        Console.WriteLine("\n--- Rental Summary ---");
        Console.WriteLine("Customer: " + customer.GetName());
        Console.WriteLine("Vehicle: " + vehicle.GetModel());
        Console.WriteLine("Days: " + rentalDays);
        Console.WriteLine("Total Cost: " + totalCost);
    }
}

// Main System
class VehicleRentalSystem
{
    static void Main()
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        List<Customer> customers = new List<Customer>();
        List<RentalTransaction> rentals = new List<RentalTransaction>();

        int choice;

        do
        {
            Console.WriteLine("\n---- Vehicle Rental System ----");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Add Customer");
            Console.WriteLine("3. Rent Vehicle");
            Console.WriteLine("4. Return Vehicle");
            Console.WriteLine("5. View Vehicles");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Vehicle ID: ");
                    int vid = int.Parse(Console.ReadLine());
                    Console.Write("Model: ");
                    string model = Console.ReadLine();
                    Console.Write("Rate per day: ");
                    double rate = double.Parse(Console.ReadLine());

                    Console.WriteLine("Select Type: 1.Car 2.Bike 3.Truck");
                    int type = int.Parse(Console.ReadLine());

                    if (type == 1)
                        vehicles.Add(new Car(vid, model, rate));
                    else if (type == 2)
                        vehicles.Add(new Bike(vid, model, rate));
                    else
                        vehicles.Add(new Truck(vid, model, rate));

                    Console.WriteLine("Vehicle added successfully.");
                    break;

                case 2:
                    Console.Write("Customer ID: ");
                    int cid = int.Parse(Console.ReadLine());
                    Console.Write("Customer Name: ");
                    string cname = Console.ReadLine();
                    customers.Add(new Customer(cid, cname));
                    Console.WriteLine("Customer added.");
                    break;

                case 3:
                    Console.Write("Enter Vehicle ID: ");
                    int rentVid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Customer ID: ");
                    int rentCid = int.Parse(Console.ReadLine());
                    Console.Write("Enter rental days: ");
                    int days = int.Parse(Console.ReadLine());

                    Vehicle vehicle = vehicles.Find(v => v.GetVehicleId() == rentVid && v.IsAvailable());
                    Customer customer = customers.Find(c => c.GetCustomerId() == rentCid);

                    if (vehicle != null && customer != null)
                    {
                        vehicle.Rent();
                        RentalTransaction rt = new RentalTransaction(vehicle, customer, days);
                        rentals.Add(rt);
                        rt.DisplayTransaction();
                    }
                    else
                    {
                        Console.WriteLine("Vehicle not available or customer not found.");
                    }
                    break;

                case 4:
                    Console.Write("Enter Vehicle ID to return: ");
                    int returnId = int.Parse(Console.ReadLine());
                    Vehicle returnVehicle = vehicles.Find(v => v.GetVehicleId() == returnId);

                    if (returnVehicle != null)
                    {
                        returnVehicle.Return();
                        Console.WriteLine("Vehicle returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Vehicle not found.");
                    }
                    break;

                case 5:
                    foreach (var v in vehicles)
                        v.DisplayVehicle();
                    break;

                case 6:
                    Console.WriteLine("Thank you for using Vehicle Rental System.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }
}
