// TheApi.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "TheApi.h"
#include "IComponent.h"
#include "../TheComponent/TheComponent.h"


THEAPI_API int Bar(void)
{
	myComponent->Foo();
	return 0;
}

THEAPI_API void Initialize()
{
	myComponent = new CTheComponent();
}
