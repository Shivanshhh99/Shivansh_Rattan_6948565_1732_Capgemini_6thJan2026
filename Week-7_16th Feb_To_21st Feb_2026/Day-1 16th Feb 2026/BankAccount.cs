using System;

namespace BankAccount
{
    public class BankAccountUtil
    {
        public decimal Balance { get; private set; }

        public BankAccountUtil(decimal initialBalance)
        {
            Balance = initialBalance;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
                throw new InvalidOperationException("Insufficient funds");

            Balance -= amount;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccountUtil account = new BankAccountUtil(1000);

            Console.WriteLine("Initial Balance: " + account.Balance);

            account.Deposit(500);
            Console.WriteLine("After Deposit: " + account.Balance);

            account.Withdraw(300);
            Console.WriteLine("After Withdraw: " + account.Balance);

            try
            {
                account.Withdraw(2000);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}