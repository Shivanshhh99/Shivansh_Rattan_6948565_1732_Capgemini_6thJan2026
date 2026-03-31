using System;
using System.Linq;

namespace TeamStrengthMaximizer
{
    class MaxTeamStrength
    {
        static void Main(string[] args)
        {
            // Input skill levels
            Console.WriteLine("Enter employee skill levels (space-separated):");
            int[] skills = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // Input team sizes
            Console.WriteLine("Enter team sizes (space-separated):");
            int[] teamSizes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            long totalStrength = MaxTotalTeamStrength(skills, teamSizes);

            Console.WriteLine("Maximum total team strength: " + totalStrength);
        }

        static long MaxTotalTeamStrength(int[] skills, int[] teamSizes)
        {
            Array.Sort(skills); // Sort skills ascending
            Array.Sort(teamSizes); // Sort team sizes ascending

            int n = skills.Length;
            long totalStrength = 0;

            int left = 0;           // smallest skill index
            int right = n - 1;      // largest skill index

            // First, assign the largest skills as team max
            for (int i = teamSizes.Length - 1; i >= 0; i--)
            {
                int teamSize = teamSizes[i];

                int maxSkill = skills[right--];  // pick largest remaining skill
                int minSkill;

                if (teamSize == 1)
                {
                    minSkill = maxSkill;  // team has only one member
                }
                else
                {
                    minSkill = skills[left]; // pick smallest remaining skill
                    left += teamSize - 1;    // skip other team members (already counted as min)
                }

                totalStrength += maxSkill + minSkill;
            }

            return totalStrength;
        }
    }
}
