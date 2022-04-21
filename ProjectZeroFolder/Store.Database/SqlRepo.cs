using ProjectZero.Classes;
using ProjectZero;
using System.Data.SqlClient;

namespace ProjectZero.Database
{
    public class SqlRepo :IRepo
    {
        //Fields
        private readonly string _connString;

        //Constructor
        public SqlRepo(string connString)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }


        //Methods

        public void AddNewCustomer(string fname, string lname)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            connect.Open();

            string cmdText = @"INSERT INTO Store.Customer (NameFirst, NameLast) VALUES (@fname, @lname);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);

            cmd.ExecuteNonQuery();
            connect.Close();
        }
    }
}