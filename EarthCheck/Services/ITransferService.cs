namespace EarthCheck.Services;

public interface ITransferService
{
	void TransferAmount(Account accountFrom, Account accountTo, decimal amount);
}
