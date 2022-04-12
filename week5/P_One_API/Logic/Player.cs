using System.Text;

namespace Logic
{
    public class Player
    {    
        internal string? playerName { get; set; }
        public int playerID { get; set; }
        public int hp { get; set; }
        public int str { get; set; }
        public int dex { get; set; }

        public Player() { }

        public Player(string playerName)
        {
            this.playerName = playerName;
        }
        public Player(string playerName, int playerID, int hp, int str, int dex)
        {
            this.playerName = playerName;
            this.playerID = playerID;
            this.hp = hp;
            this.str = str;
            this.dex = dex;
        }

        public string? GetName()
        {
            return playerName;
        }
        public string PlayerInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"[{playerID}] - {playerName} \n");
            return sb.ToString();
        }

        public List<int> GetStats()
        {
            List<int> stats = new List<int>();
            stats.Add(hp);
            stats.Add(str);
            stats.Add(dex);
            return stats;
        }

    }
}