namespace BallPassingGame
{
    internal class Program
    {
        static string LastPass(int N, int T)
        {
            int position = 1;
            int dir = 1;
            int from = -1, to = -1;

            for (int t = 1; t <= T; t++)
            {
                from = position;
                position += dir;

                if (position == N) dir = -1;
                else if (position == 1) dir = 1;

                to = position;
            }

            return from + " -> " + to;
        }

        public static void Main()
        {
            int N = 4;
            int T = 10;

            Console.WriteLine(LastPass(N, T));
        }
    }
}