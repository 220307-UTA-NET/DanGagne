
using PZero.Classes;
//using ProjectZero;
using System.Data.SqlClient;

namespace PZero.Database
{
    public class SqlRepo : IRepo
    {
        //Fields
        private readonly string _connString;

        //Constructor
        public SqlRepo(string connString)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }


        //Methods

        public void AddNewCustomer(string fname, string lname, int storeID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Customer (NameFirst, NameLast, DefaultStoreLocation) VALUES (@fname, @lname, @storeID);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@storeID", storeID);

            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public IEnumerable<Customer> SearchCustomers(string inputname)
        {
            List<Customer> custList = new List<Customer>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Customer WHERE @inputname=NameFirst OR @inputname=NameLast;";
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
                custList.Add(new(fname, lname, address, city, state, country));
            }
            connect.Close();
            return custList;

        }
        public Customer CustomerLogin(string username, string password)
        {
            Customer cust1=new Customer();
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
                int custID = reader.GetInt32(7);
                int storeID = reader.GetInt32(8);
                cust1 = new(fname, lname, custID, storeID);
                return cust1;

            }
            connect.Close();
            return cust1;
        }
        public IEnumerable<Item> SearchInventory(string inputname, int storeID)
        {
            List<Item> itemList = new List<Item>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Stock WHERE StoreLocationID=@storeID AND (@inputname=ItemName OR @inputname=Material);";
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
                if (quantity > 30)
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

        public void DeleteItem(Item item, int storeID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"UPDATE Store.Stock SET Quantity=Quantity-@quantity WHERE StoreLocationID=@storeID AND SKU=@sku;";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@sku", item.GetSku());
            cmd.Parameters.AddWithValue("@quantity", item.GetQuantity());
            cmd.Parameters.AddWithValue("@storeID", storeID);
 
            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public int CheckQuantity(Item item, int storeID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"SELECT * FROM Store.Stock WHERE StoreLocationID=@storeID AND SKU=@sku;";
            using SqlCommand cmd = new (cmdText, connect);
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

        public void AddToOrderHistory(Item item, int storeID, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Purchase(CustomerID, StoreLocationID, SKU, Quantity) VALUES (@custID, @storeID, @itemSku, @itemQuantity);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@custID", custID);
            cmd.Parameters.AddWithValue("@storeID", storeID);
            cmd.Parameters.AddWithValue("@itemSku", item.GetSku());
            cmd.Parameters.AddWithValue("itemQuantity", item.GetQuantity());

            cmd.ExecuteNonQuery();
            connect.Close();
        }

        public void UpdateCustomerAddress(string address, string city, string state, int custID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"UPDATE Store.Customer SET Address=@address, City=@city, State=@state, Country=USA WHERE CustomerID=@custID";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@state", state);
            cmd.Parameters.AddWithValue("@custID", custID);

            cmd.ExecuteNonQuery();
            connect.Close();
        }
        public IEnumerable<Item> PopulateOrderHistory(int cust_storeID)
        {
            List<Item> itemList = new List<Item>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText="";
            if(cust_storeID >99)
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
                DateTime date = reader.GetDateTime(4);
                string itemName = reader.GetString(6);
                decimal price = reader.GetDecimal(7);
                string material = reader.GetString(10);
                itemList.Add(new(itemName, material, price, quantity, sku, date));
            }
            connect.Close();
            return itemList;

        }
    }
}