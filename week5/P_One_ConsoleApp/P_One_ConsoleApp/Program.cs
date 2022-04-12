﻿
using P_One_UI;

namespace P_One_ConsoleApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7083/");
            Game game = new Game(client);
            await game.StartGame();
        }
    }
}
