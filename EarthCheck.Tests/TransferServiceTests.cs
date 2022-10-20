using EarthCheck.Services;
using Xunit;

namespace EarthCheck.Tests;

public class TransferServiceTests
{
	private readonly ITransferService _transferService;

	public TransferServiceTests()
	{
		_transferService = new TransferService();
	}

	[Fact]
	public void TransferFromEmptyAccount()
	{
		var accountFrom = new Account(1, 0);
		var accountTo = new Account(2, 0);
		var amount = 50;

		Assert.Throws<TransferService_AccountHasNoCoverage_Exception>(() => _transferService.TransferAmount(accountFrom, accountTo, amount));
	}

	[Fact]
	public void TransferFromNegativeAccount()
	{
		var accountFrom = new Account(1, -11.11m);
		var accountTo = new Account(2, 0);
		var amount = 50;

		Assert.Throws<TransferService_AccountHasNoCoverage_Exception>(() => _transferService.TransferAmount(accountFrom, accountTo, amount));
	}

	[Fact]
	public void TransferWithZeroAmount()
	{
		var accountFrom = new Account(1, 0);
		var accountTo = new Account(2, 0);
		var amount = 0;

		Assert.Throws<TransferService_ZeroOrNegativeAmount_Exception>(() => _transferService.TransferAmount(accountFrom, accountTo, amount));
	}

	[Fact]
	public void TransferWithNegativeAmount()
	{
		var accountFrom = new Account(1, 0);
		var accountTo = new Account(2, 0);
		var amount = -5;

		Assert.Throws<TransferService_ZeroOrNegativeAmount_Exception>(() => _transferService.TransferAmount(accountFrom, accountTo, amount));
	}

	[Fact]
	public void TransferSuccsessful()
	{
		var accountFromBaseValue = 50;
		var accountFrom = new Account(1, accountFromBaseValue);
		var accountToBaseValue = 43.15m;
		var accountTo = new Account(2, accountToBaseValue);
		var amount = 5.5m;

		_transferService.TransferAmount(accountFrom, accountTo, amount);

		Assert.Equal(accountFromBaseValue - amount, accountFrom.Balance);
		Assert.Equal(accountToBaseValue + amount, accountTo.Balance);
	}

}