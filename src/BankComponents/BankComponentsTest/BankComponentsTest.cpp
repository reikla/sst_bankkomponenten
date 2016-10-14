#include "stdafx.h"
#include <list>
#include <iostream>
#include "../BankComponents/Api.h"
#include "../Shared/Account.h"
#include "../Shared/Customer.h"
#include "../Shared/Transaction.h"
#include "../Shared/AccountType.h"
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

	Account * a = new Account("Giro Konto", ACCOUNT_TYPE.SavingsAccount);
	Customer * c = new Customer();
	c->setFirstName("Franz");
	a->setName("Hansi");

	a->GetDisposers()->push_back(c);

	c->addAccount(a);



	
	return 0;

}


