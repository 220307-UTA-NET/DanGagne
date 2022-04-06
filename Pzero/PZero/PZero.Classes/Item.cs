using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("PZero.Test")]

namespace PZero.Classes
{
    public class Item
    {
        //Fields
        internal string? itemName { get; }
        internal int sku { get; set; }
        internal decimal price { get; set; }
        internal int storeID { get; set; }
        internal int quantity { get; set; }
        internal string? material { get; }
        internal string? purchasedate {get;}
     
        //Constructors
        public Item() { }

        public Item(string itemName, decimal price, int sku, int quantity)
        {
            this.itemName = itemName;
            this.price = price;
            this.sku = sku;
            this.quantity = quantity;
        }

        public Item(string itemName, string material, decimal price, int quantity, int sku, string purchasedate)
        {
            this.itemName = itemName;
            this.material = material;
            this.price = price;
            this.quantity = quantity;
            this.sku = sku;
            this.purchasedate = purchasedate;
        }
        public Item(string itemName, decimal price, int quantity, string material, int sku, int storeID )
        {
            this.itemName = itemName;
            this.price = price;
            this.quantity = quantity;
            this.material = material;
            this.storeID = storeID;
            this.sku = sku;
        }

        //Methods

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\n{this.itemName}({this.material}): price ${this.price} ea\n{this.quantity} available.\nSKU:{this.sku}");
            return sb.ToString();
        }

        public string OrderInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{this.itemName}({this.material}) SKU: {this.sku} \nQuantity purchased:{this.quantity} @ ${this.price}ea.\nTotal sell price ${this.price*this.quantity} \nSale Date: {this.purchasedate}");
            return sb.ToString();
        }

        public string ItemAdded()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"\n{this.itemName}({this.material}): price ${this.price} ea\nSKU:{this.sku} \n{this.quantity} in shopping cart.");
            return sb.ToString();
        }

        public int GetSku()
        {
            return this.sku;
        }

        public int GetQuantity()
        {
            return this.quantity;
        }
        public decimal GetPrice()
        {
            return this.price;
        }

    }
}
