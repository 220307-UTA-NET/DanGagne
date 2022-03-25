using System.Xml.Serialization;

namespace ProjectZero
{
    public class Store
    {
        //Fields
        string location;
        string[] inventory;

        List<Inventory> shoppingList =new List<Inventory>();
        List<Inventory> stock =new List<Inventory>();
        
        private List<Customer> allCustomers = new List<Customer>();

        public XmlSerializer Serializer{get;}=new( typeof(List<Customer>));


        //Constructor
        public Store(string location)
        {
            this.location=location;
        }

        //Methods

        public void AddCustomer(string fname, string lname)
        {
            var newCustomer = new Customer(fname, lname, this.location);
            allCustomers.Add(newCustomer);
        }
        public void CheckInventory()
        {
            foreach (var i in stock)
            {
                Console.WriteLine("Item : "+i.item+" \nPrice: $"+i.price+"\n");
            }
        }

        public void AddToInventory(string item, double price)
        {
            var itemToAdd = new Inventory(item, price);
            this.stock.Add(itemToAdd);
        }


        public void AddToShoppingCart()
        {
            Console.WriteLine("Enter which item you want to add.");
            var add= Console.ReadLine();
            if(stock.Exists(x => x.item == add))
            {
                var move =stock.LastOrDefault(x=> x.item==add);
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
            
        }
        public void RemoveFromShoppingCart()
        {
            Console.WriteLine("Enter which item you want to remove.");
            var remove = Console.ReadLine();
            if(shoppingList.Exists(x => x.item == remove))
            {
                var move =shoppingList.LastOrDefault(x=> x.item==remove);
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
        public void CheckOut()
        {
            double total = 0;
            foreach (var item in shoppingList)
            {
                total+=item.price;
                var move=stock.LastOrDefault(x=> x.item==item.item);
                    if(item != null)
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

         public void SerializeAsXml()
        {
            var newStringWriter = new StringWriter();
            Serializer.Serialize(newStringWriter, allCustomers);
            newStringWriter.Close();

            string path =$@".\CustomerList_{this.location}.xml";
            File.WriteAllText(path, newStringWriter.ToString());

        }


      
    }
}
