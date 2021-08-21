using IPLManagementSystemDoL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using IPLManagementSystemDL;
namespace IPLManagementSystemUnitTesting
{
    /*To perform a unit test call the Data Layer and Domain Layer to send the data and operations execute the data perform all the operations*/
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Users_Insert_forAdmin()
        {
            //Arrange
            User insertUsersTest = new User()
            {
                UserId = 1,
                Username = "akhilchukkala999@gmail.com",
                Password = "ahkhi123",
                Firstname = "Akhil",
                Lastname = "Chukkala"
            };
            DataLayerClass dataLayerClass = new DataLayerClass();
            bool result = dataLayerClass.InsertUserDl(insertUsersTest);
            Assert.AreEqual(false, result,"Another Error");
        }
    }
}
