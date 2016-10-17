// PersistenceModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"

PERSISTENCEMODULE_API int Load()
{
	auto storage = GetStorage();
	storage->clear();
	//ich würde dir vorschlagen dass du den storage deiner klasse übergibst, dann kannst du schön mit c++ arbeiten.
	return E_OK;
}

PERSISTENCEMODULE_API int Store()
{
	auto storage = GetStorage();
	//ich würde dir vorschlagen dass du den storage deiner klasse übergibst, dann kannst du schön mit c++ arbeiten.
	return E_OK;
}
