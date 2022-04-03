using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZero.Database;
using PZero.Classes;

namespace PZero.App
{
    internal class Store
    {
        IRepo repo;

        public Store(IRepo repo)
        {
            this.repo = repo;
        }

        //Methods
        public void AddCustomer(int storeID)
        {
            while (true)
            {
                Console.WriteLine("Please enter your first name.");
                string inputfname = Console.ReadLine();
                Console.WriteLine("Please enter your last name.");
                string inputlname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Is this the name you wish to create an account for? \n" + inputfname + " " + inputlname + "\nY/N");
                string confirmName=Console.ReadLine().ToUpper();
                if (confirmName == "Y" || confirmName == "YES")
                {
                    this.repo.AddNewCustomer(inputfname, inputlname, storeID);
                    Console.WriteLine("Account Successfully created!\nPress any key to return to the main menu.");
                    Console.ReadLine();
                    break;
                }
                else if (confirmName == "N" || confirmName == "NO")
                {
                    Console.Clear();
                    Console.WriteLine("Let's try again."); 
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Input not recognized.  Account creation cancelled.\nPress any key to return to the main menu.");
                    Console.ReadLine();
                    break;
                }
            }
                
        }

        public void SearchCustomers()
        {
            while(true)
                { 
                string inputname = Console.ReadLine();
                IEnumerable<Customer> custList = this.repo.SearchCustomers(inputname);
                Console.WriteLine(custList.Count() + " : Entries Found");
                foreach (Customer cust in custList)
                {
                    if (cust.GetAddress() == "\n\n , ")
                    {
                        Console.WriteLine("\n"+cust.GetCustName() + "\nNo address on record.");
                    }
                    else
                    {
                        Console.WriteLine("\n"+cust.GetCustName() + cust.GetAddress());
                    }
                }
                Console.WriteLine("\nSearch for someone else?\nY/N");
                string yesno = Console.ReadLine().ToUpper();
                if (yesno == "Y" || yesno == "YES")
                {
                    Console.Clear();
                    Console.WriteLine("OK, enter a new name to search for.");
                }
                else
                {
                    Console.Clear();
                    break;
                }
            }
            Console.WriteLine("Search Completed.\nReturning to the menu");
            Thread.Sleep(900);
        }

        public void SearchStoreInventory(int storeID, string userLocation, ShoppingCart shoppingCart)
        {
            while (true)
            {
                Console.WriteLine("Enter the item you wish to search for by name or material.");
                string inputname = Console.ReadLine();
                IEnumerable<Item> itemList = this.repo.SearchInventory(inputname, storeID);
                Console.Clear();
                Console.WriteLine(itemList.Count() + " : Entries Found at the " + userLocation + " branch.");
                foreach (Item i in itemList)
                {
                    Console.WriteLine(i.GetInfo());
                }

                Console.WriteLine("\nTo add an item to your cart enter the five digit SKU.\nTo search for something else press enter.");
                if (Int32.TryParse(Console.ReadLine(), out int skuAdd))
                {
                    Console.Clear();
                    shoppingCart.AddToCart(this.repo.FindOneItem(skuAdd, storeID));
                    Thread.Sleep(900);
                }              
                Console.WriteLine("\nContinue searching?\nY/N");
                string yesno = Console.ReadLine().ToUpper();
                if (yesno == "Y" || yesno == "YES")
                {
                    Console.Clear();                      
                }
                else
                {
                    Console.Clear();
                    break;
                }
              
            }
            Console.WriteLine("Search Completed.\nReturning to the main menu");
            Thread.Sleep(900);
        }
        
        public string CheckOut(ShoppingCart shoppingCart,int storeID,int custID)
        {
            
            if(custID==99)
            {
                Console.Clear();
                Customer cust=this.LogIn();
                custID = cust.GetCustID();
                if (custID == 99)
                { return "Invalid Username or password.\nOrder cannot be processed."; }
            }          
            List<Item> itemsInCart=shoppingCart.GetCart();
            foreach(Item i in itemsInCart)
            {
                int stockquantity=this.repo.CheckQuantity(i, storeID);
                if (stockquantity -i.GetQuantity() < 0)
                {
                    Console.Clear();
                    return "Not enough items in stock at this location.\nOrder cancelled.";
                }
            }
            Console.WriteLine("Your total will be: $"+shoppingCart.GetCartTotalPrice()+"\nEnter \'Confirm\' to place your order.");
            string confirmed =Console.ReadLine().ToUpper();
            if(confirmed != "CONFIRM")
            { 
                return "Order Cancelled.";
            }
            foreach(Item i in itemsInCart)
            {
                this.repo.DeleteItem(i, storeID);
                this.repo.AddToOrderHistory(i, storeID, custID);
            }
            Console.Clear();
            shoppingCart.EmptyCart();
            return "Transaction successfully completed!";

            
            //add all items to purchase database with customerID and storeID
        }
        
        public Customer LogIn()
        { 
                Console.Clear();
                Console.WriteLine("Enter your username(first name):");
                string username = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Enter your password(last name):");
                string password = Console.ReadLine();
                Console.Clear();
                return this.repo.CustomerLogin(username, password);
           
        }

        public string UpdateAddress(int custID)
        {
            if (custID == 99)
            {
                Console.Clear();
                if (this.LogIn().GetCustID() == 99) ;
                return "Invalid Username or password.\nRequest cannot be processed.";
            }
            while (true)
            {
                Console.WriteLine("Please enter your house or apartment number with the street.\nIE: 123 Sesame Street");
                string address = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Please enter your city\nIE: Brooklyn");
                string city = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Please enter the two character abbreviation for your state.\nIE: NY");
                string state = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Is this the correct address? \n{address}\n{city} {state}\nEnter[Y/N]");
                string confirmAddress = Console.ReadLine().ToUpper();
                if (confirmAddress == "Y" || confirmAddress == "YES")
                {
                    this.repo.UpdateCustomerAddress(address, city, state, custID);
                    Console.Clear();
                    return "Address Updated!";
                }
                else if (confirmAddress == "N" || confirmAddress == "NO")
                {
                    Console.Clear();
                    Console.WriteLine("Let's try again.");
                }
                else
                {
                    Console.Clear();
                    return "Input not recognized.  Account creation cancelled.\nPress any key to return to the main menu.";
                    
                }
                
            }
            
        }

        public string CustOrderHistory(int custID)
        {
            decimal totalSpent = 0;
            if (custID == 99)
            {
                Console.Clear();
                Customer cust = this.LogIn();
                custID = cust.GetCustID();
                if (custID == 99)
                { return "Invalid Username or password.\nRequest cannot be processed."; }
            }
            IEnumerable<Item> itemList = this.repo.PopulateOrderHistory(custID);
            foreach (Item i in itemList)
                {
                Console.WriteLine("\n"+i.OrderInfo());
                totalSpent += i.GetQuantity() * i.GetPrice();
                }
            return "\nTotal for all purchases: $"+totalSpent.ToString()+"\n\nPress enter to return to the main menu.";
        }
        public string StoreOrderHistory(int storeID)
        {
            decimal totalSales = 0;
            IEnumerable<Item> itemList = this.repo.PopulateOrderHistory(storeID);
            foreach (Item i in itemList)
            {
                Console.WriteLine("\n" + i.OrderInfo());
                totalSales += i.GetQuantity() * i.GetPrice();
            }
            return "\nTotal for all purchases: $" + totalSales.ToString() + "\n\nPress enter to return to the main menu.";
        }
    }
    
}
