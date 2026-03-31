using System;

namespace NextGreaterDivisible
{
    class NextGreaterDivisibleCounter
    {
        static void Main(string[] args)
        {
            // Input array size
            Console.WriteLine("Enter the number of elements (N):");
            int N = Convert.ToInt32(Console.ReadLine());

            int[] arr = new int[N];

            // Input array elements
            Console.WriteLine("Enter the array elements separated by space:");
            string[] elements = Console.ReadLine().Split(' ');
            for (int i = 0; i < N; i++)
            {
                arr[i] = Convert.ToInt32(elements[i]);
            }

            int count = CountNextGreaterDivisible(arr);

            // Output result
            Console.WriteLine("Count of elements having next greater divisible element: " + count);
        }

        // Method to count elements with next greater divisible element
        static int CountNextGreaterDivisible(int[] arr)
        {
            int count = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                int X = arr[i];
                bool found = false;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] > X && arr[j] % X == 0)
                    {
                        found = true;
                        break; // Only need the first such element
                    }
                }

                if (found)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
