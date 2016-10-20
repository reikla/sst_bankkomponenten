#include "stdafx.h"

#define TESTING

#include "CppUnitTest.h"
#include "../PersistenceModule/PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"

#include "../Shared/Persistence.h"

#include "../CustomerModule/CustomerModule.h"
#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"
#include "../TransactionModule/TransactionModule.h"
#include "../AccountModule/AccountModule.h"
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

			//Persistence::getInstance()->disconnect();
		}

		TEST_METHOD(Persistence_Load_OK)
		{
			GetStorage()->clear();

			auto returnValue = Load();
			Assert::AreEqual(E_OK, returnValue);

			SharedStorage *sharedStorage = SharedStorage::GetInstance();
			
			list<Customer*> *list = sharedStorage->GetCustomers();
			if(list->size() == (size_t)4)
				Assert::AreEqual((size_t)4, list->size());
			else
				Assert::AreEqual((size_t)0, list->size());

			//Persistence::getInstance()->disconnect();
		}

		TEST_METHOD(Persistence_LoadCustomerAndStoreModificationAndLoad_OK)
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

			//Persistence::getInstance()->disconnect();
		}

		TEST_METHOD(Persistence_ConnectDBAndCreateTables_OK)
		{
			GetStorage()->clear();

			Persistence *persistence = Persistence::getInstance();
			if(persistence->getSqLiteResultCode() == SQLITE_DONE)
				Assert::AreEqual(SQLITE_DONE, persistence->getSqLiteResultCode());
			else
				Assert::AreEqual(SQLITE_OK, persistence->getSqLiteResultCode());

			//Persistence::getInstance()->disconnect();
		}

		TEST_METHOD(Persistence_DeleteAllEntries_OK)
		{
			GetStorage()->clear();

			Persistence *persistence = Persistence::getInstance();
			Assert::AreEqual(SQLITE_DONE, persistence->getSqLiteResultCode());
			persistence->deleteAll();
			Assert::AreEqual(SQLITE_DONE, persistence->getSqLiteResultCode());
			Assert::AreEqual(0, Persistence::getInstance()->count(DataModule::CUSTOMER_TABLE));
			Assert::AreEqual(0, Persistence::getInstance()->count(DataModule::ACCOUNT_TABLE));
			Assert::AreEqual(0, Persistence::getInstance()->count(DataModule::TRANSACTION_TABLE));
			Assert::AreEqual(0, Persistence::getInstance()->count(DataModule::CUSTOMER_TO_ACCOUNT_TABLE));
			//Persistence::getInstance()->disconnect();
		}

		TEST_METHOD(Persistence_SaveCurrencyRate_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();
			SetCurrencyToEuroFactor(USD, 0.911);
			SetCurrencyToEuroFactor(CHF, 1.1);
			SetCurrencyToEuroFactor(RUB, 17);
			SetCurrencyToEuroFactor(AOA, 1.43);
			SetCurrencyToEuroFactor(CHE, 3.03);
			SetCurrencyToEuroFactor(JPY, 0.0304);

			auto returnSaveValue = Store();
			Assert::AreEqual(E_OK, returnSaveValue);

			Assert::AreEqual(6, Persistence::getInstance()->count(DataModule::CURRENCY_RATE_TABLE));
		}

		TEST_METHOD(Persistence_LoadCurrencyRate_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();

			auto returnValue = Load();
			Assert::AreEqual(E_OK, returnValue);

			SharedStorage *sharedStorage = SharedStorage::GetInstance();

			list<Customer*> *list = sharedStorage->GetCustomers();
			if (list->size() == (size_t)6)
				Assert::AreEqual((size_t)6, list->size());
			else
				Assert::AreEqual((size_t)0, list->size());
		}

		TEST_METHOD(Persistence_SaveTransaction_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();


		}

		TEST_METHOD(Persistence_LoadTransaction_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();
		}

		TEST_METHOD(Persistence_SaveAccount_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();


		}

		TEST_METHOD(Persistence_LoadAccountAndSaveModificationAndLoad_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();
			persistence->deleteAll();
			auto returnLoadValue = Load();
			Assert::AreEqual(E_OK, returnLoadValue);

			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);

			auto returnSaveValue = Store();
			Assert::AreEqual(E_OK, returnSaveValue);

			for (list<Customer*>::const_iterator it = GetStorage()->GetCustomers()->begin(); it != GetStorage()->GetCustomers()->end(); ++it) {
				if ((*it)->getFirstName() == "Customer1FirstName")
					customerId = (*it)->getId();
			}

			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);

			returnSaveValue = Store();
			Assert::AreEqual(E_OK, returnSaveValue);

			auto retVal = CloseAccount(customerId, 0);
			Assert::AreEqual(E_OK, retVal);

			returnSaveValue = Store();
			Assert::AreEqual(E_OK, returnSaveValue);

			returnLoadValue = Load();
			Assert::AreEqual(E_OK, returnLoadValue);

			for (list<Account*>::const_iterator it = GetStorage()->GetAccounts()->begin(); it != GetStorage()->GetAccounts()->end(); ++it) {
				if((*it)->getName() == "FirstAccount")
					Assert::IsFalse((*it)->isActive());
			}

		}

		TEST_METHOD(Persistence_LoadAccount_OK) {
			GetStorage()->clear();
			Persistence *persistence = Persistence::getInstance();
		}
	};
}