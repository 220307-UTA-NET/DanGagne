using Xunit;
using PZero.Classes;
//using PZero.Database;

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
            
            //ACT
            Customer testCustomer = new Customer("John", "Smith");
            //ASSERT
            Assert.Equal(a, testCustomer.fname);
            Assert.Equal(b, testCustomer.lname);
            
        }
    }
}