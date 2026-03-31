namespace StreamBuzzCreator
{

    public class CreatorStats
    {
        public string CreatorName { get; set; }
        public double[] WeeklyLikes { get; set; }
    }

    internal class Program
    {
        public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();

        public void RegisterCreator(CreatorStats record)
        {
            EngagementBoard.Add(record);
        }

        public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var creator in records)
            {
                int count = creator.WeeklyLikes.Count(l => l >= likeThreshold);
                if (count > 0)
                    result.Add(creator.CreatorName, count);
            }

            return result;
        }

        public double CalculateAverageLikes()
        {
            if (EngagementBoard.Count == 0)
                return 0;

            double total = 0;
            int count = 0;

            foreach (var creator in EngagementBoard)
            {
                foreach (var like in creator.WeeklyLikes)
                {
                    total += like;
                    count++;
                }
            }

            return total / count;
        }

        public static void Main(string[] args)
        {
            Program p = new Program();
            bool running = true;

            while (running)
            {
                Console.WriteLine("1. Register Creator");
                Console.WriteLine("2. Show Top Posts");
                Console.WriteLine("3. Calculate Average Likes");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Creator Name:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                        double[] likes = new double[4];
                        for (int i = 0; i < 4; i++)
                            likes[i] = Convert.ToDouble(Console.ReadLine());
                        p.RegisterCreator(new CreatorStats { CreatorName = name, WeeklyLikes = likes });
                        Console.WriteLine("Creator registered successfully");
                        break;

                    case 2:
                        Console.WriteLine("Enter like threshold:");
                        double threshold = Convert.ToDouble(Console.ReadLine());
                        var result = p.GetTopPostCounts(EngagementBoard, threshold);
                        if (result.Count == 0)
                            Console.WriteLine("No top-performing posts this week");
                        else
                            foreach (var item in result)
                                Console.WriteLine(item.Key + " - " + item.Value);
                        break;

                    case 3:
                        Console.WriteLine("Overall average weekly likes: " + p.CalculateAverageLikes());
                        break;

                    case 4:
                        Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                        running = false;
                        break;
                }
            }
        }
    }
}