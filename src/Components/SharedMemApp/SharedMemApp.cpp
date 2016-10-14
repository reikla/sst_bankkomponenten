// SharedMemApp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../UseSharedMemA/UseSharedMemA.h"
#include "../UseSharedMemB/UseSharedMemB.h"

#include <iostream>

using namespace std;


int main()
{
	int i = 0;
	setSharedMemA(17);
	cout << "Shared mem: " << getSharedMemB() << endl;
	cin >> i;
}

