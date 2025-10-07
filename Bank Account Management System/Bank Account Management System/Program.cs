abstract class Account
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
