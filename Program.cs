using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.ServiceLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using static DebuggingAndRefactoringTask1.Enums.Enums;
using static DebuggingAndRefactoringTask1.Repository.AccountRepository;
using static DebuggingAndRefactoringTask1.Repository.TransactionRepository;

namespace BankingSystem
{
   
    public static class Program
    {           
        static void Main(string[] args)
        {
            var accountServices = new AccountServices();
            var generalServices = new GeneralServices();
            var transactionServices = new TransactionServices();

            while (true)
            {
                RunApplication(accountServices, generalServices, transactionServices);
            }
        } 
        public static void RunApplication(AccountServices accountServices, GeneralServices generalServices, TransactionServices transactionServices)
        {
            var choiceParsed = generalServices.DisplayMenu();
            

            switch (choiceParsed)
            {
                case 1:
                    AddAccount(accountServices, generalServices);
                    break;
                case 2:
                    MonetaryInteraction(AccountInteractionType.Deposit, accountServices, generalServices, transactionServices);
                    break;
                case 3:
                    MonetaryInteraction(AccountInteractionType.Withdraw, accountServices, generalServices, transactionServices);
                    break;
                case 4:
                    DisplayAccountDetails(accountServices, generalServices);
                    break;
                case 5:
                    DisplayAccountTransactionHistory(accountServices, generalServices, transactionServices);
                    break;
                case 6:
                    MonetaryInteraction(AccountInteractionType.Tranfer, accountServices, generalServices, transactionServices);                    
                    break;
                case 7:
                    EditAccountName(accountServices, generalServices, transactionServices);                    
                    break;
                case 8:
                    DeleteAccount(accountServices, generalServices, transactionServices);
                    break;
                case 9:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public static void DeleteAccount(AccountServices accountServices, GeneralServices generalServices, TransactionServices transactionServices)
        {
            try
            {
                var accountCodeParsed = generalServices.GatherNumericInput("Enter Account Code: ");

                if (accountCodeParsed != null)
                {
                    var confirmed = generalServices.GatherTextualInput("Enter 'Y' to delete Account: ");

                    if (confirmed.ToUpper() == "Y")
                    {
                        var success = accountServices.ProcessDeleteAccount(accountCodeParsed.Value);
                        if (success)
                            generalServices.DisplayMessage(MessageType.Success, $"Account : {accountCodeParsed} has been deleted.");
                        else
                            generalServices.DisplayMessage(MessageType.Warning, $"Account : {accountCodeParsed} has NOT been deleted.");
                    }
                    else
                        generalServices.DisplayMessage(MessageType.Warning, $"Account : {accountCodeParsed} has NOT been deleted by choice.");

                }
                else
                {
                    generalServices.DisplayMessage(MessageType.Error, "Account Code was null - Please try again");
                }
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");
            }
        }

        public static void EditAccountName(AccountServices accountServices, GeneralServices generalServices, TransactionServices transactionServices)
        {
            try
            {
                var accountCodeParsed = generalServices.GatherNumericInput("Enter Account Code: ");

                if (accountCodeParsed != null)
                {
                    var newAccountNameParsed = generalServices.GatherTextualInput("Enter new Name: ");

                    var success = accountServices.ProcessEditAccountName(accountCodeParsed.Value, newAccountNameParsed);
                    if (success)
                        generalServices.DisplayMessage(MessageType.Success, $"Account Name updated.");
                    else
                        generalServices.DisplayMessage(MessageType.Warning, $"Account Name was not updated.");

                }
                else
                {
                    generalServices.DisplayMessage(MessageType.Error, "Account Code was null - Please try again");
                }
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");
            }
        }

        public static void DisplayAccountTransactionHistory(AccountServices accountServices, GeneralServices generalServices, TransactionServices transactionServices)
        {
            try
            {
                var accountCodeParsed = generalServices.GatherNumericInput("Enter Account Code: ");

                if (accountCodeParsed != null)
                {                   
                        var transactionLog = transactionServices.DisplayAccountTransactionHistoryDetails(accountCodeParsed.Value);
                    if (transactionLog.Length > 0)
                        generalServices.DisplayMessage(MessageType.Information, $"Account Transaction History : \n {transactionLog}");
                    else
                        generalServices.DisplayMessage(MessageType.Warning, $"No recorded Transactions ");

                }
                else
                {
                    generalServices.DisplayMessage(MessageType.Error, "Account Code was null - Please try again");
                }
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");
            }
        }

        public static void AddAccount(AccountServices accountServices, GeneralServices generalServices)
        {
            try
            {               
                var accountCodeParsed = generalServices.GatherNumericInput("Enter Account Code: ");

                if (accountCodeParsed != null)
                {
                    var accountNameParsed = generalServices.GatherTextualInput("Enter Account Holder Name: ");

                    if (accountNameParsed != null)
                    {
                        var sucess = accountServices.ProcessAccount(accountCodeParsed.Value, accountNameParsed);
                        if (sucess)
                            generalServices.DisplayMessage(MessageType.Success, $"Account : {accountCodeParsed} has been added.");
                        else
                            generalServices.DisplayMessage(MessageType.Warning, $"Account : {accountCodeParsed} has NOT been added. Account code alreay exists.");
                    }
                }
                else
                {
                    generalServices.DisplayMessage(MessageType.Error, "Account Code was null - Please try again");
                }
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");
            }
        }

        public static void MonetaryInteraction(AccountInteractionType accountInteractionType, AccountServices accountServices, GeneralServices generalServices, TransactionServices transactionServices)
        {
            try
            {
                var parsedAccountCode = generalServices.GatherNumericInput("Enter Account Code: ");
                var parsedAccountCodeRecipient = accountInteractionType == AccountInteractionType.Tranfer ? generalServices.GatherNumericInput("Enter Recipient Account Code: ") : null;

                var parsedMonetarylAmount = accountInteractionType == AccountInteractionType.Deposit ? generalServices.GatherDoublelInput("Enter Amount to Deposit: ") : accountInteractionType == AccountInteractionType.Deposit ? generalServices.GatherDoublelInput("Enter Amount to Transfer: ") : generalServices.GatherDoublelInput("Enter Amount to Withdraw: ");

                if (parsedMonetarylAmount <= 0)
                {
                    generalServices.DisplayMessage(MessageType.Warning, "Deposit, Withdrawal or transfer cannot be equal to or less than 0.00 or 0");
                    return;
                }

                var success = false;
                var transactionSuccess = false;
                if (parsedAccountCode != null && parsedMonetarylAmount != null)
                {
                    switch (accountInteractionType)
                    {
                        case AccountInteractionType.Deposit:
       
                            success = accountServices.DepositAccount(parsedAccountCode.Value, parsedMonetarylAmount.Value);
                            if (success)
                            {
                                transactionSuccess = transactionServices.ProcessTransaction(parsedAccountCode.Value, parsedAccountCode.Value, parsedMonetarylAmount.Value, accountInteractionType);
                                generalServices.DisplayMessage(MessageType.Success, "Deposit successful.");
                                if (transactionSuccess)
                                    generalServices.DisplayMessage(MessageType.Success, "Transaction record successful.");
                                else
                                    generalServices.DisplayMessage(MessageType.Warning, "Transaction record unsuccessful.");

                            }
                            else
                                generalServices.DisplayMessage(MessageType.Warning, "Deposit unsuccessful.");
                            break;
                        case AccountInteractionType.Withdraw:
                            success = accountServices.WithdrawAccount(parsedAccountCode.Value, parsedMonetarylAmount.Value);
                            if (success)
                            {
                                transactionSuccess = transactionServices.ProcessTransaction(parsedAccountCode.Value, parsedAccountCode.Value, parsedMonetarylAmount.Value, accountInteractionType);
                                generalServices.DisplayMessage(MessageType.Success, "Withdrawal successful.");
                                if (transactionSuccess)
                                    generalServices.DisplayMessage(MessageType.Success, "Transaction record successful.");
                                else
                                    generalServices.DisplayMessage(MessageType.Warning, "Transaction record unsuccessful.");
                            }
                            else
                                generalServices.DisplayMessage(MessageType.Warning, "Withdrawal unsuccessful. Insufficient funds");
                            break;
                        case AccountInteractionType.Tranfer:

                            success = accountServices.TransferBetweenAccounts(parsedAccountCode.Value, parsedAccountCodeRecipient.Value, parsedMonetarylAmount.Value);
                            if (success)
                            {
                                transactionSuccess = transactionServices.ProcessTransaction(parsedAccountCode.Value, parsedAccountCodeRecipient.Value, parsedMonetarylAmount.Value, accountInteractionType);
                                generalServices.DisplayMessage(MessageType.Success, $"Transfer successful, you sent {parsedMonetarylAmount} to {parsedAccountCodeRecipient} ");
                                if (transactionSuccess)
                                    generalServices.DisplayMessage(MessageType.Success, "Transaction record successful.");
                                else
                                    generalServices.DisplayMessage(MessageType.Error, "Transaction record unsuccessful.");

                            }
                            else
                                generalServices.DisplayMessage(MessageType.Warning, "Transfer successful, Insufficient funds");
                            break;
                        default:
                            break;
                    }                
            
                }
                else
                    generalServices.DisplayMessage(MessageType.Error, "Account not found.");
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");
            }          
        }     

        public static void DisplayAccountDetails(AccountServices accountServices, GeneralServices generalServices)
        {
            try
            {
                var parsedAccountCode = generalServices.GatherNumericInput("Enter Account Code: ");

                if (parsedAccountCode != null)
                {
                    var account = accountServices.GetAccountDetails(parsedAccountCode.Value);

                    if (account.Id != -1)
                    {
                        generalServices.DisplayMessage(MessageType.MenuItem, $"Account Code: {account.AccountCode}");
                        generalServices.DisplayMessage(MessageType.MenuItem, $"Account Holder: {account.AccountName}");
                        generalServices.DisplayMessage(MessageType.MenuItem, $"Balance: {account.AccountBalance.ToString("0.00")}");
                    }
                    else
                    generalServices.DisplayMessage(MessageType.Error, "Account not found.");
                }
                else
                    generalServices.DisplayMessage(MessageType.Error, "Account Code not parseable.");
            }
            catch (Exception e)
            {
                generalServices.DisplayMessage(MessageType.Error, "Exception Hit.");                
            }
        }
    }
}
