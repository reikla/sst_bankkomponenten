#pragma once
typedef Account;
class Transaction
{
public:
	Transaction();
	~Transaction();
	double getAmount();
	void setAmount(double);

	Account getFrom();
	void setFrom(Account*);
	
	Account getTo();
	void setTo(Account*);

private:

};

