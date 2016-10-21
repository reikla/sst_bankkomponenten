#include "stdafx.h"
#include "CppUnitTest.h"

#include "../Shared/Currency.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedStorage.h"
#include "../TransactionModule/TransactionModule.h"

#include "../AccountModule/AccountModule.h"

#include "../CustomerModule/CustomerModule.h"

#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace TransactionModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(Transaction_NoTransaction_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);

			auto returnValue = AccountBalancing(0, 0, EUR, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(0.0, amount);

		}

		TEST_METHOD(Transaction_PayIn_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			PayIn(0, 0, 150, EUR);

			auto returnValue = AccountBalancing(0, 0, EUR, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(150.0, amount);
		}

		TEST_METHOD(Transaction_PayInTwice_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			PayIn(0, 0, 150, EUR);
			PayIn(0, 0, 150, EUR);

			auto returnValue = AccountBalancing(0, 0, EUR, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(300.0, amount);
		}

		TEST_METHOD(Transaction_PayInForeignCurrency_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			SetCurrencyToEuroFactor(USD, 2);

			PayIn(0, 0, 150, USD);
			PayIn(0, 0, 150, USD);

			auto returnValue = AccountBalancing(0, 0, EUR, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(600.0, amount);
		}

		TEST_METHOD(Transaction_PayInForeignCurrencyGetForeignCurrencyBalance_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			SetCurrencyToEuroFactor(USD, 2);

			PayIn(0, 0, 150, USD);
			PayIn(0, 0, 150, USD);

			auto returnValue = AccountBalancing(0, 0, USD, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(300.0, amount);
		}

		TEST_METHOD(Transaction_GetAccountStatement_OK)
		{
			SharedStorage::GetInstance()->clear();


			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);

			PayIn(0, 0, 150, EUR);

			S_TRANSACTION* transactions = __nullptr;

			int numOfEntries = 0;

			auto sizeForStruct = (size_t) AccountStatement(0, 0, transactions, numOfEntries);

			if (sizeForStruct > 0) // ansonsten fehler
			{
				transactions = (S_TRANSACTION*) malloc(sizeForStruct);
				auto returnValue = AccountStatement(0, 0, transactions, numOfEntries);

				Assert::AreEqual(E_OK, returnValue);

				Assert::AreEqual(transactions[0].amount, 150.0);
				Assert::AreEqual(transactions[0].factor, 1.0);
				Assert::AreEqual(transactions[0].disposer, 0);
				Assert::IsTrue(transactions[0].currency == EUR);
				Assert::AreEqual(transactions[0].fromAccount, BAR_TRANSACTION);
				Assert::AreEqual(transactions[0].toAccount, 0);
				free(transactions);
			}
		}

		TEST_METHOD(Transaction_GetAccountStatementNoTransaction_OK)
		{
			SharedStorage::GetInstance()->clear();


			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);

			S_TRANSACTION * transactions = __nullptr;
			int numOfEntries = 0;

			auto returnValue = AccountStatement(0, 0, transactions, numOfEntries);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(0, numOfEntries);
		}

		TEST_METHOD(Transaction_PayInNegative_Error)
		{
			SharedStorage::GetInstance()->clear();


			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);


			auto returnValue = PayIn(0, 0, -150, EUR);

			Assert::AreEqual(E_INVALID_PARAMETER, returnValue);
		}

		TEST_METHOD(Transaction_PayOut_OK)
		{
			SharedStorage::GetInstance()->clear();


			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			PayIn(0, 0, 150, EUR);
			auto returnValue = PayOut(0, 0, 150, EUR);


			Assert::AreEqual(E_OK, returnValue);
		}

		TEST_METHOD(Transaction_PayOut_InsufficientFunds)
		{
			SharedStorage::GetInstance()->clear();

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			auto returnValue = PayOut(0, 0, 150, EUR);


			Assert::AreEqual(E_INSUFFICIENT_FUNDS, returnValue);
		}

		TEST_METHOD(Transaction_GetBalancePayInPayOut_OK)
		{
			SharedStorage::GetInstance()->clear();

			double amount;

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			PayIn(0, 0, 150, EUR);
			PayOut(0, 0, 150, EUR);

			auto returnValue = AccountBalancing(0, 0, EUR, amount);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(0.0, amount);
		}

		TEST_METHOD(Transaction_PayOutLoanAccount_OK)
		{
			SharedStorage::GetInstance()->clear();

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", LoanAccount, id);
			auto returnValue = PayOut(0, 0, 150, EUR);


			Assert::AreEqual(E_OK, returnValue);
		}

		TEST_METHOD(Transaction_PayOutLoanAccountGetBalance_OK)
		{
			SharedStorage::GetInstance()->clear();

			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", LoanAccount, id);
			PayOut(0, 0, 150, EUR);
			double balance = 100;
			auto returnValue = AccountBalancing(0, 0, EUR, balance);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(-150.0, balance);
		}

		TEST_METHOD(Transaction_GetAccountStatementPayInPayOut_OK)
		{
			SharedStorage::GetInstance()->clear();


			int id = 0;
			CreateCustomer("", "", "", 1000, id);
			CreateAccount(0, "", SavingsAccount, id);
			SetCurrencyToEuroFactor(USD, 0.5);

			PayIn(0, 0, 150, EUR);
			PayOut(0, 0, 150, USD);

			S_TRANSACTION * transactions = __nullptr;
			int numOfEntries = 0;

			auto returnValue = AccountStatement(0, 0, transactions, numOfEntries);

			transactions = (S_TRANSACTION*) malloc(returnValue);

			returnValue = AccountStatement(0, 0, transactions, numOfEntries);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(2, numOfEntries);


			Assert::AreEqual(transactions[0].amount, 150.0);
			Assert::AreEqual(transactions[0].factor, 1.0);
			Assert::AreEqual(transactions[0].disposer, 0);
			Assert::IsTrue(  transactions[0].currency == EUR);
			Assert::AreEqual(transactions[0].fromAccount, BAR_TRANSACTION);
			Assert::AreEqual(transactions[0].toAccount, 0);

			Assert::AreEqual(transactions[1].amount, 150.0);
			Assert::AreEqual(transactions[1].factor, 0.5);
			Assert::AreEqual(transactions[1].disposer, 0);
			Assert::IsTrue(  transactions[1].currency == USD);
			Assert::AreEqual(transactions[1].fromAccount, 0);
			Assert::AreEqual(transactions[1].toAccount, BAR_TRANSACTION);

			free(transactions);
		}
	};
}