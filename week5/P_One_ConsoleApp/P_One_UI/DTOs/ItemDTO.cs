using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_One_UI.DTOs
{
    internal class ItemDTO
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public int itemWeight { get; set; }
        public int hp { get; set; }
        public int str { get; set; }
        public int dex { get; set; }
    }
}
