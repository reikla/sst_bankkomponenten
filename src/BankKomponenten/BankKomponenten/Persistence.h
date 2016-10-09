#pragma once
#include <list>
#include <string>
typedef Customer;
typedef Account;
typedef Transaction;

class Persistence
{
public:
	Persistence();
	virtual ~Persistence();

	std::list<Customer> GetCustomers();
	std::list<Account> GetAccounts();
	std::list<Transaction> GetTransactions();

	bool StoreCustomer(Customer*);
	bool StoreAccount(Account*);
	bool StoreTransaction(Transaction*);
};

