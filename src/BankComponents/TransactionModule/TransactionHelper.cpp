#include "TransactionHelper.h"
#include "../Shared/Transaction.h"
#include "../Shared/Account.h"
#include "../Shared/SharedFunctions.h"
#include <list>



bool TransactionHelper::IsFrom(Transaction * transaction, Account* account)
{
	return(transaction->getFrom() == account);
}

bool TransactionHelper::IsTo(Transaction * transaction, Account* account)
{
	return(transaction->getTo() == account);
}

double TransactionHelper::GetEuroAmount(Transaction * transaction)
{
	return transaction->getAmount() * transaction->getFactor();
}

std::list<Transaction*> * TransactionHelper::GetAccountsTransactions(Account * account)
{
	auto transactions = GetStorage()->GetTransactions();

	std::list<Transaction*> * transactionList = new std::list<Transaction *>();

	for (auto it = transactions->begin(); it != transactions->end(); ++it)
	{
		auto transaction = *it;
		if (IsFrom(transaction, account) || IsTo(transaction, account))
		{
			transactionList->push_back(transaction);
		}
	}
	return transactionList;
}


TransactionHelper::TransactionHelper()
{
}


TransactionHelper::~TransactionHelper()
{
}
