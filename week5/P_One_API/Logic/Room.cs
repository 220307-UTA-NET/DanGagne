using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Room
    {
        public int roomID { get; set; }
        public string roomName { get; set; }
        public string roomDescription { get; set; }
        public Item item1 { get; set; }
        public Item item2 { get; set; }
        public Item item3 { get; set; }

        public Room() { }

        public Room(int roomID, string roomName, string roomDescription, Item item1, Item item2, Item item3)
        {
            this.roomID = roomID;
            this.roomName = roomName;
            this.roomDescription = roomDescription;
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
        }

    }
}
