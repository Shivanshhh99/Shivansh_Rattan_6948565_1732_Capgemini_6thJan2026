namespace SpeedValidationUsingCustomException
{
    class SpeedInvalidException : Exception
    {
        public SpeedInvalidException(string message) : base(message) { }
    }

    class CarSpeed
    {
        public string speed;
    }

    class CarSpeedImplementation
    {
        public static string SetCarSpeed(CarSpeed sp, int spd)
        {
            if (spd < 30 || spd > 90)
                throw new SpeedInvalidException("Invalid Speed");

            sp.speed = spd.ToString();
            return "Speed Set Successfully";
        }
    }

    class Program
    {
        static void Main()
        {
            CarSpeed car = new CarSpeed();
            try
            {
                Console.WriteLine(CarSpeedImplementation.SetCarSpeed(car, 50));
            }
            catch (SpeedInvalidException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}