#pragma once
#include <string>
#include <list>

using namespace std;
class Account;

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
	void setIsActive(bool);

	list<Account*> getAccounts();
	void addAccount(Account*);
private:
	string m_firstName;
	string m_lastName;
	string m_street;
	int m_zip;
	
	int m_id;
	bool m_active;
	
	list<Account*> m_accounts;
};

