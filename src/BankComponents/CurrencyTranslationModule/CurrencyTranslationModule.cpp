// CurrencyTranslationModule.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CurrencyTranslationModule.h"
#include "../Shared/Checks.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"
#include "../Shared/CurrencyRate.h"

CURRENCYTRANSLATIONMODULE_API int SetCurrencyEuroFactor(CURRENCY currency, double factor)
{
	if (! (CheckCurrencyFactor(factor) && currency != EUR)) //Euro -> Euro darf nicht gesetzt werden. 
	{
		return E_INVALID_PARAMETER;
	}

	auto rates = GetStorage()->GetCurrencyRates();
	for (auto it = rates->begin(); it != rates->end(); ++it)
	{
		auto rate = *it;
		if (rate->GetCurrency() == currency) 
		{
			rate->SetFactor(factor);
			return E_OK;
		}
	}
	CurrencyRate* rate = new CurrencyRate(currency, factor);
	rates->push_back(rate);
	return E_OK;
}

CURRENCYTRANSLATIONMODULE_API int GetCurrencyEuroFactor(CURRENCY currency, double & factor)
{
	if (currency == EUR) 
	{
		factor = 1.0;
		return S_OK;
	}

	auto rates = GetStorage()->GetCurrencyRates();

	for (auto it = rates->begin(); it != rates->end(); ++it)
	{
		auto rate = *it;
		if (rate->GetCurrency() == currency)
		{
			factor = rate->GetFactor();
			return E_OK;
		}
	}

	return E_CURRENCY_FACTOR_NOT_STORED;
}
