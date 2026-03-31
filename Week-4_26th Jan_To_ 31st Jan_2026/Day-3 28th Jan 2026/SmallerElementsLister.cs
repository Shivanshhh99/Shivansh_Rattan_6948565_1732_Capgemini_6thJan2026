using System;
using System.Collections.Generic;

class SmallerElementsLister
{
    public static List<int> GetElements(List<int> input1, int input2)
    {
        List<int> result = new List<int>();

        foreach (int num in input1)
        {
            if (num < input2)
            {
                result.Add(num);
            }
        }

        if (result.Count == 0)
        {
            result.Add(-1);
            return result;
        }

        // Sort in descending order
        result.Sort();
        result.Reverse();

        return result;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter number of elements:");
        int n = int.Parse(Console.ReadLine());

        List<int> inputList = new List<int>();

        Console.WriteLine("Enter the elements:");
        for (int i = 0; i < n; i++)
        {
            inputList.Add(int.Parse(Console.ReadLine()));
        }

        Console.WriteLine("Enter the comparison value:");
        int input2 = int.Parse(Console.ReadLine());

        List<int> output = SmallerElementsLister.GetElements(inputList, input2);

        if (output.Count == 1 && output[0] == -1)
        {
            Console.WriteLine("No element found");
        }
        else
        {
            Console.WriteLine("Output:");
            foreach (int val in output)
            {
                Console.Write(val + " ");
            }
        }
    }
}
