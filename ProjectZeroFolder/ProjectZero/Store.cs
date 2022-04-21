using System.Xml.Serialization;

namespace ProjectZero
{
    public class Store
    {
        //Fields
      
        
        public List<Inventory> shoppingList{get; set;} =new List<Inventory>();
        public List<Inventory> stock{get; set;}=new List<Inventory>();
        public List<Store> storeLocations {get;set;}=new List<Store>();
        
        public List<Customer> allCustomers {get; set;}= new List<Customer>();

        internal XmlSerializer customerSerializer{get;}=new(typeof(List<Customer>));
        internal XmlSerializer stockSerializer{get;}=new(typeof(List<Inventory>));
        internal XmlSerializer orderSerializer{get;}=new(typeof(List<Inventory>));

        //Constructor
        public Store()
        { }
           
        //Methods

        public int AddCustomer(string userLocation)
        {
            Console.WriteLine("Thank you for choosing to create an account! \nPlease enter your first name");
            string inputfname = Console.ReadLine();
            Console.WriteLine("Please enter your last name.");
            string inputlname = Console.ReadLine();
            Console.WriteLine("Is this the name you wish to create an account for: "+inputfname +" "+inputlname+" ?");
            Console.ReadLine();
            var newCustomer = new Customer(inputfname.ToUpper(), inputlname.ToUpper(),userLocation.ToUpper());
            allCustomers.Add(newCustomer);
            newCustomer.customerId=allCustomers.Count;
            return newCustomer.customerId;
        }

        /*public int CheckCustomerList(string givenfname) 
        {
            var actualCustomer =allCustomers.LastOrDefault(x=> x.fname==givenfname);
            if(givenfname != null)
            {
                return actualCustomer.customerId;
            }
            else
            {
                Console.WriteLine("You do not have an account");
                return 0;
            }
        }*/

        public void PrintCustomerList()
        {
            foreach (var i in allCustomers)
            {
                Console.WriteLine("Customer : "+i.fname+" "+i.lname+"\n");
            }
        }

        public void SearchCustomerList()
        {
            Console.WriteLine("Enter the name you wish to search for.");
            string searchCustomer= Console.ReadLine().ToUpper();

            var findCustomerfname =allCustomers.FindAll(x=> x.fname==searchCustomer);
            var findCustomerlname =allCustomers.FindAll(x=> x.lname==searchCustomer);
            if(findCustomerfname.Count != 0 || findCustomerlname.Count != 0)
            {
                foreach(var customer in findCustomerfname)
                {
                    Console.WriteLine("Customer Found: "+customer.fname+" "+customer.lname);
                }
                foreach(var customer in findCustomerlname)
                {
                    Console.WriteLine("Customer Found: "+customer.fname+" "+customer.lname);
                }
            }
            else
            {
                Console.WriteLine("Could not find a customer by that name.");
            }
        }

        public void PrintInventory()
        {
            Console.WriteLine("There are "+stock.Count+" item(s) in stock.");
            foreach (var i in stock)
            {
                Console.WriteLine("Item : "+i.item+" \nPrice: $"+i.price+"\n");
            }
        }

        public void AddToInventory()
        {
            Console.WriteLine("Enter Admin access code.");
            if(Console.ReadLine()=="0000")
            {
                Console.WriteLine("Enter the item to add to Inventory.");
                string item = Console.ReadLine();
                Console.WriteLine("Enter the item's price in dollars.");
                double price = double.Parse(Console.ReadLine());
                var itemToAdd = new Inventory(item.ToUpper(), price);
                this.stock.Add(itemToAdd);
            }
            else
            { 
                Console.WriteLine("Access Denied!!");
            }
        }

