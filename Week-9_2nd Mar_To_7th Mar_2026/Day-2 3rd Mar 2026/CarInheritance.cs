using System;

abstract class Car
{
    protected int fuel;
    protected int efficiency;

    public Car(int fuel, int efficiency)
    {
        this.fuel = fuel;
        this.efficiency = efficiency;
    }

    public abstract int CalculateRange();
}

class PetrolCar : Car
{
    public PetrolCar(int fuel, int efficiency) : base(fuel, efficiency) { }

    public override int CalculateRange()
    {
        return fuel * efficiency;
    }
}

class DieselCar : Car
{
    public DieselCar(int fuel, int efficiency) : base(fuel, efficiency) { }

    public override int CalculateRange()
    {
        return fuel * efficiency;
    }
}

class ElectricCar : Car
{
    public ElectricCar(int fuel, int efficiency) : base(fuel, efficiency) { }

    public override int CalculateRange()
    {
        return fuel * efficiency;
    }
}

class Program
{
    static void Main()
    {
        // Petrol car
        Console.WriteLine("Enter Petrol Car fuel and efficiency:");
        string[] p = Console.ReadLine().Split();
        PetrolCar petrol = new PetrolCar(int.Parse(p[0]), int.Parse(p[1]));
        Console.WriteLine($"Petrol Car Range: {petrol.CalculateRange()}");

        // Diesel car
        Console.WriteLine("Enter Diesel Car fuel and efficiency:");
        string[] d = Console.ReadLine().Split();
        DieselCar diesel = new DieselCar(int.Parse(d[0]), int.Parse(d[1]));
        Console.WriteLine($"Diesel Car Range: {diesel.CalculateRange()}");

        // Electric car
        Console.WriteLine("Enter Electric Car battery and efficiency:");
        string[] e = Console.ReadLine().Split();
        ElectricCar electricCar = new ElectricCar(int.Parse(e[0]), int.Parse(e[1]));
        Console.WriteLine($"Electric Car Range: {electricCar.CalculateRange()}");
    }
}