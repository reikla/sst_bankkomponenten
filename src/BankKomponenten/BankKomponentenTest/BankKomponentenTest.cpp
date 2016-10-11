#include "stdafx.h"
#include <list>
#include <iostream>
#include "../BankKomponenten/Api.h"
using namespace std;

int main() {

	std::string hallo = "hallo";
	std::list<std::string> strings;

	CURRENCY f = CURRENCY::USD;
	CURRENCY euro = CURRENCY::EUR;

// 	Foo(f);
// 	Foo(euro);

	int newId;

	if (CreateCustomer("Daniel", "Komohorov", L"Alterbachstraﬂe 12", 5020, newId) == 0) {
		std::cout << "Customer with id " << newId <<" succesffully Created" << endl;
	}

	return 0;

}


