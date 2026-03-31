using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class Mcq_answer
    {
       public void question()
        {
            Console.WriteLine("What is the capital of Himachal Pradesh?");
            Console.WriteLine("a) Una");
            Console.WriteLine("b) Chamba");
            Console.WriteLine("c) Kullu");
            Console.WriteLine("d) Shimla");

            Console.Write("Enter your answer: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid Choice");
                return;
            }

            char answer = char.ToLower(input[0]);

            switch (answer)
            {
                case 'a':
                case 'b':
                case 'c':
                    Console.WriteLine("Incorrect Answer");
                    break;

                case 'd':
                    Console.WriteLine("Correct Answer");
                    break;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }

        public static void Main(string[] args)
        {
            Mcq_answer p = new Mcq_answer();
            p.question();
        }
    }
}