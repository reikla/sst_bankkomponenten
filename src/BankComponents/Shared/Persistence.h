#pragma once
#include "sqlite3.h"
#include <string>
#include <sstream> //for stringstream
#include <vector>
#include <iostream>
#include <list>
#include "Customer.h"
#include "Account.h"
#include "Transaction.h"
#include "CurrencyRate.h"
#include "Shared.h"

#include <stdlib.h>

using namespace std;

typedef enum DataModule
{
	CUSTOMER_TABLE,ACCOUNT_TABLE,TRANSACTION_TABLE, CUSTOMER_TO_ACCOUNT_TABLE,CURRENCY_RATE_TABLE
}DataModule;

class SHARED_API Persistence
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
	void insertOrReplace(list<CurrencyRate*>* currencyRateList);
	list<Customer*>* getAllCustomers();
	list<Account*>* getAllAccounts();
	list<Transaction*>* getAllTransactions();
	list<CurrencyRate*>* getAllCurrencyRates();
	string getDbName();
	Persistence *setDbName(string dbName);
	int getSqLiteResultCode();
	static sqlite3 *getDbHandle();
	int count(DataModule);
	void deleteAll();
	
private:
	Persistence();
	Customer* unmarshallingCustomer(vector<string> row);
	Account* unmarshallingAccount(vector<string> row);
	Transaction* unmarshallingTransaction(vector<string> row, Account *from, Account *to, Customer *disposer);
	CurrencyRate* unmarshallingCurrencyRate(vector<string> row);
	static Persistence *instance;
	//SELECT-STATEMENTS
	const string CURRENCY_RATE_SELECT = "SELECT CURRENCY, FACTOR FROM CURRENCY_RATE";
	const string CUSTOMER_SELECT = "SELECT ID, FIRST_NAME, LAST_NAME, STREET, ZIP, ACTIVE FROM CUSTOMER";
	const string ACCOUNT_SELECT = "SELECT ACCOUNT_NUMBER ,NAME, TYPE, ACTIVE FROM ACCOUNT";
	const string TRANSACTION_SELECT = "SELECT ID, AMOUNT ,CURRENCY, FACTOR, SOURCE_ACCOUNT, TARGET_ACCOUNT, DISPOSER FROM TRANSFER";
	//CREATE-STATEMENTS
	const string CREATE_CURRENCY_RATE = "CREATE TABLE IF NOT EXISTS CURRENCY_RATE(CURRENCY INTEGER NOT NULL, FACTOR REAL NOT NULL);";
	const string CREATE_CUSTOMER = "CREATE TABLE IF NOT EXISTS CUSTOMER( ID INTEGER PRIMARY KEY NOT NULL, FIRST_NAME TEXT NOT NULL, LAST_NAME TEXT NOT NULL, STREET TEXT NOT NULL, ZIP INTEGER NOT NULL, ACTIVE BOOLEAN NOT NULL );";
	const string CREATE_ACCOUNT = "CREATE TABLE IF NOT EXISTS ACCOUNT(ACCOUNT_NUMBER INTEGER PRIMARY KEY NOT NULL, NAME TEXT NOT NULL, TYPE INTEGER NOT NULL, ACTIVE BOOLEAN NOT NULL DEFAULT 1 );";
	const string CREATE_CUSTOMER_TO_ACCOUNT = "CREATE TABLE IF NOT EXISTS CUSTOMER_TO_ACCOUNT( CUSTOMER_ID REFERENCES CUSTOMER (ID), ACCOUNT_ID REFERENCES ACCOUNT (ACCOUNT_NUMBER), PRIMARY KEY (CUSTOMER_ID, ACCOUNT_ID) );";
	const string CREATE_TRANSACTION = "CREATE TABLE IF NOT EXISTS TRANSFER( ID INTEGER PRIMARY KEY NOT NULL, CURRENCY INTEGER NOT NULL, FACTOR REAL NOT NULL, AMOUNT REAL NOT NULL, SOURCE_ACCOUNT REFERENCES ACCOUNT (ACCOUNT_NUMBER), TARGET_ACCOUNT REFERENCES ACCOUNT (ACCOUNT_NUMBER), DISPOSER REFERENCES CUSTOMER (CUSTOMER_ID) );";
	//TRANSACTION-STATEMENTS
	const string BEGIN_TRANSACTION = "BEGIN TRANSACTION;";
	const string COMMIT = "END TRANSACTION;";
	const string ROLLBACK = "ROLLBACK;";
	//DELETE-STATEMENTS
	const string DELETE_ALL_CUSTOMER = "DELETE FROM CUSTOMER;";
	const string DELETE_ALL_CUSTOMER_TO_ACCOUNT = "DELETE FROM CUSTOMER_TO_ACCOUNT;";
	const string DELETE_ALL_ACCOUNT = "DELETE FROM ACCOUNT;";
	const string DELETE_ALL_TRANSACTION = "DELETE FROM TRANSFER;";
	const string DELETE_ALL_CURRENCY_RATE = "DELETE FROM CURRENCY_RATE;";
	bool isConnected = false;
	int sqliteResultCode = 0;
	string dbName = "bank.db";
	static sqlite3 *dbHandle;
	sqlite3_stmt *statement;
	void createTables();
	string quote(string value);
	int createHelper(string query);
	vector<vector<string>> query(const char* query);
	void insertOrReplaceRelationCustomerToAccount(list<Account *>::const_iterator disposerIterator);
};