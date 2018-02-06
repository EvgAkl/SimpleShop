using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Controllers;

namespace SimpleShop.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { Id = 1, Name = "Game 1"},
                new Game { Id = 2, Name = "Game 2"},
                new Game { Id = 3, Name = "Game 3"},
                new Game { Id = 4, Name = "Game 4"},
                new Game { Id = 5, Name = "Game 5"},
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;
            // Act
            IEnumerable<Game> result = (IEnumerable<Game>)controller.List(2).Model;
            // Assert
            List<Game> games = result.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Game 4");
            Assert.AreEqual(games[1].Name, "Game 5");
        } // end Can_Paginate()




    } // end Test class

} // end namespace
