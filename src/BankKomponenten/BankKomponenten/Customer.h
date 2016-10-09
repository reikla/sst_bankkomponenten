#pragma once
#include <string>
#include <list>

using namespace std;
typedef Account;

class Customer
{
public:
	Customer();
	virtual ~Customer();

	string getFirstName();
	void setFirstName(string);
	
	string getLastName();
	void setLastName(string);
	
	string getStreet();
	void setStreet(string);
	
	int getZip();
	void setZip(int);
    
	
	int getId();
	void setId(int);

	bool isActive();
	void setActive(bool);

	list<Account> getAccounts();
	void addAccount(Account*);

};

