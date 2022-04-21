using System;
using Xunit;
using BankAccounts.App;


namespace BankAccounts.Test
{

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //ARRANGE-any set up that is required to perform the test
            //ACT-where we invoke the behavior to test
            //ASSERT-compare the result of the ACT to an expected value
            Assert.Equal(true, true);
            //Assert.Equal(what we expect to get back, what we actually got back)
        }

        //typical naming condition
        //UnitOfTest_TestCondition_CorrectBehavior
        [Fact]
        public void SavingAccount_CreatSavingsAcount_ValidAcount()
        {
            //ARRANGE
            string expected = "testAccount";
            double savingsAccountBalance = 500.5;
            
            //ACT
            var account = new SavingsAccount("testAccount",500.50);
            double actualAccountBalance = account.accountBalance;

            //ASSERT
            Assert.Equal(account.accountName, expected);
            Assert.Equal(savingsAccountBalance, actualAccountBalance);

        }

        [Fact]
        public void Transaction_CreateTransaction_ValidTransaction()
        {
            //ARRANGE
            DateTime time = DateTime.Now;
            //ACT
            Transaction tran = new Transaction(1500.50, time, "TestDeposit");
            //ASSERT
            Assert.Equal(tran.Date, time);
        }

        [Fact]
        public void Account_GetInterestRate_ReturnInterestRate()
        {
            //ARRANGE
            SavingsAccount saving = new SavingsAccount("Test", 10000);
            double expectedInterestRate = 0.003;

            //ACT
            double actual = saving.GetInterestRate();

            //ASSERT
            Assert.Equal(expectedInterestRate, actual);
        }

        [Fact]
        public void Account_InvalidWithdrawal_allTransactionsLengthUnchanged()
        {
            //ARRANGE
            SavingsAccount savings = new SavingsAccount("Test", 10000);
            int expectedAllTransactionsCount = 1;

            //ACT
            savings.Withdrawal(15000);
            int actualAllTransactionCount = savings.allTransactions.Count;

            //Assert
            Assert.Equal(expectedAllTransactionsCount, actualAllTransactionCount);
        }

        [Fact]
        public void DummyTest_CharNotInString()
        {
            //This test is to experiment with the Assert.DoesNotContain() method

            //ARRANGE
            var expected = "&";
            string actual = "This string does not contain an ampersand";
            
            //ACT

            //Assert
            Assert.DoesNotContain(expected, actual);
        }
    }
}