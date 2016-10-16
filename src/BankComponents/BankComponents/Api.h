#pragma once
#include "../Shared/Currency.h"
#include "../Shared/AccountType.h"


#ifdef BANKKOMPONENTEN_EXPORTS
#define BANK_API __declspec(dllexport)
#else
#define BANK_API __declspec(dllimport)
#endif

extern "C" 
{
	BANK_API void StringTest(wchar_t& Name, const wchar_t * outName);

	//************************************
	// Method:    CreateCustomer
	// FullName:  CreateCustomer
	// Access:    public 
	// Returns:   BANK_API int
	// Qualifier:
	// Parameter: wchar_t * firstName
	// Parameter: wchar_t * lastName
	// Parameter: wchar_t * street
	// Parameter: int zip
	// Parameter: int & id
	//************************************
// 	BANK_API int CreateCustomer(wchar_t* firstName, wchar_t* lastName, wchar_t* street, int zip, int& id);
// 
// 	BANK_API int DeleteCustomer(int id);
// 
// 	BANK_API int ModifyCustomer(int id, wchar_t* firstName, wchar_t* lastName, wchar_t* street, int zip);
// 
// 	
// 	
// 	BANK_API int CreateAccount(int customerId, wchar_t* accountName, ACCOUNT_TYPE accountType, int& accountNumber);
// 
// 	BANK_API int CloseAccount(int customerId, int accountNumber);
// 
// 	BANK_API int AddDisposer(int accountNumber, int newDisposerId, int disposerId);
// 
// 	BANK_API int RemoveDisposer(int accountNumber, int disposerToRemoveId, int disposerId);
// 
// 	
// 	BANK_API int PayOut(int accountNumber, int disposer, double amount, CURRENCY currency);
// 
// 	BANK_API int PayIn(int accountNumber, int disposer, double amount, CURRENCY currency);
// 
// 	BANK_API int Transfer(int fromAccountNumber, int toAccountNumber, int disposerId, double ammount, CURRENCY currency);
// 	
// 	//TODO: data pointer muss noch geändert werden auf eine Sturkur denke ich.
// 	BANK_API int AccountStatement(int accountNumber, int disposer, void** data, int& numberOfEntries);
// 
// 	BANK_API int AccountBalancing(int accountNumber, int disposer, CURRENCY currency, double& balance);
// 
// 	BANK_API int ConvertValues(double amount, CURRENCY fromCurrency, CURRENCY toCurrency, double& result);
}