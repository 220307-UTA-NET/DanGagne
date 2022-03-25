using System;
using System.Xml.Serialization;

namespace BankAccounts
{
    //template for class, all methods designed to be overriden
    class Accounts
    {
        //Fields

        private static int accountSeed=111111111;
        public string? accountName;
        protected int accountNumber;
        protected double accountBalance
        {
            get
            {
                double balance = 0;
                foreach (var item in allTransations)
                {
                    balance+=item.Amount;
                }
                return balance;
            }
        }
        protected double interestRate;
        private List<Transaction> allTransations = new List<Transaction>();
        public XmlSerializer Serializer{get;}=new( typeof(List<Transaction>));

        //Constructor
        protected Accounts( )
        {
           this.accountNumber=accountSeed; 
           accountSeed++;
           this.ReadFromXml();
        }

        //Methods

        public int GetAccountNumber()
        {
            return this.accountNumber;
        }

        public void GetInfo()
        {
            Console.WriteLine("Account Number: " +this.GetAccountNumber());
            Console.WriteLine("Current Balance: " +this.accountBalance);
            Console.WriteLine("Current interest rate: "+this.GetInterestRate());
        }

        public double GetInterestRate()
        {
            return this.interestRate;
        }

        public void Withdrawal (double amount, string note = "")
        {
            if(amount<0)
            {
                Console.WriteLine("Withdrawals must be a positive value.");
                Console.WriteLine("Transaction Cancelled.");
                Console.WriteLine("Your balance is: "+accountBalance);
                return;
            }
            else if ((this.accountBalance-amount)<0)
            {
                Console.WriteLine("Balance cannot be negative.");
                Console.WriteLine("Transaction Cancelled.");
                Console.WriteLine("Your balance is: "+accountBalance);
                return;
            }
            else
            {
                var withdrawal = new Transaction(-amount, DateTime.Now, note);
                allTransations.Add(withdrawal);
                Console.WriteLine("Withdrawal of " +amount+". Your balance is: "+accountBalance);
            }
            
        }

        public void Deposit ( double amount, string note="")
        {
             if(amount<0)
            {
                Console.WriteLine("Deposits must be a positive value.");
                Console.WriteLine("Transaction Cancelled.");
                Console.WriteLine("Your balance is: "+accountBalance);
                return;
            }
            else
            {
                var deposit = new Transaction(amount, DateTime.Now, note);
                allTransations.Add(deposit);
                //this.Record(deposit);
                Console.WriteLine("Deposit of " +amount+". Your balance is: "+accountBalance);
                
            }
            
        }
        
        protected void Record(double transactionAmount)
        {
            string path = $@".\TransactionHistory.txt";
            string currentTime =DateTime.Now.ToString("MM/dd/yyy HH:mm:ss");
            string [] content = new string [] {currentTime+ "\t" +this.accountNumber + "\t" +this.accountName+ "\t" +transactionAmount+ "\t" +this.accountBalance};

 
            if( !File.Exists(path))
            {
                //create and write to
                File.WriteAllLines(path,content);
            }
            else
            {
                //append file
                File.AppendAllLines(path,content);
            }

        }

        public void SerializeAsXml()
        {
            var newStringWriter = new StringWriter();
            Serializer.Serialize(newStringWriter, allTransations);
            newStringWriter.Close();

            string path =$@".\Transactions-{this.accountNumber}.xml";
            File.WriteAllText(path, newStringWriter.ToString());

        }
    
        public void ReadFromXml()
        {
            try
            {
                using StreamReader reader = new( $@".\Transactions-{this.accountNumber}.xml");
                var records = (List<Transaction>?)Serializer.Deserialize(reader);
                reader.Dispose();

                if (records is null) throw new InvalidDataException();
                this.allTransations=records;
            }
            catch (Exception e)
            {
                return;
            }
        }
    
    }
}