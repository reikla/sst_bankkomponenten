// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the TRANSACTIONMODULE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// TRANSACTIONMODULE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef TRANSACTIONMODULE_EXPORTS
#define TRANSACTIONMODULE_API __declspec(dllexport)
#else
#define TRANSACTIONMODULE_API __declspec(dllimport)
#endif

#include "../Shared/Currency.h"
#include "../Shared/TransactionStruct.h"

extern "C" { 
	TRANSACTIONMODULE_API int PayOut(int accountNumber, int disposerId, double amount, CURRENCY currency);

	TRANSACTIONMODULE_API int PayIn(int accountNumber, int disposerId, double amount, CURRENCY currency);

	TRANSACTIONMODULE_API int Transfer(int fromAccountNumber, int toAccountNumber, int disposerId, double ammount, CURRENCY currency);

	TRANSACTIONMODULE_API int AccountStatement(int accountNumber, int disposerId, S_TRANSACTION** data, int& numberOfEntries);

	TRANSACTIONMODULE_API int AccountBalancing(int accountNumber, int disposerId, CURRENCY currency, double& balance);
}