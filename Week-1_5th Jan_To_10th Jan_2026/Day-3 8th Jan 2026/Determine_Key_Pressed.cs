using System;
using System.Collections.Generic;
using System.Text;
namespace Democonsole
{
    internal class Determine_Key_Pressed
    {
        public char num;

        public void checkkey()
        {
            Console.WriteLine("Press a Key from 0-9");
            num= char.Parse(Console.ReadLine());
            if(num=='1' || num=='2'|| num=='3' || num=='4' || num=='5'|| num=='6'|| num=='7'||num=='8'|| num=='9'|| num=='0')
            {
                Console.WriteLine(num +" is pressed");
            }
            else {
                Console.WriteLine("Please enter a valid input");
            }
        }

        public static void Main(String[] args)
        {
            Determine_Key_Pressed p= new Determine_Key_Pressed();
            p.checkkey();
        }
    }
}