using System;

class Add_first_to_last_of_another_array
{
    public int size;
    public int[] arr1;
    public int[] arr2;
    public int[] resultant;

    public int[] result()
    {
        Console.Write("Enter the size of arrays: ");
        size = Convert.ToInt32(Console.ReadLine());
        if(size<0)
        {
            return new int[]{-2};
        }

        arr1 = new int[size];
        arr2 = new int[size];

        Console.WriteLine("Enter the elements of Array 1:");
        for (int i = 0; i < size; i++)
        {
            arr1[i] = Convert.ToInt32(Console.ReadLine());
            if(arr1[i]<0)
            {
                return new int[]{-1};
            }
        }

        Console.WriteLine("Enter the elements of Array 2:");
        for (int i = 0; i < size; i++)
        {
            arr2[i] = Convert.ToInt32(Console.ReadLine());
            if(arr2[i]<0)
            {
                return new int[]{-1};
            }
        }
        resultant = new int[size];

        for (int i = 0; i < size; i++)
        {
            resultant[i] = arr1[i] + arr2[size - 1 - i];
        }

        return resultant;
    }

    static void Main()
    {
        Add_first_to_last_of_another_array obj = new Add_first_to_last_of_another_array();
        int[] arr = obj.result();

        Console.WriteLine("Resultant Array:");
        for (int i = 0; i < obj.size; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }
}
