using System;

namespace TimeValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Time :");
            string str = Console.ReadLine();

            int ans = UserProgramCode.validateTime(str);

            if (ans == 1)
                Console.WriteLine("Valid time format");
            else
                Console.WriteLine("Invalid time format");
        }
    }

    class UserProgramCode
    {
        public static int validateTime(string str)
        {
            int hr, min;

            // Basic length check to avoid runtime errors
            if (str.Length != 8)
                return -1;

            hr = int.Parse(str.Substring(0, 2));
            min = int.Parse(str.Substring(3, 2));
            string suf = str.Substring(6, 2).ToLower();

            if (hr < 1 || hr > 12)
                return -1;

            if (min < 0 || min > 59)
                return -1;

            if (suf != "am" && suf != "pm")
                return -1;

            if (str[2] != ':' || str[5] != ' ')
                return -1;

            return 1;
        }
    }
}
