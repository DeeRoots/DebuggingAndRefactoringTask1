using DebuggingAndRefactoringTask1.Models;

namespace DebuggingAndRefactoringTask1.Repository
{
    public static class TransactionRepository
    {
         public static List<AccountTransaction> transactions = new List<AccountTransaction>();

        public static bool AddAccountTransaction(AccountTransaction transaction) 
        {
            try
            {
                transactions.Add(transaction);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string GetAccountTransactions(int accountCode)
        {
            string returnString = string.Empty;

            foreach (var transaction in transactions.Where(t => t.AccountCode == accountCode))
            {
                //string interpolation for ease of reading.
                returnString += $"On {transaction.TransactionDateTime.ToString("dd/MM/yyyy HH:mm:ss")} AccountCode(sender) {transaction.AccountCode} sent { transaction.Amount} to AccountCode(reciever) {transaction.AccountCodeTo}.\n";
            }

            return returnString;
        }
    }
}
