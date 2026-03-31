using System;

namespace TripletsDivisibleByD
{
    class Program
    {
        static void Main(string[] args)
        {
            // Input array
            Console.WriteLine("Enter array elements (space-separated):");
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            // Input d
            Console.WriteLine("Enter integer d:");
            int d = Convert.ToInt32(Console.ReadLine());

            int count = CountDivisibleTriplets(arr, d);

            Console.WriteLine("Number of triplets divisible by " + d + ": " + count);
        }

        static int CountDivisibleTriplets(int[] arr, int d)
        {
            int n = arr.Length;
            int count = 0;

            // Triple nested loop for all i<j<k
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = i + 1; j < n - 1; j++)
                {
                    for (int k = j + 1; k < n; k++)
                    {
                        if ((arr[i] + arr[j] + arr[k]) % d == 0)
                        {
                            count++;
                        }
                    }
                }
            }

            return count;
        }
    }
}
