// CDllConsumer.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <stdlib.h>
#include "../CComponent/CComponent.h"
#include "../CComponent/AClass.h"


int main()
{
	MyStruct myStruct;
	myStruct.mystr = "Wie gehts dir?";
	myStruct.size = 0;
	printf("Hallo");

	MyStruct* ms2 = (MyStruct*) malloc(sizeof(MyStruct));
	ms2->mystr = "asdf";
	ms2->size = 17;

	AClass a;
	

	ManipulateStruct(ms2);
	UseClass(&a);

	getchar();

    return 0;
}


