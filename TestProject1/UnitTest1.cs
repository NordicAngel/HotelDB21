using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotelApp.Models;
using RazorPageHotelApp.Services;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void select()
        {
            //arrange
            string connectionstring = "";//removed for privacy
            HotelService hs = new HotelService(connectionstring);
                
            //act
            List<Hotel> h = hs.GetAllHotelAsync().Result;

            //assert
            //der skal ikke tjekes noget da det i act vil kaste en exception hvis det ikke virker
        }
    }
}
