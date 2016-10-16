#pragma once
#include "SharedStorage.h"
#include "Customer.h"

inline Customer * GetCustomer(int id)
{
	auto customers = SharedStorage::GetInstance()->GetCustomers();

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

inline SharedStorage * GetStorage()
{
	return SharedStorage::GetInstance();
}