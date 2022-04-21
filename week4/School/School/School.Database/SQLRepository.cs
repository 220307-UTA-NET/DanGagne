using School.Logic;
using System.Data.SqlClient;

namespace School.Database
{
    public class SQLRepository : IRepository
    {
        //Will hold all of the communication to and from the database

        //FIELDS
        private readonly string _connectionString;

        //CONSTRUCTOR
        public SQLRepository (string connectionString)
        {
            this._connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        //Methods
        public IEnumerable<Teacher>GetAllTeachers()
        {
            List<Teacher> result = new List<Teacher>();

            using SqlConnection conn = new SqlConnection(this._connectionString);
            conn.Open();

            using SqlCommand cmd = new(
                "SELECT *"+
                "FROM School.Teacher;", conn);

            using SqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                int ID = reader.GetInt32(0);
                string Name = reader.GetString(1);
                result.Add(new(ID, Name));
            }
            //reader.??? is parsing the response from the database to C# datatypes 

            conn.Close();
            return result;
        }

        public Teacher CreateNewTeacher(string Name)
        {
            using SqlConnection conn = new SqlConnection (this._connectionString);
            conn.Open ();

            string cmdText =
                @"INSERT INTO School.Teacher (Name)
                VALUES
                (@Name);";

            using SqlCommand cmd = new SqlCommand(cmdText, conn);

            cmd.Parameters.AddWithValue("@Name", Name);

            cmd.ExecuteNonQuery();

            cmdText = 
                @"SELECT Teacher_ID, Name
                FROM School.Teacher
                WHERE Name=@Name;";

            using SqlCommand cmd2 = new SqlCommand (cmdText, conn);

            cmd2.Parameters.AddWithValue("@Name", Name);

            using SqlDataReader reader = cmd2.ExecuteReader();

            Teacher teacher;
            while (reader.Read())
            {               
                return teacher = new Teacher(reader.GetInt32(0), reader.GetString(1));
            }
            conn.Close();
            Teacher noTeacher = new();
            return noTeacher;
            
            

        }
    
        public string GetStudentName(int ID)
        {
            string? name="";
            using SqlConnection conn = new SqlConnection(this._connectionString);
            conn.Open();

            string cmdText = @"SELECT Name FROM School.Student WHERE Student_ID=@id;";

            using SqlCommand cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@id", ID);
        
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                name = reader.GetString(0);
            }
            conn.Close();
            if (name != null)
            {
                return name;
            }
            else return null;
        }
    }
}