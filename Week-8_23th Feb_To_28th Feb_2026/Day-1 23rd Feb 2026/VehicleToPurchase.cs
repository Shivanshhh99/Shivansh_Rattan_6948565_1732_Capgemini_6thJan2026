namespace VehicleToPurchase
{
    class Vehicle
    {
        public string name;
        public double price;
    }

    class VehicleImplementation
    {
        public static double SumOfPrices(List<Vehicle> list)
        {
            return list.Sum(v => v.price);
        }

        public static List<string> GetVehicleList(List<Vehicle> list)
        {
            return list
                .Where(v => v.price > 25000)
                .Select(v => v.name)
                .ToList();
        }

        public static double MaxPrice(List<Vehicle> list)
        {
            return list.Max(v => v.price);
        }
    }

    class Program
    {
        static void Main()
        {
            List<Vehicle> vehicles = new List<Vehicle>
        {
            new Vehicle{name="BMW", price=50000},
            new Vehicle{name="Swift", price=20000},
            new Vehicle{name="Audi", price=60000}
        };

            Console.WriteLine(VehicleImplementation.SumOfPrices(vehicles));
            Console.WriteLine(string.Join(",", VehicleImplementation.GetVehicleList(vehicles)));
            Console.WriteLine(VehicleImplementation.MaxPrice(vehicles));
        }
    }
}