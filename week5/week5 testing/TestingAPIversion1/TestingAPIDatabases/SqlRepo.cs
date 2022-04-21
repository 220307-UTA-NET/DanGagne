using System.Data.SqlClient;
using ClassLibrary;

namespace TestingAPIDatabases
{
    public class SqlRepo :IRepo
    {
        public readonly string _connString= File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");
        public SqlRepo(string connString)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }

        public List<Customer> GetCustomer(int custID)
        {
           List <Customer> list = new List<Customer>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = $"SELECT * FROM Store.Customer WHERE CustomerID='{custID}';";
            using SqlCommand cmd = new(cmdText, connect);
            //cmd.Parameters.AddWithValue("@storeID", custID);

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
                custID = reader.GetInt32(7);
                int storeID = reader.GetInt32(8);
                list.Add(new(fname, lname, address, city, state, country, custID, storeID));
            }
            connect.Close();
            return list;

        }
        public List<Customer> AllCustomers()
        {
            List<Customer> list = new List<Customer>();

            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = $"SELECT * FROM Store.Customer;";
            using SqlCommand cmd = new(cmdText, connect);

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
                list.Add(new(fname, lname, address, city, state, country, custID, storeID));
            }
            connect.Close();
            return list;
        }

        public string DropOneCustomer(int custID) 
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = $"DELETE FROM Store.Customer WHERE CustomerID = '{custID}';";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.ExecuteNonQuery();
            connect.Close();
            return $"{custID} deleted.";
            
        }

        public string AddOneCustomer(Customer newCust)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Customer (NameFirst, NameLast, DefaultStoreLocation) VALUES (@fname, @lname, @storeID);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@fname", newCust.fname);
            cmd.Parameters.AddWithValue("@lname", newCust.lname);
            cmd.Parameters.AddWithValue("@storeID", newCust.storeID);
            cmd.ExecuteNonQuery();
            return $"{newCust.fname} {newCust.lname} added!";

        }
    }
}