#include "Persistence.h"

Persistence *Persistence::instance;
sqlite3 *Persistence::dbHandle;

Persistence::Persistence() {
	connect();
}

Persistence::~Persistence() {

}

Persistence *Persistence::getInstance() {
	if (Persistence::instance == NULL) {
		Persistence::instance = new Persistence();
	}
	return Persistence::instance;
}

bool Persistence::connect() {
	this->sqliteResultCode = sqlite3_open(this->dbName.c_str(), &dbHandle);
	if (this->sqliteResultCode == SQLITE_OK) {
		this->isConnected = true;//do tables exist?
		createTables();
		return this->isConnected;
	}
	return this->isConnected = false;
}

void Persistence::disconnect() {
	if (this->isConnected == true)
		sqlite3_close(dbHandle);
}

void Persistence::deleteAll() {
	/*this->query(DELETE_ALL_CUSTOMER_TO_ACCOUNT.c_str());
	this->query(DELETE_ALL_CUSTOMER.c_str());
	this->query(DELETE_ALL_ACCOUNT.c_str());
	this->query(DELETE_ALL_TRANSACTION.c_str());*/
	//delete all in a single transaction (to be ACID) 
	//causes that the db is locked for a certain time, which blocks running all tests.
	//As this is a performance issue the tables will be deleted in multiple transactions instead.
	query(BEGIN_TRANSACTION.c_str());
	query(DELETE_ALL_CUSTOMER_TO_ACCOUNT.c_str());
	if (this->sqliteResultCode == SQLITE_DONE) {
		query(DELETE_ALL_CUSTOMER.c_str());
		if (this->sqliteResultCode == SQLITE_DONE) {
			query(DELETE_ALL_ACCOUNT.c_str());
			if (this->sqliteResultCode == SQLITE_DONE) {
				query(DELETE_ALL_TRANSACTION.c_str());
				if (this->sqliteResultCode == SQLITE_DONE) {
					query(COMMIT.c_str());
					//return true;
				}
				else {
					query(ROLLBACK.c_str());
				}
			}
			else {
				query(ROLLBACK.c_str());
			}
		}
		else {
			query(ROLLBACK.c_str());
		}
	}
	else {
		query(ROLLBACK.c_str());
	}
	//return false;
}

bool Persistence::getIsConnected() {
	return this->isConnected;
}

string Persistence::quote(string value) {
	return "'" + value + "'";
}

void Persistence::insertOrReplace(list<Customer*>* customerList){
	ostringstream insertQuery;
	for(list<Customer*>::const_iterator it = customerList->begin(); it != customerList->end(); ++it){
		insertQuery <<
			"REPLACE INTO CUSTOMER(ID, FIRST_NAME, LAST_NAME, STREET, ZIP, ACTIVE) "
			<< "VALUES ( "
			<< (*it)->getId() << ", "
			<< quote((*it)->getFirstName()) << ", "
			<< quote((*it)->getLastName()) << ", "
			<< quote((*it)->getStreet()) << ", "
			<< (*it)->getZip() << ", "
			<< (*it)->isActive() << " );";
		query(insertQuery.str().c_str());
		insertQuery.str(string());//clears the stream
	}
	
}

void Persistence::insertOrReplace(list<Account*>* accountList) {
	ostringstream insertQuery;
	for (list<Account*>::const_iterator it = accountList->begin(); it != accountList->end(); ++it) {
		insertQuery <<
			"REPLACE INTO ACCOUNT(ACCOUNT_NUMBER, NAME, TYPE, ACTIVE) "
			<< "VALUES ( "
			<< (*it)->GetAccountNumber() << ", "
			<< quote((*it)->getName()) << ", "
			<< (*it)->GetAccountType() << ", "
			<< (*it)->isActive() << " );";
		query(insertQuery.str().c_str());
		insertOrReplaceRelationCustomerToAccount(it);
		insertQuery.str(string());//clears the stream
	}
}

void Persistence::insertOrReplaceRelationCustomerToAccount(list<Account *>::const_iterator disposerIterator) {
	ostringstream insertQuery;
	//carries about the many-to-many relation table between customer and account
	for (list<Customer*>::const_iterator it = (*disposerIterator)->GetDisposers()->begin(); it != (*disposerIterator)->GetDisposers()->end(); ++it) {
		insertQuery <<
			"REPLACE INTO CUSTOMER_TO_ACCOUNT(CUSTOMER_ID, ACCOUNT_ID) "
			<< "VALUES ( "
			<< (*it)->getId() << ", "
			<< (*disposerIterator)->GetAccountNumber() << " );";
		query(insertQuery.str().c_str());
		insertQuery.str(string());//clears the stream
	}
}

