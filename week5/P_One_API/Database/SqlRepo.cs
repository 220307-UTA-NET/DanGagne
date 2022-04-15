
using System.Data.SqlClient;
using Logic;
using Microsoft.Extensions.Logging;


namespace Database
{
    public class SqlRepo : IRepo
    {
        private readonly string _connString;
        private readonly ILogger<SqlRepo> _logger;

        public SqlRepo(string connString, ILogger<SqlRepo> logger)
        {
            this._connString = connString ?? throw new ArgumentNullException(nameof(connString));
            this._logger = logger;
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
                int trash = reader.GetInt32(2);
                int load = reader.GetInt32(3);
                int moves = reader.GetInt32(4);

                playerList.Add(new(name, id, trash, load, moves));
            }
            await connect.CloseAsync();

            _logger.LogInformation("Executed: SqlRepo.AllPlayers");
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

            string cmdText = @"INSERT INTO ProjOne.Player (PlayerName, Trash, Load, Moves) VALUES (@name, 0, 0, 0);";
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
                int trash = reader.GetInt32(2);
                int load = reader.GetInt32(3);
                int moves = reader.GetInt32(4);
                currentPlayer = new(name, id, trash, load, moves);
            }
            await connect.CloseAsync();
            return currentPlayer;
        }
        public async Task AddPlayerMove(int playerID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"UPDATE ProjOne.Player SET Moves=Moves+1 WHERE PlayerID={playerID};";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.ExecuteNonQuery();
            await connect.CloseAsync();
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
        public async Task<string> FillAllRooms(int playerID)
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
                cmdText = $"INSERT INTO ProjOne.Player{playerID}RoomItems(RoomID, ItemID1, ItemID2, ItemID3) VALUES ({i}, FLOOR(RAND() * (14 - 0 + 1)) + 0, FLOOR(RAND() * (14 - 0 + 1)) + 0, FLOOR(RAND() * (14 - 0 + 1)) + 0);";
                using SqlCommand cmd2 = new(cmdText, connect2);
                cmd2.ExecuteNonQuery();
            }
            await connect2.CloseAsync();
            return "Rooms Filled!";
        }

        public async Task<Room> GetRoom(int roomID)
        {
            Room currentRoom = new Room();
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText = $"SELECT * FROM ProjOne.Room WHERE RoomID={roomID};";
            using SqlCommand cmd = new(cmdText, connect);

            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {                
                string roomName = reader.GetString(1);
                string roomDetails = reader.GetString(2);
                int adjRoom1 = reader.GetInt32(3);
                int adjRoom2 = reader.GetInt32(4);
                int adjRoom3 = reader.GetInt32(5);
                currentRoom = new(roomID, roomName, roomDetails, adjRoom1, adjRoom2, adjRoom3);
            }
            await connect.CloseAsync();
            return currentRoom;

        }
        public async Task<List<Room>> GetAdjRoomNames(List<int> roomIDs)
        {
            List<Room> adjRooms = new List<Room>();
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();
            foreach (int i in roomIDs)
            { 
                string cmdText = $"SELECT * FROM ProjOne.Room WHERE RoomID={i};";
                using SqlCommand cmd = new(cmdText, connect);

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string roomName = reader.GetString(1);
                    int roomID = reader.GetInt32(0);                  
                    adjRooms.Add(new(roomName, roomID));
                }
            }
            await connect.CloseAsync();
            return adjRooms;
        }
        public async Task<List<Item>> GetRoomInventory(int roomID, int playerID)
        {
            List<Item> roomInventory = new List<Item>();
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            for (int i = 1; i < 4; i++)
            { 
                string cmdText = $"SELECT * FROM ProjOne.Items INNER JOIN ProjOne.Player{playerID}RoomItems  ON Items.ItemID = Player{playerID}RoomItems.ItemID{i} WHERE ProjOne.Player{playerID}RoomItems.RoomID = {roomID};";
                using SqlCommand cmd = new(cmdText, connect); 

                using SqlDataReader reader =cmd.ExecuteReader();
                while(reader.Read())
                {
                    int itemID = reader.GetInt32(0);
                    string itemName = reader.GetString(1);
                    int itemWeight = reader.GetInt32(2);
                    roomInventory.Add(new(itemID, itemName, itemWeight));
                }          
            }
            await connect.CloseAsync();
            return roomInventory;
        }

        public async Task CreatePlayerItemTable(int playerID)
        {
            using SqlConnection connect = new SqlConnection(this._connString);
            await connect.OpenAsync();

            string cmdText =$"CREATE TABLE ProjOne.Player{playerID}RoomItems(RoomID INT PRIMARY KEY CONSTRAINT FK_Player{playerID}_RoomID_Inventory FOREIGN KEY(RoomID) REFERENCES ProjOne.Room(RoomID) ON DELETE CASCADE, ItemID1 INT, ItemID2 INT, ItemID3 INT,);";
            using SqlCommand cmd = new(cmdText, connect);
            cmd.ExecuteNonQuery();
            await connect.CloseAsync();
        }

    }
}