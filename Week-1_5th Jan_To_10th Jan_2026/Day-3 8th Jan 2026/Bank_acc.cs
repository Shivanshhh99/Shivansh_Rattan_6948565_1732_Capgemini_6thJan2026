using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp_CU
{
    internal class Bank_acc
    {
        public string cust_name, pancard;
        public int age, aadhar, currentamount;

        public void actions()
        {
            Console.WriteLine("****** WELCOME TO THE LOK KALYAAN BANK ******");
            Console.WriteLine("Choose what ever you want to perform:");
            Console.WriteLine(" 1. Deposit amount");
            Console.WriteLine(" 2. Withdraw amount");
            Console.WriteLine(" 3. Check current Balance");
            Console.WriteLine("Enter the sequence number of action you want to perform");

            int x = int.Parse(Console.ReadLine());
            if (x == 1)
            {
                deposit();
            }
            else if (x == 2)
            {
                withdraw();
            }
            else if (x == 3)
            {
                showbalance();
            }
            else
            {
                Console.WriteLine("Enter a valid input");
            }
        }

        public void deposit()
        {
            Console.WriteLine("Do you want to deposit the money? Enter Y or N");
            char c = char.Parse(Console.ReadLine());
            if (c == 'N')
                Console.WriteLine("Okay!. Visit again");
            else if (c == 'Y')
                Console.Write("Enter the amount you want to depost:");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine(x + " Amount is deposited");
            Console.WriteLine(x + currentamount + " is your new current balance");
        }
        public void withdraw()
        {
            Console.WriteLine("Do Want to withdraw money? Enter Y or N");
            char d = char.Parse(Console.ReadLine());
            if (d == 'N')
                Console.WriteLine("Okay!. Visit Again");
            else if (d == 'Y')
            {
                Console.WriteLine("Enter the amount you want to withdraw");
                int am = int.Parse(Console.ReadLine());
                if (am > currentamount)
                {
                    Console.WriteLine("Current balance is Low");
                }
                else
                {
                    Console.WriteLine(am + " amount debited from your account");
                    Console.WriteLine(currentamount - am + " is your current balance");
                }
            }
            else
            {
                Console.WriteLine("Enter a valid input");
            }
        }

        public void showbalance()
        {
            Console.WriteLine("Current balance is " + currentamount);
        }


        static void Main(string[] args)
        {
            Bank_acc Shivansh = new Bank_acc();
            Shivansh.actions();
        }
    }
}
