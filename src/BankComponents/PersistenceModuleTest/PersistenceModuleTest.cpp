#include "stdafx.h"
#include "CppUnitTest.h"
#include "../PersistenceModule/PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"

#include "../PersistenceModule/Persistence.h"

#include "../CustomerModule/CustomerModule.h"
#include <algorithm>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PersistenceModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		//Bitte deine Tests auch nach dem Schema: Module_Operation_Ergebnis
		TEST_METHOD(Persistence_Save_OK)
		{
			GetStorage()->clear();
			int id = -1;
			auto returnValue = CreateCustomer("Franz", "Müller", "Hintertupfing", 5020, id);
			Assert::AreEqual(E_OK, returnValue);
			returnValue = CreateCustomer("Seppi", "Bauer", "Obergwandt", 8137, id);
			Assert::AreEqual(E_OK, returnValue);
			returnValue = CreateCustomer("Lisa", "Bayer", "Seitenneben", 2488, id);
			Assert::AreEqual(E_OK, returnValue);
			returnValue = CreateCustomer("Max", "Mustermann", "Wien", 1010, id);
			Assert::AreEqual(E_OK, returnValue);

			returnValue = Store();
			Assert::AreEqual(E_OK, returnValue);

			Assert::AreEqual(4, Persistence::getInstance()->count(DataModule::CUSTOMER_TABLE));
		}

		TEST_METHOD(Persistence_Load_OK)
		{
			GetStorage()->clear();

			auto returnValue = Load();
			Assert::AreEqual(E_OK, returnValue);

			SharedStorage *sharedStorage = SharedStorage::GetInstance();
			
			list<Customer*> *list = sharedStorage->GetCustomers();
			Assert::AreEqual((size_t)4, list->size());

		}

		TEST_METHOD(Persistence_LoadAndStoreModificationAndLoad_OK)
		{
			GetStorage()->clear();
			auto returnLoadValue = Load();
			Assert::AreEqual(E_OK, returnLoadValue);
			
			int newZip = 5020;
			ModifyCustomer(2, "Hans", "Meier", "Daheim", &newZip);
			auto returnSaveValue = Store();
			Assert::AreEqual(E_OK, returnSaveValue);

			auto returnValue = Load();
			Assert::AreEqual(E_OK, returnValue);

			auto list = *(GetStorage()->GetCustomers());

			unsigned index = 2;
			if (list.size() > index)
			{
				std::list<Customer*>::iterator it = list.begin();
				std::advance(it, index);
				Assert::AreEqual(string("Hans"), (*it)->getFirstName());
				Assert::AreEqual(std::string("Meier"),(*it)->getLastName());
				Assert::AreEqual(std::string("Daheim"),(*it)->getStreet());
				Assert::AreEqual(5020,(*it)->getZip());
			}


		}

		TEST_METHOD(Persistence_ConnectDBAndCreateTables_OK)
		{
			GetStorage()->clear();

			Persistence *persistence = Persistence::getInstance();
			Assert::AreEqual(SQLITE_OK, persistence->getSqLiteResultCode());

		}
	};
}