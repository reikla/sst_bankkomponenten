#include "stdafx.h"
#include <list>
#include <iostream>
#include "../BankComponents/Api.h"
#include "../Shared/Account.h"
#include "../Shared/Customer.h"
#include "../Shared/Transaction.h"
#include "../Shared/AccountType.h"
#include "../Shared/SharedStorage.h"
#include "../CustomerModule/CustomerModule.h"
#include "../AccountModule/AccountModule.h"

using namespace std;

void Foo(wchar_t** name) {
	if (name != __nullptr) {
		wcout << name << endl;
		*name = L"Hans";
	}
}
void TestDifferentModules()
{
	int id = 0;
	CreateCustomer("", "", "", 1000, id);
	CreateAccount(0, "", SavingsAccount, id);
	CloseAccount(0, 0);
}

void Bar(int** a) {
	*(*a) = 12;
}

int main() {

	TestDifferentModules();

	SharedStorage * ptr = SharedStorage::GetInstance();


	std::string hallo = "hallo";
	std::list<std::string> strings;

	CURRENCY f = CURRENCY::USD;
	CURRENCY euro = CURRENCY::EUR;

	Account * a = new Account("Giro Konto", LoanAccount);
	Customer * c = new Customer("Reimar","klammer","Daheim", 1112);
	c->setFirstName("Franz");
	a->setName("Hansi");
	ptr->StoreAccount(a);

	a->GetDisposers()->push_back(c);

	c->addAccount(a);

	getchar();

	
	return 0;

}


