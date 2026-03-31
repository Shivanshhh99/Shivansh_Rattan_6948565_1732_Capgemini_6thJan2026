using System.Collections.Generic;

namespace CountSimilarPairs
{
    internal class Program
    {
        static int SimilarPairs(string[] words)
        {
            int count = 0;

            for (int i = 0; i < words.Length - 1; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (Check(words[i], words[j]))
                        count++;
                }
            }

            return count;
        }

        static bool Check(string s, string t)
        {
            HashSet<char> sset = new HashSet<char>(s);
            HashSet<char> tset = new HashSet<char>(t);
            return sset.SetEquals(tset);
        }

        static void Main()
        {
            string[] words = { "aba", "aabb", "abcd", "bac", "aabc" };
            Console.WriteLine(SimilarPairs(words));
        }
    }
}