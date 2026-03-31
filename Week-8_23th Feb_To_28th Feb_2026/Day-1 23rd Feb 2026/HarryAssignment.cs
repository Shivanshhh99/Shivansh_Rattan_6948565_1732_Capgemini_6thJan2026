namespace HarryAssignment
{

    class StringPlay
    {
        public int convert;
        public int max;
    }

    class StringMethods
    {
        public static int ConvertToInt(StringPlay sp, string s)
        {
            sp.convert = int.Parse(s);
            return sp.convert;
        }

        public static int GetMax(StringPlay sp, string s, char ch)
        {
            sp.max = 0;
            foreach (char c in s)
                if (c == ch) sp.max++;

            return sp.max;
        }
    }

    class Program
    {
        static void Main()
        {
            StringPlay sp = new StringPlay();
            Console.WriteLine(StringMethods.ConvertToInt(sp, "123"));
            Console.WriteLine(StringMethods.GetMax(sp, "banana", 'a'));
        }
    }
}