namespace EarthCheck.Services;

public class TransferServiceWithOverdraft : ITransferService
{
	//TODO - This value should come from configuration or DataBase
	private const decimal _overdraftFeePercentage = 2;

	public void TransferAmount(Account accountFrom, Account accountTo, decimal amount)
	{
		CheckAmountValidity(accountFrom, accountTo, amount);

		CheckSourceAccountCoverage(accountFrom, accountTo, amount);

		var overdraftFee = IsOverdraft(accountFrom, amount) ? CalculateOverdraftFee(accountFrom.Balance - amount) : 0;

		try
		{
			accountFrom.Withdraw(amount + overdraftFee);
			accountTo.Deposit(amount);
		}
		catch (Exception ex)
		{
			throw new TransferService_Transfer_Exception(accountFrom, accountTo, amount, ex);
		}
	}

	private void CheckSourceAccountCoverage(Account accountFrom, Account accountTo, decimal amount)
	{
		if (accountFrom.Balance < 0)
			throw new TransferService_AccountHasNegativeAmount_Exception(accountFrom, accountTo, amount);
	}

	private void CheckAmountValidity(Account accountFrom, Account accountTo, decimal amount)
	{
		if (amount <= 0)
			throw new TransferService_ZeroOrNegativeAmount_Exception(accountFrom, accountTo, amount);
	}

	private bool IsOverdraft(Account accountFrom, decimal amount) =>
		accountFrom.Balance - amount < 0;

	private decimal CalculateOverdraftFee(decimal overdraftAmount) =>
		Math.Round(Math.Abs(overdraftAmount) * (_overdraftFeePercentage / 100), 2);
}