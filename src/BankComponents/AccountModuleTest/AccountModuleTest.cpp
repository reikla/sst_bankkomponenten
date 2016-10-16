#include "stdafx.h"
#include "CppUnitTest.h"
#include "../AccountModule/AccountModule.h"
#include "../CustomerModule/CustomerModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedStorage.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace AccountModuleTest
{
	TEST_CLASS(UnitTest1)
	{
	public:

		TEST_METHOD(CloseAccount_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(0, 0);
			
			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(CloseAccount_Unauthorized)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(1, 0);
			
			Assert::AreEqual(E_UNAUTHORIZED, retVal);
		}

		TEST_METHOD(CloseAccount_CustomerNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(17, 0);
			
			Assert::AreEqual(E_CUSTOMER_NOT_FOUND, retVal);
		}

		TEST_METHOD(CloseAccount_AccountNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(0, 7);
		
			Assert::AreEqual(E_ACCOUNT_NOT_FOUND, retVal);
		}

		TEST_METHOD(CloseAccount_AccountClosed)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			CloseAccount(0, 0);
			
			auto retVal = CloseAccount(0, 0);
			
			Assert::AreEqual(E_ACCOUNT_NOT_FOUND, retVal);
		}

		TEST_METHOD(CloseAccount_CustomerDeleted)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			DeleteCustomer(0);
			
			auto retVal = CloseAccount(0, 0);
			
			Assert::AreEqual(E_CUSTOMER_NOT_FOUND, retVal);
		}

		TEST_METHOD(AddDisposer_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = AddDisposer(0, 0, 1);
			
			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(AddDisposer_NewDisposerNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);

			auto retVal = AddDisposer(0, 0, 33);

			Assert::AreEqual(E_NEW_DISPOSER_NOT_FOUND, retVal);
		}

		TEST_METHOD(RemoveDisposer_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);

			AddDisposer(0, 0, 1);

			auto retVal = RemoveDisposer(0, 0, 1);


			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(RemoveDisposer_CanNotRemoveSelf)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);


			auto retVal = RemoveDisposer(0, 0, 0);


			Assert::AreEqual(E_CANNOT_REMOVE_SELF, retVal);
		}

		TEST_METHOD(RemoveDisposer_DisposerToRemoveNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);


			auto retVal = RemoveDisposer(0, 0, 17);


			Assert::AreEqual(E_REMOVE_DISPOSER_NOT_FOUND, retVal);
		}
	};
}