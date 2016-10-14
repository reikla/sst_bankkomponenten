#pragma once

#include <string>
#include "Shared.h"
#include "Currency.h"

class Account;

using namespace std;

class SHARED_API Transaction
{
public:
	Transaction();
	~Transaction();
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



private:
	double m_amount;
	double m_factor;
	CURRENCY m_currency;
	Account* m_from;
	Account* m_to;
};

