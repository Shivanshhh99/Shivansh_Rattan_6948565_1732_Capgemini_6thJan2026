namespace MaximumTeamStrength
{
    internal class Program
    {
        static int MaximizeStrength(int[] empSkill, int[] teamSize)
        {
            Array.Sort(empSkill);
            Array.Sort(teamSize);

            int n = empSkill.Length;
            int m = teamSize.Length;

            int left = 0;
            int right = n - 1;
            long total = 0;

            int[] maxVals = new int[m];

            for (int i = m - 1; i >= 0; i--)
                maxVals[i] = empSkill[right--];

            for (int i = 0; i < m; i++)
            {
                if (teamSize[i] == 1)
                {
                    total += 2L * maxVals[i];
                }
                else
                {
                    total += maxVals[i] + empSkill[left];
                    left += teamSize[i] - 1;
                }
            }

            return (int)total;
        }

        static void Main()
        {
            int[] empSkill = { 1, 10, 5, 9, 9 };
            int[] teamSize = { 1, 1, 3 };

            Console.WriteLine(MaximizeStrength(empSkill, teamSize));
        }
    }
}