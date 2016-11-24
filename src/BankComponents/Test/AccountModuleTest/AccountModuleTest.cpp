#include "stdafx.h"
#include "CppUnitTest.h"
#include "../../AccountModule/AccountModule.h"
#include "../../CustomerModule/CustomerModule.h"
#include "../../Shared/ErrorCodes.h"
#include "../../Shared/SharedStorage.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace AccountModuleTest
{
	TEST_CLASS(UnitTest1)
	{
	public:

		TEST_METHOD(Account_CloseAccount_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(customerId, accountId);
			
			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(Account_CloseAccount_Unauthorized)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(1, 0);
			
			Assert::AreEqual(E_UNAUTHORIZED, retVal);
		}

		TEST_METHOD(Account_CloseAccount_CustomerNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(17, 0);
			
			Assert::AreEqual(E_CUSTOMER_NOT_FOUND, retVal);
		}

		TEST_METHOD(Account_CloseAccount_AccountNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = CloseAccount(0, 7);
		
			Assert::AreEqual(E_ACCOUNT_NOT_FOUND, retVal);
		}

		TEST_METHOD(Account_CloseAccount_AccountClosed)
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

		TEST_METHOD(Account_CloseAccount_CustomerDeleted)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);
			DeleteCustomer(customerId);
			
			auto retVal = CloseAccount(customerId, accountId);
			
			Assert::AreEqual(E_CUSTOMER_NOT_FOUND, retVal);
		}

		TEST_METHOD(Account_AddDisposer_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(1, "FirstAccount", LoanAccount, accountId);
			
			auto retVal = AddDisposer(1, 0, 2);
			
			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(Account_AddDisposer_NewDisposerNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);

			auto retVal = AddDisposer(customerId, 0, 33);

			Assert::AreEqual(E_NEW_DISPOSER_NOT_FOUND, retVal);
		}

		TEST_METHOD(Account_RemoveDisposer_OK)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);

			AddDisposer(customerId, 0, 1);

			auto retVal = RemoveDisposer(2, 0, 1);


			Assert::AreEqual(E_OK, retVal);
		}

		TEST_METHOD(Account_RemoveDisposer_CanNotRemoveSelf)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(0, "FirstAccount", LoanAccount, accountId);


			auto retVal = RemoveDisposer(0, 0, 0);


			Assert::AreEqual(E_CANNOT_REMOVE_SELF, retVal);
		}

		TEST_METHOD(Account_RemoveDisposer_DisposerToRemoveNotFound)
		{
			SharedStorage::GetInstance()->clear();
			int customerId = 0;
			int accountId = 0;
			CreateCustomer("Customer1FirstName", "Customer1", "Street", 5020, customerId);
			CreateAccount(customerId, "FirstAccount", LoanAccount, accountId);


			auto retVal = RemoveDisposer(customerId, 0, 17);


			Assert::AreEqual(E_REMOVE_DISPOSER_NOT_FOUND, retVal);
		}
	};
}