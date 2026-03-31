using System;
using System.Collections.Generic;

class DonationCalculator
{
    public static int getDonation(string[] input1, int input2)
    {
        HashSet<string> uniqueEntries = new HashSet<string>();
        int totalDonation = 0;

        foreach (string entry in input1)
        {
            // Business Rule 1: Duplicate check
            if (uniqueEntries.Contains(entry))
                return -1;

            uniqueEntries.Add(entry);

            // Business Rule 2: Special character check
            foreach (char c in entry)
            {
                if (!char.IsDigit(c))
                    return -2;
            }

            // Extract location code and donation amount
            int locationCode = int.Parse(entry.Substring(3, 3));
            int donationAmount = int.Parse(entry.Substring(6, 3));

            if (locationCode == input2)
            {
                totalDonation += donationAmount;
            }
        }

        return totalDonation;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter number of records:");
        int n = int.Parse(Console.ReadLine());

        string[] input1 = new string[n];

        Console.WriteLine("Enter donation details:");
        for (int i = 0; i < n; i++)
        {
            input1[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter location code:");
        int input2 = int.Parse(Console.ReadLine());

        int result = DonationCalculator.getDonation(input1, input2);

        Console.WriteLine("Output:");
        Console.WriteLine(result);
    }
}
