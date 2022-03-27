using System;
using System.Xml.Serialization;

namespace ProjectZero
{
    public class Customer
    {
        //Fields
        //private static int customerSeed=1;
        [XmlAttribute]
        public string? fname{get; set;}
        [XmlAttribute]
        public string? lname{get; set;}
        [XmlAttribute]
        public int customerId { get; set;}
        [XmlAttribute]
        public string? customerLocation { get; set;}
        string? address{get; set;}

        //string[] orderHistory;
        
        //Constructor

        public Customer(){}
        public Customer(string fname, string lname, string customerLocation)
        {
            this.fname=fname;
            this.lname=lname;
            this.customerLocation=customerLocation;
            this.customerId=customerId;
            this.address=address;

        }

        //Methods

        public void updateOrderHistory()
        {}
    }
}