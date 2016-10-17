#pragma once

#include <string>
#include "Shared.h"
#include "Currency.h"

class Account;
class Customer;

using namespace std;

class SHARED_API Transaction
{
public:
	Transaction(double amount, double factor, CURRENCY currency, Account * from, Account * to, Customer* disposer);
	virtual ~Transaction();
	double getAmount();
	void setAmount(double);

	double getFactor();
	void setFactor(double);

	CURRENCY getCurrency();
	void setCurrency(CURRENCY currency);

	Account* getFrom();
	void setFrom(Account* fromAccount);
	
	Account* getTo();
	void setTo(Account* toAccount);

	Customer* getDisposer();
	void setDisposer(Customer* disposer);



private:
	double m_amount;
	double m_factor;
	CURRENCY m_currency;
	Account* m_from;
	Account* m_to;
	Customer* m_disposer;
};

