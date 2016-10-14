#pragma once
#include <list>
#include <string>
#include "Shared.h"

class Customer;
class Account;
class Transaction;

class SHARED_API Persistence
{
public:
	Persistence();
	virtual ~Persistence();

	std::list<Customer*> * GetCustomers();
	std::list<Account*> * GetAccounts();
	std::list<Transaction*> * GetTransactions();

	bool StoreCustomer(Customer*);
	bool StoreAccount(Account*);
	bool StoreTransaction(Transaction*);

private:
	std::list<Customer*> * m_customers;
	std::list<Account*> * m_accounts;
	std::list<Transaction*> * m_transactions;
};

