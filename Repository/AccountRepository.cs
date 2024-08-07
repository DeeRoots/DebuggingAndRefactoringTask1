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
    }
}
