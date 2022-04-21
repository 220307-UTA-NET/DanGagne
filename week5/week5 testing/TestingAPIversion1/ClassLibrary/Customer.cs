using System.Text.Json.Serialization;

namespace ClassLibrary
{
    public class Customer
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }

        [System.ComponentModel.DataAnnotations.Key]
        public int custID { get; set; }
        public int storeID { get; set; }

        public Customer () { }

        public Customer (string fname, string lname, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.storeID = storeID;
        }

        public Customer(string fname, string lname, string address, string city, string state, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.address = address;
            this.city = city;
            this.state = state;
            this.storeID = storeID;
        }

        public Customer(string fname, string lname, string address, string city, string state, string country, int custID, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.address = address;
            this.city = city;
            this.state = state;
            this.country = country;
            this.custID = custID;
            this.storeID = storeID;
        }
    }
}