using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HungryHungryGeek.Controllers;

namespace HungryHungryGeek.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Simple application for employees to order out food.", result.ViewBag.Message);
        }
    }
}
