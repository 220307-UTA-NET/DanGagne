using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PZero.Classes;
[assembly: InternalsVisibleTo("PZero.Test")]

namespace PZero.Database
{
    public interface IRepo
    {
        Customer AddNewCustomer(string fname, string lname, int storeID);

        IEnumerable<Customer> SearchCustomers(string inputname);
        IEnumerable<Item> SearchInventory(string inputname, int storeID);
        Item FindOneItem(int sku, int storeID);
        int DeleteItem(Item item, int storeID );
        int CheckQuantity(Item item, int storeID);
        Customer CustomerLogin(string username, string password);
        Customer CustomerLogin(int custID);
        void AddToOrderHistory(Item item, int storeID, int custID, string purchasedate);
        int UpdateCustomerAddress(string address, string city, string state, int custID);
        IEnumerable<Item>PopulateOrderHistory(int custID);

        string GetStoreName(int storeID);

        List <string> ListStoreNames();

        void SaveItemToCustCart(Item item, int custID);
        void UpdateItemInCart(Item item, int custID);
        bool CheckIfItemExistsInCart(Item item, int custID);
        IEnumerable<Item> GetCustCart(int custID);
    }
}
