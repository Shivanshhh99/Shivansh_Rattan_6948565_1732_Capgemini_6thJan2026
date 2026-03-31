using System;

class FahrenheitToCelsius
{
    static void Main()
    {
        Console.Write("Enter temperature in Fahrenheit: ");
        double fahrenheit = Convert.ToDouble(Console.ReadLine());
        double output;

        if (fahrenheit < 0)
        {
            output = -1;
        }
        else
        {
            output = (fahrenheit - 32) * 5 / 9;
        }

        Console.WriteLine("Output: " + output);
    }
}
