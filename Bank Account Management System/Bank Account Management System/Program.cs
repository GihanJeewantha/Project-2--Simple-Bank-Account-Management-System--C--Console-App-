public abstract class Account
{
    string AccountNumber;
    decimal Balance;

    public Account (string accountNumber, decimal balance)
    {
        this.AccountNumber = accountNumber;
        this.Balance = balance;
    }


    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited {amount:C} successfully!");
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
            Console.WriteLine($"Withdrawn {amount:C} successfully!");
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
        Console.WriteLine($"[Savings] Account No: {AccountNumber} | Balance: {Balance:C} | Interest Rate: {InterestRate}%");
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
            Console.WriteLine($"Withdrawn {amount:C} successfully (Overdraft used if needed).");
        }
        else
        {
            Console.WriteLine("Invalid amount or exceeds overdraft limit!");
        }
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"[Current] Account No: {AccountNumber} | Balance: {Balance:C} | Overdraft Limit: {OverdraftLimit:C}");
    }
}



