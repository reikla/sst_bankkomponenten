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
#include "PersistenceModule.h"

using namespace std;

typedef enum DataModule
{
	CUSTOMER_TABLE,ACCOUNT_TABLE,TRANSACTION_TABLE,CURRENCY_TABLE
}DataModule;

class PERSISTENCEMODULE_API Persistence
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
	Persistence *setDbName(string dbName);
	int getSqLiteResultCode();
	sqlite3 *getDbHandle();
	int count(DataModule);
private:
	Persistence();
	Customer* unmarshallingCustomer(vector<string> row);
	Account* unmarshallingAccount(vector<string> row);
	Transaction* unmarshallingTransaction(vector<string> row, Account *from, Account *to, Customer *disposer);
	static Persistence *instance;
	const string CUSTOMER_SELECT = "SELECT ID, FIRST_NAME, LAST_NAME, STREET, ZIP, ACTIVE FROM CUSTOMER";
	const string ACCOUNT_SELECT = "SELECT ACCOUNT_NUMBER ,NAME, TYPE, ACTIVE FROM ACCOUNT";
	const string TRANSACTION_SELECT = "SELECT ID, AMOUNT ,CURRENCY, FACTOR, SOURCE_ACCOUNT, TARGET_ACCOUNT, DISPOSER FROM TRANSFER";
	const string CREATE_CUSTOMER = "CREATE TABLE IF NOT EXISTS CUSTOMER( ID INTEGER PRIMARY KEY NOT NULL, FIRST_NAME TEXT NOT NULL, LAST_NAME TEXT NOT NULL, STREET TEXT NOT NULL, ZIP INTEGER NOT NULL, ACTIVE BOOLEAN NOT NULL ); ";
	const string CREATE_ACCOUNT = "CREATE TABLE IF NOT EXISTS ACCOUNT(ACCOUNT_NUMBER INTEGER PRIMARY KEY NOT NULL, NAME TEXT NOT NULL, TYPE INTEGER NOT NULL, ACTIVE BOOLEAN NOT NULL DEFAULT 1 );";
	const string CREATE_CUSTOMER_TO_ACCOUNT = "CREATE TABLE IF NOT EXISTS CUSTOMER_TO_ACCOUNT( CUSTOMER_ID REFERENCES CUSTOMER (ID), ACCOUNT_ID REFERENCES ACCOUNT (ACCOUNT_NUMBER), PRIMARY KEY (CUSTOMER_ID, ACCOUNT_ID) );";
	const string CREATE_TRANSACTION = "CREATE TABLE IF NOT EXISTS TRANSFER( ID INTEGER PRIMARY KEY NOT NULL, CURRENCY INTEGER NOT NULL, FACTOR REAL NOT NULL, AMOUNT REAL NOT NULL, SOURCE_ACCOUNT REFERENCES CUSTOMER_TO_ACCOUNT (ACCOUNT_ID), TARGET_ACCOUNT REFERENCES ACCOUNT (ACCOUNT_NUMBER), DISPOSER REFERENCES CUSTOMER_TO_ACCOUNT (CUSTOMER_ID) );";
	bool isConnected = false;
	int sqliteResultCode = 0;
	string dbName = "bank.db";
	sqlite3 *dbHandle = nullptr;
	sqlite3_stmt *statement;
	void createTables();
	int createHelper(string query);
	vector<vector<string>> query(const char* query);
	void insertOrReplaceRelationCustomerToAccount(list<Account *>::const_iterator disposerIterator);
};