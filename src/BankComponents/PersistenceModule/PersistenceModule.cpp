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
	//ich w�rde dir vorschlagen dass du den storage deiner klasse �bergibst, dann kannst du sch�n mit c++ arbeiten.
	return E_OK;
}

PERSISTENCEMODULE_API int Store()
{
	auto storage = GetStorage();
	//ich w�rde dir vorschlagen dass du den storage deiner klasse �bergibst, dann kannst du sch�n mit c++ arbeiten.
	return E_OK;
}
