using System;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("BankAccounts.Test")]

namespace BankAccounts.App
{
    //template for class, all methods designed to be overriden
    internal class SavingsAccount : Accounts
    {
        //Fields
        //declared in Accounts class

        //Constructors
        public SavingsAccount( string accountName, double accountBalance)
        {
            this.accountName=accountName;
            this.Deposit(accountBalance, "First Deposit");
            this.interestRate=0.003;
        }

        //Methods

        // public override void Withdrawal (double withdrawal)
        // {
        //     if(withdrawal<0)
        //     {
        //         Console.WriteLine("Withdrawals must be a positive value.");
        //         Console.WriteLine("Transaction Cancelled.");
        //         Console.WriteLine("Your balance is: "+accountBalance);
        //         return;
        //     }
        //     else if ((this.accountBalance-withdrawal)<0)
        //     {
        //         Console.WriteLine("Balance cannot be negative.");
        //         Console.WriteLine("Transaction Cancelled.");
        //         Console.WriteLine("Your balance is: "+accountBalance);
        //         return;
        //     }
        //     else
        //     {
        //         this.accountBalance-=withdrawal;
        //         this.Record(-withdrawal);
        //         Console.WriteLine("Your balance is: "+accountBalance);
        //     }
            
        // }

        // public override void Deposit (double deposit)
        // {
        //      if(deposit<0)
        //     {
        //         Console.WriteLine("Deposits must be a positive value.");
        //         Console.WriteLine("Transaction Cancelled.");
        //         Console.WriteLine("Your balance is: "+accountBalance);
        //         return;
        //     }
        //     else
        //     {
        //         this.accountBalance+=deposit;
        //         this.Record(deposit);
        //         Console.WriteLine("Your balance is: "+accountBalance);
        //     }
            
        // }
    
        public void AccumulateInterest()
        {
            this.Deposit(Math.Round((this.accountBalance*this.interestRate), 2), "Interest Payment");
            Console.WriteLine("^ Interest Payment");
        }    
    }
}