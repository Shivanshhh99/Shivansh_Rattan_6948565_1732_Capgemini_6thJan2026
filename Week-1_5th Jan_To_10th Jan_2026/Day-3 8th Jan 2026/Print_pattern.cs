using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class Print_pattern
    {
        public void display()
        {
            Console.WriteLine("Enter a value:");
            int x= int.Parse(Console.ReadLine());
            for(int i=x;i>0;i--)
            {
                for(int j=0;j<i;j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public static void Main()
        {
            Print_pattern p= new Print_pattern();
            p.display();
        }
    }
}