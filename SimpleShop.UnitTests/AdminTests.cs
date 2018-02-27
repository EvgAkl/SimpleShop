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
        } // end Index_Contains_All_Games()

        [TestMethod]
        public void Can_Edit_Game()
        {
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
            Game game1 = controller.Edit(1).ViewData.Model as Game;
            Game game2 = controller.Edit(2).ViewData.Model as Game;
            Game game3 = controller.Edit(3).ViewData.Model as Game;
            // Assert
            Assert.AreEqual(game1.Id, 1);
            Assert.AreEqual(game2.Id, 2);
            Assert.AreEqual(game3.Id, 3);
        } // end Can_Edit_Game()

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Game()
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
            Game result = (Game)controller.Edit(6).ViewData.Model;
            // Assert
            Assert.IsNull(result);
        } // end Cannot_Edit_Nonexistent_Game()

        [TestMethod]
        public void Can_Save_Valid_Changes()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            AdminController controller = new AdminController(mock.Object);
            Game game = new Game { Name = "Test" };
            // Act
            ActionResult result = controller.Edit(game);
            // Assert
            mock.Verify(m => m.SaveGame(game));
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        } // end Can_Save_Valid_Changes()

        [TestMethod]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            AdminController controller = new AdminController(mock.Object);
            Game game = new Game { Name = "Name" };
            controller.ModelState.AddModelError("error", "error");
            // Act
            ActionResult result = controller.Edit(game);
            // Assert
            mock.Verify(m => m.SaveGame(It.IsAny<Game>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        } // end Cannot_Save_Invalid_Changes()










    } // end class

} // end ndamespace
