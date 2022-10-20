namespace EarthCheck.Services;

public class TransferService : ITransferService
{
	public void TransferAmount(Account accountFrom, Account accountTo, decimal amount)
	{
		checkAmountValidity(accountFrom, accountTo, amount);

		checkSourceAccountCoverage(accountFrom, accountTo, amount);

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

	private void checkSourceAccountCoverage(Account accountFrom, Account accountTo, decimal amount)
	{
		if (accountFrom.Balance < amount)
			throw new TransferService_AccountHasNoCoverage_Exception(accountFrom, accountTo, amount);
	}

	private void checkAmountValidity(Account accountFrom, Account accountTo, decimal amount)
	{
		if (amount <= 0)
			throw new TransferService_ZeroOrNegativeAmount_Exception(accountFrom, accountTo, amount);
	}
}
