#include "Api.h"
#include <iostream>


BANK_API void StringTest(wchar_t & Name,const wchar_t * outName)
{
	outName = L"HANS";
}

BANK_API int CreateCustomer(wchar_t* firstName, wchar_t* lastName, wchar_t* street, int zip, int& id)
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
