#pragma once
#pragma warning( push )
#pragma warning( disable : 4251)
#include <list>
#include <string>
#include "AccountType.h"
#include "Shared.h"

class Transaction;
class Customer;
using namespace std;

class SHARED_API Account
{


public:
	Account(string accountName, AccountType type);
	virtual ~Account();
	
	bool isActive();
	void setIsActive(bool isActive);
	
	string getName();
	void setName(string accountName);

	AccountType GetAccountType();
	void SetAccountType(ACCOUNT_TYPE type);
	
	list<Transaction*> * GetTransactions();
	
	list<Customer*> * GetDisposers();

	int GetAccountNumber();
	void SetAccountNumber(int accountNumber);

private:
	bool m_active;
	int m_accountNumber;
	string m_name;
	list<Transaction*> * m_transactions;
	ACCOUNT_TYPE m_accountType;
	list<Customer*> * m_disposers;
};
#pragma warning( pop ) 