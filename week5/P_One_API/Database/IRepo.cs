
using Logic;


namespace Database
{
    public interface IRepo
    {
        //List<Customer> GetCustomer(int custID);
        //List<Customer> AllCustomers();
        Task<List<Player>> AllPlayers();
        Task<string> NewPlayer(Player player);
        Task<Player> GetPlayer(int playerID);
        Task<string> RemovePlayer(int playerID);
        Task EmptyAllRooms();
        Task<string> FillAllRooms(int playerID);
        Task<Room> GetRoom(int roomID);

        Task<List<Room>> GetAdjRoomNames(List<int> roomIDs);
        Task AddPlayerMove(int playerID);
        Task<List<Item>> GetRoomInventory(int roomID, int playerID);
        Task CreatePlayerItemTable(int playerID);
    }
}
