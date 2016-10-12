#pragma once
#include <list>
#include <string>
#include "AccountType.h"

class Transaction;
class Customer;

class Account
{
public:
	Account();
	~Account();
	bool isActive();
	void setIsActive(bool);

	std::string getName();
	
	std::list<Transaction*> GetTransactions();
	void setName(std::string);

	AccountType GetAccountType();
	void SetAccountType(AccountType);

	std::list<Customer*> GetDisposers();

	int GetAccountNumber();

	void SetAccountNumber(int accountNumber);


private:
	bool m_active;
	int m_accountNumber;
	std::string m_name;
	std::list<Transaction*> m_transactions;
	AccountType m_accountType;
	std::list<Customer*> m_disposers;
};

