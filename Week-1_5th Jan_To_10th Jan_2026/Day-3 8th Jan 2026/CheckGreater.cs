using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class CheckGreater
    {
        public int a, b,c;
        public void greater()
        {
            Console.WriteLine("Enter the value of a:");
            a= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the value of b:");
            b= int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the value of c:");
            c=int.Parse(Console.ReadLine());

            if(a>b && b>c)
            {
                Console.WriteLine("A is greater");
            }
            else if(b>a && b>c)
            {
                Console.WriteLine("B is greater");
            }
            else
            {
                Console.WriteLine("C is greater");
            }
        }

        static void Main(String[] args)
        {
            CheckGreater p= new CheckGreater();
            p.greater();
        }
    }
}
