// AccountModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "AccountModule.h"
#include "../Shared/Account.h"

ACCOUNTMODULE_API int CreateAccount(int disposerId, char * accountName, ACCOUNT_TYPE accountType, int & accountNumber)
{
	if (!(CheckId(disposerId) && CheckString(accountName)))
	{
		return E_INVALID_PARAMETER;
	}
	auto customer = GetCustomer(disposerId);
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

ACCOUNTMODULE_API int CloseAccount(int disposerId, int accountNumber)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc = __nullptr;
	Customer* disp = __nullptr;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &disp);
	if (returnValue != E_OK) 
	{
		return returnValue;
	}

	acc->setIsActive(false);
	acc->GetDisposers()->remove(disp);
	disp->getAccounts()->remove(acc);

	return E_OK;
}

ACCOUNTMODULE_API int AddDisposer(int disposerId, int accountNumber, int newDisposerId)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber) && CheckId(disposerId)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc = __nullptr;
	Customer* disp = __nullptr;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &disp);
	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto newDisposer = GetCustomer(newDisposerId);

	if (newDisposer == __nullptr)
	{
		return E_NEW_DISPOSER_NOT_FOUND;
	}

	acc->GetDisposers()->push_back(newDisposer);
	newDisposer->getAccounts()->push_back(acc);
	
	return E_OK;
}

ACCOUNTMODULE_API int RemoveDisposer(int disposerId, int accountNumber, int disposerToRemoveId)
{
	if (!(CheckId(disposerId) && CheckId(accountNumber) && CheckId(disposerId)))
	{
		return E_INVALID_PARAMETER;
	}

	Account* acc = __nullptr;
	Customer* disp = __nullptr;

	if (disposerId == disposerToRemoveId) 
	{
		return E_CANNOT_REMOVE_SELF;
	}

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &disp);
	if (returnValue != E_OK)
	{
		return returnValue;
	}

	auto disposerToRemove = GetCustomer(disposerToRemoveId);

	if (disposerToRemove == __nullptr)
	{
		return E_REMOVE_DISPOSER_NOT_FOUND;
	}

	acc->GetDisposers()->remove(disposerToRemove);
	disposerToRemove->getAccounts()->remove(acc);

	return E_OK;
}



int FindAccountAndAuthorizedDisposer(int accountNumber, int disposerId, Account ** account, Customer ** disposer)
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

	if (disp == __nullptr || !disp->isActive()) // Kunde nicht gefunden oder nicht mehr aktiv
	{
		return E_CUSTOMER_NOT_FOUND;
	}

	auto accountsDisposers = acc->GetDisposers();

	bool isAuthorized = false;

	for (auto it = accountsDisposers->begin(); it != accountsDisposers->end(); ++it)
	{
		if ((*it)->getId() == disposerId) // kunde berechtigt.
		{
			isAuthorized = true;
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
