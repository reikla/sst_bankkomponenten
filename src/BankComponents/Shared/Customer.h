#pragma once
#pragma warning( push )
#pragma warning( disable : 4251)
#include <string>
#include <list>
#include "Shared.h"

using namespace std;
class Account;

class SHARED_API Customer
{
public:
	Customer(string firstName, string lastName, string street, int zip);
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
	void setIsActive(bool);

	list<Account*> * getAccounts();
	void addAccount(Account*);

private:
	string m_firstName;
	string m_lastName;
	string m_street;
	int m_zip;
	
	int m_id;
	bool m_active;
	
	list<Account*> * m_accounts;
};
#pragma warning( pop ) 