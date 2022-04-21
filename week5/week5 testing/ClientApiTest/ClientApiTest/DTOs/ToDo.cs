using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApiTest.DTOs
{

    //DTOs (Data Transfer Object)
    //aren't meant to model a behavior
    //just the values of an object

    //When we start to use a client application to communicate with a server.
    //the server cannot use Console.Read or Console.Write, so all User Interaction 
    //must come thorugh th console application (the client application)
    public class ToDo
    {
        //Fields
        public string fname { get; set; }
        public string lname { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public int custID { get; set; }
        public int storeID { get; set; }


        public ToDo(string fname, string lname, string address, string city, string state, string country, int storeID)
        {
            this.fname = fname;
            this.lname = lname;
            this.address = address;
            this.city = city;
            this.state = state;
            this.country=country;
            this.storeID = storeID;
        }
        public string Introduce()
        {
            return $"Hi my name is {this.fname} {this.lname}\n";
        }
    }
}
