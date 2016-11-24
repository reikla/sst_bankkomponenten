// AccountModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "AccountModule.h"
#include "../Shared/Account.h"

ACCOUNTMODULE_API int CreateAccount(int disposerId, const char* accountName, ACCOUNT_TYPE accountType, int& accountNumber)
{
	if (!(CheckId(disposerId) && CheckString(accountName)))
	{
		return E_INVALID_PARAMETER;
	}
	auto customer = GetCustomer(disposerId);
	if (customer == __nullptr || !customer->isActive())
	{
		return E_CUSTOMER_NOT_FOUND;
	}

	auto account = new Account(accountName, accountType);
	account->setIsActive(true);

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

	for (std::list<Customer*>::iterator accIterator = acc->GetDisposers()->begin(); accIterator != acc->GetDisposers()->end();) {
		Customer* customer = *accIterator;
		if (customer->getId() == disposerToRemove->getId())
			accIterator = acc->GetDisposers()->erase(accIterator);
		else
			++accIterator;
	}

	for (std::list<Account*>::iterator disposerIterator = disposerToRemove->getAccounts()->begin(); disposerIterator != disposerToRemove->getAccounts()->end();) {
		Account* account = *disposerIterator;
		if (account->GetAccountNumber() == acc->GetAccountNumber())
			disposerIterator = disposerToRemove->getAccounts()->erase(disposerIterator);
		else
			++disposerIterator;
	}

	return E_OK;
}



