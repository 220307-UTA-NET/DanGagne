using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    class Item
    {
        public int itemID { get; set; }
        public string itemName { get; set; }
        public int itemWeight { get; set; }
        public int hp { get; set; }
        public int str { get; set; }
        public int dex { get; set; }

        public Item() { }
        public Item (int itemID, string itemName)
        {
            this.itemID = itemID;
            this.itemName = itemName;
        }

        public Item(int itemID, string itemName, int itemWeight, int hp, int str, int dex)
        {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemWeight = itemWeight;
            this.hp = hp;   
            this.str = str;
            this.dex = dex;
        }

    }
}
