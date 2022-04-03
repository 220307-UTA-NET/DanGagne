using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using PZero.App;
using PZero.Classes;

namespace PZero.Database
{
    public interface IRepo
    {
        void AddNewCustomer(string fname, string lname, int storeID);

        IEnumerable<Customer> SearchCustomers(string inputname);
        IEnumerable<Item> SearchInventory(string inputname, int storeID);
        Item FindOneItem(int sku, int storeID);
        void DeleteItem(Item item, int storeID );
        int CheckQuantity(Item item, int storeID);
        Customer CustomerLogin(string username, string password);
        void AddToOrderHistory(Item item, int storeID, int custID);
        void UpdateCustomerAddress(string address, string city, string state, int custID);
        IEnumerable<Item>PopulateOrderHistory(int custID);
    }
}