void Persistence::insertOrReplace(list<Transaction*>* transactionList) {
	ostringstream insertQuery;
	query("DELETE FROM TRANSFER;");//Using a delete without a where clause: The TRUNCATE optimizer removes all data from the table without the need to visit each row in the table.
	insertQuery.str(string());
	for (list<Transaction*>::const_iterator it = transactionList->begin(); it != transactionList->end(); ++it) {
		insertQuery <<
			"INSERT INTO TRANSFER(ID, CURRENCY, FACTOR, AMOUNT, SOURCE_ACCOUNT, TARGET_ACCOUNT, DISPOSER) "
			<< "VALUES ( "
			<< (*it)->getTransactionId() << ", "
			<< (*it)->getCurrency() << ", "
			<< (*it)->getFactor() << ", "
			<< (*it)->getAmount() << ", "
			<< (*it)->getFrom() << ", " 
			<< (*it)->getTo() << ", "
			<< (*it)->getDisposer() << " );";
		query(insertQuery.str().c_str());
		insertQuery.str(string());//clears the stream
	}
}


list<Customer*>* Persistence::getAllCustomers() {
	list<Customer*>* entityList = new list<Customer*>();
	vector<vector<string>> resultVector = this->query((CUSTOMER_SELECT+string(";")).c_str());

	for (vector<vector<string> >::iterator it = resultVector.begin(); it != resultVector.end(); ++it) {
		vector<string> row = *it;
		//cout << "CUSTOMER: (ID=" << row.at(0) << ", FIRST_NAME=" << row.at(1) << ", LAST_NAME = " << row.at(2) << ", STREET = " << row.at(3) << ", ZIP = " << row.at(4) << ", ACTIVE = " << row.at(5) << ")" << endl;
		
		Customer *customer = unmarshallingCustomer(row);

		//Select the related Accounts to this Customer
		ostringstream getAccountsToCustomerQuery;
		getAccountsToCustomerQuery << ACCOUNT_SELECT <<" JOIN CUSTOMER_TO_ACCOUNT ON CUSTOMER_TO_ACCOUNT.ACCOUNT_ID = ACCOUNT.ACCOUNT_NUMBER WHERE CUSTOMER_TO_ACCOUNT.CUSTOMER_ID = '" << customer->getId() << "';";
		vector<vector<string>> AccountsToThisCustomerVector = this->query(getAccountsToCustomerQuery.str().c_str());
		for (vector<vector<string> >::iterator it = AccountsToThisCustomerVector.begin(); it < AccountsToThisCustomerVector.end(); ++it) {
			vector<string> row = *it;
			Account *account = unmarshallingAccount(row);
			customer->getAccounts()->push_back(account);
		}

		entityList->push_back(customer);
	}

	return entityList;
}
	
