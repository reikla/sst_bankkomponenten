// AccountModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "AccountModule.h"
#include "../Shared/Account.h"

ACCOUNTMODULE_API int CreateAccount(int customerId, char * accountName, ACCOUNT_TYPE accountType, int & accountNumber)
{
	if (!(CheckId(customerId) && CheckString(accountName)))
	{
		return E_INVALID_PARAMETER;
	}
	auto customer = GetCustomer(customerId);
	if (customer == __nullptr)
	{
		return E_CUSTOMER_NOT_FOUND;
	}

	auto account = new Account(accountName, accountType);
	GetStorage()->StoreAccount(account);

	customer->getAccounts()->push_back(account);
	account->GetDisposers()->push_back(customer);

	accountNumber = account->GetAccountNumber();

	return E_OK;
}
