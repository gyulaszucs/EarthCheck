public class Account
{
	private decimal balance;
	public int accountNumber { get; }
	public Account(int accountNumber, decimal balance)
	{
		this.balance = balance;
		this.accountNumber = accountNumber;
	}

	public decimal Balance
	{
		get { return this.balance; }
	}
	public void Withdraw(decimal amount)
	{
		this.balance -= amount;
	}
	public void Deposit(decimal amount)
	{
		this.balance += amount;
	}
}
