using System.Xml.Serialization;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("BankAccounts.Test")]

namespace BankAccounts.App
{
    public class Transaction
    {
        //Fields, tagged as Xml attributes
        [XmlAttribute]
        public double Amount {get; set;}

        public DateTime Date {get; set;}
       // DateTime.Now.ToString("MM/dd/yyy HH:mm:ss")

        public string? Note {get; set;}

        //Constructor
        public Transaction(){}
        public Transaction(double Amount, DateTime Date, string Note="")
        {
            this.Amount=Amount;
            this.Date=Date;
            this.Note=Note;
        }
    }
}