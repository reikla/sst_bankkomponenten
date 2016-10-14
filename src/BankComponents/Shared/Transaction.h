#pragma once

#include <string>
#include "Shared.h"

class Account;

class SHARED_API Transaction
{
public:
	Transaction();
	~Transaction();
	double getAmount();
	void setAmount(double);

	double getFactor();
	void setFactor(double);

// 	std::string getCurrency();
// 	void setCurrency(std::string);

	

	Account* getFrom();
	void setFrom(Account*);
	
	Account* getTo();
	void setTo(Account*);



private:
	double m_amount;
	double m_factor;
	std::string m_currency;
	Account* m_from;
	Account* m_to;
};

