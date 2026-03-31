using NUnit.Framework;
using System;
using BankAccount;

namespace BankAccountTests
{
    [TestFixture]
    public class Tests
    {
        private BankAccountUtil account;

        [SetUp]
        public void Setup()
        {
            account = new BankAccountUtil(100);
        }

        [Test]
        public void Deposit_ValidAmount_IncreasesBalance()
        {
            account.Deposit(50);
            Assert.That(account.Balance, Is.EqualTo(150));
        }

        [Test]
        public void Withdraw_ValidAmount_DecreasesBalance()
        {
            account.Withdraw(40);
            Assert.That(account.Balance, Is.EqualTo(60));
        }

        [Test]
        public void Withdraw_InsufficientFunds_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(() => account.Withdraw(200));
        }
    }
}