#include "stdafx.h"
#include "CppUnitTest.h"
#include "../CurrencyTranslationModule/CurrencyTranslationModule.h"
#include "../Shared/Currency.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedStorage.h"
#include <cmath>

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace CurrencyTranslationModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		
		TEST_METHOD(CurrencyTranslation_SetFactor_OK)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyToEuroFactor(USD, 1);

			Assert::AreEqual(E_OK, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_SetFactor_EuroInvalidParameter)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyToEuroFactor(EUR, 1);

			Assert::AreEqual(E_INVALID_PARAMETER, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_SetFactor_FactorInvalidParameter)
		{
			SharedStorage::GetInstance()->clear();

			auto returnValue = SetCurrencyToEuroFactor(USD, -1.2);

			Assert::AreEqual(E_INVALID_PARAMETER, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_GetFactor_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyToEuroFactor(USD, 1.7);
			double factor;

			auto returnValue = GetCurrencyToEuroFactor(USD, factor);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1.7, factor);
		}

		TEST_METHOD(CurrencyTranslation_GetFactorEUR_OK)
		{
			SharedStorage::GetInstance()->clear();
			double factor;

			auto returnValue = GetCurrencyToEuroFactor(EUR, factor);

			Assert::AreEqual(E_OK, returnValue);
			Assert::AreEqual(1.0, factor);
		}

		TEST_METHOD(CurrencyTranslation_GetFactor_NotStored)
		{
			SharedStorage::GetInstance()->clear();
			double factor;

			auto returnValue = GetCurrencyToEuroFactor(USD, factor);

			Assert::AreEqual(E_CURRENCY_FACTOR_NOT_STORED, returnValue);
		}

		TEST_METHOD(CurrencyTranslation_TranslateToEuro_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyToEuroFactor(USD, 0.911);

			double result;
			auto returnValue = TranslateToEuro(USD, 1, result);

			auto diff = 0.911 - result;

			auto diffOk = abs(diff) < 0.01;

			Assert::AreEqual(E_OK, returnValue);
			Assert::IsTrue(diffOk);
		}

		TEST_METHOD(CurrencyTranslation_TranslateFromEuro_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyToEuroFactor(USD, 0.911);

			double result;
			auto returnValue = TranslateFromEuro(USD, 1, result);

			auto diff = 1.0971 - result;

			auto diffOk = abs(diff) < 0.01;

			Assert::AreEqual(E_OK, returnValue);
			Assert::IsTrue(diffOk);
		}

		TEST_METHOD(CurrencyTranslation_TranslateSameVariable_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyToEuroFactor(USD, 0.911);

			double result = 1;
			auto returnValue = TranslateFromEuro(USD, result, result);

			auto diff = 1.0971 - result;

			auto diffOk = abs(diff) < 0.01;

			Assert::AreEqual(E_OK, returnValue);
			Assert::IsTrue(diffOk);
		}

		TEST_METHOD(CurrencyTranslation_StoreSameCurrencyTwice_OK)
		{
			SharedStorage::GetInstance()->clear();
			SetCurrencyToEuroFactor(USD, 0.911);
			SetCurrencyToEuroFactor(USD, 17);

			double factor = 1;
			auto returnValue = TranslateFromEuro(USD, factor, factor);

			GetCurrencyToEuroFactor(USD, factor);

			Assert::AreEqual(17.0, factor);

		}

	};
}