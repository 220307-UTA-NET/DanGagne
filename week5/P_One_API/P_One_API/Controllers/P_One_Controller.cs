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

            _logger.LogInformation("Get all players");

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
            _logger.LogInformation("Get one player");

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
        public async Task<ContentResult> AddPlayerAsync([FromBody] string playerName)
        {
            Player player = new Player(playerName);
            string json = await _repo.NewPlayer(player);
            _logger.LogInformation("Player created");

            return new ContentResult()
            {
                StatusCode = 201,
                ContentType = "application/json",
                Content = json
            };
        }
        [HttpPut("/player/current/moves")]
        public async Task<ContentResult> UpdatePlayerMovesAsync([FromBody] int playerID)
        {
            
            await _repo.AddPlayerMove(playerID);

            return new ContentResult()
            {
                StatusCode = 200,
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
        public async Task<ContentResult> DeleteRoomInventoryAsync()
        {
            await _repo.EmptyAllRooms();

            return new ContentResult()
            {
                StatusCode = 204
            };
        }
        [HttpPost("/room/inventory/fill")]
        public async Task<ContentResult> FillAllRoomsInventoryAsync([FromBody]int playerID)
        {
            await _repo.FillAllRooms(playerID);

            _logger.LogInformation("Player table filled");

            return new ContentResult()
            {
                StatusCode = 200,
            };
        }
        [HttpGet("/room/current/inventory")]
        public async Task<ContentResult> GetOneRoomInventoryAsync([FromQuery] int[] r_pID)
        {
            List<Item> roomInventory = await _repo.GetRoomInventory(r_pID[0], r_pID[1]);
            string json = JsonSerializer.Serialize(roomInventory);

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpGet("/room/current")]
        public async Task<ContentResult>GetRoomCurrentAsync(int roomID)
        {
            var currentRoom = await _repo.GetRoom(roomID);
            string json = JsonSerializer.Serialize(currentRoom);

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpGet("/room/adjacent")]
        public async Task<ContentResult>GetAdjRoomsAsync([FromQuery]int[] roomIDs)
        {
            List<Room> adjRooms = new List<Room>();
            foreach (int roomID in roomIDs)
            {
                Room adjRoom = await _repo.GetRoom(roomID);
                adjRooms.Add(adjRoom);
            }
            string json = JsonSerializer.Serialize(adjRooms);

            return new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json",
                Content = json
            };
        }

        [HttpPost("/player/create/table")]
        public async Task<ContentResult>MakePlayerItemTable([FromBody] int playerID)
        {
            await _repo.CreatePlayerItemTable(playerID);
            return new ContentResult()
            {
                StatusCode = 200,
            };
        }


    }
}