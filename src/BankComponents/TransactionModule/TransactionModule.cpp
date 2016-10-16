
#include "stdafx.h"
#include "TransactionModule.h"
#include "TransactionHelper.h"
#include "../Shared/SharedFunctions.h"
#include "../Shared/Checks.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/Transaction.h"
#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"


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
	if (returnValue != S_OK)
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

	TranslateFromEuro(currency, balance, balance);
	return S_OK;
}
