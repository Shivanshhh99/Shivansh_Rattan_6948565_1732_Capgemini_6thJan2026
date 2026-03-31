using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class Factorial
    {
        public int output1,fact;

        public int find_Factorial()
        {
            Console.Write("Enter the Number : ");
            int x= int.Parse(Console.ReadLine());
            if(x<0)
            {
                return output1=-1;
            }
            else if(x>7)
            {
               return output1=-2;
            }
            else{
                fact=1;
                while(x>0)
                {
                    fact*=x;
                    x--;
                }
                return fact;
            }
        }

        public static void Main(String[] args)
        {
            Factorial f= new Factorial();
            int result= f.find_Factorial();
            Console.Write(result);
        }
    }
}