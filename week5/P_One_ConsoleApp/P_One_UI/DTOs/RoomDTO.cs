using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_One_UI.DTOs
{
    internal class RoomDTO
    {
        public int roomID { get; set; }
        public string roomName { get; set; }
        public string roomDescription { get; set; }
        public ItemDTO item1 { get; set; }
        public ItemDTO item2 { get; set; }
        public ItemDTO item3 { get; set; }
    }
}
