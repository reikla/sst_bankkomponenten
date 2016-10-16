#pragma once
#include "SharedStorage.h"
#include "Customer.h"
#include "Account.h"

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