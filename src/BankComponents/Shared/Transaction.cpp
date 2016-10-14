#include "Transaction.h"
#include "Account.h"



Transaction::Transaction()
{
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
