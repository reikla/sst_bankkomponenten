// CustomerModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CustomerModule.h"

#include "../Shared/Customer.h"


CUSTOMERMODULE_API int CreateCustomer(char* firstName, char* lastName, char* street, int zip, int& id)
{
	if (!(CheckString(firstName) && CheckString(lastName) && CheckString(street) && CheckZip(zip)))
	{
		return E_INVALID_PARAMETER;
	}
	Customer * c = new Customer(firstName, lastName, street, zip);
	SharedStorage::GetInstance()->StoreCustomer(c);
	id = c->getId();
	return E_OK;
}

CUSTOMERMODULE_API int DeleteCustomer(int id)
{
	if (!CheckId(id))
	{
		return E_INVALID_PARAMETER;
	}

	auto customers = SharedStorage::GetInstance()->GetCustomers();
	auto customer = GetCustomer(id);
	if (customer == __nullptr || !customer->isActive())
	{
		return E_CUSTOMER_NOT_FOUND;
	}
	
	customer->setIsActive(false);
	return E_OK;
}

CUSTOMERMODULE_API int ModifyCustomer(int id, char * firstName, char * lastName, char * street, int zip)
{
	if (!(CheckId(id) && CheckString(firstName) && CheckString(lastName) && CheckString(street) && CheckZip(zip)))
	{
		return E_INVALID_PARAMETER;
	}
	auto customer = GetCustomer(id);
	if (customer == __nullptr)
	{
		return E_CUSTOMER_NOT_FOUND;
	}
	customer->setFirstName(firstName);
	customer->setLastName(lastName);
	customer->setStreet(street);
	customer->setZip(zip);
	return E_OK;
}