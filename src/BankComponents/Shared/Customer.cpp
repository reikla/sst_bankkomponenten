#include "Customer.h"



Customer::Customer()
{
	m_accounts = new std::list<Account*>();
}


Customer::~Customer()
{
	delete m_accounts;
}

std::string Customer::getFirstName()
{
	return m_firstName;
}

void Customer::setFirstName(string firstName)
{
	m_firstName = firstName;
}

std::string Customer::getLastName()
{
	return m_lastName;
}

void Customer::setLastName(string lastName)
{
	m_lastName = lastName;
}

std::string Customer::getStreet()
{
	return m_street;
}

void Customer::setStreet(string street)
{
	m_street = street;
}

int Customer::getZip()
{
	return m_zip;
}

void Customer::setZip(int zip)
{
	m_zip = zip;
}

int Customer::getId()
{
	return m_id;
}

void Customer::setId(int id)
{
	m_id = id;
}

bool Customer::isActive()
{
	return m_active;
}

void Customer::setIsActive(bool isActive)
{
	m_active = isActive;
}

std::list<Account*> * Customer::getAccounts()
{
	return m_accounts;
}

void Customer::addAccount(Account*)
{

}
