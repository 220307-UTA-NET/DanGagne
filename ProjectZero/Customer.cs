using System.Xml.Serialization;

namespace ProjectZero
{
    public class Customer
    {
        //Fields
        [XmlAttribute]
        public string fname{get; set;}
        [XmlAttribute]
        public string lname{get; set;}
        [XmlAttribute]
        public string defaultStore{get; set;}
        string address{get; set;}

        //string[] orderHistory;
        
        //Constructor

        public Customer(){}
        public Customer(string fname, string lname, string defaultStore)
        {
            this.fname=fname;
            this.lname=lname;
            this.defaultStore=defaultStore;
            this.address=address;
        }

        //Methods

        public void updateOrderHistory()
        {}
    }
}