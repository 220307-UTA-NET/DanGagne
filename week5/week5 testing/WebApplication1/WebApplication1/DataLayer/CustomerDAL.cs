using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.DataLayer
{
    public class CustomerDAL
    {
        public string cnn = "";

        public CustomerDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public List<Customers> GetAllCustomers()
        {
            List<Customers> customers = new List<Customers>();
            using SqlConnection cn = new SqlConnection(cnn);

            using SqlCommand cmd = new("SELECT * FROM Store.Customer", cn);

            if (cn.State == System.Data.ConnectionState.Closed)
            {
                cn.Open();

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new Customers()
                    {
                        fname = reader.GetString(0),
                        lname = reader.GetString(1),
                        custID = reader.GetInt32(7),
                        storeID = reader.GetInt32(8),
                    });
                }

            }
            cn.Close();
            return customers;

        }
    }
}

