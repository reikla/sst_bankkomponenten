#pragma once
#include "stdafx.h"

inline bool CheckString(char * string) 
{
	if (string == __nullptr) {
		return false;
	}
	return true;
}

inline bool CheckZip(int zip) 
{
	if (zip < 1000 || zip > 9999) {
		return false;
	}
	return true;
}

inline bool CheckId(int id)
{
	return id >= 0;
}