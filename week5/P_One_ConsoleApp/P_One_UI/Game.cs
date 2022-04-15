using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using P_One_UI.DTOs;

namespace P_One_UI
{
    public class Game
    {
        private HttpClient client { get; set; }
        private string? gPlayerName { get; set; }
        private int gRoom { get; set; }
        private int gPlayerID { get; set; }
        private int gTrash { get; set; }
        private int gLoad { get; set; }
        private int gMoves { get; set; }

        public Game(HttpClient client)
        {
            this.client = client;
        }

        public async Task StartGame()
        {

            Console.WriteLine("Congratulation on your new job cleaning houses!\nPlease enter your name, so we can pay you.");
            await ListAllPlayers();
            string input = Console.ReadLine();          
            if (input == "999")
            {
                Console.WriteLine("Enter the player number to delete.");
                Int32.TryParse(Console.ReadLine(), out int delete);
                gPlayerID = delete;
                DeletePlayer(gPlayerID);
                Console.Clear();
                await StartGame();
            }
            else
            {
                await NamePlayer();
                PlayerDTO current = await GetPlayer(gPlayerID);
                SetCurrentPlayerStats(current);
                await CreatePlayerTable(gPlayerID);
                Console.Clear();
                //await EmptyRoom();
                await EnterHouse();
            }


        }
        public async Task EnterHouse()
        {
            gRoom = 1;
            await FillRooms(gPlayerID);
            await ChangeRoom(gRoom);

        }
        public async Task ChangeRoom(int gRoom)
        {
            Console.Clear();
            GameHeader();
            RoomDTO currentRoom = await GetRoom(gRoom);
            int[] roomIDs = new int[] { currentRoom.adjRoom1, currentRoom.adjRoom2, currentRoom.adjRoom3 };
            List<RoomDTO> otherRooms = await GetRooms(roomIDs);
            List<ItemDTO> roomTrash = await GetRoomInv(gRoom, gPlayerID);
            FormatRoomDescription(currentRoom, otherRooms, roomTrash);
            //choose to move || to pick up trash || look at inventory//
            gRoom = await ChooseNextRoom(currentRoom);
            await ChangeRoom(gRoom);
        }
        public async Task<int> ChooseNextRoom(RoomDTO currentRoom)
        {
            int gRoom = currentRoom.roomID;
            if (Int32.TryParse(Console.ReadLine(), out int newRoom))
            {
                foreach (int i in currentRoom.AdjRooms())
                {
                    if (i == newRoom && i != 0)
                    { 
                        gRoom = i;
                        gMoves++;
                        await UpdatePlayerMoves(gPlayerID);
                        break; 
                    }
                }
            }
            return gRoom;
        }
        public void GameHeader()
        {
            //ADD total trash
            StringBuilder sb = new StringBuilder();
            sb.Append($"{gPlayerName} TRASH-{gTrash}, LOAD-{gLoad}, MOVES-{gMoves}\n");
            Console.WriteLine(sb.ToString());
        }
        public void SetCurrentPlayerStats(PlayerDTO current)
        {
            gPlayerID = current.playerID;
            gPlayerName = current.playerName;
            gTrash = current.trash;
            gLoad = current.load;
            gMoves = current.moves;
        }
        public void FormatRoomDescription(RoomDTO currentRoom, List<RoomDTO> otherRooms, List<ItemDTO> roomTrash)
        {
            Console.WriteLine($"You stand in the {currentRoom.roomName}.\n{currentRoom.roomDescription}");
            if (roomTrash == null)
            { Console.WriteLine("This room is clean!"); }
            else
            { Console.WriteLine("On the floor there is trash from whoever used to live here.\n\nThe trash includes:"); }
            foreach (ItemDTO i in roomTrash)
            {
                if (i.itemName != null)
                    Console.WriteLine($"\t{i.itemName}");
            }
            int doors = 0;
            foreach (RoomDTO r in otherRooms)
            {
                if (r.roomName != null)
                { doors++; }
            }
            Console.WriteLine($"\nYou see {doors} other door(s) leading to:");
            foreach (RoomDTO r in otherRooms)
            {
                if (r.roomName != null)
                    Console.WriteLine($"\t[{r.roomID}] - The {r.roomName}");
            }
            Console.WriteLine("\nEnter room id to change rooms.");
        }
        public async Task ListAllPlayers()
        {
            var information = "";
            HttpResponseMessage response = await client.GetAsync($"player/all");
            if (response.IsSuccessStatusCode) 
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            List<string> allPlayers = JsonSerializer.Deserialize<List<string>>(information);

            foreach (string p in allPlayers)
            {
                Console.WriteLine(p);
            }
        }
        public async Task<PlayerDTO> GetPlayer(int playerID)
        {
            var information = "";
            HttpResponseMessage response = await client.GetAsync($"player/current?playerID={playerID}");
            if (response.IsSuccessStatusCode)
            {              
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            PlayerDTO current = JsonSerializer.Deserialize<PlayerDTO>(information);
            return current;
        }
        public async Task DeletePlayer(int playerID)
        {

            var information = "";
            HttpResponseMessage response = await client.DeleteAsync($"player/delete?playerID={playerID}");
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task NamePlayer()
        {
            Console.WriteLine("Enter a name for your character!");
            string newPlayer = Console.ReadLine();
            await CreatePlayer(newPlayer);         
        } 
        public async Task CreatePlayer(string newPlayer)
        {
            var information = "";
            HttpResponseMessage response = await client.PostAsJsonAsync($"player/create", newPlayer);
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1}) {2}", (int)response.StatusCode, response.ReasonPhrase, response.Headers);
            }
        }
        public async Task UpdatePlayerMoves(int gPlayerID)
        {
            var information = "";         
            HttpResponseMessage response = await client.PutAsJsonAsync($"player/current/moves", gPlayerID);
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task EmptyRoom()
        {
            var information = "";
            HttpResponseMessage response = await client.DeleteAsync($"room/inventory/delete");
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task CreatePlayerTable(int gPlayerID)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"player/create/table", gPlayerID);
            if (response.IsSuccessStatusCode)
            {
                var information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task FillRooms(int gPlayerID)
        {
            var information = "";
            HttpResponseMessage response = await client.PostAsJsonAsync($"/room/inventory/fill", gPlayerID);
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task<RoomDTO> GetRoom(int gRoom)
        {
            var information = "";
            HttpResponseMessage response = await client.GetAsync($"room/current?roomID={gRoom}");
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            RoomDTO currentRoom = JsonSerializer.Deserialize<RoomDTO>(information);
            return currentRoom;
        }
        public async Task<List<RoomDTO>> GetRooms(int[]adjRoomIDs)
        {
            var information = "";
            HttpResponseMessage response = await client.GetAsync($"room/adjacent?roomIDs={adjRoomIDs[0]}&roomIDs={adjRoomIDs[1]}&roomIDs={adjRoomIDs[2]}");
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            List<RoomDTO> otherRooms = JsonSerializer.Deserialize<List<RoomDTO>>(information);
            return otherRooms;
        }
        public async Task<List<ItemDTO>> GetRoomInv(int gRoom, int gPlayerID)
        {
            int[] gRgPID = new int[] {gRoom,gPlayerID};
            var information = "";
            HttpResponseMessage response = await client.GetAsync($"room/current/inventory?r_pID={gRgPID[0]}&r_pID={gRgPID[1]}");
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            List<ItemDTO> roomInventory = JsonSerializer.Deserialize<List<ItemDTO>>(information);
            return roomInventory;
        }
    }
}