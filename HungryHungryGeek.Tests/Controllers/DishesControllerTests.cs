using System.Web.Mvc;
using HungryHungryGeek.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HungryHungryGeek.Tests.Controllers
{
    [TestClass()]
    public class DishesControllerTests
    {
        [TestMethod()]
        public void DetailsTest()
        {
            // Arrange
            DishesController controller = new DishesController();

            // Act
            var result = controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        
    }
}