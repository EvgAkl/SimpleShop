using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Controllers;


namespace SimpleShop.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Name = "Game1" },
                new Game { Id = 2, Name = "Game2" },
                new Game { Id = 3, Name = "Game3" },
                new Game { Id = 4, Name = "Game4" },
                new Game { Id = 5, Name = "Game5" }
            });

            AdminController controller = new AdminController(mock.Object);
            // Act
            List<Game> result = ((IEnumerable<Game>)controller.Index().ViewData.Model).ToList();
            // Assert
            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Game1", result[0].Name);
            Assert.AreEqual("Game2", result[1].Name);
            Assert.AreEqual("Game3", result[2].Name);
        } // Index_Contains_All_Games()

    } // end class

} // end ndamespace
