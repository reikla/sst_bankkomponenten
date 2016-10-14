// UseSharedMemA.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "UseSharedMemA.h"
#include "../SharedMem/SharedMem.h"

USESHAREDMEMA_API int getSharedMemA(void)
{
	return nSharedMem;
}

USESHAREDMEMA_API void setSharedMemA(int value)
{
	nSharedMem = value;
}
