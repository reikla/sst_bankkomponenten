#include "Account.h"



Account::Account(std::string accountName, AccountType type)
{
	m_disposers = new std::list<Customer*>();
	m_name = accountName;
	m_accountType = type;
}


Account::~Account()
{
}

bool Account::isActive() 
{
	return m_active;
}

void Account::setIsActive(bool isActive) 
{
	m_active = isActive;
}

std::string Account::getName()
{
	return m_name;
}

void Account::setName(std::string name)
{
	m_name = name;
}

AccountType Account::GetAccountType()
{
	return m_accountType;
}

void Account::SetAccountType(ACCOUNT_TYPE accountType)
{
	m_accountType = accountType;
}

std::list<Customer*> * Account::GetDisposers() 
{
	return m_disposers;
}

int Account::GetAccountNumber()
{
	return m_accountNumber;
}

void Account::SetAccountNumber(int accountNumber)
{
	m_accountNumber = accountNumber;
}
