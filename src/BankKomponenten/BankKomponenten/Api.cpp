#include "Api.h"
#include <iostream>


BANK_API void Foo(CURRENCY currency)
{
	switch (currency)
	{
	case CURRENCY::USD:
		std::cout << "USD";
		break;
	case CURRENCY::EUR:
		std::cout << "EUR";
		break;
	default:
		break;
	}
}