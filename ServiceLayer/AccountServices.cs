﻿using DebuggingAndRefactoringTask1.Models;
using static DebuggingAndRefactoringTask1.Repository.AccountRepository; 

namespace DebuggingAndRefactoringTask1.ServiceLayer
{
    public class AccountServices
    {
        public bool ProcessAccount(int accountCode, string accountName)
        {
            //Validate
            bool accountCodeExists = DoesAccountCodeExist(accountCode);
            if (!accountCodeExists)
            {
                //Process
                var success = AddAccount(new Account { Id = accounts.Count() + 1, AccountCode = accountCode, AccountName = accountName, AccountBalance = 0.00 });
                if (success) 
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public bool DoesAccountCodeExist(int accountCode)
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
                //Checks if there is sufficient balance - if not reset original balance.
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
                //Checks sender has sufficient funds before sending to recipient and updating balance.
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
        }     
    }
}
