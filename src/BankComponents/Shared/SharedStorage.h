#pragma once
#include <list>
#include <string>
#include "Shared.h"

class Customer;
class Account;
class Transaction;
class CurrencyRate;
using namespace std;

class SHARED_API SharedStorage
{
public:
	virtual ~SharedStorage();

	static SharedStorage * GetInstance();

	list<Customer*> * GetCustomers();
	list<Account*> * GetAccounts();
	list<Transaction*> * GetTransactions();
	list<CurrencyRate*> * GetCurrencyRates();

	bool StoreCustomer(Customer*);
	bool StoreAccount(Account*);
	bool StoreTransaction(Transaction*);
	void clear();


private:
	SharedStorage();
	static SharedStorage * instance;
	list<Customer*> * m_customers;
	list<Account*> * m_accounts;
	list<Transaction*> * m_transactions;
	list<CurrencyRate *> * m_rates;
};

