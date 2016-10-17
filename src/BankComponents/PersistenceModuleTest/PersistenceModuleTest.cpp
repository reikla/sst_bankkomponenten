#include "stdafx.h"
#include "CppUnitTest.h"
#include "../PersistenceModule/PersistenceModule.h"
#include "../Shared/ErrorCodes.h"
#include "../Shared/SharedFunctions.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace PersistenceModuleTest
{		
	TEST_CLASS(UnitTest1)
	{
	public:
		//Bitte deine Tests auch nach dem Schema: Module_Operation_Ergebnis
		TEST_METHOD(Persistence_Save_OK)
		{
			auto returnValue = Store();
			Assert::AreEqual(E_OK, returnValue);
		}

	};
}