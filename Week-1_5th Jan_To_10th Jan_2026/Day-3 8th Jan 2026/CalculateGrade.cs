using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class CalculateGrade
    {
        public int marks;

        void determine_grade()
        {
            Console.WriteLine("Enter your Marks: ");
            marks= int.Parse(Console.ReadLine());
            if(marks>=90)
            {
                Console.WriteLine("A GRADE");
            }
            else if (marks>=80 && marks<90)
            {
                Console.WriteLine("B GRADE");
            }
            else if(marks>=70 && marks<80)
            {
                Console.WriteLine("C GRADE");
            }
            else if(marks>=60 && marks<70)
            {
                Console.WriteLine("D GRADE");
            }
            else if(marks>=50 && marks<60)
            {
                Console.WriteLine("E GRADE");
            }
            else{
                Console.WriteLine("F GRADE");
            }
        }

        public static void Main(String[] args)
        {
            CalculateGrade p= new CalculateGrade();
            p.determine_grade();
        }
    }
}