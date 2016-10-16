#include "stdafx.h"
#include "CppUnitTest.h"
#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"
#include "../Shared/Currency.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedStorage.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace CurrencyTranslationModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(CurrencyTranslation_SetFactor_OK)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyEuroFactor(USD, 1);

			Assert::AreEqual(E_OK, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_SetFactor_EuroInvalidParameter)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyEuroFactor(EUR, 1);

			Assert::AreEqual(E_INVALID_PARAMETER, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_SetFactor_FactorInvalidParameter)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyEuroFactor(USD, -1.2);

			Assert::AreEqual(E_INVALID_PARAMETER, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_GetFactor_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyEuroFactor(USD, 1.7);
			double factor;

			auto returnValue = GetCurrencyEuroFactor(USD, factor);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1.7, factor);
		}

		TEST_METHOD(CurrencyTranslation_GetFactorEUR_OK)
		{
			SharedStorage::GetInstance()->clear();
			double factor;

			auto returnValue = GetCurrencyEuroFactor(EUR, factor);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1.0, factor);
		}

		TEST_METHOD(CurrencyTranslation_GetFactor_NotStored)
		{
			SharedStorage::GetInstance()->clear();
			double factor;

			auto returnValue = GetCurrencyEuroFactor(USD, factor);

			Assert::AreEqual(E_CURRENCY_FACTOR_NOT_STORED, returnValue);
		}

	};
}