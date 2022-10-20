namespace EarthCheck.Services;

public class TransferService_Transfer_Exception : Exception
{
	public TransferService_Transfer_Exception(Account account, Account accountTo, decimal amount, Exception ex)
		: base($"Something went wrong while transfering {amount} amount from account {account.accountNumber} to account {accountTo}.", ex)
	{

	}
}

public class TransferService_AccountHasNoCoverage_Exception : Exception
{
	public TransferService_AccountHasNoCoverage_Exception(Account accountFrom, Account accountTo, decimal amount)
		: base($"The account {accountFrom.accountNumber} has no coverage to be charged with {amount} amount to transfer to {accountTo}.")
	{

	}
}

public class TransferService_ZeroOrNegativeAmount_Exception : Exception
{
	public TransferService_ZeroOrNegativeAmount_Exception(Account account, Account accountTo, decimal amount)
		: base($"The account {account.accountNumber} can not be charged with zero or negative amount {amount} to transfer to {accountTo}.")
	{

	}
}

public class TransferService_AccountHasNegativeAmount_Exception : Exception
{
	public TransferService_AccountHasNegativeAmount_Exception(Account accountFrom, Account accountTo, decimal amount)
		: base($"The account {accountFrom.accountNumber} has negative amount (possible previous overcharge) can not be charged with {amount} amount to transfer to {accountTo}.")
	{

	}
}