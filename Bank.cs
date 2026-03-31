// using System;

// namespace Bank
// {
//     internal class Bank
//     {
//         const int MIN_BALANCE = 1000;

//         static string BankName;
//         static int TotalAccounts;
//         public readonly int accountNumber;
//         public string cust_name = "", pancard = "";
//         public int age, aadhar, currentamount;

//         static Case_study_bank_acc()
//         {
//             BankName = "LOK KALYAAN BANK";
//             TotalAccounts = 0;
//             Console.WriteLine("Bank system initialized");
//         }

//         public Case_study_bank_acc()
//         {
//             accountNumber = new Random().Next(10000, 99999);
//             TotalAccounts++;
//         }

//         public Case_study_bank_acc(string name, int age, int aadhar, string pan, int balance)
//         {
//             cust_name = name;
//             this.age = age;
//             this.aadhar = aadhar;
//             pancard = pan;
//             currentamount = balance;

//             accountNumber = new Random().Next(10000, 99999);
//             TotalAccounts++;
//         }

//         public static void ShowBankDetails()
//         {
//             Console.WriteLine("\nBank Name: " + BankName);
//             Console.WriteLine("Total Accounts Opened: " + TotalAccounts);
//         }

//         public void actions()
//         {
//             int choice;
//             do
//             {
//                 Console.WriteLine($"\n****** WELCOME TO {BankName} ******");
//                 Console.WriteLine("1. Deposit");
//                 Console.WriteLine("2. Withdraw");
//                 Console.WriteLine("3. Check Balance");
//                 Console.WriteLine("4. Exit");

//                 choice = int.Parse(Console.ReadLine()!);

//                 switch (choice)
//                 {
//                     case 1: deposit(); break;
//                     case 2: withdraw(); break;
//                     case 3: showbalance(); break;
//                     case 4: Console.WriteLine("Thank you for banking with us"); break;
//                     default: Console.WriteLine("Invalid choice"); break;
//                 }
//             } while (choice != 4);
//         }

//         public void deposit()
//         {
//             Console.Write("Enter amount to deposit: ");
//             int amt = int.Parse(Console.ReadLine()!);
//             currentamount += amt;
//             Console.WriteLine("Amount Deposited Successfully");
//         }

//         public void withdraw()
//         {
//             Console.Write("Enter amount to withdraw: ");
//             int amt = int.Parse(Console.ReadLine()!);

//             if (currentamount - amt < MIN_BALANCE)
//             {
//                 Console.WriteLine("Minimum balance of " + MIN_BALANCE + " must be maintained");
//             }
//             else
//             {
//                 currentamount -= amt;
//                 Console.WriteLine("Amount Withdrawn Successfully");
//             }
//         }

//         public void showbalance()
//         {
//             Console.WriteLine("Current Balance: " + currentamount);
//         }

//         static void Main(string[] args)
//         {
//             Bank acc = new Bank("Shivansh", 22, 123456, "ABCDE1234F", 5000);
//             ShowBankDetails();
//             acc.actions();
//         }
//     }
// }
