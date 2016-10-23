
#include "stdafx.h"
#include "TransactionModule.h"
#include "TransactionHelper.h"
#include "../Shared/SharedFunctions.h"
#include "../Shared/Checks.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/Transaction.h"
#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"


TRANSACTIONMODULE_API int PayOut(int disposerId, int accountNumber, double amount, CURRENCY currency)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber) && CheckAmount(amount)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc;
	Customer* customer;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &customer);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	double balance = 0;

	returnValue = AccountBalancing(disposerId, accountNumber, EUR, balance);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	double euroAmount = 0;

	returnValue = TranslateToEuro(currency, amount, euroAmount);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	if (acc->GetAccountType() == SavingsAccount && euroAmount > balance)
	{
		return E_INSUFFICIENT_FUNDS;
	}
	double factor = 0;

	//Währung muss schon gespeichert sein, sonst wären wir nicht so weit gekommen.
	GetCurrencyToEuroFactor(currency, factor);

	auto transaction = new Transaction(amount, factor, currency, acc, __nullptr, customer);

	GetStorage()->StoreTransaction(transaction);
	return E_OK;
}

TRANSACTIONMODULE_API int PayIn(int disposerId, int accountNumber, double amount, CURRENCY currency)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber) && CheckAmount(amount)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc;
	Customer* customer;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &customer);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	double factor;

	returnValue = GetCurrencyToEuroFactor(currency, factor);
	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto transaction = new Transaction(amount, factor, currency, __nullptr, acc, customer);
	GetStorage()->StoreTransaction(transaction);
	return E_OK;
}

TRANSACTIONMODULE_API int Transfer(int disposerId, int fromAccountNumber, int toAccountNumber, double amount, CURRENCY currency)
{
	if (!(CheckId(disposerId) && CheckId(fromAccountNumber) && CheckAmount(amount)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc;
	Customer* customer;

	auto returnValue = FindAccountAndAuthorizedDisposer(fromAccountNumber, disposerId, &acc, &customer);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto toAccount = GetAccount(toAccountNumber);

	if (toAccount == __nullptr || !toAccount->isActive())
	{
		return E_TARGET_ACCOUNT_NOT_FOUND;
	}

	double balance = 0;

	returnValue = AccountBalancing(disposerId, fromAccountNumber, EUR, balance);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	double euroAmount = 0;

	returnValue = TranslateToEuro(currency, amount, euroAmount);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	if (acc->GetAccountType() == SavingsAccount && euroAmount > balance)
	{
		return E_INSUFFICIENT_FUNDS;
	}
	double factor = 0;

	//Währung muss schon gespeichert sein, sonst wären wir nicht so weit gekommen.
	GetCurrencyToEuroFactor(currency, factor);

	auto transaction = new Transaction(amount, factor, currency, acc, toAccount, customer);

	GetStorage()->StoreTransaction(transaction);
	return E_OK;
}


TRANSACTIONMODULE_API int AccountStatement(int disposerId, int accountNumber, S_TRANSACTION * data, int & numberOfEntries)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber)))
	{
		return E_INVALID_PARAMETER;
	}

	Account * acc;
	Customer * customer;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &customer);

	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto transactions = TransactionHelper::GetAccountsTransactions(acc);
	numberOfEntries = transactions->size();

	if (data == __nullptr)
	{
		//möglicher Fehler weil sizeof uint liefert, wir aber nicht. Ist in der Praxis kein Problem weil unsere sizeof Transaction nicht sonderlich groß ist.
		return numberOfEntries * sizeof(S_TRANSACTION);
	}


	if (numberOfEntries == 0) // Keine Transaktionen gefunden -> kein Fehler
	{
		return E_OK;
	}

	auto tr = new S_TRANSACTION*[transactions->size()];

	auto it = transactions->begin();

	for (int i = 0; i < numberOfEntries; ++i, ++it)
	{
		auto transaction = *it;

		(*data).amount = transaction->getAmount();
		(*data).factor = transaction->getFactor();
		(*data).currency = transaction->getCurrency();

		//Transaktion kann auch eine Bar Einzahlung from = BAR_TRANSACTION oder Bar Auszahlung to = BAR_TRANSACTION sein.
		(*data).fromAccount = BAR_TRANSACTION;
		(*data).toAccount = BAR_TRANSACTION;

		if (transaction->getFrom() != __nullptr)
		{
			(*data).fromAccount = transaction->getFrom()->GetAccountNumber();
		}

		if (transaction->getTo() != __nullptr)
		{
			(*data).toAccount = transaction->getTo()->GetAccountNumber();
		}

		(*data).disposer = transaction->getDisposer()->getId();
		data++;
	}

	return E_OK;
}

TRANSACTIONMODULE_API int AccountBalancing(int disposerId, int accountNumber, CURRENCY currency, double& balance)
{
	if (!(CheckId(accountNumber) && CheckId(disposerId)))
	{
		return E_INVALID_PARAMETER;
	}

	Account * acc;
	Customer * customer;

	balance = 0;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &customer);
	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto transactions = TransactionHelper::GetAccountsTransactions(acc);

	for (auto it = transactions->begin(); it != transactions->end(); ++it)
	{
		auto transaction = *it;
		if (TransactionHelper::IsFrom(transaction, acc))
		{
			balance -= transaction->getAmount() * transaction->getFactor();
		}
		else
		{
			balance += transaction->getAmount() * transaction->getFactor();
		}
	}

	returnValue = TranslateFromEuro(currency, balance, balance);
	if (returnValue != E_OK) 
	{
		return returnValue;
	}
	return E_OK;
}
