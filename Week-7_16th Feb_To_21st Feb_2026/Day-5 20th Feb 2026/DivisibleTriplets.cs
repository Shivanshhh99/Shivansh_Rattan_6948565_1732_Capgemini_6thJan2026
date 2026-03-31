using System.Collections.Generic;

namespace DivisibleTriplets
{
    internal class Program
    {
        static int DivisibleTripletCount(int[] arr, int d)
        {
            int n = arr.Length;
            int ans = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int k = 1; k < n; k++)
            {
                int expected = (d - (arr[k] % d) + d) % d;

                if (map.ContainsKey(expected))
                    ans += map[expected];

                for (int i = 0; i < k; i++)
                {
                    int x = (arr[i] + arr[k]) % d;
                    if (!map.ContainsKey(x))
                        map[x] = 0;
                    map[x]++;
                }
            }

            return ans;
        }

        static void Main()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            int d = 3;
            Console.WriteLine(DivisibleTripletCount(arr, d));
        }
    }
}