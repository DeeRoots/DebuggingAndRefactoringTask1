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
                var success = AddAccount(new Account { Id = accounts.Count() + 1, AccountCode = accountCode, AccountName = accountName, AccountBalance = 0.00 });
                if (success) 
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool DoesAccountCodeExist(int accountCode)
        {
            if (accounts.Count > 0)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == accountCode))
                {
                    return true;
                }
            }
            return false;
        }

        public Account GetAccountDetails(int accountCode)
        {

            return GetAccount(accountCode);
            //foreach (var account in accounts.Where(a => a.AccountCode == accountCode))
            //{
            //    return account;
            //}

            //return new Account {Id = -1 };
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

        public bool TransferBetweenAccounts(int accountCode, int accountCodeTo, double amount)
        {
            var account = GetAccount(accountCode);
            var recipientAccount = GetAccount(accountCodeTo);
            var accountOriginalBalance = account.AccountBalance;
            if (account != null && recipientAccount != null)
            {
                recipientAccount.AccountBalance = amount <= account.AccountBalance ? recipientAccount.AccountBalance += amount : account.AccountBalance = accountOriginalBalance;
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

        public bool ProcessDeleteAccount(int accountCode)
        {
            bool accountCodeExists = DoesAccountCodeExist(accountCode);
            if (accountCodeExists)
            {
                var success = DeleteAccount(accountCode);
                if (success)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool ProcessEditAccountName(int accountCode, string? newAccountName)
        {
            bool accountCodeExists = DoesAccountCodeExist(accountCode);           

            if (accountCodeExists)
            {             
                var success = EditAccountName(accountCode, newAccountName);
                if (success)
                    return true;
                else
                    return false;
            }
            else
                return false;

            var account = GetAccount(accountCode);
        }

     
    }
}
