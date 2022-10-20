namespace EarthCheck.Services;

public class TransferService : ITransferService
{
	public void TransferAmount(Account accountFrom, Account accountTo, decimal amount)
	{
		CheckAmountValidity(accountFrom, accountTo, amount);

		CheckSourceAccountCoverage(accountFrom, accountTo, amount);

		try
		{
			accountFrom.Withdraw(amount);
			accountTo.Deposit(amount);
		}
		catch (Exception ex)
		{
			throw new TransferService_Transfer_Exception(accountFrom, accountTo, amount, ex);
		}


	}

	private void CheckSourceAccountCoverage(Account accountFrom, Account accountTo, decimal amount)
	{
		if (accountFrom.Balance < amount)
			throw new TransferService_AccountHasNoCoverage_Exception(accountFrom, accountTo, amount);
	}

	private void CheckAmountValidity(Account accountFrom, Account accountTo, decimal amount)
	{
		if (amount <= 0)
			throw new TransferService_ZeroOrNegativeAmount_Exception(accountFrom, accountTo, amount);
	}
}
