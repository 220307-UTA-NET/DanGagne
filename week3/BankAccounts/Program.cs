using System;

namespace BankAccounts
{
    class Program
    {
        static void Main()
            {
                SavingsAccount newSavingsAccount = new SavingsAccount("Personal Savings", 2500.75, 0.003);

                Console.WriteLine(newSavingsAccount.accountName);
                newSavingsAccount.GetInfo();

                newSavingsAccount.Withdrawal(1500, "First Withdrawal");
                newSavingsAccount.Deposit(500, "Second Deposit");
                newSavingsAccount.AccumulateInterest();
                newSavingsAccount.Deposit(250, "Third Deposit");
                newSavingsAccount.Deposit(500, "Fourth Deposit");
               

                CheckingAccount newCheckingAccount = new CheckingAccount("Personal Checking", 1000);             
                Console.WriteLine(newCheckingAccount.accountName);
                newCheckingAccount.GetInfo();
                newCheckingAccount.Deposit(250);
                newCheckingAccount.Deposit(500);

                newCheckingAccount.SerializeAsXml();
                newSavingsAccount.SerializeAsXml();
                
            }
    }
}
