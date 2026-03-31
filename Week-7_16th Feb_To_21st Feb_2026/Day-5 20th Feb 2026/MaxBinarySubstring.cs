using System.Text;

namespace MaxBinarySubstring
{
    internal class Program
    {
        static int LongestNonDecreasing(string s)
        {
            int maxLen = 1;
            int curLen = 1;

            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] >= s[i - 1])
                    curLen++;
                else
                    curLen = 1;

                maxLen = Math.Max(maxLen, curLen);
            }

            return maxLen;
        }

        static int MaxLength(string s)
        {
            List<string> pairs = new List<string>();

            for (int i = 0; i < s.Length; i += 2)
            {
                if (i + 1 < s.Length)
                    pairs.Add(s.Substring(i, 2));
                else
                    pairs.Add(s[i].ToString());
            }

            pairs = pairs
                .OrderByDescending(p => p.Count(c => c == '0'))
                .ToList();

            StringBuilder sb = new StringBuilder();
            foreach (var p in pairs)
                sb.Append(p);

            return LongestNonDecreasing(sb.ToString());
        }

        static void Main()
        {
            string s = "1100101";
            Console.WriteLine(MaxLength(s));
        }
    }
}