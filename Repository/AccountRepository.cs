using DebuggingAndRefactoringTask1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DebuggingAndRefactoringTask1.Repository
{

    public static class AccountRepository
    {
        public static List<Account> accounts = new List<Account>();

        public static bool AddAccount(Account account)
        {
            try
            {
                accounts.Add(account);
                return true;
            }
            catch (Exception e)
            {
                return false;                
            }            
        }

        public static bool DeleteAccount(int accountCode)
        {
            try
            {
                accounts.RemoveAll(r => r.AccountCode == accountCode);
                
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool EditAccountName(int accountCode, string updatedName)
        {
            try
            {
                var account = GetAccount(accountCode);
                account.AccountName = updatedName;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static Account GetAccount(int accountCode)
        {
            foreach (var account in accounts.Where(a => a.AccountCode == accountCode))
            {
                return account;
            }

            return new Account { Id = -1 };
        }
    }
}
