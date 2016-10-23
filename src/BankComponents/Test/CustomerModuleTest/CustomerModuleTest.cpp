#include "stdafx.h"
#include "CppUnitTest.h"
#include "../../CustomerModule/CustomerModule.h"
#include "../../Shared/SharedStorage.h"
#include "../../Shared/SharedFunctions.h"
#include "../../Shared/Customer.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace CustomerModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(Customer_AddCustomer_OK)
		{
			GetStorage()->clear();
			int id = -1;
			auto returnValue = CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(0, id);
		}

		TEST_METHOD(Customer_AddTwoCustomersID_OK)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			Assert::AreEqual(1, id);
		}

		TEST_METHOD(Customer_DeleteCstomer_OK)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			auto returnValue = DeleteCustomer(id);
			Assert::AreEqual(E_OK, returnValue);
		}

		TEST_METHOD(Customer_AddDeleteCustomerNewId_OK)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			DeleteCustomer(id);
			auto returnValue = CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1, id);
		}

		TEST_METHOD(Customer_ModifyCustomer_OK)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 1000, id);
			int newZip = 5020;
			auto returnValue = ModifyCustomer(0,"Hans", "Meier", "Daheim", newZip);

			Assert::AreEqual(E_OK, returnValue);
			auto customer = *(GetStorage()->GetCustomers()->begin());

			Assert::AreEqual(std::string("Hans"), customer->getFirstName());
			Assert::AreEqual(std::string("Meier"), customer->getLastName());
			Assert::AreEqual(std::string("Daheim"), customer->getStreet());
			Assert::AreEqual(5020, customer->getZip());
		}

		TEST_METHOD(Customer_ModifyCustomer_InvalidZip)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 1000, id);
			int newZip = 50120;
			auto returnValue = ModifyCustomer(0, "Hans", "Meier", "Daheim", newZip);
			auto customer = *(GetStorage()->GetCustomers()->begin());

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1000, customer->getZip());


		}

		TEST_METHOD(Customer_ModifyCustomerOnlyOneParam_OK)
		{
			GetStorage()->clear();
			int id = -1;
			CreateCustomer("Franz", "Müller", "Hintertupfing", 1000, id);
			int newZip = 5020;
			auto returnValue = ModifyCustomer(0, "Hans", NULL, NULL, NULL);

			Assert::AreEqual(E_OK, returnValue);
			auto customer = *(GetStorage()->GetCustomers()->begin());

			Assert::AreEqual(std::string("Hans"), customer->getFirstName());
			Assert::AreEqual(std::string("Müller"), customer->getLastName());
			Assert::AreEqual(std::string("Hintertupfing"), customer->getStreet());
			Assert::AreEqual(1000, customer->getZip());
		}


	};
}