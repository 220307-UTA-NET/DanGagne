
using PZero.Classes;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("PZero.Test")]

namespace PZero.Database
{
    public class SqlRepo : IRepo
    {
        //Fields
        internal readonly string _connString;

        //Constructor
        public SqlRepo(string connString)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }


        //Methods

        public Customer AddNewCustomer(string fname, string lname, int storeID)
        {
            Customer cust1 = new Customer();
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Customer (NameFirst, NameLast, DefaultStoreLocation) VALUES (@fname, @lname, @storeID);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@storeID", storeID);
            cmd.ExecuteNonQuery();

            cmdText = @"SELECT * FROM Store.Customer WHERE @fname=NameFirst AND @lname=NameLast;";
            using SqlCommand cmd2 = new(cmdText, connect);
            cmd2.Parameters.AddWithValue("@fname", fname);
            cmd2.Parameters.AddWithValue("@lname", lname);
            using SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                fname = reader.GetString(0);
                lname = reader.GetString(1);
                int custID = reader.GetInt32(7);
                storeID = reader.GetInt32(8);
                cust1 = new(fname, lname, custID, storeID);
            }
            connect.Close();
            return cust1;

        }

        public IEnumerable<Customer> SearchCustomers(string inputname)
        {
            List<Customer> custList = new List<Customer>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();
            string cmdText;
            if (inputname == "showall")
            {
                cmdText = @"SELECT* FROM Store.Customer;";
            }
            else
            { cmdText = @"SELECT * FROM Store.Customer WHERE @inputname=NameFirst OR @inputname=NameLast;"; }

            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@inputname", inputname);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string fname = reader.GetString(0);
                string lname = reader.GetString(1);
                string address;
                if (reader.IsDBNull(2))
                { address = ""; }
                else { address = reader.GetString(2); }
                string city;
                if (reader.IsDBNull(3))
                { city = ""; }
                else { city = reader.GetString(3); }
                string state;
                if (reader.IsDBNull(4))
                { state = ""; }
                else { state = reader.GetString(4); }
                string country;
                if (reader.IsDBNull(5))
                { country = ""; }
                else { country = reader.GetString(5); }
                int custID = reader.GetInt32(7);
                int storeID = reader.GetInt32(8);
                custList.Add(new(fname, lname, address, city, state, country, custID, storeID));
            }
            connect.Close();
            return custList;

        }
        public Customer CustomerLogin(string username, string password)
        {
            Customer cust1 = new Customer();
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Customer WHERE Customer.NameFirst=@username AND Customer.NameLast=@password;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                string fname = reader.GetString(0);
                string lname = reader.GetString(1);
                string address;
                if (reader.IsDBNull(2))
                { address = ""; }
                else { address = reader.GetString(2); }
                string city;
                if (reader.IsDBNull(3))
                { city = ""; }
                else { city = reader.GetString(3); }
                string state;
                if (reader.IsDBNull(4))
                { state = ""; }
                else { state = reader.GetString(4); }
                string country;
                if (reader.IsDBNull(5))
                { country = ""; }
                else { country = reader.GetString(5); }
                int custID = reader.GetInt32(7);
                int storeID = reader.GetInt32(8);
                cust1 = new(fname, lname, address, city, state, country, custID, storeID);
                return cust1;

            }
            connect.Close();
            return cust1 = new("Guest", "", "No Address on file.", 99, 1);
        }

        public Customer CustomerLogin(int custID)
        {
            Customer cust1 = new Customer();
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Customer WHERE Customer.CustomerID=@custID;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                string fname = reader.GetString(0);
                string lname = reader.GetString(1);
                string address;
                if (reader.IsDBNull(2))
                { address = ""; }
                else { address = reader.GetString(2); }
                string city;
                if (reader.IsDBNull(3))
                { city = ""; }
                else { city = reader.GetString(3); }
                string state;
                if (reader.IsDBNull(4))
                { state = ""; }
                else { state = reader.GetString(4); }
                string country;
                if (reader.IsDBNull(5))
                { country = ""; }
                else { country = reader.GetString(5); }
                int storeID = reader.GetInt32(8);
                cust1 = new(fname, lname, address, city, state, country, custID, storeID);
                return cust1;

            }
            connect.Close();
            return cust1 = new("Guest","","No Address on file.", 99, 1);
        }

        public IEnumerable<Item> SearchInventory(string inputname, int storeID)
        {
            List<Item> itemList = new List<Item>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText;
            if (inputname == "showall")
            {
                cmdText = @"SELECT* FROM Store.Stock WHERE StoreLocationID=@storeID;";
            }
            else
            { cmdText = @"SELECT * FROM Store.Stock WHERE StoreLocationID=@storeID AND (@inputname=ItemName OR @inputname=Material);"; }
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@inputname", inputname);
            cmd.Parameters.AddWithValue("@storeID", storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string itemName = reader.GetString(0);
                decimal price = reader.GetDecimal(1);
                int quantity = reader.GetInt32(2);
                string material = reader.GetString(4);
                int sku = reader.GetInt32(5);
                itemList.Add(new(itemName, price, quantity, material, sku, storeID));
            }
            connect.Close();
            return itemList;

        }

        public Item FindOneItem(int sku, int storeID)
        {
            Item item = new Item();
            Console.WriteLine("How many would you like to add to your cart?");
            if (Int32.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.Clear();
                if (quantity > 50)
                {
                    Console.WriteLine("This is an abnormally high order.  Please call the store directly for an order this large.");
                    return item;
                }
            }
            else
            {
                Console.WriteLine("Not a valid entry.  No item added to shopping cart.");
                return item;
            }

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Stock WHERE StoreLocationID=@storeID AND SKU=@sku;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@sku", sku);
            cmd.Parameters.AddWithValue("@storeID", storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string itemName = reader.GetString(0);
                decimal price = reader.GetDecimal(1);
                string material = reader.GetString(4);
                item = new(itemName, price, quantity, material, sku, storeID);
            }
            connect.Close();
            return item;

        }

        public int DeleteItem(Item item, int storeID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"UPDATE Store.Stock SET Quantity=Quantity-@quantity WHERE StoreLocationID=@storeID AND SKU=@sku;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@sku", item.GetSku());
            cmd.Parameters.AddWithValue("@quantity", item.GetQuantity());
            cmd.Parameters.AddWithValue("@storeID", storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int itemQuantity = reader.GetInt32(2);
                return itemQuantity;
            }
            connect.Close();
            return 0;
        }

        public int CheckQuantity(Item item, int storeID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Stock WHERE StoreLocationID=@storeID AND SKU=@sku;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@sku", item.GetSku());
            cmd.Parameters.AddWithValue("@storeID", storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int itemQuantity = reader.GetInt32(2);
                return itemQuantity;

            }
            connect.Close();
            return 0;

        }

        public void AddToOrderHistory(Item item, int storeID, int custID, string purchasedate)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Purchase(CustomerID, StoreLocationID, SKU, Quantity, PurchaseDate) VALUES (@custID, @storeID, @itemSku, @itemQuantity, @purchasedate);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@storeID", storeID);
            cmd.Parameters.AddWithValue("@itemSku", item.GetSku());
            cmd.Parameters.AddWithValue("@itemQuantity", item.GetQuantity());
            cmd.Parameters.AddWithValue("@purchasedate", purchasedate);

            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public int UpdateCustomerAddress(string address, string city, string state, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"UPDATE Store.Customer SET Address=@address, City=@city, State=@state, Country='USA' WHERE CustomerID=@custID";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.ExecuteNonQuery();

            cmdText = @"SELECT * FROM Store.Customer WHERE @custID=CustomerID;";
            using SqlCommand cmd2 = new(cmdText, connect);
            cmd2.Parameters.AddWithValue("@custID", custID);
            using SqlDataReader reader = cmd2.ExecuteReader();

            while (reader.Read())
            {
                custID = reader.GetInt32(7);
                return custID;
            }
            connect.Close();
            return custID = 99;
        }
        public IEnumerable<Item> PopulateOrderHistory(int cust_storeID)
        {
            List<Item> itemList = new List<Item>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = "";
            if (cust_storeID > 99)
            {
                cmdText = @"SELECT * FROM Store.Purchase INNER JOIN Store.Stock ON (Store.Stock.SKU=Store.Purchase.SKU AND Store.Stock.StoreLocationID=Store.Purchase.StoreLocationID) WHERE CustomerID=@cust_storeID;";
            }
            else
            {
                cmdText = @"SELECT * FROM Store.Purchase INNER JOIN Store.Stock ON (Store.Stock.SKU=Store.Purchase.SKU AND Store.Stock.StoreLocationID=Store.Purchase.StoreLocationID) WHERE Purchase.StoreLocationID=@cust_storeID;";
            }

            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@cust_storeID", cust_storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int sku = reader.GetInt32(2);
                int quantity = reader.GetInt32(3);
                string date = reader.GetString(4);
                string itemName = reader.GetString(6);
                decimal price = reader.GetDecimal(7);
                string material = reader.GetString(10);
                itemList.Add(new(itemName, material, price, quantity, sku, date));
            }
            connect.Close();
            return itemList;

        }

        public string GetStoreName(int storeID)
        {
            string storeName = "Rochester";

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Location WHERE StoreLocationID=@storeID;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@storeID", storeID);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                storeName = reader.GetString(1);
                return storeName;
            }
            connect.Close();
            return storeName;


        }

        public List<string> ListStoreNames()
        {
            List<string> storeNames = new List<string>();
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Location";
            using SqlCommand cmd = new(cmdText, connect);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int storeID = reader.GetInt32(0);
                string storeName = reader.GetString(1);
                string nameID = $"{storeID}-{ storeName}";
                storeNames.Add(nameID);
            }
            connect.Close();
            return storeNames;
        }
        public void SaveItemToCustCart(Item item, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.ShoppingCart(CustomerID, StoreLocationID, SKU, Quantity) VALUES (@custID, '1', @itemSku, @itemQuantity);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@itemSku", item.GetSku());
            cmd.Parameters.AddWithValue("@itemQuantity", item.GetQuantity());


            cmd.ExecuteNonQuery();
            connect.Close();
        }
        public void UpdateItemInCart(Item item, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"UPDATE Store.ShoppingCart SET Quantity = Quantity+@itemQuantity WHERE CustomerID=@custID AND SKU=@itemSKU;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@itemSku", item.GetSku());
            cmd.Parameters.AddWithValue("@itemQuantity", item.GetQuantity());

            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public bool CheckIfItemExistsInCart(Item item, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.ShoppingCart WHERE CustomerID=@custID AND SKU=@itemSku;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@itemSku", item.GetSku());        

            using SqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {             
                return true;
            }
            connect.Close();
            return false;

        }

        public IEnumerable<Item> GetCustCart(int custID)
        {
            List<Item> itemList = new List<Item>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.ShoppingCart INNER JOIN Store.Stock ON Store.ShoppingCart.SKU=Store.Stock.SKU  WHERE Store.ShoppingCart.CustomerID=@custID AND Store.Stock.StoreLocationID=1";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID",custID);          

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int storeID = reader.GetInt32(1);
                int sku = reader.GetInt32(2);
                string itemName = reader.GetString(4);
                decimal price = reader.GetDecimal(5);
                string material = reader.GetString(8);
                int quantity = reader.GetInt32(3);          
                itemList.Add(new(itemName, price, quantity, material, sku, storeID));
            }
            connect.Close();

            using SqlConnection connect2 = new SqlConnection(this._connString);
            connect2.Open();

            cmdText = @"DELETE FROM Store.ShoppingCart WHERE CustomerID=@custID;";
            using SqlCommand cmd2 = new(cmdText, connect2);
            cmd2.Parameters.AddWithValue("@custID", custID);

            cmd2.ExecuteNonQuery();
            connect.Close();

            
            return itemList;
        }
    }
}