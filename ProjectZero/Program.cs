namespace ProjectZero
{
    public class Program
    {
        static void Main()
        {
            // Console.WriteLine("Testing");
            Store store1 = new Store("Rochester");
            // Store store2 = new Store("Buffalo");
            // Store store3= new Store("Syracuse");
            // store1.AddCustomer("Dan", "Gagne");
            // store2.AddCustomer("Arron", "Pinser");
            // store1.AddCustomer("Lauren", "Hopps");
            // store1.SerializeAsXml();
            store1.AddToInventory("Table", 400);
            store1.AddToInventory("Table", 400);
            store1.AddToInventory("Desk", 600);
            store1.AddToInventory("Desk", 600);
            store1.AddToInventory("Chair", 200);
            store1.AddToInventory("Chair", 200);
            store1.AddToInventory("Chair", 200);
            store1.AddToInventory("Chair", 200);
            store1.AddToInventory("Chair", 200);
            store1.CheckInventory();
            store1.AddToShoppingCart();
            store1.AddToShoppingCart();
            store1.RemoveFromShoppingCart();
            store1.CheckOut();

        }
    }
}
