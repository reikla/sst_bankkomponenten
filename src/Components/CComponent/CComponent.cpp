// CComponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CComponent.h"
#include "AClass.h"


// This is an example of an exported variable
CCOMPONENT_API int APIVERSION = 1;
int number = 0;

// This is an example of an exported function.
CCOMPONENT_API int GetNumber(void)
{
    return number+=5;
}

CCOMPONENT_API int ManipulateStruct(MYSTRUCT* myStruct) {
	if (myStruct != NULL) {
		//myStruct->mystr = "Hallo";
		return 0;
	}
	return -1;
}

CCOMPONENT_API void UseClass(AClass *b) 
{
	AClass a;
	a.Foo();
}