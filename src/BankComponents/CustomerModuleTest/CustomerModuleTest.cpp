// CustomerModuleTest.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <ctime>
using namespace std;
#include "../CustomerModule/CustomerModule.h"
#include "../Shared/ErrorCodes.h"

void TestAddCustomer(char * firstName, char * lastName, char * street, int zip)
{
	int id = 0;

	int err = CreateCustomer(firstName,lastName,street,zip, id);
	switch (err)
	{
	case E_OK:
		cout << "Customer created" << endl;
		break;
	case E_INVALID_PARAMETER:
		cout << "Parameter invalid" << endl;
		break;
	default:
		cout << "Something else went wrong. Errorcode '" << err << "'" << endl;
		break;
	}
}





int main()
{
	unsigned int start = clock();

	getchar();

	for (int i = 0; i < 100000; i++) {

	TestAddCustomer("Sepp", "Müller", "Hintertupfing 17", 5020);
	//TestAddCustomer(NULL, "Müller", "Hintertupfing 17", 5020);
	//TestAddCustomer("Sepp", "Müller", "Hintertupfing 17", 5020);
	getchar();
	}

	cout << "Done in " << clock() - start << "ms" << endl;
	getchar();

}

