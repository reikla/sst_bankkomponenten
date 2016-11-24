#include "SharedStorage.h"
#include "Customer.h"
#include "Account.h"
#include "Transaction.h"

SharedStorage * SharedStorage::instance;

SharedStorage::SharedStorage()
{
	m_customers = new std::list<Customer*>();
	m_accounts = new std::list<Account*>();
	m_transactions = new std::list<Transaction*>();
	m_rates = new std::list<CurrencyRate*>();
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

list<CurrencyRate*>* SharedStorage::GetCurrencyRates()
{
	return m_rates;
}

bool SharedStorage::StoreCustomer(Customer* customer)
{
	auto id = m_customers->size()+1;
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
	transaction->setTransactionId(m_transactions->size());
	m_transactions->push_back(transaction);
	return true;
}

void SharedStorage::clear()
{
	m_accounts->clear();
	m_customers->clear();
	m_transactions->clear();
	m_rates->clear();
}
