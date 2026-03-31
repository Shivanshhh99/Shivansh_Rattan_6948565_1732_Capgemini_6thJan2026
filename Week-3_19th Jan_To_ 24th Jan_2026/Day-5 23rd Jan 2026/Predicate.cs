using System;
using System.Collections.Generic;

class Predicate
{
    static void Main()
    {
        Predicate<int> isEvenNumber = number => number % 2 == 0;

        Console.WriteLine(isEvenNumber(4)); //True it is
        Console.WriteLine(isEvenNumber(7)); // False it is

        List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };
        int firstEven = numbers.Find(isEvenNumber);
        Console.WriteLine(firstEven); 
    }
}
