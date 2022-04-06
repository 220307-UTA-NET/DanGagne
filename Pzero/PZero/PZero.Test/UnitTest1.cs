using Xunit;
using PZero.Classes;
using PZero.Database;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace PZero.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Customer_CreateCustomer_ValidCustomer()
        {
            //ARRANGE
            string a = "John";
            string b = "Smith";
            int c = 123;
            int d = 456; 
            
            //ACT
            Customer testCustomer = new Customer("John", "Smith", 123, 456);

            //ASSERT
            Assert.Equal(a, testCustomer.fname);
            Assert.Equal(b, testCustomer.lname);
            Assert.Equal(c, testCustomer.custID);
            Assert.Equal(d, testCustomer.storeID);          
        }

        [Fact]
        public void Store_AddNewCustomer_CustomerCreated()
        {
            //ARRANGE
            Mock<IRepo> mockRepo = new();
            Customer expected = new Customer("testfname", "testlname", 3, 4);
            mockRepo.Setup(x => x.AddNewCustomer("testfname", "testlname", 4)).Returns(expected);
            var store = new App.Store(mockRepo.Object);

            //ACT
            Customer test = store.CallAddNewCustomer("testfname", "testlname", 4);

            //ASSERT
            Assert.Equal(expected.fname, test.fname);
            Assert.Equal(expected.lname, test.lname);
            Assert.Equal(expected.storeID, test.storeID);
            Assert.Equal(expected.custID, test.custID);
        }

        [Fact]
        public void Store_StoreLogin_GetCustomerAccount()
        {
            //ARRANGE
            Mock<IRepo> mockRepo = new();
            Customer expected1 = new Customer("testfname", "testlname",10,20);
            Customer expected2 = new Customer("testfname2", "testlname2", 100, 200);
            mockRepo.Setup(x => x.CustomerLogin(10)).Returns(expected1);
            mockRepo.Setup(x => x.CustomerLogin(100)).Returns(expected2);
            var store = new App.Store(mockRepo.Object);

            //ACT
            Customer actual1 = store.StoreLogin(10);
            Customer actual2 = store.StoreLogin(100);

            //ASSERT
            Assert.Equal(expected1.fname, actual1.fname);
            Assert.Equal(expected2.fname, actual2.fname); 
            Assert.Equal(expected1.lname, actual1.lname);   
            Assert.Equal(expected2.lname, actual2.lname);
            Assert.Equal(expected1.custID, actual1.custID);
            Assert.Equal(expected2.custID, actual2.custID);
            Assert.Equal(expected1.storeID, actual1.storeID);
            Assert.Equal(expected2.storeID, actual2.storeID);
        }

         [Fact]

        public void Store_GetOneItem_ValidItem()
        {
            //ARRANGE
            Mock<IRepo> mockRepo = new();
            Item expected = new Item("testItem", 100, 200, "imaginary", 300, 400);
            mockRepo.Setup(x => x.FindOneItem(300, 400)).Returns(expected);
            var store = new App.Store(mockRepo.Object);

            //ACT
            Item test = store.GetOneItem(300, 400);           

            //ASSERT
            Assert.Equal(expected.itemName, test.itemName);
            Assert.Equal(expected.price, test.price);
            Assert.Equal(expected.quantity, test.quantity);
            Assert.Equal(expected.material, test.material);
            Assert.Equal(expected.sku, test.sku);
            Assert.Equal(expected.storeID, test.storeID);
            
            
        }

        [Fact]
        public void Store_StoreName_ValidStoreName()
        {
            //ARRANGE
            Mock<IRepo> mockRepo = new();
            
            mockRepo.Setup(x => x.GetStoreName(20)).Returns("TestStore");
            var store = new App.Store(mockRepo.Object);

            //ACT

            string storeName = store.StoreName(20);

            //ASSERT

            string expected = "TestStore";
            Assert.Equal(expected, storeName);
        }

        [Fact]
        public void Item_CreateItem_ValidItem()
        {
            //ARRANGE
            string a = "testItem";
            decimal b = 50;
            int c = 1;
            int d = 2;  

            //ACT
            Item item1 = new Item("testItem", 50, 1, 2);

            //ASSERT
            Assert.Equal(a, item1.itemName);
            Assert.Equal(b, item1.price);
            Assert.Equal(c, item1.sku);
            Assert.Equal(d, item1.quantity);

        }
        [Fact]
        public void Store_DeleteItem_ItemQuantityDecrease()
        {
            //ARRANGE
            Mock<IRepo> mockRepo = new();
            Item expected = new Item("testItem", 100, 200, "imaginary", 300, 400);
            Item expected2 = new Item("testItem", 100, 100, "imaginary", 300, 400);
            mockRepo.Setup(x => x.DeleteItem(expected, 400)).Returns(expected.quantity-expected.quantity);
            mockRepo.Setup(x => x.DeleteItem(expected2, 400)).Returns(expected.quantity - expected2.quantity);
            var store = new App.Store(mockRepo.Object);


            //ACT
            int actualquantity = store.RemoveItem(expected, 400);
            int actualquantity2 = store.RemoveItem(expected2, 400);

            //ASSERT 
            Assert.Equal(0, actualquantity);
            Assert.Equal(100, actualquantity2);
        }
            
        [Fact]
        public void ShoppingCart_AddToCart_ItemsAdded()
        {
            //ARRANGE
            Item item1 = new Item("testItem1", 50, 1, 2);
            Item item2 = new Item("testItem2", 75, 2, 3);

            List<Item> fakeShoppingCart = new List<Item>();
            fakeShoppingCart.Add(item1);
            fakeShoppingCart.Add(item2);

            //ACT
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(item1);
            shoppingCart.AddToCart(item2);

            //ASSERT
            Assert.Equal(fakeShoppingCart.Count, shoppingCart.GetCart().Count);
            Assert.Equal(fakeShoppingCart.Contains(item1), shoppingCart.GetCart().Contains(item1));
            Assert.Equal(fakeShoppingCart.Contains(item2), shoppingCart.GetCart().Contains(item2));
        }
        [Fact]

        public void ShoppingCart_EmptyCart_AllItemsRemoved()
        {
            //ARRANGE
            Item item1 = new Item("testItem1", 50, 1, 2);
            Item item2 = new Item("testItem2", 75, 2, 3);
            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.AddToCart(item1);
            shoppingCart.AddToCart(item2);

            //ACT
            shoppingCart.EmptyCart();

            //ASSERT
            Assert.Empty(shoppingCart.GetCart());
        }

        [Fact]

        public void Store_PopulateOrderHistory_GetCompletedOrders()
        {
            //ARRANGE
            Item item1 = new Item("testItem1", 50, 1, 2);
            Item item2 = new Item("testItem2", 75, 2, 3);

            List<Item> testOrderHistory = new List<Item>();
            testOrderHistory.Add(item1);
            testOrderHistory.Add(item2);

            Mock<IRepo> mockRepo = new();
            mockRepo.Setup(x => x.PopulateOrderHistory(150)).Returns(testOrderHistory);
            var store = new App.Store(mockRepo.Object);

            //ACT
            IEnumerable<Item> actualOrderHistory = store.CallPopulateOrderHistory(150);
            var actualOrderHistoryList =actualOrderHistory.ToList();

            //ASSERT
            Assert.Equal(testOrderHistory.Count, actualOrderHistoryList.Count);
            Assert.Equal(testOrderHistory.Contains(item1), actualOrderHistoryList.Contains(item1));
            Assert.Equal(testOrderHistory.Contains(item2), actualOrderHistoryList.Contains(item2));


        }
    }
}