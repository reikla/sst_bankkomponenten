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

BANK_API int CreateCustomer(char* firstName, char* lastName, wchar_t* street, int zip, int& id)
{
	std::cout << "Creating customer" << std::endl;
	std::cout << "First Name: " << firstName << std::endl;
	std::cout << "Last Name: " << lastName << std::endl;
	std::wcout << "Street: " << street << std::endl;
	std::cout << "Zip: " << zip << std::endl;

	id = 100;
	std::cout << "New CustomerId: " << id << std::endl;

	return 0;
}
