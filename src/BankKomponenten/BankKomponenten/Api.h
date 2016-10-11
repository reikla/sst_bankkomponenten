#pragma once
#include "Currency.h"


#ifdef BANKKOMPONENTEN_EXPORTS
#define BANK_API __declspec(dllexport)
#else
#define BANK_API __declspec(dllimport)
#endif

extern "C" 
{
	BANK_API void Foo(CURRENCY);
}