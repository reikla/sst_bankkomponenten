#include "Account.h"



Account::Account()
{
	m_disposers = new std::list<Customer*>();
	m_transactions = new std::list<Transaction*>();
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

std::list<Transaction*> * Account::GetTransactions()
{
	return m_transactions;
}

void Account::setName(std::string name)
{
	m_name = name;
}

AccountType Account::GetAccountType()
{
	return m_accountType;
}

void Account::SetAccountType(AccountType accountType)
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