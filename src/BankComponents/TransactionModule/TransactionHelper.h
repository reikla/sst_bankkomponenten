#pragma once

#include <list>

class Transaction;
class Account;

class TransactionHelper
{
public:
	static bool IsFrom(Transaction * transaction, Account * account);
	static bool IsTo(Transaction * transaction, Account * account);
	static double GetEuroAmount(Transaction * transaction);
	static std::list<Transaction*> * GetAccountsTransactions(Account * account);
	TransactionHelper();
	virtual ~TransactionHelper();
};

