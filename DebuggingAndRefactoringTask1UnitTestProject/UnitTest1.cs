using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Security.Principal;

namespace DebuggingAndRefactoringTask1UnitTestProject
{
    public class Tests
    {

        enum MessageType
        {
            Information = 0,
            Success = 1,
            Warning = 2,
            Error = 3,
            MenuItem = 4,
            Input = 5,
            BoundToFail = 6


        }
        enum InputType
        {
            Numeric = 0,
            Textual = 1,
        }
        enum AccountInteractionType
        {
            Deposit = 0,
            Withdraw = 1
        }
        static List<Account> accounts = new List<Account>();

        [SetUp]
        public void Setup()
        {
            accounts.Add(new Account { Id = 1, AccountCode = 1, AccountName = "Mr Test", AccountBalance = 1.50 }); 
        }

        [Test]
        public void AddAccount_Test()
        {
            var accountCodeParsed = 2;

            if (accountCodeParsed != null)
            {
                var name = "Mr Second Test";

                if (name != null)
                {
                    var accountCount = accounts.Count();
                    Account account = new Account { Id = accounts.Count + 1, AccountCode = accountCodeParsed, AccountName = name.Trim(), AccountBalance = 0 };
                    accounts.Add(account);

                    var accountCountAfterAdd = accounts.Count();

                    if (accountCountAfterAdd == accountCount + 1)
                        Assert.Pass("AddAccount() - Account Added Sucessfully");
                    else
                        Assert.Fail("AddAccount() - Account NOT Added Sucessfully");
                }
            }
            else
            {
                Assert.Fail("MonetaryInteraction() - pre-tested functions would have returned null");
            }
        }

        [Test]
        public void MonetaryInteraction_Test()
        {
            var accountInteractionType = AccountInteractionType.Deposit;

            var parsedAccountCode = 1;

            var parsedMonetarylAmount = 1.00;

            if (parsedAccountCode != null && parsedMonetarylAmount != null)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                {
                    var originalBalance = account.AccountBalance;
                    account.AccountBalance = accountInteractionType == AccountInteractionType.Deposit ? account.AccountBalance += parsedMonetarylAmount : account.AccountBalance >= parsedMonetarylAmount ? account.AccountBalance -= parsedMonetarylAmount : originalBalance;

                    if (accountInteractionType == AccountInteractionType.Deposit)
                        if (account.AccountBalance - parsedMonetarylAmount == originalBalance)
                            Assert.Pass("MonetaryInteraction() - Deposit addition checks successful");
                        else
                            Assert.Fail("MonetaryInteraction() - Deposit addition checks unsuccessful");

                    else
                    {
                        if (account.AccountBalance == originalBalance)
                            Assert.Fail("MonetaryInteraction() - Deposit should not get here");
                        else
                            Assert.Pass("MonetaryInteraction() - Deposit successful");
                    }
                }
            }
            else
                Assert.Fail("MonetaryInteraction() - pre-tested functions would have returned null");


            accountInteractionType = AccountInteractionType.Withdraw;
            parsedAccountCode = 1;
            parsedMonetarylAmount = 0.5;

            if (parsedAccountCode != null && parsedMonetarylAmount != null)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                {
                    var originalBalance = account.AccountBalance;
                    account.AccountBalance = accountInteractionType == AccountInteractionType.Deposit ? account.AccountBalance += parsedMonetarylAmount : account.AccountBalance >= parsedMonetarylAmount ? account.AccountBalance -= parsedMonetarylAmount : originalBalance;

                    if (accountInteractionType == AccountInteractionType.Deposit)
                        if (account.AccountBalance - parsedMonetarylAmount == originalBalance)
                            Assert.Fail("MonetaryInteraction() - Withdrawal should not he here");
                        else
                            Assert.Fail("MonetaryInteraction() - Withdrawal should not he here");

                    else
                    {
                        if (account.AccountBalance == originalBalance)
                            Assert.Fail("MonetaryInteraction() - In bounds Withdrawal should not get here");
                        else
                            Assert.Pass("MonetaryInteraction() - Withdrawal successful");
                    }
                }
            }
            else
                Assert.Fail("MonetaryInteraction() - pre-tested functions would have returned null");

            accountInteractionType = AccountInteractionType.Withdraw;
            parsedAccountCode = 1;
            parsedMonetarylAmount = 0.75;

