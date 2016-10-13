// TheComponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "TheComponent.h"
#include <iostream>


// This is an example of an exported variable
THECOMPONENT_API int nTheComponent=0;

// This is an example of an exported function.
THECOMPONENT_API int fnTheComponent(void)
{
    return 42;
}

// This is the constructor of a class that has been exported.
// see TheComponent.h for the class definition
CTheComponent::CTheComponent()
{
    return;
}

void CTheComponent::Foo()
{
	std::cout << "CTheComponent::Foo()" << std::endl;
}
