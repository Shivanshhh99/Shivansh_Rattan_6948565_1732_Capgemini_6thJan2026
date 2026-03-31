using System.Collections.Generic;

namespace SequenceSum
{

    internal class Program
    {
        static List<string> CheckStrings(string[] a, string[] b)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < a.Length; i++)
            {
                HashSet<char> set = new HashSet<char>(b[i]);
                bool ok = true;

                foreach (char c in a[i])
                {
                    if (!set.Contains(c))
                    {
                        ok = false;
                        break;
                    }
                }

                result.Add(ok ? "Yes" : "No");
            }

            return result;
        }

        static void Main()
        {
            string[] a = { "Hello", "Bee", "Tree" };
            string[] b = { "World", "By", "Box" };

            var result = CheckStrings(a, b);

            foreach (var r in result)
                Console.WriteLine(r);
        }
    }
}