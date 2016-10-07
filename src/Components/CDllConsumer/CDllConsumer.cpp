// CDllConsumer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../CComponent/CComponent.h"


int main()
{
	MyStruct myStruct;
	myStruct.mystr = "Wie gehts dir?";
	myStruct.size = 0;
	printf("Hallo");
	

	ManipulateStruct(&myStruct);

	getchar();

    return 0;
}

