using System;

class Multiple_of_three
{
    public int size,output1,count=0;

    public int multiple()
    {
        Console.WriteLine("Enter the size of the array: ");
        size=Convert.ToInt32(Console.ReadLine());
        if(size<0)
        {
            return -2;
        }
        int[] arr= new int[size];
        Console.WriteLine("Enter the elements of the array: ");

        for(int i=0;i<size;i++)
        {
            arr[i]=Convert.ToInt32(Console.ReadLine());
            if(arr[i]<0)
            {
                output1=-1;
                Console.WriteLine("Negative Number Found");
                return -1;
            }
        }

        for(int i=0;i<size;i++)
        {
            if(arr[i]%3==0)
            {
                count++;
            }
        }

        return count;
    }

    static void Main()
    {
        Multiple_of_three m= new Multiple_of_three();
        int x= m.multiple();
        Console.WriteLine("Output1: " +x);
    }
}