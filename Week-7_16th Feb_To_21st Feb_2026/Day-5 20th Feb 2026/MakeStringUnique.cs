using System;
using System.Collections.Generic;
using System.Text;

namespace MakeStringUnique
{
    internal class Program
    {
        static string MakeUnique(string s)
        {
            HashSet<char> seen = new HashSet<char>();
            StringBuilder sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!seen.Contains(c))
                {
                    sb.Append(c);
                    seen.Add(c);
                }
            }

            return sb.ToString();
        }

        public static void Main()
        {
            string input = "abcbbck";
            Console.WriteLine(MakeUnique(input));
        }
    }
}