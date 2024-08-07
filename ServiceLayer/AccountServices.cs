using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DebuggingAndRefactoringTask1.Models;
using static DebuggingAndRefactoringTask1.Repository.AccountRepository; 

namespace DebuggingAndRefactoringTask1.ServiceLayer
{
    public class AccountServices
    {
        public bool ProcessAccount(int accountCode, string accountName)
        {
            bool accountCodeExists = DoesAccountCodeExist(accountCode);
            if (!accountCodeExists)
            {
                AddAccount(new Account { Id = accounts.Count(), AccountCode = accountCode, AccountName = accountName, AccountBalance = 0.00 });
                return true;
            }
            else
                return false;
        }

        private bool DoesAccountCodeExist(int accountCode)
        {
            foreach (var account in accounts.Where(a => a.AccountCode == accountCode))
            {
                return true;
            }            
            return false;
        }

        public Account GetAccount(int accountCode)
        {
            foreach (var account in accounts.Where(a => a.AccountCode == accountCode))
            {
                return account;
            }

            return new Account {Id = -1 };
        }

        public bool DepositAccount(int accountCode, double amount)
        {
            var account = GetAccount(accountCode);
            if (account != null)
            {
                account.AccountBalance = account.AccountBalance += amount;
                return true;
            }
            else
            {
                return false;
            }          
        }

        public bool WithdrawAccount(int accountCode, double amount)
        {
            var account = GetAccount(accountCode);
            var accountOriginalBalance = account.AccountBalance;            
            if (account != null)
            {
                account.AccountBalance = amount <= account.AccountBalance ? account.AccountBalance -= amount : account.AccountBalance = accountOriginalBalance;
                if (account.AccountBalance < accountOriginalBalance) 
                    return true;
                else
                    return false;   
            }
            else
            {
                return false;
            }
        }
    }
}
