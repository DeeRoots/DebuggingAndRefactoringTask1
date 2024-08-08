using DebuggingAndRefactoringTask1.Models;
using static DebuggingAndRefactoringTask1.Enums.Enums;
using static DebuggingAndRefactoringTask1.Repository.TransactionRepository;

namespace DebuggingAndRefactoringTask1.ServiceLayer
{
    public class TransactionServices
    {
        public bool ProcessTransaction(int accountCode, int accountCodeTo, double amount, AccountInteractionType accountInteractionType)
        {
            var success = AddAccountTransaction(new AccountTransaction { Id = transactions.Count() + 1, AccountCode = accountCode, AccountCodeTo = accountCodeTo, Amount = amount, TransactionDateTime = DateTime.Now, TranactionType = accountInteractionType });
            return success;
        }

        public string DisplayAccountTransactionHistoryDetails(int accountCode)
        {
            //Gets log as \n separated string.
            var value = GetAccountTransactions(accountCode);
            return value;
        }
    }
}
