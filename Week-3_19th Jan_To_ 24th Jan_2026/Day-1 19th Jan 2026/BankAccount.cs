using System;

class BankAccount
{
    private int accountNumber;
    private string accountHolder;
    protected double balance;

    public BankAccount(int accNo, string name, double initialBalance)
    {
        accountNumber = accNo;
        accountHolder = name;
        balance = initialBalance;
    }

    public void Deposit(double amount)
    {
        if (amount > 0)
        {
            balance += amount;
            Console.WriteLine("Deposit successful.");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount.");
        }
    }

    public void Withdraw(double amount)
    {
        if (amount > 0 && amount <= balance)
        {
            balance -= amount;
            Console.WriteLine("Withdrawal successful.");
        }
        else
        {
            Console.WriteLine("Insufficient balance or invalid amount.");
        }
    }

    public virtual void DisplayAccountDetails()
    {
        Console.WriteLine("\nAccount Number: " + accountNumber);
        Console.WriteLine("Account Holder: " + accountHolder);
        Console.WriteLine("Balance: " + balance);
    }
}

// Savings Account
class SavingsAccount : BankAccount
{
    private double interestRate;

    public SavingsAccount(int accNo, string name, double balance, double rate)
        : base(accNo, name, balance)
    {
        interestRate = rate;
    }

    public void CalculateInterest()
    {
        double interest = balance * interestRate / 100;
        balance += interest;
        Console.WriteLine("Interest added: " + interest);
    }

    public override void DisplayAccountDetails()
    {
        base.DisplayAccountDetails();
        Console.WriteLine("Account Type: Savings");
        Console.WriteLine("Interest Rate: " + interestRate + "%");
    }
}

// Checking Account
class CheckingAccount : BankAccount
{
    public CheckingAccount(int accNo, string name, double balance)
        : base(accNo, name, balance)
    {
    }

    public override void DisplayAccountDetails()
    {
        base.DisplayAccountDetails();
        Console.WriteLine("Account Type: Checking");
    }
}

class BankManagementSystem
{
    static void Main()
    {
        BankAccount account = null;
        int choice;

        Console.WriteLine("----- Bank Management System -----");
        Console.Write("Enter Account Number: ");
        int accNo = int.Parse(Console.ReadLine());

        Console.Write("Enter Account Holder Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Initial Balance: ");
        double balance = double.Parse(Console.ReadLine());

        Console.WriteLine("\nSelect Account Type:");
        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Checking Account");
        int accType = int.Parse(Console.ReadLine());

        if (accType == 1)
        {
            Console.Write("Enter Interest Rate: ");
            double rate = double.Parse(Console.ReadLine());
            account = new SavingsAccount(accNo, name, balance, rate);
        }
        else
        {
            account = new CheckingAccount(accNo, name, balance);
        }

        do
        {
            Console.WriteLine("\n---- Menu ----");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Display Account Details");
            Console.WriteLine("4. Calculate Interest (Savings Only)");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter amount to deposit: ");
                    double depositAmount = double.Parse(Console.ReadLine());
                    account.Deposit(depositAmount);
                    break;

                case 2:
                    Console.Write("Enter amount to withdraw: ");
                    double withdrawAmount = double.Parse(Console.ReadLine());
                    account.Withdraw(withdrawAmount);
                    break;

                case 3:
                    account.DisplayAccountDetails();
                    break;

                case 4:
                    if (account is SavingsAccount savings)
                        savings.CalculateInterest();
                    else
                        Console.WriteLine("Interest not applicable for Checking Account.");
                    break;

                case 5:
                    Console.WriteLine("Thank you for using Bank Management System.");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 5);
    }
}
