// UseSharedMemB.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "UseSharedMemB.h"
#include "../SharedMem/SharedMem.h"


USESHAREDMEMB_API int getSharedMemB(void)
{
	return nSharedMem;
}

USESHAREDMEMB_API void setSharedMemB(int value)
{
	nSharedMem = value;
}
