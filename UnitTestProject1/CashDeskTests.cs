using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using  CrmBl.Model;
using System.Collections.Generic;

namespace CrmBl.Model.Tests
{
    [TestClass]
    public class CashDeskTests
    {
        [TestMethod]
        public void CashDeskTest()
        {
            //arrange
           var customer1 = new Customer()
            {
               Name = "TestUser1",
               CustomerId = 2
            };
            var customer2 = new Customer()
            {
                Name = "TestUser2",
                CustomerId = 2
            };
            var seller = new Seller()
            {
                Name = "Test Seller",
                SellerId = 1
            };
            var product1 = new Product()
            {
                ProductId = 1,
                Name = "pr1",
                Price = 100,
                Count = 10
            };
            var product2 = new Product()
            {
                ProductId = 2,
                Name = "pr2",
                Price = 200,
                Count = 20
            };
            var cart1 = new Cart(customer1);
            cart1.Add(product1);
            cart1.Add(product1);
            cart1.Add(product2);
            var cart2 = new Cart(customer2);
            cart1.Add(product2);
            cart1.Add(product1);
            cart1.Add(product2);
            cart1.Add(product2);
            cart1.Add(product1);
            cart1.Add(product2);

            var cashDesk = new CashDesk(1, seller);
            cashDesk.Enqueue(cart1);
            cashDesk.Enqueue(cart2);

            //act


            //assert

        }
    }
}