            if (parsedAccountCode != null && parsedMonetarylAmount != null)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                {
                    var originalBalance = account.AccountBalance;
                    account.AccountBalance = accountInteractionType == AccountInteractionType.Deposit ? account.AccountBalance += parsedMonetarylAmount : account.AccountBalance >= parsedMonetarylAmount ? account.AccountBalance -= parsedMonetarylAmount : originalBalance;

                    if (accountInteractionType == AccountInteractionType.Deposit)
                        if (account.AccountBalance - parsedMonetarylAmount == originalBalance)
                            Assert.Fail("MonetaryInteraction() - Withdrawal should not he here");
                        else
                            Assert.Fail("MonetaryInteraction() - Withdrawal should not he here");

                    else
                    {
                        if (account.AccountBalance == originalBalance)
                            Assert.Pass("MonetaryInteraction() - Withdrawal should fail here insufficient funds");
                        else
                            Assert.Fail("MonetaryInteraction() - Withdrawal successful");
                    }
                }
            }
            else
                Assert.Fail("MonetaryInteraction() - pre-tested functions would have returned null");

        }
        [Test]
        public void DisplayAccountDetails_Test()
        {
            var parsedAccountCode = 1;

            if (parsedAccountCode != null)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                {
                    Assert.Pass("DisplayAccountDetails() - Account is be found.");

                    return;
                }
                Assert.Fail("DisplayAccountDetails() - Account Should be found.");
            }
            else
                Assert.Fail("DisplayAccountDetails() - Account Should be found because parsed acc code is null.");

            parsedAccountCode = 9999;

            if (parsedAccountCode != null)
            {
                foreach (var account in accounts.Where(a => a.AccountCode == parsedAccountCode))
                {
                    Assert.Fail("DisplayAccountDetails() - Account doesn't exist.");

                    return;
                }
                Assert.Pass("DisplayAccountDetails() - Account Should not be found.");
            }
            else
                Assert.Fail("DisplayAccountDetails() - Account Should be found because parsed acc code is null.");
        }
        [Test]
        public void GatherTextuallInput_Test()
        {
            var rawInput = "fg";

            if (string.IsNullOrEmpty(rawInput))
                Assert.Fail("GatherTextuallInput() - fg should not be null or empty");
            else
                Assert.Pass("GatherTextuallInput() - fg is not null or empty");

            rawInput = "123";

            if (string.IsNullOrEmpty(rawInput))
                Assert.Fail("GatherTextuallInput() - 123 should not be null or empty");
            else
                Assert.Pass("GatherTextuallInput() - 123 is not null or empty");

            rawInput = "<html> Boo </html>";

            if (string.IsNullOrEmpty(rawInput))
                Assert.Fail("GatherTextuallInput() - <html> Boo </html> should not be null or empty");
            else
                Assert.Pass("GatherTextuallInput() - <html> Boo </html> is not null or empty");

        }
        [Test]
        public void GatherNumericInput_Test()
        {
            var resultParse = int.TryParse("1", out int result);

            if (resultParse)
            {
                var numericInput = result;
                Assert.Pass("GatherNumericInput() - 1 has parsed");
            }
            else
                Assert.Fail("GatherNumericInput() - 1 has not parsed");


            resultParse = int.TryParse("1.30", out int result2);

            if (resultParse)
            {
                var numericInput = result2;
                Assert.Fail("GatherNumericInput() - 1.30 has parsed");
            }
            else
                Assert.Pass("GatherNumericInput() - 1.30 has not parsed");


            resultParse = int.TryParse("fg", out int result3);

            if (resultParse)
            {
                var numericInput = result3;
                Assert.Fail("GatherNumericInput() - fg has parsed");
            }
            else
                Assert.Pass("GatherNumericInput() - 1 has not parsed");



        }
        [Test]
        public void GatherDoublelInput_Test()
        {
            var resultParse = double.TryParse("1.00", out double result);

            if (resultParse)
            {
                var numericInput = result;
                Assert.Pass("GatherDoublelInput() - 1.00 has parsed");
            }
            else
                Assert.Fail("GatherDoublelInput() - 1.00 has not parsed");

            resultParse = double.TryParse("1", out double result2);

            if (resultParse)
            {
                var numericInput = result2;
                Assert.Pass("GatherDoublelInput() - 1 has parsed");
            }
            else
                Assert.Fail("GatherDoublelInput() - 1 has not parsed");

            resultParse = double.TryParse("fg", out double result3);

            if (resultParse)
            {
                var numericInput = result3;
                Assert.Fail("GatherDoublelInput() - fg has parsed");
            }
            else
                Assert.Pass("GatherDoublelInput() - fg has not parsed");

        }
        [Test]
        public void DisplayMessage_Test()
        {
            var messageType = MessageType.Information;

            switch (messageType)
            {
                case MessageType.Information:
                    Assert.Pass("DisplayMessage() - correct Message Selected");
                    break;
                case MessageType.Success:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Warning:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Error:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.MenuItem:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Input:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                default:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
            }

            messageType = MessageType.Error;
            switch (messageType)
            {
                case MessageType.Information:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Success:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Warning:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Error:
                    Assert.Pass("DisplayMessage() - correct Message Selected");
                    break;
                case MessageType.MenuItem:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Input:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                default:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
            }

            messageType = MessageType.BoundToFail;

            switch (messageType)
            {
                case MessageType.Information:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Success:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Warning:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Error:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.MenuItem:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                case MessageType.Input:
                    Assert.Fail("DisplayMessage() - incorrect Message Selected");
                    break;
                default:
                    Assert.Pass("DisplayMessage() - correct default case Selected");
                    break;
            }

        }

        class Account
        {
            public int Id { get; set; }
            public int AccountCode { get; set; }
            public string AccountName { get; set; }
            public double AccountBalance { get; set; }
        }
    }
}