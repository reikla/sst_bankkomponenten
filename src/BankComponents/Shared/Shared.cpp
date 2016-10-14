// Shared.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Shared.h"


// This is an example of an exported variable
SHARED_API int nShared=0;

// This is an example of an exported function.
SHARED_API int fnShared(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see Shared.h for the class definition
CShared::CShared()
{
    return;
}
