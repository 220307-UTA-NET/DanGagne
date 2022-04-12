using System.Text.Json;
using Database;
using Logic;
using Microsoft.AspNetCore.Mvc;



namespace P_One_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class P_One_Controller : ControllerBase
    {
        //private static readonly List<Player> _players = new() { };


        //private readonly string? connString; //= System.IO.File.ReadAllText(@"\Revature\DanGagne\ConnectionString\SchoolStringDB.txt");


        private readonly ILogger<P_One_Controller> _logger;
        private readonly IRepo _repo;

        public P_One_Controller(ILogger<P_One_Controller> logger, IRepo repo)
        {
            _logger = logger;
            _repo = repo;
        }

        [HttpGet("/player/all")]
        public async Task<ContentResult> GetAllPlayersAsync()
        {
            List<string> playerinfo = new List<string>();
            foreach (Player p in await _repo.AllPlayers())
            {
                playerinfo.Add(p.PlayerInfo());
            }

            string json = JsonSerializer.Serialize(playerinfo);

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };

        }

        [HttpGet("/player/current")]
        public async Task<ContentResult> GetOnePlayerAsync(int playerID)
        {
            var current = await _repo.GetPlayer(playerID);          
            string json = JsonSerializer.Serialize(current);

            return new ContentResult()
            {
                StatusCode = 200, 
                ContentType = "application/json",
                Content = json
            };
        }
        [HttpGet("/player/current/name")]
        public async Task<ContentResult> GetOnePlayerNameAsync(int playerID)
        {
            var current = await _repo.GetPlayer(playerID);
            string json = JsonSerializer.Serialize(current.GetName());

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
        }
        [HttpGet("/player/current/stats")]
        public async Task<ContentResult> GetOnePlayerStatsAsync(int playerID)
        {
            var current = await _repo.GetPlayer(playerID);
            string json = JsonSerializer.Serialize(current.GetStats());

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpPost("/player/create")]
        public async Task<ContentResult> AddPlayerAsync(string playerName)
        {
            Player player = new Player(playerName);
            string json = await _repo.NewPlayer(player);

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpDelete("/player/delete")]
        public async Task<ContentResult> DeletePlayerAsync(int playerID)
        {
            //SqlRepo repo = new SqlRepo(connString);
            string json = await _repo.RemovePlayer(playerID);

            return new ContentResult()
            {
                StatusCode = 204,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpDelete("/room/inventory/delete")]
        public async Task<ContentResult> DeleteRoomInventory()
        {
            await _repo.EmptyAllRooms();

            return new ContentResult()
            {
                StatusCode = 204             
            };
        }
        [HttpPut("/room/inventory/fill")]
        public async Task<ContentResult> FillAllRoomsInventory()
        {
            await _repo.FillAllRooms();

            return new ContentResult()
            {
                StatusCode = 201
            };
        }
       
        
    }
}