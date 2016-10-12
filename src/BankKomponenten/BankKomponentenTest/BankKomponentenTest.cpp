#include "stdafx.h"
#include <list>
#include <iostream>
#include "../BankKomponenten/Api.h"
using namespace std;

void Foo(wchar_t** name) {
	if (name != __nullptr) {
		wcout << name << endl;
		*name = L"Hans";
	}
}

void Bar(int** a) {
	*(*a) = 12;
}

int main() {

	std::string hallo = "hallo";
	std::list<std::string> strings;

	CURRENCY f = CURRENCY::USD;
	CURRENCY euro = CURRENCY::EUR;

// 	Foo(f);
// 	Foo(euro);

	int newId;

	if (CreateCustomer(L"Daniel", L"Komohorov", L"Alterbachstraﬂe 12", 5020, newId) == 0) 
	{
		std::cout << "Customer with id " << newId <<" succesffully Created" << endl;
	}

	wchar_t* a = L"Franz";

	Foo(&a);
	wcout << a;
	int c = 1;

	int *b = &c;

	Bar(&b);

	//StringTest(L"Hans", L"Franz");

	return 0;

}


