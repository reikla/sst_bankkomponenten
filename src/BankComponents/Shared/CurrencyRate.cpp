#include "CurrencyRate.h"



CurrencyRate::CurrencyRate(CURRENCY currency, double factor)
{
	m_currency = currency;
	m_factor = factor;
}


CurrencyRate::~CurrencyRate()
{
}
