// PersistenceModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"
#include "Persistence.h"

PERSISTENCEMODULE_API int Load()
{
	auto storage = GetStorage();
	storage->clear();
	Persistence *persistence = Persistence::getInstance();
	if (!persistence->getIsConnected())
		if (!persistence->connect()) {
			return persistence->getSqLiteResultCode();
		}
	
	storage->GetCurrencyRates()->splice(storage->GetCurrencyRates()->end(), *(persistence->getAllCurrencyRates()));
	storage->GetCustomers()->splice(storage->GetCustomers()->end(), *(persistence->getAllCustomers()));
	storage->GetAccounts()->splice(storage->GetAccounts()->end(), *(persistence->getAllAccounts()));
	storage->GetTransactions()->splice(storage->GetTransactions()->end(), *(persistence->getAllTransactions()));
	
	int resultCode = persistence->getSqLiteResultCode();
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return resultCode;
	}
		return E_OK;
}

PERSISTENCEMODULE_API int Store()
{
	auto storage = GetStorage();
	Persistence *persistence = Persistence::getInstance();
	if (!persistence->getIsConnected())
		if (!persistence->connect()) {
			return persistence->getSqLiteResultCode();
		}

	persistence->deleteAll();
	int resultCode = persistence->getSqLiteResultCode();
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return resultCode;
	}

	persistence->insertOrReplace(storage->GetCurrencyRates());
	persistence->insertOrReplace(storage->GetCustomers());
	persistence->insertOrReplace(storage->GetAccounts());
	persistence->insertOrReplace(storage->GetTransactions());

	resultCode = persistence->getSqLiteResultCode();
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return resultCode;
	}
		return E_OK;
}
