#include "stdafx.h"
#include "CppUnitTest.h"

#include "../Shared/Currency.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedStorage.h"
#include "../TransactionModule/TransactionModule.h"

#include "../AccountModule/AccountModule.h"

#include "../CustomerModule/CustomerModule.h"

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

	};
}