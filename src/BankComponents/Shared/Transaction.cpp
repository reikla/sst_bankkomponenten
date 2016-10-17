#include "Transaction.h"
#include "Account.h"



Transaction::Transaction(double amount, double factor, CURRENCY currency,Account * from, Account * to, Customer* disposer)
{
	m_amount = amount;
	m_factor = factor;
	m_currency = currency;
	m_from = from;
	m_to = to;
	m_disposer = disposer;
}


Transaction::~Transaction()
{
}

double Transaction::getAmount()
{
	return m_amount;
}

void Transaction::setAmount(double amount)
{
	m_amount = amount;
}

double Transaction::getFactor()
{
	return m_factor;
}

void Transaction::setFactor(double factor)
{
	m_factor = factor;
}

CURRENCY Transaction::getCurrency()
{
	return m_currency;
}

void Transaction::setCurrency(CURRENCY currency)
{
	m_currency = currency;
}

Account* Transaction::getFrom()
{
	return m_from;
}

void Transaction::setFrom(Account* from)
{
	m_from = from;
}

Account* Transaction::getTo()
{
	return m_to;
}

void Transaction::setTo(Account* to)
{
	m_to = to;
}

Customer * Transaction::getDisposer()
{
	return m_disposer;
}

void Transaction::setDisposer(Customer * disposer)
{
	m_disposer = disposer;
}
