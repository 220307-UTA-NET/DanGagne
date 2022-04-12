
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
        Task FillAllRooms();
    }
}
