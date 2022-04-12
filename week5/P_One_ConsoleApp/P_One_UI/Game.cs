using System.Text;
using System.Text.Json;
using P_One_UI.DTOs;

namespace P_One_UI
{
    public class Game
    {
        private HttpClient client { get; set; }
        private string? gPlayerName { get; set; }
        private int gPlayerID { get; set; }
        private int gHP { get; set; }
        private int gSTR { get; set; }
        private int gDEX { get; set; }

        public Game(HttpClient client)
        {
            this.client = client;
        }

        public async Task StartGame()
        {

            Console.WriteLine("Welcome to Dan's Game.\nCreate a character or resume a previous game..\n");
            Console.WriteLine("[0] - Create a new character!\n");
            await ListAllPlayers();
            if(Int32.TryParse(Console.ReadLine(), out int input))
            { 
                gPlayerID = input; 
            }
            else
            {
                Console.WriteLine("Invalid input. Game exiting.");
                return;
            }
            if (gPlayerID == 0)
            {
                NamePlayer();
                GetPlayerName(gPlayerID);
                SetCurrentPlayerStats(gPlayerID);
                Console.Clear();
                LevelOne();
            }
            else if (gPlayerID == 999)
            {
                Console.WriteLine("Enter the player number to delete.");
                Int32.TryParse(Console.ReadLine(), out input);
                gPlayerID=input;
                DeletePlayer(gPlayerID);
                Console.Clear();
                await StartGame();
            }
            else
            {
                GetPlayerName(gPlayerID);
                SetCurrentPlayerStats(gPlayerID);
                Console.Clear();
                LevelOne();
            }


        }
        public async Task LevelOne()
        {
            GameHeader();           
            await FillRooms();
            //DescribeRoom();
            Console.ReadLine();
        }
        public async Task ListAllPlayers()
        {
            var information = "";
            await client.GetAsync($"player/all");
            HttpResponseMessage response = client.GetAsync($"player/all").Result;
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
        public void GetPlayerName(int playerID)
        {
            var information = "";
            HttpResponseMessage response = client.GetAsync($"player/current/name?playerID={playerID}").Result;
            if (response.IsSuccessStatusCode)
            {              
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            gPlayerName = JsonSerializer.Deserialize<string>(information);
        }
        public void DeletePlayer(int playerID)
        {

            var information = "";
            HttpResponseMessage response = client.DeleteAsync($"player/delete?playerID={playerID}").Result;
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public void NamePlayer()
        {
            Console.WriteLine("Enter a name for your character!");
            string newPlayer = Console.ReadLine();
            CreatePlayer(newPlayer);
        }
        public void SetCurrentPlayerStats(int playerID)
        {
            var information = "";
            HttpResponseMessage response = client.GetAsync($"player/current/stats?playerID={playerID}").Result;
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            List<int>stats = JsonSerializer.Deserialize<List<int>>(information);
            gHP = stats[0];
            gSTR = stats[1];
            gDEX = stats[2];
        }
        public void GameHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{gPlayerName} HP-{gHP}, STR-{gSTR}, DEX-{gDEX}");
            Console.WriteLine(sb.ToString());
        }
        public void CreatePlayer(string newPlayer)
        {
            var information = "";
            var c = new StringContent(newPlayer, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync($"player/create?playerName={newPlayer}", c).Result;
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("{0} ({1}) {2}", (int)response.StatusCode, response.ReasonPhrase, response.Headers);
                Console.ReadLine();
            }
        }
        public async Task EmptyRoom()
        {
            var information = "";
            await client.DeleteAsync($"room/inventory/delete");
            HttpResponseMessage response = client.DeleteAsync($"room/inventory/delete").Result;
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
        public async Task FillRooms()
        {
            var information = "";
            var c = new StringContent("", UnicodeEncoding.UTF8, "application/json");
            await client.PutAsync($"room/inventory/fill",c);
            HttpResponseMessage response = client.PutAsync($"room/inventory/fill",c).Result;
            if (response.IsSuccessStatusCode)
            {
                information = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}