using System;

// Define a delegate type
public delegate int CalculationDelegate(int x, int y);

class DelegateDemo
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static int Multiply(int a, int b)
    {
        return a * b;
    }

    static void Main()
    {
        CalculationDelegate calc = Add;
        Console.WriteLine(calc(5, 3));

        calc = Multiply;
        Console.WriteLine(calc(5, 3)); 
    }
}
