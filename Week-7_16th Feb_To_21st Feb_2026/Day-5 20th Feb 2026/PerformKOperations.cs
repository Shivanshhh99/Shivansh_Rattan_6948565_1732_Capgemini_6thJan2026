namespace PerformKOperations
{

    internal class Program
    {
        static int MaxAfterKOperations(int[] nums, int k)
        {
            PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>();

            foreach (int num in nums)
                maxHeap.Enqueue(num, -num);

            for (int i = 0; i < k; i++)
            {
                int max = maxHeap.Dequeue();
                int reduced = max / 2;
                maxHeap.Enqueue(reduced, -reduced);
            }

            return maxHeap.Peek();
        }

        static void Main()
        {
            int[] nums = { 5, 19, 8, 1 };
            int k = 3;
            Console.WriteLine(MaxAfterKOperations(nums, k));
        }
    }
}