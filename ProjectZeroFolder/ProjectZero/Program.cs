namespace ProjectZero
{
    public class Program
    {
        static void Main()
        {
            // Console.WriteLine("Testing");
            Store store1 = new Store();
            /*store1.AddCustomer("Dan", "Gagne", "Rochester");
            store1.AddCustomer("Helga", "Snips", "Buffalo");
            store1.AddCustomer("George","Peters", "Rochester");
            store1.AddCustomer("Frank", "Elma", "Syracuse");
            store1.AddCustomer("Frank", "Petunia", "Buffalo");
            store1.AddCustomer("Elsa", "Bernard","Buffalo");
            store1.AddCustomer("Laural", "Silverson","Buffalo");*/
            
            
            string userLocation="Rochester";
            

                while(true)
                {
                    Console.Clear();
                    store1.ReadFromXml(userLocation);
                    Console.WriteLine("Welcome to the Furniture store.");                   
                    Console.WriteLine("Current location: "+userLocation);
                    Console.WriteLine("[1] - Create Account (Add Customer)");
                    Console.WriteLine("[2] - Search Customers");
                    Console.WriteLine("[3] - Change Store");
                    Console.WriteLine("[4] - Search Inventory");
                    Console.WriteLine("[5] - Add To Inventory(Admin Only)");
                    Console.WriteLine("[6] - Add to Shopping Cart");
                    Console.WriteLine("[0] - Exit");
                 
          
                    switch(Console.ReadLine())
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Current number of customers: "+store1.allCustomers.Count);
                            Console.WriteLine("Your user id is: "+store1.AddCustomer(userLocation));
                            store1.SerializeAsXml(userLocation);
                            Console.WriteLine("New number of customers: "+store1.allCustomers.Count);
                            Console.WriteLine("Press Enter to return to the menu.");
                            Console.ReadLine();
                            break;
                        case "2":
                            Console.Clear();
                            store1.SearchCustomerList();
                            Console.WriteLine("Press Enter to return to the menu");
                            Console.ReadLine();
                            break;
                        case "3":
                            Console.Clear();
                            Console.WriteLine("Select your location. \n[1]-Rochester\n[2]-Buffalo\n[3]-Syracuse");
                            switch(Console.ReadLine())
                                { 
                                    case "1":
                                    userLocation = "Rochester";
                                    break;
                                    case"2":
                                    userLocation ="Buffalo";
                                    break;
                                    case"3":
                                    userLocation ="Syracuse";
                                    break;
                                    default:
                                    Console.WriteLine("Invalid selection.");
                                    break;
                                }
                            Console.WriteLine("Store set to: "+userLocation+".\nPress Enter to return to the menu.");
                            Console.ReadLine();
                            break;
                        case "4":
                            Console.Clear();
                            Console.WriteLine("Inventory in stock at the "+userLocation+" store.");
                            store1.PrintInventory();
                            Console.WriteLine("Press enter to return to the menu");
                            Console.ReadLine();
                            break;
                        case "5":
                            Console.Clear();
                            store1.AddToInventory();
                            store1.SerializeAsXml(userLocation);
                            Console.WriteLine("Press enter to return to the menu");
                            Console.ReadLine();
                            break;
                        case "6":
                            Console.Clear();
                            store1.AddToShoppingCart();
                            store1.RemoveFromShoppingCart();
                            store1.CheckOut();
                            store1.SerializeAsXml(userLocation);
                            Console.WriteLine("Press enter to return to the menu");
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


            
            

      
            //store1.SerializeAsXml("buffalo");
            //buffalo.PrintInventory();
            //buffalo.PrintCustomerList();
            //buffalo.SearchCustomerList();
           
        }
    }


}
