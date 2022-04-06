using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZero.Database;
using PZero.Classes;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PZero.Test")]

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
        public void MainMenu(int custID, int storeID, ShoppingCart shoppingCart)
        {
            

            while (true)
            {
                Customer cust1 = StoreLogin(custID);
                string guestName = cust1.GetCustName();
                storeID = cust1.GetStoreID();
                Console.Clear();              
                Console.WriteLine("Welcome to the Furniture store.");
                Console.WriteLine("Current user: " + guestName + "\n");
                Console.WriteLine("[1] - Account Menu");
                Console.WriteLine("[2] - Shopping Menu");
                Console.WriteLine("[3] - Store Employees Only");
                Console.WriteLine("[0] - Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        cust1 = StoreLogin(AccountMenu(custID, storeID, guestName, shoppingCart));
                        custID = cust1.GetCustID();
                        storeID = cust1.GetStoreID();
                        break;
                    case "2":
                        Console.Clear();
                        storeID = ShoppingMenu(custID, storeID, shoppingCart);
                        break;
                    case "3":
                        Console.Clear();
                        StoreMenu(storeID);
                        break;                
                    case "0":
                        Console.Clear();
                        if(custID != 99)
                        { SaveShoppingCart(shoppingCart,custID); }
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.  Please try again.");
                        break;
                }
            }
        }
        public int AccountMenu(int custID, int storeID, string guestName, ShoppingCart shoppingCart) 
        {
            string guestAddress = StoreLogin(custID).GetAddress();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Account Menu.\nWelcome {guestName}! \nAddress:{guestAddress}");
                Console.WriteLine("[1] - Create Account or Log In");
                Console.WriteLine("[2] - Update Customer Address");
                Console.WriteLine("[3] - View Shopping Cart");
                Console.WriteLine("[4] - View Customer Order History");
                Console.WriteLine("[5] - Log Out");
                Console.WriteLine("[0] - Return to the Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        if(custID != 99)
                        { 
                            Console.WriteLine("You are already logged in!");
                            Thread.Sleep(900);
                            break;
                        }
                        Customer cust1 = LogIn();
                        if (cust1.GetCustID() == 99)
                        {
                            Console.WriteLine("No account found. Please create an account if you do not have one.");
                            cust1 = AddCustomer(storeID);
                        }
                        else
                        {                           
                            Console.WriteLine("Welcome Back!");
                            Thread.Sleep(900);
                        }
                        custID = cust1.GetCustID();
                        guestName = cust1.GetCustName();
                        storeID = cust1.GetStoreID();
                        guestAddress = cust1.GetAddress();
                        shoppingCart = FillCustCart(custID, shoppingCart);
                        break;
                    case "2":
                        Console.Clear();
                        guestAddress=UpdateAddress(custID);
                        Thread.Sleep(900);
                        break;
                    case "3":
                        Console.Clear();
                        shoppingCart.ViewCart();
                        Console.WriteLine("\nPress enter to return to the Account menu.");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine(CustOrderHistory(custID));
                        Console.ReadLine();
                        break;
                    case "5":
                        Console.Clear();
                        SaveShoppingCart(shoppingCart, custID);
                        cust1=LogOut();
                        guestName = cust1.GetCustName();
                        guestAddress = cust1.GetAddress();
                        custID = cust1.GetCustID();
                        shoppingCart.EmptyCart();
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Returning to main menu.");
                        Thread.Sleep(900);
                        return custID;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.  Please try again.");
                        break;
                }
            }
        }
        public int ShoppingMenu(int custID, int storeID, ShoppingCart shoppingCart)
        {
            while (true)
            {
                Console.Clear();
                string userLocation=repo.GetStoreName(storeID);
                Console.WriteLine($"Shopping Menu\nCurrent Location: {userLocation}\n");
                Console.WriteLine("[1] - Search Inventory");
                Console.WriteLine("[2] - View Shopping Cart");
                Console.WriteLine("[3] - Remove from Shopping Cart");
                Console.WriteLine("[4] - Check Out");
                Console.WriteLine("[5] - Change Store");
                Console.WriteLine("[0] - Return to the Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        SearchStoreInventory(storeID, userLocation, shoppingCart);
                        break;
                    case "2":
                        Console.Clear();
                        shoppingCart.ViewCart();
                        Console.WriteLine("\nPress enter to return to the Shopping menu.");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        shoppingCart.ViewCart();
                        Console.WriteLine(shoppingCart.RemoveFromCart());
                        Thread.Sleep(900);
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine(CheckOut(shoppingCart, storeID, custID));
                        Console.WriteLine("\nPress enter to return to the Shopping menu.");
                        Console.ReadLine();
                        break;
                    case "5":
                        Console.Clear();
                        storeID = GetListStoreNames();                     
                        break;
                    case "0":
                        Console.Clear();
                        Console.WriteLine("Returning to main menu.");
                        Thread.Sleep(900);
                        return storeID;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.  Please try again.");
                        break;
                }

            }

        }
        public void StoreMenu(int storeID)
        {
            while (true)
            {
                Console.Clear();
                string userLocation = StoreName(storeID);
                Console.WriteLine("Currently viewing: " + userLocation+"\n");
                Console.WriteLine("[1] - Select Store");
                Console.WriteLine("[2] - Search for a Customer");
                Console.WriteLine("[3] - View Store Order History");
                Console.WriteLine("[0] - Return to Main Menu");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        storeID = GetListStoreNames();
                        break;
                    case "2":
                        Console.Clear();
                        SearchCustomers();
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(StoreOrderHistory(storeID));
                        Console.ReadLine();
                        break;
                    case "0":
                        Console.Clear();                      
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option.  Please try again.");
                        break;

                }
            }
        }
        public Customer AddCustomer(int storeID)
        {
            while (true)
            {
                Console.WriteLine("Please enter your first name.");
                string inputfname = Console.ReadLine();
                Console.WriteLine("Please enter your last name.");
                string inputlname = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Is this the name you wish to create an account for? \n" + inputfname + " " + inputlname + "\nY/N\nPress any other key to quit account creation.");
                string confirmName = Console.ReadLine().ToUpper();
                if (confirmName == "Y" || confirmName == "YES")
                {
                    Console.WriteLine("Account Successfully created!\nPress any key to return to the main menu.");
                    return CallAddNewCustomer(inputfname, inputlname, storeID);
                }
                else if (confirmName == "N" || confirmName == "NO")
                {
                    Console.Clear();
                    Console.WriteLine("Let's try again.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Input not recognized.  Account creation cancelled.\nPress any key to return to the Account menu.");
                    Console.ReadLine();
                    Customer cust1 = new("Guest", "No Address on file.", 99);
                    return cust1;
                }
            }

        }
        public void SearchCustomers()
        {
            while (true)
            {
                Console.WriteLine("Search for a customer by first or last name.");
                string inputname = Console.ReadLine();
                IEnumerable<Customer> custList = this.repo.SearchCustomers(inputname);
                Console.WriteLine(custList.Count() + " : Entries Found");
                foreach (Customer cust in custList)
                {
                    Console.WriteLine("\n" + cust.GetCustName() + cust.GetAddress());
                }
                Console.WriteLine("\nSearch for someone else?\nY/N");
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
            Console.WriteLine("Search Completed.\nReturning to the menu");
            Thread.Sleep(900);
        }
        public void SearchStoreInventory(int storeID, string userLocation, ShoppingCart shoppingCart)
        {
            while (true)
            {
                Console.WriteLine("Enter the item you wish to search for by name or material.");
                string inputname = Console.ReadLine();
                IEnumerable<Item> itemList = repo.SearchInventory(inputname, storeID);
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
                    shoppingCart.AddToCart(GetOneItem(skuAdd, storeID));
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
        public string CheckOut(ShoppingCart shoppingCart, int storeID, int custID)
        {

            if (custID == 99)
            {
                Console.Clear();
                Customer cust = LogIn();
                custID = cust.GetCustID();
                if (custID == 99)
                { return "Invalid Username or password.\nOrder cannot be processed."; }
            }
            shoppingCart = FillCustCart(custID, shoppingCart);
            List<Item> itemsInCart = shoppingCart.GetCart();
            if(itemsInCart.Count == 0)
            {
                Console.Clear();
                return "You have no items in your cart!";
            }
            foreach (Item i in itemsInCart)
            {
                int stockquantity = repo.CheckQuantity(i, storeID);
                if (stockquantity - i.GetQuantity() < 0)
                {
                    Console.Clear();
                    return "Not enough items in stock at this location.\nOrder cancelled.";
                }
            }
            shoppingCart.ViewCart();
            Console.WriteLine("Your total will be: $" + shoppingCart.GetCartTotalPrice() + "\nEnter \'Confirm\' to place your order.");
            string confirmed = Console.ReadLine().ToUpper();
            if (confirmed != "CONFIRM")
            {
                return "Order Cancelled.";
            }
            string purchasedate= DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            foreach (Item i in itemsInCart)
            {
                RemoveItem(i, storeID);
                repo.AddToOrderHistory(i, storeID, custID, purchasedate);
            }
            Console.Clear();
            shoppingCart.EmptyCart();
            return "Transaction successfully completed!";
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
            return repo.CustomerLogin(username, password);

        }
        public string UpdateAddress(int custID)
        {
            if (custID == 99)
            {
                Console.Clear();
                Console.WriteLine("You must login before you can update your address.");
                return StoreLogin(custID).GetAddress();
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
                Console.WriteLine($"Is this the correct address? \n\n{address}\n{city} {state}\n\nEnter[Y/N]");
                string confirmAddress = Console.ReadLine().ToUpper();
                if (confirmAddress == "Y" || confirmAddress == "YES")
                {
                    custID=repo.UpdateCustomerAddress(address, city, state, custID);
                     //StoreLogin(custID).GetAddress();
                    Console.Clear();
                    return StoreLogin(custID).GetAddress();
                }
                else if (confirmAddress == "N" || confirmAddress == "NO")
                {
                    Console.Clear();
                    Console.WriteLine("Let's try again.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Input not recognized.\nRequest cannot be processed.");
                    return StoreLogin(custID).GetAddress();

                }

            }

        }
        public string CustOrderHistory(int custID)
        {
            decimal totalSpent = 0;
            if (custID == 99)
            {
                Console.Clear();
                Customer cust = LogIn();
                custID = cust.GetCustID();
                if (custID == 99)
                { return "Invalid Username or password.\nRequest cannot be processed."; }
            }           
            IEnumerable<Item> itemList = CallPopulateOrderHistory(custID);
            foreach (Item i in itemList)
            {
                Console.WriteLine("\n" + i.OrderInfo());
                totalSpent += i.GetQuantity() * i.GetPrice();
            }
            return "\nTotal for all purchases: $" + totalSpent.ToString() + "\n\nPress enter to return to the main menu.";
        }
        public string StoreOrderHistory(int storeID)
        {
            decimal totalSales = 0;
            IEnumerable<Item> itemList = repo.PopulateOrderHistory(storeID);
            foreach (Item i in itemList)
            {
                Console.WriteLine("\n" + i.OrderInfo());
                totalSales += i.GetQuantity() * i.GetPrice();
            }
            return "\nTotal for all sales at the "+StoreName(storeID)+": $" + totalSales.ToString() + "\n\nPress enter to return to the main menu.";
        }
        public Customer StoreLogin(int custID)
        {
            return repo.CustomerLogin(custID);
        }
        public string StoreName(int storeID)
        {
            return repo.GetStoreName(storeID);
        }
        public int GetListStoreNames()
        {
            Console.Clear();
            Console.WriteLine("Select a Store Location by entering its number.\n");
            foreach(string s in repo.ListStoreNames())
            {
                Console.WriteLine(s+"\n");
            }
            bool success =Int32.TryParse(Console.ReadLine(), out int storeID);
            if(success)
            { return storeID; }
            else 
            {
                Console.Clear();
                Console.WriteLine("Invalid Selection.  Store set to default.");
                Thread.Sleep(900);
                return 1; 
            }
                     
        }
        public Item GetOneItem(int skuAdd, int storeID)
        {
            return repo.FindOneItem(skuAdd, storeID);
        }
        public int RemoveItem(Item i, int storeID)
        {
            return repo.DeleteItem(i, storeID);
        }  
        public Customer CallAddNewCustomer(string inputfname, string inputlname, int storeID)
        {
            return repo.AddNewCustomer(inputfname, inputlname, storeID);
        }
        public IEnumerable<Item> CallPopulateOrderHistory(int custID)
        {
            return repo.PopulateOrderHistory(custID);
        }
        public Customer LogOut()
        {
            Customer cust1 = new Customer("Guest", "No Address on file.", 99);
            return cust1;
        }
        public void SaveShoppingCart(ShoppingCart shoppingCart, int custID)
        {
            foreach(Item item in shoppingCart.GetCart())
            {
                bool alreadythere =repo.CheckIfItemExistsInCart(item, custID);
                if(alreadythere==true)
                {
                    repo.UpdateItemInCart(item, custID);
                    Console.WriteLine("Item exists in cart");
                    Thread.Sleep(900);
                }
                else
                {
                    repo.SaveItemToCustCart(item, custID);
                }
            }
        } 
        public ShoppingCart FillCustCart(int custID, ShoppingCart shoppingCart)
        {
            foreach (Item item in repo.GetCustCart(custID))
            {
                shoppingCart.AddToCart(item);
            }
            return shoppingCart;
        }
    }
    
}
