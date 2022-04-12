
using System.Data.SqlClient;
using Logic;

namespace Database
{
    public class SqlRepo : IRepo
    {
        private readonly string _connString = File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");
        public SqlRepo(string connString)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }

        public async Task<List<Player>> AllPlayers()
        {
            List<Player> playerList = new List<Player>();

            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"SELECT * FROM ProjOne.Player;";
            using SqlCommand cmd = new(cmdText, connect);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int hp = reader.GetInt32(2);
                int str = reader.GetInt32(3);
                int dex = reader.GetInt32(4);

                playerList.Add(new(name, id, hp, str, dex));
            }
            await connect.CloseAsync();
            return playerList;
        }

        public async Task<string> RemovePlayer(int playerID)
        {
            string deletedPlayer = "";
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"SELECT * FROM ProjOne.Player WHERE PlayerID = '{playerID}';";
            using SqlCommand cmd = new(cmdText, connect);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                deletedPlayer = reader.GetString(1);
            }
            await connect.CloseAsync();

            await connect.OpenAsync();
            cmdText = $"DELETE FROM ProjOne.Player WHERE PlayerID = '{playerID}';";
            using SqlCommand cmd2 = new(cmdText, connect);
            cmd2.ExecuteNonQuery();
            await connect.CloseAsync();
            return $"{deletedPlayer} deleted.";

        }

        public async Task<string> NewPlayer(Player player)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = @"INSERT INTO ProjOne.Player (PlayerName, HP, STR, DEX) VALUES (@name, 10, 1, 1);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.Parameters.AddWithValue("@name", player.GetName());

            cmd.ExecuteNonQuery();
            await connect.CloseAsync();
            return $"New Player {player.GetName()} added!";

        }
        public async Task<Player> GetPlayer(int playerID)
        {
            Player currentPlayer = new Player();
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText;
            if (playerID == 0)
            {
                cmdText = "SELECT TOP 1 * FROM ProjOne.Player ORDER BY PlayerID DESC;";
            }
            else
            {
                cmdText = $"SELECT * FROM ProjOne.Player WHERE PlayerID= {playerID};";
            }
            using SqlCommand cmd = new(cmdText, connect);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                int hp = reader.GetInt32(2);
                int str = reader.GetInt32(3);
                int dex = reader.GetInt32(4);
                currentPlayer = new(name, id, hp, str, dex);
            }
            await connect.CloseAsync();
            return currentPlayer;
        }
        public async Task EmptyAllRooms()
        {           
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"DELETE FROM ProjOne.RoomInventory";
            using SqlCommand cmd2 = new(cmdText, connect);
            cmd2.ExecuteNonQuery();
            await connect.CloseAsync();           
        }
        public async Task FillAllRooms()
        {
            int numberOfRooms = 0;
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"SELECT TOP 1 * FROM ProjOne.Room ORDER BY RoomID DESC;";
            using SqlCommand cmd = new(cmdText, connect);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                numberOfRooms = reader.GetInt32(0);
            }
            await connect.CloseAsync();

            using SqlConnection connect2 = new SqlConnection(this._connString);
            await connect2.OpenAsync();
            for (int i = 1; i <= numberOfRooms; i++)
            {
                cmdText = $"INSERT INTO ProjOne.RoomInventory(RoomID, ItemID1, ItemID2, ItemID3) VALUES ({i}, FLOOR(RAND() * (14 - 0 + 1)) + 0, FLOOR(RAND() * (14 - 0 + 1)) + 0, FLOOR(RAND() * (14 - 0 + 1)) + 0);";
                using SqlCommand cmd2 = new(cmdText, connect2);
                cmd2.ExecuteNonQuery();
            }
            await connect2.CloseAsync();

        }
    }
}