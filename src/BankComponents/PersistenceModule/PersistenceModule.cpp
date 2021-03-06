// PersistenceModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"
#include "../Shared/Persistence.h"

#ifdef TESTING
#define DISCONNECT persistence->disconnect();
#endif
#ifndef TESTING
#define DISCONNECT
#endif // !TESTING


PERSISTENCEMODULE_API int Load()
{
	auto storage = GetStorage();
	storage->clear();
	Persistence *persistence = Persistence::getInstance();
	if (!persistence->getIsConnected())
		if (!persistence->connect()) {
			return E_PERSISTENCE_ERROR;//persistence->getSqLiteResultCode();
		}
	
	storage->GetCurrencyRates()->splice(storage->GetCurrencyRates()->end(), *(persistence->getAllCurrencyRates()));
	storage->GetCustomers()->splice(storage->GetCustomers()->end(), *(persistence->getAllCustomers()));
	storage->GetAccounts()->splice(storage->GetAccounts()->end(), *(persistence->getAllAccounts()));
	storage->GetTransactions()->splice(storage->GetTransactions()->end(), *(persistence->getAllTransactions()));
	
	int resultCode = persistence->getSqLiteResultCode();
	DISCONNECT
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return E_PERSISTENCE_ERROR;//resultCode;
	}
		return E_OK;
}

PERSISTENCEMODULE_API int Store()
{
	auto storage = GetStorage();
	Persistence *persistence = Persistence::getInstance();
	if (!persistence->getIsConnected())
		if (!persistence->connect()) {
			return E_PERSISTENCE_ERROR;//persistence->getSqLiteResultCode();
		}

	persistence->deleteAll();
	int resultCode = persistence->getSqLiteResultCode();
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return E_PERSISTENCE_ERROR;//resultCode;
	}

	persistence->insertOrReplace(storage->GetCurrencyRates());
	persistence->insertOrReplace(storage->GetCustomers());
	persistence->insertOrReplace(storage->GetAccounts());
	persistence->insertOrReplace(storage->GetTransactions());

	resultCode = persistence->getSqLiteResultCode();
	DISCONNECT
	if (resultCode != SQLITE_OK) {
		if (resultCode != SQLITE_DONE)
			return E_PERSISTENCE_ERROR;//resultCode;
	}
		return E_OK;
}
