using System;
using System.Collections;
using System.Collections.Generic;

namespace BankingSystem
{
    class Program
    {
        enum MessageType
        {
            Information = 0,
            Success =1,
            Warning = 2,
            Error = 3,
            MenuItem = 4,
            Input = 5

        }       
        enum AccountInteractionType 
        {
            Deposit = 0,
            Withdraw = 1
        }

        static List<Account> accounts = new List<Account>();

        static void Main(string[] args)
        {
            while (true)
            {
                RunApplication();
            }
        } 
        static void RunApplication()
        {
            DisplayMessage(MessageType.Information, $"Options:\n");
            DisplayMessage(MessageType.MenuItem, $"1. Add Account");
            DisplayMessage(MessageType.MenuItem, $"2. Deposit Money");
            DisplayMessage(MessageType.MenuItem, $"3. Withdraw Money");
            DisplayMessage(MessageType.MenuItem, $"4. Display Account Details");
            DisplayMessage(MessageType.MenuItem, $"5. Exit \n");

            var choiceParsed = GatherNumericInput($"Choice: ");

            switch (choiceParsed)
            {
                case 1:
                    AddAccount();
                    break;
                case 2:
                    MonetaryInteraction(AccountInteractionType.Deposit);
                    break;
                case 3:
                    MonetaryInteraction(AccountInteractionType.Withdraw);
                    break;
                case 4:
                    DisplayAccountDetails();
                    break;
                case 5:
                    break;
                default:
                    break;
            }
        }

        static void AddAccount()
        {
            try
            {
                var accountCodeParsed = GatherNumericInput("Enter Account Code: ");

                if (accountCodeParsed != null)
                {
                    var name = GatherTextualInput("Enter Account Holder Name: ");

                    if (name != null)
                    {
                        Account account = new Account { Id = accounts.Count + 1, AccountCode = accountCodeParsed.Value, AccountName = name.Trim(), AccountBalance = 0 };
                        accounts.Add(account);

                        DisplayMessage(MessageType.Success, "Account added successfully.");
                    }
                }
                else
                {
                    DisplayMessage(MessageType.Error, "Account not added - Please try again");
                }
            }
            catch (Exception e)
            {
                DisplayMessage(MessageType.Error, "Exception Hit.");
            }          
        }

        static void MonetaryInteraction(AccountInteractionType accountInteractionType)
        {
            try
            {
                var parsedAccountCode = GatherNumericInput("Enter Account Code: ");

                var parsedMonetarylAmount = accountInteractionType == AccountInteractionType.Deposit ? GatherDoublelInput("Enter Amount to Deposit: ") : GatherDoublelInput("Enter Amount to Withdraw: ");

                if (parsedAccountCode != null && parsedMonetarylAmount != null)
                {
                    foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                    {
                        var originalBalance = account.AccountBalance;
                        account.AccountBalance = accountInteractionType == AccountInteractionType.Deposit ? account.AccountBalance += parsedMonetarylAmount.Value : account.AccountBalance >= parsedMonetarylAmount ? account.AccountBalance -= parsedMonetarylAmount.Value : originalBalance;

                        if (accountInteractionType == AccountInteractionType.Deposit)
                            DisplayMessage(MessageType.Success, "Deposit successful.");
                        else
                        {
                            if (account.AccountBalance == originalBalance)
                                DisplayMessage(MessageType.Warning, "Insufficient balance.");
                            else
                                DisplayMessage(MessageType.Success, "Withdrawal successful.");
                        }
                    }
                }
                else
                    DisplayMessage(MessageType.Error, "Account not found.");
            }
            catch (Exception e)
            {
                DisplayMessage(MessageType.Error, "Exception Hit.");
            }
          
        }

      

        static void DisplayAccountDetails()
        {
            try
            {
                var parsedAccountCode = GatherNumericInput("Enter Account Code: ");

                if (parsedAccountCode != null)
                {
                    foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                    {
                        DisplayMessage(MessageType.MenuItem, $"Account ID (Should be hidden): {account.Id}");
                        DisplayMessage(MessageType.MenuItem, $"Account Code: {account.AccountCode}");
                        DisplayMessage(MessageType.MenuItem, $"Account Holder: {account.AccountName}");
                        DisplayMessage(MessageType.MenuItem, $"Balance: {account.AccountBalance}");

                        return;
                    }
                    DisplayMessage(MessageType.Error, "Account not found.");
                }
                else
                    DisplayMessage(MessageType.Error, "Account not found.");
            }
            catch (Exception e)
            {
                DisplayMessage(MessageType.Error, "Exception Hit.");                
            }
        }


        #region Input
        static string? GatherTextualInput(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
            var rawInput = Console.ReadLine();

            if (string.IsNullOrEmpty(rawInput))
            {
                DisplayMessage(MessageType.Error, "Please provide input");
                return null;
            }
            else
            {
                return rawInput;
            }
        }
        static int? GatherNumericInput(string message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.White;
                var resultParse = int.TryParse(Console.ReadLine(), out int result);
                Console.Write(Environment.NewLine);
                if (resultParse && result <= int.MaxValue && result >= int.MinValue)
                {
                    var numericInput = result;
                    return numericInput;
                }
                else
                {
                    DisplayMessage(MessageType.Error, "Value MUST be numeric.");
                }
                return null;
            }
            catch (Exception e)
            {
                DisplayMessage(MessageType.Error, "Exception Hit.");
                return null;
            }        
        }
        static double? GatherDoublelInput(string message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.White;
                var resultParse = double.TryParse(Console.ReadLine(), out double result);
                if (resultParse && result <= double.MaxValue && result >= double.MinValue)
                {
                    var returnValue = result;
                    if (returnValue == 0.00)
                    {
                        DisplayMessage(MessageType.Error, "Value MUST include decimal place and numbers only.");
                        return null;
                    }
                    else
                        return returnValue;
                }
                else
                {
                    DisplayMessage(MessageType.Error, "Value MUST include decimal place and numbers only.");
                }
                return null;
            }
            catch (Exception e)
            {
                DisplayMessage(MessageType.Error, "Exception Hit.");
                return null;
            }        
        }
        #endregion

        #region Messages
        static void DisplayMessage(MessageType type, string message)
        {
            switch (type)
            {
                case MessageType.Information:
                    DisplayInformationMessage(message);
                    break;
                case MessageType.Success:
                    DisplaySuccessMessage(message);
                    break;
                case MessageType.Warning:
                    DisplayWarningMessage(message);
                    break;
                case MessageType.Error:
                    DisplayErrorMessage(message);
                    break;
                case MessageType.MenuItem:
                    DisplayMenuMessage(message);
                    break;
                case MessageType.Input:
                    DisplayInputMessage(message);
                    break;

                default:
                    break;
            }
        }     
        static void DisplayInformationMessage(string message) 
        {            
            Console.WriteLine(message);
            Console.WriteLine("");
        }
        static void DisplayWarningMessage(string message) 
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DisplayErrorMessage(string message) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DisplaySuccessMessage(string message) 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DisplayMenuMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void DisplayInputMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }

    class AccountRepositoryLayer
    { 
    }

    class Account
    {
        public int Id { get; set; }
        public int AccountCode {get; set; }
        public string AccountName { get; set; }
        public double AccountBalance { get; set; }
    }
}
