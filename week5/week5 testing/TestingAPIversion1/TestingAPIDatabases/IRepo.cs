using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace TestingAPIDatabases
{
    public interface IRepo
    {
        List<Customer> GetCustomer(int custID);
        List<Customer> AllCustomers();
        string DropOneCustomer(int custID);

        string AddOneCustomer(Customer newCust);
    }
}
