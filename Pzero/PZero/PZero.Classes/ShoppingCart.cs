using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZero.Classes
{
    public class ShoppingCart
    {
        //fields
        List<Item> itemsInCart { get; set; } = new List<Item>();

        //Constructor
        public ShoppingCart() { }
        public ShoppingCart(List<Item> itemsInCart)
        {
            this.itemsInCart = itemsInCart;
        }

        //Methods

        public void AddToCart(Item add)
        {
            if (add.itemName == null)
            {
                Console.WriteLine("Invalid item or item quantity.\n");
                Thread.Sleep(1600);
                Console.Clear();
            }
            else if(itemsInCart.Exists(x => x.sku == add.sku))
            {
                foreach(Item i in itemsInCart)
                {
                    if(i.sku == add.sku)
                    { i.quantity=i.quantity+add.quantity; }
                }
                Console.WriteLine("Quantity of item updated shopping cart.");
                Thread.Sleep(900);
            }
            else
            { 
                this.itemsInCart.Add(add);
                Console.WriteLine(add.quantity+" item(s) added to shopping cart.");
                Thread.Sleep(900);
            }
        }
        public string RemoveFromCart()
        {
            Console.WriteLine("\nEnter the five digit SKU for the item you wish to remove.");
            if(Int32.TryParse(Console.ReadLine(), out int remove))
            {
                foreach(Item item in this.itemsInCart)
                {
                    if(item.sku == remove)
                    {
                        int i = 1;
                        if (item.quantity > 1)
                        {
                            Console.WriteLine("How many are you removing?");
                            if (Int32.TryParse(Console.ReadLine(), out int quantity))
                            {
                                
                                for (i=0; i < quantity; i++)
                                {
                                    item.quantity--;

                                    if (item.quantity == 0)
                                    {
                                        i++;
                                        this.itemsInCart.Remove(item);
                                        Console.Clear();
                                        return $"{i} {item.itemName}(s) removed from cart.\nReturning to main menu";
                                    }
                                }
                            }
                            else
                            {
                                Console.Clear();
                                return "Invalid entry.\nReturning to menu.";
                            }
                                 
                        }
                        else
                        {
                            this.itemsInCart.Remove(item);                          
                        }
                        Console.Clear();
                        return $"{i} {item.itemName}(s) removed from cart.\nReturning to main menu";
                    }
                }    
            }
            Console.Clear();
            return "Item not found in cart.\nReturning to main menu.";
 
        }
        public void EmptyCart()
        { 
            itemsInCart.Clear();
        //Write purchases to XML with CustomerName
        //Empty all items in shopping cart list
        }
        public void ViewCart()
        {
            Console.WriteLine("**SHOPPING CART**");
            if (itemsInCart.Count ==0)
            { Console.WriteLine("\n******EMPTY******"); }
            else
            {
                foreach (Item item in this.itemsInCart)
                {
                    Console.WriteLine(item.ItemAdded());
                }
            }
            
        }
        public List<Item> GetCart()
        {
            return this.itemsInCart;
        }
        public decimal GetCartTotalPrice()
        {
            decimal total = 0;
            foreach(Item i in this.itemsInCart)
            {
                total += i.GetPrice() * i.GetQuantity();
            }
            return total;
        }
    }
}
