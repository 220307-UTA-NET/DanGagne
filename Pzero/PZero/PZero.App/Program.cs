using System;
using PZero.Database;
using PZero.Classes;

namespace PZero.App
{
    public class Program
    {
        static void Main()
        {
            string connectString = File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");
            IRepo repo = new SqlRepo(connectString);
            Store store = new Store(repo);
            ShoppingCart shoppingCart = new ShoppingCart();          
            int custID = 99;
            int storeID = 1;
            store.MainMenu(custID, storeID, shoppingCart);  

        }
    }
}