using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("PZero.Test")]

namespace PZero.Classes
{
    public class Customer
    {
        //Fields
        internal string? fname { get; set; }
        internal string? lname { get; set; }
        internal string? address { get; set; }
        internal string? city { get; set; }
        internal string? state { get; set; }
        internal string? country { get; set; }
        internal int storeID { get; set; }
        internal int custID { get; set; }
 
        //Constructor

        public Customer() { }
        public Customer(string fname, string address,int custID)
        { 
            this.fname = fname;
            this.address = address;
            this.custID = custID; 
        }

        public Customer(string fname, string lname, int custID, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.custID = custID;
            this.storeID = storeID;
        }
        public Customer(string fname, string lname, string address, int custID, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.custID = custID;
            this.storeID = storeID;
            this.address=address;
        }

        public Customer(string fname, string lname, string address, string city, string state, string country, int custID, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.address = address;
            this.city = city;
            this.state = state; 
            this.country = country;
            this.custID=custID;
            this.storeID=storeID;
        }

        //Method
        public string GetCustName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.fname} {this.lname}");
            return sb.ToString();
        }
        public int GetCustID()
        {
            return this.custID;
        }
        public int GetStoreID()
        {
            return this.storeID;
        }
        public string GetAddress()
        {
            if(this.address == "No Address on file.")
            {
                return this.address;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append($"\n{this.address}\n{this.city} {this.state}, {this.country}");
            if(sb.ToString()=="\n\n , ")
            {
                return "\nNo Address on file.";
            }
            return sb.ToString();

        }
        
    }
}
