using EarthCheck.Services;
using Xunit;

namespace EarthCheck.Tests;

public class TransferServiceWithOverdraftTests
{
	private readonly ITransferService _transferService;

	public TransferServiceWithOverdraftTests()
	{
		_transferService = new TransferServiceWithOverdraft();
	}

	[Fact]
	public void TransferFromNegativeAccount()
	{
		var accountFrom = new Account(1, -0.001m);
		var accountTo = new Account(2, 0);
		var amount = 50;

		Assert.Throws<TransferService_AccountHasNegativeAmount_Exception>(() => _transferService.TransferAmount(accountFrom, accountTo, amount));
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

	[Fact]
	public void TransferWithOverdraftFeeApplied()
	{
		var accountFromBaseValue = 2.2m;
		var accountFrom = new Account(1, accountFromBaseValue);
		var accountToBaseValue = 43.15m;
		var accountTo = new Account(2, accountToBaseValue);
		var amount = 3.3m;
		//TODO this value should come from configuration or DB to be same as in TransferServiceWithOverdraft
		decimal overdraftFeePercentage = 2m;
		var overdraftFee = Math.Round(Math.Abs(accountFromBaseValue - amount) * (overdraftFeePercentage / 100), 2);

		_transferService.TransferAmount(accountFrom, accountTo, amount);

		Assert.Equal(accountFromBaseValue - amount - overdraftFee, accountFrom.Balance);
		Assert.Equal(accountToBaseValue + amount, accountTo.Balance);
	}

	[Fact]
	public void TransferWithVeryLittleOverdraftFeeApplied()
	{
		var accountFromBaseValue = 1m;
		var accountFrom = new Account(1, accountFromBaseValue);
		var accountToBaseValue = 43.15m;
		var accountTo = new Account(2, accountToBaseValue);
		var amount = 1.0005m;
		//TODO this value should come from configuration or DB to be same as in TransferServiceWithOverdraft
		decimal overdraftFeePercentage = 2m;
		var overdraftFee = Math.Round(Math.Abs(accountFromBaseValue - amount) * (overdraftFeePercentage / 100), 2);

		_transferService.TransferAmount(accountFrom, accountTo, amount);

		Assert.Equal(accountFromBaseValue - amount - overdraftFee, accountFrom.Balance);
		Assert.Equal(accountToBaseValue + amount, accountTo.Balance);
	}
}
