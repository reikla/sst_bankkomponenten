#pragma once
#include "Currency.h"
typedef struct TransactionStruct
{
	double amount;
	double factor;
	CURRENCY currency;
	int fromAccount;
	int toAccount;
	int disposer;
}S_TRANSACTION;