        public void AddToShoppingCart()
        {
            while (true)
            { 
                Console.WriteLine("Enter which item you want to add.");
                var add= Console.ReadLine().ToUpper();

                var move =stock.LastOrDefault(x=> x.item==add);
                if(move != null)
                {
                    this.shoppingList.Add(move);
                }
                else
                {
                    Console.WriteLine("Item is not in stock");
                }
            
                Console.WriteLine("Your shopping cart has:"+shoppingList.Count+" item(s)");

                foreach(var item in shoppingList)
                {
                    Console.WriteLine(item.item);
                }

                Console.WriteLine("Add another item?\nY/N");
                string yesOrNo = Console.ReadLine().ToUpper();
                if( yesOrNo == "Y" || yesOrNo == "YES")
                {Console.Clear();}
                else
                {break;}                
            }
        }

        public void RemoveFromShoppingCart()
        {
            Console.Clear();
            Console.WriteLine("Do you need to remove any items from your Shopping Cart?\nY/N");
            string yesOrNo = Console.ReadLine();
            if( yesOrNo=="Y" || yesOrNo=="YES")
            { 
                Console.WriteLine("Enter which item you want to remove.");
                var remove = Console.ReadLine().ToUpper();
            
                var move =shoppingList.LastOrDefault(x=> x.item==remove);
                if( move != null )
                {
                    this.shoppingList.Remove(move);
                }
                else
                {
                    Console.WriteLine("Item is not in your shopping cart");
                }

                Console.WriteLine("Your shopping cart has:"+shoppingList.Count+" item(s)");
                foreach (var item in shoppingList)
                {
                    Console.WriteLine(item.item);
                }
            }
            
        }

        public void CheckOut()
        {   
            Console.Clear();
            Console.WriteLine("Your shopping cart has:"+shoppingList.Count+" item(s)");
                foreach (var item in shoppingList)
                {
                    Console.WriteLine(item.item);
                }

            Console.WriteLine("Are you ready to check out?\nY/N");
            string yesOrNo = Console.ReadLine().ToUpper();
            if( yesOrNo=="Y" || yesOrNo=="YES")
            { 
                double total = 0;
                foreach (var item in shoppingList)
                {
                    total+=item.price;
                    var move=stock.LastOrDefault(x=> x.item==item.item);
                        if(move != null)
                        {
                            this.stock.Remove(move);
                        }
                    //remove from stock list on checkout;
                }

                shoppingList.Clear();
                Console.WriteLine("The total price of your shopping list is: $"+total);
                Console.WriteLine("Items left in stock: "+stock.Count);
                Console.WriteLine("Items left in shopping list "+shoppingList.Count);
                //removes items from shopping list
                //returns total price
                //writes to CustomerPurchaseHistory
            }
            
        }

        public void PlaceOrder()
        {
            Order order1 = new Order(); 
        }

        public void SerializeAsXml(string location)
        {
            //Save Stock to XML
            var stockStringWriter = new StringWriter();
            stockSerializer.Serialize(stockStringWriter, stock);
           
            string path =$@".\Stock_{location.ToUpper()}.xml";
            File.WriteAllText(path, stockStringWriter.ToString());
            stockStringWriter.Close();

            //Save Customers to XML
            var custStringWriter = new StringWriter();
            customerSerializer.Serialize(custStringWriter, allCustomers);
            custStringWriter.Close();

            path =$@".\Customers.xml";
            File.WriteAllText(path, custStringWriter.ToString());
            
        }

        public void ReadFromXml(string location)
        {
            //Populate Customer list for store
            try
            {
                using StreamReader reader = new( $"./Customers.xml");
                var customerList = (List<Customer>?)customerSerializer.Deserialize(reader);
                reader.Dispose();

                if (customerList is null) throw new InvalidDataException();

                this.allCustomers = customerList;
            }
            catch (InvalidDataException)
            {
                return;
            }

            //Populate Stock for store
             try
            {
                using StreamReader reader = new( $"./Stock_{location.ToUpper()}.xml");
                var allStock = (List<Inventory>?)stockSerializer.Deserialize(reader);
                reader.Dispose();

                if (allStock is null) throw new InvalidDataException();

                this.stock = allStock;
            }
            catch (InvalidDataException)
            {
                return;
            }
        }

      
    }
}
