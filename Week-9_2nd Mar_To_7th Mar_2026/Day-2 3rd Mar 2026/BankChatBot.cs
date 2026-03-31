using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public interface IBankAccountOperation
{
    void Deposit(decimal d);
    void Withdraw(decimal d);
    decimal ProcessOperation(string message);
}

class BankOperations : IBankAccountOperation
{
    private decimal balance = 0;

    public void Deposit(decimal d)
    {
        balance += d;
    }

    public void Withdraw(decimal d)
    {
        if (balance >= d)
        {
            balance -= d;
        }
    }

    public decimal ProcessOperation(string message)
    {
        message = message.ToLower();

        decimal amount = 0;

        // extract number from sentence
        Match match = Regex.Match(message, @"\d+");

        if (match.Success)
        {
            amount = Convert.ToDecimal(match.Value);
        }

        if (message.Contains("deposit") ||
            message.Contains("put") ||
            message.Contains("invest") ||
            message.Contains("transfer"))
        {
            Deposit(amount);
        }
        else if (message.Contains("withdraw") ||
                 message.Contains("pull"))
        {
            Withdraw(amount);
        }

        return balance;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankOperations opt = new BankOperations();

        Console.WriteLine("Enter number of operations:");
        int n = Convert.ToInt32(Console.ReadLine());

        List<string> inputs = new List<string>();

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter operation message {i + 1}:");
            inputs.Add(Console.ReadLine());
        }

        Console.WriteLine("\nProcessing Operations...\n");

        foreach (var item in inputs)
        {
            Console.WriteLine(opt.ProcessOperation(item));
        }

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }
}