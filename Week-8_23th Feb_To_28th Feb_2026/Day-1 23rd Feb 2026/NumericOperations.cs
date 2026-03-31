namespace NumericOperations
{
    class Source
    {
        public static int Sum(List<int> list)
        {
            return list.Sum();
        }

        public static int? GetItemAtIndex(List<int> list, int index)
        {
            if (index < 0 || index >= list.Count)
                return null;

            return list[index];
        }

        public static List<int> SplitAndReverse(List<int> list)
        {
            int mid = list.Count / 2;

            var first = list.Take(mid).Reverse();
            var second = list.Skip(mid).Reverse();

            return first.Concat(second).ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6 };

            Console.WriteLine(Source.Sum(nums));
            Console.WriteLine(Source.GetItemAtIndex(nums, 2));
            Console.WriteLine(string.Join(",", Source.SplitAndReverse(nums)));
        }
    }
}