list<Account*>* Persistence::getAllAccounts() {
	list<Account*>* entityList = new list<Account*>();
	vector<vector<string>> resultVector = this->query((ACCOUNT_SELECT+string(";")).c_str());

	for (vector<vector<string> >::iterator it = resultVector.begin(); it != resultVector.end(); ++it) {
		vector<string> row = *it;
		//cout << "ACCOUNT: (ID=" << row.at(0) << ", ACCOUNT_NUMBER=" << row.at(1) << ", NAME = " << row.at(2) << ", TYPE = " << row.at(3) << ", ACTIVE = " << row.at(4) << ")" << endl;

		Account *account = unmarshallingAccount(row);
		
		//Set the disposer-list
		ostringstream getCustomersToAccountQuery;
		getCustomersToAccountQuery << CUSTOMER_SELECT <<" JOIN CUSTOMER_TO_ACCOUNT ON CUSTOMER_TO_ACCOUNT.CUSTOMER_ID = CUSTOMER.ID WHERE CUSTOMER_TO_ACCOUNT.ACCOUNT_ID = '" << account->GetAccountNumber() << "';";
		vector<vector<string>> CustomerToThisAccountVector = this->query(getCustomersToAccountQuery.str().c_str());
		for (vector<vector<string> >::iterator it = CustomerToThisAccountVector.begin(); it != resultVector.end(); ++it) {
			vector<string> row = *it;
			Customer *customer = unmarshallingCustomer(row);
			account->GetDisposers()->push_back(customer);
		}

		//TODO: has to be removed as the transaction-list is not a part of the account class anymore
		//Set the transaction-list
		/*ostringstream getTransactionsToAccountQuery;
		getTransactionsToAccountQuery << TRANSACTION_SELECT << " WHERE TRANSACTION.SOURCE_ACCOUNT = " << account->GetAccountNumber() << " OR TRANSACTION.TARGET_ACCOUNT = " << account->GetAccountNumber() << ";";
		vector<vector<string>> TransactionsToThisAccountVector = this->query(getTransactionsToAccountQuery.str().c_str());
		for (vector<vector<string> >::iterator it = TransactionsToThisAccountVector.begin(); it < resultVector.end(); ++it) {
			vector<string> row = *it;
			
			Account *sourceAccount;
			Account *targetAccount;
			Customer *disposer;

			//Get the source-Account
			ostringstream getAdditionalQuery;
			getAdditionalQuery << ACCOUNT_SELECT << " WHERE ACCOUNT_NUMBER = " << row.at(4) << ";";
			vector<vector<string>> sourceAccountVector = this->query((getAdditionalQuery.str().c_str()));
			for (vector<vector<string> >::iterator it = sourceAccountVector.begin(); it < sourceAccountVector.end(); ++it) {
				vector<string> row = *it;
				sourceAccount = unmarshallingAccount(row);
			}
			getAdditionalQuery.clear();
			//Get the target-Account
			getAdditionalQuery << ACCOUNT_SELECT << " WHERE ACCOUNT_NUMBER = " << row.at(5) << ";";
			vector<vector<string>> targetAccountVector = this->query((getAdditionalQuery.str().c_str()));
			for (vector<vector<string> >::iterator it = targetAccountVector.begin(); it < targetAccountVector.end(); ++it) {
				vector<string> row = *it;
				targetAccount = unmarshallingAccount(row);
			}
			getAdditionalQuery.clear();
			//Get the disposer
			getAdditionalQuery << CUSTOMER_SELECT << " WHERE ID = " << row.at(6) << ";";
			vector<vector<string>> disposerVector = this->query((getAdditionalQuery.str().c_str()));
			for (vector<vector<string> >::iterator it = disposerVector.begin(); it < disposerVector.end(); ++it) {
				vector<string> row = *it;
				disposer = unmarshallingCustomer(row);
			}
			getAdditionalQuery.clear();

			Transaction *transaction = unmarshallingTransaction(row, sourceAccount, targetAccount, disposer);
			account->GetTransactions()->push_back(transaction);
		}*/

		entityList->push_back(account);
	}

	return entityList;
}

Account* Persistence::unmarshallingAccount(vector<string> row) {
	int accountNumber = atoi(row.at(0).c_str());
	string name = row.at(1);
	ACCOUNT_TYPE type = static_cast<ACCOUNT_TYPE>(atoi(row.at(2).c_str()));
	Account *account = new Account(name, type);
	account->setIsActive(row.at(3) == "1" ? true : false);
	account->SetAccountNumber(accountNumber);

	return account;
}

Customer* Persistence::unmarshallingCustomer(vector<string> row) {
	string firstname = row.at(1);
	string lastname = row.at(2);
	string street = row.at(3);
	int zip = atoi(row.at(4).c_str());
	Customer *customer = new Customer(firstname, lastname, street, zip);
	customer->setIsActive(row.at(5) == "1" ? true : false);
	customer->setId(atoi(row.at(0).c_str()));

	return customer;
}

Transaction* Persistence::unmarshallingTransaction(vector<string> row, Account *from, Account *to, Customer *disposer) {
	int id = atoi(row.at(0).c_str());
	double amount = atof(row.at(1).c_str());
	double factor = atof(row.at(3).c_str());
	CURRENCY currency = static_cast<CURRENCY>(atoi(row.at(2).c_str()));

	Transaction *transaction = new Transaction(amount, factor, currency, from, to, disposer);
	transaction->setTransactionId(id);

	return transaction;
}

list<Transaction*>* Persistence::getAllTransactions() {
	list<Transaction*>* entityList = new list<Transaction*>();
	vector<vector<string>> resultVector = this->query((TRANSACTION_SELECT+string(";")).c_str());

	for (vector<vector<string> >::iterator it = resultVector.begin(); it != resultVector.end(); ++it) {
		vector<string> row = *it;
		//cout << "TRANSACTION: (ID=" << row.at(0) << ", AMOUNT=" << row.at(1) << ", CURRENCY = " << row.at(2) << ", FACTOR = " << row.at(3) << ", SOURCE_ACCOUNT = " << row.at(4) << ", TARGET_ACCOUNT = " << row.at(5) << ")" << endl;

		Account *sourceAccount;
		Account *targetAccount;
		Customer *disposer;

		//Get the source-Account
		ostringstream getAdditionalQuery;
		getAdditionalQuery << ACCOUNT_SELECT << " WHERE ACCOUNT_NUMBER = '" << row.at(4) << "';";
		vector<vector<string>> sourceAccountVector = this->query((getAdditionalQuery.str().c_str()));
		for (vector<vector<string> >::iterator it = sourceAccountVector.begin(); it != sourceAccountVector.end(); ++it) {
			vector<string> row = *it;
			sourceAccount = unmarshallingAccount(row);
		}
		getAdditionalQuery.str(string());//clears the stream;
		//Get the target-Account
		getAdditionalQuery << ACCOUNT_SELECT << " WHERE ACCOUNT_NUMBER = '" << row.at(5) << "';";
		vector<vector<string>> targetAccountVector = this->query((getAdditionalQuery.str().c_str()));
		for (vector<vector<string> >::iterator it = targetAccountVector.begin(); it != targetAccountVector.end(); ++it) {
			vector<string> row = *it;
			targetAccount = unmarshallingAccount(row);
		}
		getAdditionalQuery.str(string());//clears the stream;
		//Get the disposer
		getAdditionalQuery << CUSTOMER_SELECT << " WHERE ID = '" << row.at(6) << "';";
		vector<vector<string>> disposerVector = this->query((getAdditionalQuery.str().c_str()));
		for (vector<vector<string> >::iterator it = disposerVector.begin(); it != disposerVector.end(); ++it) {
			vector<string> row = *it;
			disposer = unmarshallingCustomer(row);
		}
		getAdditionalQuery.str(string());//clears the stream;

		Transaction *transaction = unmarshallingTransaction(row, sourceAccount, targetAccount, disposer);
		entityList->push_back(transaction);
	}

	return entityList;
}

