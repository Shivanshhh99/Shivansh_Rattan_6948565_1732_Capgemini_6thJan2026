using System;

class Func
{
    static void Main()
    {
        // Func with two inputs and one output
        Func<int, int, int> addNumbers = (a, b) => a + b;
        Console.WriteLine(addNumbers(5, 3)); 

        // Func with one input and one output
        Func<string, int> getLength = text => text.Length;
        Console.WriteLine(getLength("Hello"));
    }
}
