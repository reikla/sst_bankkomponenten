// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the ACCOUNTMODULE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// ACCOUNTMODULE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef ACCOUNTMODULE_EXPORTS
#define ACCOUNTMODULE_API __declspec(dllexport)
#else
#define ACCOUNTMODULE_API __declspec(dllimport)
#endif
#include "../Shared/AccountType.h"
class Customer;
class Account;

extern "C" {
	ACCOUNTMODULE_API int CreateAccount(int disposerId, const char* accountName, ACCOUNT_TYPE accountType, int& accountNumber);

	ACCOUNTMODULE_API int CloseAccount(int disposerId, int accountNumber);

	ACCOUNTMODULE_API int AddDisposer(int disposerId, int accountNumber, int newDisposerId);

	ACCOUNTMODULE_API int RemoveDisposer(int disposerId, int accountNumber, int disposerToRemoveId);
}