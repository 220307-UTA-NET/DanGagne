using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZero.Classes
{
    public class Customer
    {
        //Fields
        string fname { get; set; }
        string lname { get; set; }
        string address { get; set; }
        string city { get; set; }
        string state { get; set; }
        string country { get; set; }

        //Constructor

        public Customer() { }
        public Customer(string fname, string lname)
        {
            this.fname = fname;
            this.lname = lname;
        }



    }
}
