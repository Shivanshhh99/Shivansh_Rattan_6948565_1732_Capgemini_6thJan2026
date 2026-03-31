namespace CountryCodeValidation
{
    class InvalidCodeException : Exception
    {
        public InvalidCodeException(string msg) : base(msg) { }
    }

    class Repository
    {
        public static string GetCountryName(string countryCode)
        {
            return RepositoryImplementation.GetCountry(countryCode);
        }
    }

    class RepositoryImplementation
    {
        public static string GetCountry(string countryCode)
        {
            if (countryCode == "908")
                return "US";
            if (countryCode == "011")
                return "Dial somewhere outside of USA";

            int code = int.Parse(countryCode);
            if (code >= 70 && code <= 99)
                return "India";

            throw new InvalidCodeException("Invalid Code");
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                Console.WriteLine(Repository.GetCountryName("75"));
            }
            catch (InvalidCodeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}