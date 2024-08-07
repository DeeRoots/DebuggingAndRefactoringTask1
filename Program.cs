using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.ServiceLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using static DebuggingAndRefactoringTask1.Enums.Enums;
using static DebuggingAndRefactoringTask1.Repository.AccountRepository;

namespace BankingSystem
{
   
    class Program
    {           
        static void Main(string[] args)
        {
            var accountServices = new AccountServices();
            var generalServices = new GeneralServices();

            while (true)
            {
                RunApplication(accountServices, generalServices);
            }
        } 
        static void RunApplication(AccountServices accountServices, GeneralServices generalServices)
        {
            var choiceParsed = generalServices.DisplayMenu();
            

            switch (choiceParsed)
            {
                case 1:
                    AddAccount(accountServices, generalServices);
                    break;
                case 2:
                    MonetaryInteraction(AccountInteractionType.Deposit, accountServices, generalServices);
                    break;
                case 3:
                    MonetaryInteraction(AccountInteractionType.Withdraw, accountServices, generalServices);
                    break;
                case 4:
                    DisplayAccountDetails(accountServices, generalServices);
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }

        static void AddAccount(AccountServices accountServices, GeneralServices generalServices)
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
                            generalServices.DisplayMessage(MessageType.Error, $"Account : {accountCodeParsed} has NOT been added. Account code alreay exists.");
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

        static void MonetaryInteraction(AccountInteractionType accountInteractionType, AccountServices accountServices, GeneralServices generalServices)
        {
            try
            {
                var parsedAccountCode = generalServices.GatherNumericInput("Enter Account Code: ");

                var parsedMonetarylAmount = accountInteractionType == AccountInteractionType.Deposit ? generalServices.GatherDoublelInput("Enter Amount to Deposit: ") : generalServices.GatherDoublelInput("Enter Amount to Withdraw: ");

                if (parsedMonetarylAmount <= 0)
                {
                    generalServices.DisplayMessage(MessageType.Warning, "Deposit or Withdrawal cannot be equal to or less than 0.00 or 0");
                    return;
                }

                var success = false;
                if (parsedAccountCode != null && parsedMonetarylAmount != null)
                {
                    switch (accountInteractionType)
                    {
                        case AccountInteractionType.Deposit:
       
                            success = accountServices.DepositAccount(parsedAccountCode.Value, parsedMonetarylAmount.Value);
                            if (success)
                                generalServices.DisplayMessage(MessageType.Success, "Deposit successful.");
                            else
                                generalServices.DisplayMessage(MessageType.Warning, "Deposit unsuccessful.");
                            break;
                        case AccountInteractionType.Withdraw:
                            success = accountServices.WithdrawAccount(parsedAccountCode.Value, parsedMonetarylAmount.Value);
                            if (success)
                                generalServices.DisplayMessage(MessageType.Success, "Withdrawal successful.");
                            else
                                generalServices.DisplayMessage(MessageType.Warning, "Withdrawal unsuccessful. Insufficient");
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

        static void DisplayAccountDetails(AccountServices accountServices, GeneralServices generalServices)
        {
            try
            {
                var parsedAccountCode = generalServices.GatherNumericInput("Enter Account Code: ");

                if (parsedAccountCode != null)
                {
                    var account = accountServices.GetAccount(parsedAccountCode.Value);

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
