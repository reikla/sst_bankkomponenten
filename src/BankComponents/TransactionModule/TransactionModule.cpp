
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

	returnValue = AccountBalancing(accountNumber, disposerId, EUR, balance);

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

	GetStorage()->GetTransactions()->push_back(transaction);
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

	returnValue = AccountBalancing(fromAccountNumber, disposerId, EUR, balance);

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

	GetStorage()->GetTransactions()->push_back(transaction);
	return E_OK;
}


TRANSACTIONMODULE_API int AccountStatement(int disposerId, int accountNumber, S_TRANSACTION *** data, int & numberOfEntries)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber) && CheckPointer(data)))
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

	if (numberOfEntries == 0) // Keine Transaktionen gefunden -> kein Fehler
	{
		return E_OK;
	}

	auto tr = new S_TRANSACTION*[transactions->size()];

	auto it = transactions->begin();

	for (int i = 0; i < numberOfEntries; ++i, ++it)
	{
		auto transaction = *it;
		auto s_transaction = new S_TRANSACTION;
		s_transaction->amount = transaction->getAmount();
		s_transaction->factor = transaction->getFactor();
		s_transaction->currency = transaction->getCurrency();

		//Transaktion kann auch eine Bar Einzahlung from = BAR_TRANSACTION oder Bar Auszahlung to = BAR_TRANSACTION sein.
		s_transaction->fromAccount = BAR_TRANSACTION;
		s_transaction->toAccount = BAR_TRANSACTION;

		if (transaction->getFrom() != __nullptr)
		{
			s_transaction->fromAccount = transaction->getFrom()->GetAccountNumber();
		}

		if (transaction->getTo() != __nullptr)
		{
			s_transaction->toAccount = transaction->getTo()->GetAccountNumber();
		}

		s_transaction->disposer = transaction->getDisposer()->getId();
		tr[i] = s_transaction;
	}
	*data = tr;

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
	if (returnValue != E_OK) {
		return returnValue;
	}
	return E_OK;
}
