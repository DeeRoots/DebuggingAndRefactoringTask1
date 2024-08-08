# 1. Debugging and Refactoring

I was able to complete the task + bonus tasks + other QoL improvements.

I was unable to have the unit test project within the solution folder - it caused many reference errors
I was unable to have the unit test project within source control whilst the project was outside of the solution folder - however it works here.

What I have done is zip the Unit Test Project. If you can unzip it one layer up from the solution folder that is where I have had 30/30 tests succeeded.

If push comes to shove I have added the code at the bottom of this readme file.
If you add an nUnitTest project - add a reference from the test project to the banking tool - and add the code into the generated .cs file it will work.

My assumptions are

Everything Money based should be positive transactions from a user perspective.
AccountCodes should be numeric.
Account Id's should not be shown to the users.
Account Name should be limited to 15 characters.

Whilst using the repository - service - front end architecture - the repository is still a List<> and will not persist.

Transaction logs should be accessible by anyone using the system.

AccountName should be editable.

Account should be able to be deleted.


To run the application - download the code,open the solution in Visual Studio and hit F5.

To run the Unit Tests - After following instructions above - right click the UnitTest project in Solution Explorer navigate to 'Run Tests' and left click.
There are  30 tests which all pass.



TEST CODE-------------------------------------------------------------------------------------------------------------------------------------------------


