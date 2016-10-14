#include "Persistence.h"

#include "Customer.h"
#include "Account.h"



Persistence::Persistence()
{
	m_customers = new std::list<Customer*>();
	m_accounts = new std::list<Account*>();
	m_transactions = new std::list<Transaction*>();
}


Persistence::~Persistence()
{
	delete m_customers;
	delete m_accounts;
	delete m_transactions;
}

std::list<Customer*> * Persistence::GetCustomers()
{
	return m_customers;
}

std::list<Account*> * Persistence::GetAccounts()
{
	return m_accounts;
}

std::list<Transaction*> * Persistence::GetTransactions()
{
	return m_transactions;
}

bool Persistence::StoreCustomer(Customer* customer)
{
	auto id = m_customers->size();
	customer->setId(id);
	m_customers->push_back(customer);
	return true;
}
bool Persistence::StoreAccount(Account* account)
{
	auto id = m_accounts->size();
	account->SetAccountNumber(id);
	m_accounts->push_back(account);
	return true;
}

bool Persistence::StoreTransaction(Transaction* transaction)
{
	m_transactions->push_back(transaction);
	return true;
}
