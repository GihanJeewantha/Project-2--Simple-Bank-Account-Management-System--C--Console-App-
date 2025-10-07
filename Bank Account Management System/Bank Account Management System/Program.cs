using System;
using System.Collections.Generic;
using System.Globalization;

namespace BankSystem
{
    public abstract class Account
    {
        private string accountNumber;
        private decimal balance;

        public string AccountNumber
        {
            get { return accountNumber; }
            protected set { accountNumber = value; }
        }

        public decimal Balance
        {
            get { return balance; }
            protected set { balance = value; }
        }

        protected Account(string accountNumber, decimal balance)
        {
            AccountNumber = accountNumber;
            Balance = balance;
        }

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Console.WriteLine($"Deposited Rs {amount:N2} successfully!");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive!");
            }
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn Rs {amount:N2} successfully!");
            }
            else
            {
                Console.WriteLine("Invalid amount or insufficient funds!");
            }
        }

        public abstract void ShowInfo();
    }

    public class SavingsAccount : Account
    {
        public decimal InterestRate { get; set; }

        public SavingsAccount(string accNumber, decimal initialBalance, decimal interestRate)
            : base(accNumber, initialBalance)
        {
            InterestRate = interestRate;
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Savings] Account No: {AccountNumber} | Balance: Rs {Balance:N2} | Interest Rate: {InterestRate}%");
        }
    }

    public class CurrentAccount : Account
    {
        public decimal OverdraftLimit { get; set; }

        public CurrentAccount(string accNumber, decimal initialBalance, decimal overdraftLimit)
            : base(accNumber, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= (Balance + OverdraftLimit))
            {
                Balance -= amount;
                Console.WriteLine($"Withdrawn Rs {amount:N2} successfully (Overdraft used if needed).");
            }
            else
            {
                Console.WriteLine("Invalid amount or exceeds overdraft limit!");
            }
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"[Current] Account No: {AccountNumber} | Balance: Rs {Balance:N2} | Overdraft Limit: Rs {OverdraftLimit:N2}");
        }
    }

    public class Bank
    {
        private readonly List<Account> accounts = new List<Account>();

        public void AddAccount(Account acc)
        {
            accounts.Add(acc);
            Console.WriteLine("Account added successfully!\n");
        }

        public void ShowAllAccounts()
        {
            if (accounts.Count == 0)
            {
                Console.WriteLine("No accounts found.\n");
                return;
            }

            Console.WriteLine("\n--- All Accounts ---");
            foreach (var acc in accounts)
            {
                acc.ShowInfo();
            }
            Console.WriteLine();
        }

        public Account? FindAccount(string accNumber)
        {
            foreach (var acc in accounts)
            {
                if (acc.AccountNumber == accNumber)
                    return acc;
            }
            return null;
        }

        public void Deposit(string accNumber, decimal amount)
        {
            var acc = FindAccount(accNumber);
            if (acc != null)
                acc.Deposit(amount);
            else
                Console.WriteLine("Account not found!");
        }

        public void Withdraw(string accNumber, decimal amount)
        {
            var acc = FindAccount(accNumber);
            if (acc != null)
                acc.Withdraw(amount);
            else
                Console.WriteLine("Account not found!");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Bank bank = new Bank();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n🏦 Bank Management System (Rs)");
                Console.WriteLine("1. Create Savings Account");
                Console.WriteLine("2. Create Current Account");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Withdraw");
                Console.WriteLine("5. Show All Accounts");
                Console.WriteLine("6. Exit");
                Console.Write("Enter choice: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Account Number: ");
                        string? sAccNum = Console.ReadLine();
                        Console.Write("Enter Initial Balance (Rs): ");
                        decimal sBalance = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter Interest Rate (%): ");
                        decimal interest = Convert.ToDecimal(Console.ReadLine());

                        if (sAccNum != null)
                        {
                            var sAcc = new SavingsAccount(sAccNum, sBalance, interest);
                            bank.AddAccount(sAcc);
                        }
                        break;

                    case "2":
                        Console.Write("Enter Account Number: ");
                        string? cAccNum = Console.ReadLine();
                        Console.Write("Enter Initial Balance (Rs): ");
                        decimal cBalance = Convert.ToDecimal(Console.ReadLine());
                        Console.Write("Enter Overdraft Limit (Rs): ");
                        decimal overdraft = Convert.ToDecimal(Console.ReadLine());

                        if (cAccNum != null)
                        {
                            var cAcc = new CurrentAccount(cAccNum, cBalance, overdraft);
                            bank.AddAccount(cAcc);
                        }
                        break;

                    case "3":
                        Console.Write("Enter Account Number: ");
                        string? dAccNum = Console.ReadLine();
                        Console.Write("Enter Amount (Rs): ");
                        decimal dAmount = Convert.ToDecimal(Console.ReadLine());
                        if (dAccNum != null)
                            bank.Deposit(dAccNum, dAmount);
                        break;

                    case "4":
                        Console.Write("Enter Account Number: ");
                        string? wAccNum = Console.ReadLine();
                        Console.Write("Enter Amount (Rs): ");
                        decimal wAmount = Convert.ToDecimal(Console.ReadLine());
                        if (wAccNum != null)
                            bank.Withdraw(wAccNum, wAmount);
                        break;

                    case "5":
                        bank.ShowAllAccounts();
                        break;

                    case "6":
                        running = false;
                        Console.WriteLine("Goodbye! 👋");
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }
            }
        }
    }
}
