namespace PersonDetailValidationSystem
{
    class InvalidDateException : Exception
    {
        public InvalidDateException(string msg) : base(msg) { }
    }

    class InvalidEmailException : Exception
    {
        public InvalidEmailException(string msg) : base(msg) { }
    }

    class Person
    {
        public string name;
        public string dateOfBirth;
        public string email;
    }

    class Implementation
    {
        public static string validator(Person details)
        {
            if (!Regex.IsMatch(details.dateOfBirth, @"^\d{2}-\d{2}-\d{4}$"))
                throw new InvalidDateException("Invalid Date Format");

            int year = int.Parse(details.dateOfBirth.Split('-')[2]);
            if (year >= 2000)
                throw new InvalidDateException("Year must be < 2000");

            if (!details.email.EndsWith("@doselect.com"))
                throw new InvalidEmailException("Invalid Email Domain");

            return "Valid Details";
        }

        public static string submitDetails(Person details)
        {
            return validator(details) + " - Submitted Successfully";
        }
    }

    class Program
    {
        static void Main()
        {
            Person p = new Person
            {
                name = "John",
                dateOfBirth = "12-05-1995",
                email = "john@doselect.com"
            };

            try
            {
                Console.WriteLine(Implementation.submitDetails(p));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}