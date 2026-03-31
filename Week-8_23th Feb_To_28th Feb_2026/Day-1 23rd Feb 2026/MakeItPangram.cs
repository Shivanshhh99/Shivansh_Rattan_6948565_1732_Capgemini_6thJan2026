namespace MakeItPangram
{
    class Pangram
    {
        public static int MinimumAlphabet(string s)
        {
            HashSet<char> set = new HashSet<char>();

            foreach (char c in s.ToLower())
            {
                if (c >= 'a' && c <= 'z')
                    set.Add(c);
            }

            return 26 - set.Count;
        }
    }

    class Program
    {
        static void Main()
        {
            string input = "abcdefghijklmnopqrstuvwxyz";
            Console.WriteLine(Pangram.MinimumAlphabet(input));
        }
    }
}