using DebuggingAndRefactoringTask1.Models;
using DebuggingAndRefactoringTask1.ServiceLayer;
using static DebuggingAndRefactoringTask1.Enums.Enums;
using static DebuggingAndRefactoringTask1.Repository.AccountRepository;
using static DebuggingAndRefactoringTask1.Repository.TransactionRepository;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            accounts.Add(new Account { Id = 1, AccountName = "Mr Shaun", AccountCode = 1, AccountBalance = 0.00 });
            accounts.Add(new Account { Id = 2, AccountName = "Mr Scott", AccountCode = 2, AccountBalance = 0.00 });
            accounts.Add(new Account { Id = 99, AccountName = "Mr Ryan (delete)", AccountCode = 99, AccountBalance = 0.00 });

            transactions.Add(new AccountTransaction { Id = 1, AccountCode = 1, AccountCodeTo = 1, Amount = 10, TranactionType = AccountInteractionType.Deposit,TransactionDateTime = DateTime.Now });
            transactions.Add(new AccountTransaction { Id = 1, AccountCode = 1, AccountCodeTo = 1, Amount = 50, TranactionType = AccountInteractionType.Withdraw, TransactionDateTime = DateTime.Now });
            transactions.Add(new AccountTransaction { Id = 1, AccountCode = 1, AccountCodeTo = 2, Amount = 16.50, TranactionType = AccountInteractionType.Tranfer, TransactionDateTime = DateTime.Now });
        }

        #region Delete Account
        [Test]
        public void DeleteAccount_Valid()
        {
            var originalAccountCount = accounts.Count;

            var accountServices = new AccountServices();
            accountServices.ProcessDeleteAccount(99);

            var newAccountCount = accounts.Count;

            if (newAccountCount < originalAccountCount)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void DeleteAccount_Invalid()
        {
            var originalAccountCount = accounts.Count;

            var accountServices = new AccountServices();
            accountServices.ProcessDeleteAccount(6);

            var newAccountCount = accounts.Count;

            if (originalAccountCount == newAccountCount)
                Assert.Pass();
            else
                Assert.Fail();
        }

        #endregion

        #region Add Account 
        [Test]
        public void AddAccount_Valid()
        {
            var originalAccountCount = accounts.Count;

            var accountServices = new AccountServices();
            accountServices.ProcessAccount(4, "Mr Paul");

            var newAccountCount = accounts.Count;

            if (originalAccountCount < newAccountCount) 
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void AddAccount_Invalid()
        {
            var originalAccountCount = accounts.Count;

            var accountServices = new AccountServices();
            accountServices.ProcessAccount(1, "Mr Paul");

            var newAccountCount = accounts.Count;

            if (originalAccountCount == newAccountCount)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Edit Account
        [Test]
        public void EditAccount_Valid()
        {
            var originalAccountName = accounts[0].AccountName;

            var accountServices = new AccountServices();
            accountServices.ProcessEditAccountName(1, "Mr Ryan");

            var newAccountName = accounts[0].AccountName;

            if (originalAccountName != newAccountName)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void EditAccount_Invalid_Exists()
        {
            var originalAccountName = accounts[0].AccountName;

            var accountServices = new AccountServices();
            accountServices.ProcessAccount(1, "Intentionally large string to fail validation");

            var newAccountName = accounts[0].AccountName;

            if (originalAccountName == newAccountName)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Monetary Interaction Deposit
        [Test]
        public void DepositAccount_Valid()
        {
            var originalAccountBalance = accounts[0].AccountBalance;

            var accountServices = new AccountServices();
            accountServices.DepositAccount(1, 10);

            var newAccountBalance = accounts[0].AccountBalance;

            if (newAccountBalance == originalAccountBalance + 10)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void DepositAccount_Invalid()
        {
            var originalAccountBalance = accounts[0].AccountBalance;

            var accountServices = new AccountServices();
            accountServices.DepositAccount(1, 0.00);

            var newAccountBalance = accounts[0].AccountBalance;

            if (originalAccountBalance == newAccountBalance)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Monetary Interaction Withdraw
        [Test]
        public void WithdrawAccount_Valid()
        {
            var accountServices = new AccountServices();

            accounts[0].AccountBalance = 20;
            var originalAccountBalance = accounts[0].AccountBalance;

            accountServices.WithdrawAccount(1, 10);

            var newAccountBalance = accounts[0].AccountBalance;

            if (newAccountBalance == originalAccountBalance - 10)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void WithdrawAccount_Invalid()
        {          
            var accountServices = new AccountServices();

            accounts[0].AccountBalance = 1.00;
            var originalAccountBalance = accounts[0].AccountBalance;

            accountServices.WithdrawAccount(1, 2.00);

            var newAccountBalance = accounts[0].AccountBalance;

            if (originalAccountBalance == newAccountBalance)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Monetary Interaction Transfer
        [Test]
        public void TransferAccount_Valid()
        {
            var accountServices = new AccountServices();

            accounts[0].AccountBalance = 100;
            accounts[1].AccountBalance = 0;

            var originalAccountBalance = accounts[0].AccountBalance;
            var originalAccountToBalance = accounts[1].AccountBalance;

            accountServices.TransferBetweenAccounts(1,2, 50);

            var newAccountBalance = accounts[0].AccountBalance;

            var newAccountToBalance = accounts[1].AccountBalance;

            if (newAccountBalance == 50 && newAccountToBalance == 50)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void TransferAccount_Invalid()
        {
            var accountServices = new AccountServices();

            accounts[0].AccountBalance = 50.00;
            accounts[1].AccountBalance = 0.00;
            var originalAccountBalance = accounts[0].AccountBalance;
            var originalAccountToBalance = accounts[1].AccountBalance;

            accountServices.TransferBetweenAccounts(1, 2, 51.00);

            var newAccountBalance = accounts[0].AccountBalance;
            var newAccountToBalance = accounts[1].AccountBalance;

            if (newAccountBalance == originalAccountBalance)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region GetAccount
        [Test]
        public void GetAccount_Valid()
        {
            var accountServices = new AccountServices();

            var account = accountServices.GetAccountDetails(1);

            if (account.AccountCode != -1)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GetAccount_Invalid()
        {
            var accountServices = new AccountServices();

            var account = accountServices.GetAccountDetails(78);

            if (account.Id == -1)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Account Exists
        [Test]
        public void AccountExists_Valid()
        {
            var accountServices = new AccountServices();

            var exists = accountServices.DoesAccountCodeExist(1);

            if (exists)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void AccountExists_Invalid()
        {
            var accountServices = new AccountServices();

            var exists = accountServices.DoesAccountCodeExist(78);

            if (!exists)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

        #region Transactions
        [Test]
        public void ProcessTransaction_Valid()
        {
            var transactionServices = new TransactionServices();

            var success = transactionServices.ProcessTransaction(1, 1, 10, AccountInteractionType.Withdraw);

            if (success)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void DisplayTransaction_Valid()
        {
            var transactionServices = new TransactionServices();

            var success = transactionServices.DisplayAccountTransactionHistoryDetails(1);

            if (success != string.Empty)
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void DisplayTransaction_Invalid()
        {
            var transactionServices = new TransactionServices();

            var success = transactionServices.DisplayAccountTransactionHistoryDetails(75);

            if (success == string.Empty)
                Assert.Pass();
            else
                Assert.Fail();
        }

        #endregion

        #region General Services
        [Test]
        public void GatherTextualInput_Valid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("Test"));
            var success = generalServices.GatherTextualInput("");

            if (success == "Test")
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GatherTextualInput_Valid_Numeric()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("15"));
            var success = generalServices.GatherTextualInput("");
            
            if (success == "15")
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void GatherTextualInput_Invalid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("Intentionally long string to fail validation"));
            var success = generalServices.GatherTextualInput("");
            

            if (success == null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GatherNumericInput_Valid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("1"));
            var success = generalServices.GatherNumericInput("");

            if (success == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }
               
        [Test]
        public void GatherNumericInput_Invalid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("df"));
            var success = generalServices.GatherNumericInput("");


            if (success == null)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GatherDecimalInput_Valid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("1"));
            var success = generalServices.GatherDoublelInput("");

            if (success == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GatherDoubleInput_Valid_decimal_places()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("1.00"));
            var success = generalServices.GatherDoublelInput("");


            if (success == 1.00)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void GatherDoubleInput_Invalid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("fg"));
            var success = generalServices.GatherDoublelInput("");


            if (success == null)
                Assert.Pass();
            else
                Assert.Fail();
        }


        [Test]
        public void GatherMenuInput_Valid()
        {
            var generalServices = new GeneralServices();
            Console.SetIn(new StringReader("1"));
            var success = generalServices.DisplayMenu();


            if (success == 1)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void IsNumeric_Valid()
        {
            var generalServices = new GeneralServices();            
            var success = generalServices.IsNumeric("1");

            if (success)
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void IsNumeric_Invalid()
        {
            var generalServices = new GeneralServices();
            var success = generalServices.IsNumeric("fg");

            if (!success)
                Assert.Pass();
            else
                Assert.Fail();
        }
        #endregion

    }
}