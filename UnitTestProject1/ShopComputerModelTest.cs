using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using  CrmBl.Model;
using System.Collections.Generic;
using System.Threading;

namespace CrmBl.Model.Tests
{
    [TestClass]
    public class ShopComputerModelTest
    {
        [TestMethod]
        public void StartTest()
        {
            //arrange
          var model = new ShopComputerModel();


            //act
            model.Start();

            Thread.Sleep(10000);

            //assert
          


        }
    }
}
