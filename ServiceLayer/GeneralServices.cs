using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DebuggingAndRefactoringTask1.Enums.Enums;

namespace DebuggingAndRefactoringTask1.ServiceLayer
{
    public class GeneralServices
    {
        #region Input
        public  string? GatherTextualInput(string message)
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
        public  int? GatherNumericInput(string message)
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
        public double? GatherDoublelInput(string message)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(message);
                Console.ForegroundColor = ConsoleColor.White;
                var resultParse = double.TryParse(Console.ReadLine(), out double result);
                Console.Write(Environment.NewLine);
                if (resultParse && result <= double.MaxValue && result >= double.MinValue)
                {
                    var returnValue = result;
                    if (returnValue == 0.00)
                    {
                        DisplayMessage(MessageType.Error, "Value MUST not be  0.00.");
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

        public int? DisplayMenu() 
        {
            DisplayMessage(MessageType.Information, $"Options:\n");
            DisplayMessage(MessageType.MenuItem, $"1. Add Account");
            DisplayMessage(MessageType.MenuItem, $"2. Deposit Money");
            DisplayMessage(MessageType.MenuItem, $"3. Withdraw Money");
            DisplayMessage(MessageType.MenuItem, $"4. Display Account Details");
            DisplayMessage(MessageType.MenuItem, $"5. Exit \n");

            var choiceParsed = GatherNumericInput($"Choice: ");

            return choiceParsed;
        }

        public void DisplayMessage(MessageType type, string message)
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
        public static void DisplayInformationMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("");
        }
        public static void DisplayWarningMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplayErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplaySuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplayMenuMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplayInputMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion

        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
    }
}