string Persistence::getDbName(){
	return this->dbName;
}

Persistence *Persistence::setDbName(string dbName){
	this->dbName = dbName;
	return this;
}

void Persistence::createTables() {

	//CREATE-Statement for Customer
	createHelper(this->CREATE_CUSTOMER);
	//CREATE-Statement for Account
	createHelper(this->CREATE_ACCOUNT);
	//CREATE-Statement for m-to-n relation between customer and account
	createHelper(this->CREATE_CUSTOMER_TO_ACCOUNT);
	//CREATE-Statement for Transaction
	createHelper(this->CREATE_TRANSACTION);
}

int Persistence::createHelper(string str) {

	if (dbHandle != nullptr) {
		this->sqliteResultCode = sqlite3_prepare_v2(dbHandle, /*sqlCreateQuery.str().c_str()*/str.c_str(), str.length(), &this->statement, 0);
		if (this->sqliteResultCode == SQLITE_OK) {
			this->sqliteResultCode = sqlite3_step(statement);
			this->sqliteResultCode = sqlite3_finalize(statement);
		}
	}
	return this->sqliteResultCode;
}

sqlite3 *Persistence::getDbHandle() {
	return dbHandle;
}

int Persistence::count(DataModule table) {
	string str = "";
	switch (table)
	{
	case DataModule::ACCOUNT_TABLE:
		str = "SELECT COUNT(*) FROM ACCOUNT;";
		break;
	case DataModule::CUSTOMER_TABLE:
		str = "SELECT COUNT(*) FROM CUSTOMER;";
		break;
	case DataModule::CUSTOMER_TO_ACCOUNT_TABLE:
		str = "SELECT COUNT(*) FROM CUSTOMER_TO_ACCOUNT;";
		break;
	case DataModule::TRANSACTION_TABLE:
		str = "SELECT COUNT(*) FROM TRANSFER;";
		break;
	case DataModule::CURRENCY_TABLE:
		str = "SELECT COUNT(*) FROM CURRENCY;";
		break;
	}
	int resultcode = 0;
	int result = 0;
	if (sqlite3_prepare_v2(getDbHandle(), str.c_str(), str.length(), &(this->statement), 0) == SQLITE_OK) {
		resultcode = sqlite3_step(this->statement);
		//resultcode = sqlite3_finalize(this->statement);
		result = sqlite3_column_int(this->statement,0);
	}
	return result;
}

vector<vector<string>> Persistence::query(const char* query){

	sqlite3_stmt *statement;
	vector<vector<string> > results;

	if (sqlite3_prepare_v2(dbHandle, query, -1, &statement, 0) == SQLITE_OK) {
		int cols = sqlite3_column_count(statement);

		while (true) {
			this->sqliteResultCode = sqlite3_step(statement);

			if (sqliteResultCode == SQLITE_ROW) {
				vector<string> values;

				for (int col = 0; col < cols; col++) {
					std::string  val;
					char * ptr = (char*)sqlite3_column_text(statement, col);

					if (ptr) {//this is important to make sure that no NULL is pushed
						val = ptr;
					}
					else
						val = "";

					values.push_back(val);
				}
				results.push_back(values);
			}
			else {
				break;
			}
		}

		sqlite3_finalize(statement);
	}
	string error = sqlite3_errmsg(dbHandle);
	if(error != "not an error") cout << query << " " << error << endl;

	return results; 
}

int Persistence::getSqLiteResultCode() {
	return this->sqliteResultCode;
}