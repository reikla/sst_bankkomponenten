// CComponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "CComponent.h"


// This is an example of an exported variable
CCOMPONENT_API int nCComponent=0;

// This is an example of an exported function.
CCOMPONENT_API int GetNumber(void)
{
    return nCComponent+=5;
}

CCOMPONENT_API int ManipulateStruct(MYSTRUCT* myStruct) {
	if (myStruct != NULL) {
		myStruct->mystr = L"Hallo";
		return 0;
	}
	return -1;
}
