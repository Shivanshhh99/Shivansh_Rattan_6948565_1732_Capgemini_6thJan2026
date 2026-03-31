using System;

class FindSecondLargest
{
    public int size,maxi,second_maxi;
    public int[] arr;

    public int Find_second_largest()
    {
        Console.Write("Enter the size of arrays: ");
        size = Convert.ToInt32(Console.ReadLine());
        if(size<0)
        {
            return -2;
        }
        arr= new int[size];

        Console.WriteLine("Enter the elements of Array :");
        for (int i = 0; i < size; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if(arr[i]<0)
            {
                return -1;
            }
        }
        maxi=arr[0];
        second_maxi=-1;

        for(int i=0;i<size;i++)
        {
            if(arr[i]>maxi)
            {
                second_maxi=maxi;
                maxi=arr[i];
            }
            else if(second_maxi<arr[i])
            {
                second_maxi=arr[i];
            }
        }
        return second_maxi;
    }

    static void Main()
    {
        FindSecondLargest s= new FindSecondLargest();
        int result= s.Find_second_largest();
        Console.Write("Second largest is "+ result);
    }
}
