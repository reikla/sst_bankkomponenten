#include "SharedStorage.h"
#include "Customer.h"
#include "Account.h"



SharedStorage::SharedStorage()
{
	m_customers = new std::list<Customer*>();
	m_accounts = new std::list<Account*>();
	m_transactions = new std::list<Transaction*>();
}


SharedStorage::~SharedStorage()
{
	delete m_customers;
	delete m_accounts;
	delete m_transactions;
}

SharedStorage * SharedStorage::GetInstance()
{
	if (SharedStorage::instance == NULL) {
		SharedStorage::instance = new SharedStorage();
	}
	return SharedStorage::instance;
}

std::list<Customer*> * SharedStorage::GetCustomers()
{
	return m_customers;
}

std::list<Account*> * SharedStorage::GetAccounts()
{
	return m_accounts;
}

std::list<Transaction*> * SharedStorage::GetTransactions()
{
	return m_transactions;
}

bool SharedStorage::StoreCustomer(Customer* customer)
{
	auto id = m_customers->size();
	customer->setId(id);
	m_customers->push_back(customer);
	return true;
}
bool SharedStorage::StoreAccount(Account* account)
{
	auto id = m_accounts->size();
	account->SetAccountNumber(id);
	m_accounts->push_back(account);
	return true;
}

bool SharedStorage::StoreTransaction(Transaction* transaction)
{
	m_transactions->push_back(transaction);
	return true;
}
