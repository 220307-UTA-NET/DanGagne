using Xunit;
using System;
using ProjectZero;

namespace ProjectZero.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //ARRANGE
            string a = "John";
            string b = "Smith";
            string c = "Buffalo";
            //ACT
            Customer testCustomer = new Customer("John", "Smith", "Buffalo");
            //ASSERT
            Assert.Equal(a, testCustomer.fname);
            Assert.Equal(b, testCustomer.lname);
            Assert.Equal(c, testCustomer.defaultStore);

        }
    }
}