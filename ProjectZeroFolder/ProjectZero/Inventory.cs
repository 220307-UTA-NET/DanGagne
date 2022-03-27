using System.Xml.Serialization;

namespace ProjectZero
{
    public class Inventory
    {
        //fields
        [XmlAttribute]
        public string? item;
        [XmlAttribute]
        public double price;

        public Inventory(){}
        public Inventory(string item, double price)
        {
            this.item=item;
            this.price=price;
        }
    }
}