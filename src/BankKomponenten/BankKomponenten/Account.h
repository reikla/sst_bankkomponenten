#pragma once
#include <list>
#include <string>

typedef Transaction;
typedef Customer;
class Account
{
public:
	Account();
	~Account();
	bool isActive();
	void setIsActive(bool);

	std::string getName();
	
	std::list<Transaction> GetTransactions();
	void setName(std::string);

	AccountType GetAccountType();

	std::list<Customer> GetDisposers();


	void SetAccountType(AccountType);
};

enum AccountType {
	SavingsAccount = 1,
	LoanAccount = 2
};