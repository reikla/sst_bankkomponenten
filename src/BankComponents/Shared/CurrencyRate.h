#pragma once
#include "Currency.h"
#include "Shared.h"

class SHARED_API CurrencyRate
{
public:
	CurrencyRate(CURRENCY currency, double factor);
	virtual ~CurrencyRate();


	CURRENCY GetCurrency() const { return m_currency; }
	void SetCurrency(CURRENCY val) { m_currency = val; }
	double GetFactor() const { return m_factor; }
	void SetFactor(double val) { m_factor = val; }
private:
	CURRENCY m_currency;
	double m_factor;
};

