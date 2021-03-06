#pragma once
#include "SharedStorage.h"
#include "Customer.h"
#include "Account.h"
#include "ErrorCodes.h"
#include "Checks.h"

inline SharedStorage * GetStorage()
{
	return SharedStorage::GetInstance();
}

inline Customer * GetCustomer(int id)
{
	auto customers = GetStorage()->GetCustomers();

	for (auto it = customers->begin(); it != customers->end(); ++it)
	{
		auto customer = *it;
		if (customer->getId() == id) //Customer found
		{
			return customer;
		}
	}
	return __nullptr;
}

inline Account * GetAccount(int accountNumber)
{
	auto accounts = GetStorage()->GetAccounts();

	for(auto it = accounts->begin(); it != accounts->end();++it)
	{
		auto account = *it;
		if (account->GetAccountNumber() == accountNumber) 
		{
			return account;
		}
	}
	return __nullptr;
}

inline int FindAccountAndAuthorizedDisposer(int accountNumber, int disposerId, Account ** account, Customer ** disposer)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber)))
	{
		return E_INVALID_PARAMETER;
	}

	auto acc = GetAccount(accountNumber);

	if (acc == __nullptr || !acc->isActive())
	{
		return E_ACCOUNT_NOT_FOUND;
	}

	auto disp = GetCustomer(disposerId);

	if ((disp == __nullptr || !disp->isActive()) && disposerId != 0) // Kunde nicht gefunden oder nicht mehr aktiv
	{
		return E_CUSTOMER_NOT_FOUND;
	}

	auto accountsDisposers = acc->GetDisposers();

	bool isAuthorized = false;

	for (auto it = accountsDisposers->begin(); it != accountsDisposers->end(); ++it)
	{
		if ((*it)->getId() == disposerId || disposerId == 0) // kunde berechtigt.
		{
			isAuthorized = true;
			break;
		}
	}

	if (!isAuthorized)
	{
		return E_UNAUTHORIZED;
	}

	*account = acc;
	*disposer = disp;

	return E_OK;
}