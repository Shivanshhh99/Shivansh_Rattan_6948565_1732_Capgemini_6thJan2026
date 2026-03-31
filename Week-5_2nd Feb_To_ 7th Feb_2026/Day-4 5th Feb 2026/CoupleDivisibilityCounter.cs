using System;

namespace CoupleSumDivisibility
{
    class CoupleDivisibilityCounter
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter array size (N):");
            int N = Convert.ToInt32(Console.ReadLine());

            int[] arr = new int[N];

            Console.WriteLine("Enter " + N + " integers separated by space:");
            string[] inputElements = Console.ReadLine().Split(' ');

            for (int i = 0; i < N; i++)
            {
                arr[i] = Convert.ToInt32(inputElements[i]);
            }

            int totalCouplesDivisible = CountCouplesDivisibleByN(arr, N);

            Console.WriteLine("Total number of couples divisible by " + N + ": " + totalCouplesDivisible);
        }

        // Method to count adjacent couples whose sum is divisible by N
        static int CountCouplesDivisibleByN(int[] arr, int N)
        {
            int count = 0;

            for (int i = 0; i < arr.Length - 1; i++) // Non-circular, so stop at Length - 1
            {
                int sum = arr[i] + arr[i + 1];

                if (sum % N == 0)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
