using System;

class SearchElement
{
    static void Main()
    {
        Console.WriteLine("Enter the number of elements:");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine("-2");
            return;
        }

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0)
            {
                Console.WriteLine("-1");
                return;
            }
        }

        Console.WriteLine("Enter the element to search:");
        int search = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            if (arr[i] == search)
            {
                Console.WriteLine("1");   
                return;
            }
        }

        Console.WriteLine("-3");  
    }
}
