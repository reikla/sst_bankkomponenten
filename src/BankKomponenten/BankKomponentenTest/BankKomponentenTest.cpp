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

	Foo(f);
	Foo(euro);
	return 0;

}


