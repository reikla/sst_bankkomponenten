#pragma once
#include "Currency.h"


#ifdef BANKKOMPONENTEN_EXPORTS
#define BANK_API __declspec(dllexport)
#else
#define BANK_API __declspec(dllimport)
#endif

extern "C" 
{
	BANK_API void Foo(CURRENCY);

	//************************************
	// Method:    CreateCustomer
	// FullName:  CreateCustomer
	// Access:    public 
	// Returns:   BANK_API int
	// Qualifier:
	// Parameter: char * firstName
	// Parameter: char * lastName
	// Parameter: char * street
	// Parameter: int zip
	// Parameter: int & id
	//************************************
	BANK_API int CreateCustomer(char* firstName, char* lastName, wchar_t* street, int zip, int& id);
}