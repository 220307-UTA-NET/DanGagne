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
            string guestName = "Guest";
            string userLocation = "Rochester";
            int custID=99;
            int storeID = 1;
            

            while (true)
            { 
                Console.Clear();
                //store1.ReadFromXml(custID); customer shopping cart.
                Console.WriteLine("Welcome to the Furniture store.");
                Console.WriteLine("Current location: " + userLocation+"\nCurrent user: "+guestName+"\n");
                Console.WriteLine("[1] - Create Account or Log In");
                Console.WriteLine("[2] - Search Customers");
                Console.WriteLine("[3] - Change Store");
                Console.WriteLine("[4] - Search Inventory");
                Console.WriteLine("[5] - View Shopping Cart");
                Console.WriteLine("[6] - Remove Item from Cart");
                Console.WriteLine("[7] - Update Customer Address");
                Console.WriteLine("[8] - View Customer Order History");
                Console.WriteLine("[9] - View Store Order History");
                Console.WriteLine("[0] - Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Please enter your username (first name) to login.");
                        Customer cust1=store.LogIn();
                        custID = cust1.GetCustID();
                        guestName=cust1.GetCustName();
                        storeID=cust1.GetStoreID();
                        if(storeID==1)
                            { userLocation = "Rochester"; }
                        else if(storeID==2)
                            { userLocation = "Buffalo"; }
                        else { userLocation = "Syracuse"; }
                        if(custID==99)
                        {
                            Console.WriteLine("No account found. Please create an account if you do not have one.");
                            store.AddCustomer(storeID);
                        }
                        else
                        {                          
                            Console.WriteLine("Welcome Back!");
                            Thread.Sleep(900);
                        }
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter the customer you wish to search for using their first OR last name.");
                        store.SearchCustomers();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Select your location. \n[1]-Rochester\n[2]-Buffalo\n[3]-Syracuse");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                userLocation = "Rochester";
                                storeID = 1;
                                break;
                            case "2":
                                userLocation = "Buffalo";
                                storeID = 2;
                                break;
                            case "3":
                                userLocation = "Syracuse";
                                storeID = 3;
                                break;
                            default:
                                Console.WriteLine("Invalid selection.");
                                break;
                        }
                        Console.WriteLine("Store set to: " + userLocation + ".\nPress Enter to return to the menu.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();                     
                        store.SearchStoreInventory(storeID, userLocation, shoppingCart);
                        break;
                    case "5":
                        Console.Clear();
                        shoppingCart.ViewCart();                    
                        Console.WriteLine("\nWould you like to check out now?");
                        Console.ReadLine();
                        Console.WriteLine(store.CheckOut(shoppingCart, storeID, custID));
                        Console.WriteLine("\nPress any key to return to the main menu.");
                        Console.ReadLine();
                        break;
                    case "6":
                        Console.Clear();
                        shoppingCart.ViewCart();
                        Console.WriteLine(shoppingCart.RemoveFromCart());
                        Thread.Sleep(900);
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine(store.UpdateAddress(custID));
                        Thread.Sleep(900);
                        break;
                    case "8":
                        Console.Clear();
                        Console.WriteLine(store.CustOrderHistory(custID));
                        Console.ReadLine();
                        break;
                    case "9":
                        Console.Clear();
                        Console.WriteLine(store.StoreOrderHistory(storeID));
                        Console.ReadLine();
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.  Please try again.");
                        break;

                }
            }
        }
    }
}