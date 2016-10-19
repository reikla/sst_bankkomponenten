#pragma once
#include "sqlite3.h"
#include <string>
#include <sstream> //for stringstream
#include <vector>
#include <iostream>
#include <list>
#include "../Shared/Customer.h"
#include "../Shared/Account.h"
#include "../Shared/Transaction.h"
#include <stdlib.h>

using namespace std;

/*enum DataModule
{
	Customer,Account,Transaction
};*/

class Persistence
{
public:
	virtual ~Persistence();
	static Persistence *getInstance();
	bool connect();
	void disconnect();
	bool getIsConnected();
	void insertOrReplace(list<Customer*>* customerList);
	void insertOrReplace(list<Account*>* accountList);
	void insertOrReplace(list<Transaction*>* transactionList);
	list<Customer*>* getAllCustomers();
	list<Account*>* getAllAccounts();
	list<Transaction*>* getAllTransactions();
	string getDbName();
	Persistence setDbName(string dbName);
	int getSqLiteResultCode();

private:
	Persistence();
	static Persistence *instance;
	const string CUSTOMER_SELECT = "SELECT ID, FIRST_NAME, LAST_NAME, STREET, ZIP, ACTIVE FROM CUSTOMER";
	const string ACCOUNT_SELECT = "SELECT ACCOUNT_NUMBER ,NAME, TYPE, ACTIVE FROM ACCOUNT";
	const string TRANSACTION_SELECT = "SELECT ID, AMOUNT ,CURRENCY, FACTOR, SOURCE_ACCOUNT, TARGET_ACCOUNT, DISPOSER FROM ACCOUNT";
	bool isConnected = false;
	int sqliteResultCode = 0;
	string dbName = "bank.db";
	sqlite3 *dbHandle = nullptr;
	sqlite3_stmt *statement;
	int createTables();
	vector<vector<string>> query(const char* query);
	void insertOrReplaceRelationCustomerToAccount(list<Account *>::const_iterator disposerIterator);
};