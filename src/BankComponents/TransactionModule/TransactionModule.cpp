
#include "stdafx.h"
#include "TransactionModule.h"
#include "../Shared/SharedFunctions.h"
#include "../Shared/Checks.h"
#include "../Shared/ErrorCodes.h"


TRANSACTIONMODULE_API int AccountBalancing(int disposerId, int accountNumber, CURRENCY currency, double& balance)
{
	if (!(CheckId(accountNumber) && CheckId(disposerId))) 
	{
		return E_INVALID_PARAMETER;
	}

	Account * acc;
	Customer * customer;

	auto returnValue = FindAccountAndAuthorizedDisposer(accountNumber, disposerId, &acc, &customer);
	if (returnValue != S_OK)
	{
		return returnValue;
	}

	auto storage = GetStorage();

	

}
