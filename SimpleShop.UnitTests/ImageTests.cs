using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Controllers;

namespace SimpleShop.UnitTests
{
    [TestClass]
    class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange
            Game game = new Game
            {
                Id = 2,
                Name = "Game2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> { new Game { Id = 1, Name = "Game1" }, game, new Game { Id = 3, Name = "Game3" } }.AsQueryable());

            GameController controller = new GameController(mock.Object);

            // Act
            ActionResult result = controller.GetImage(2);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(game.ImageMimeType, ((FileResult)result).ContentType);
        } // end Can_Retrieve_Image_Data()

        [TestMethod]
        public void Cannot_Retrieve_Image_For_Invalid_ID()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>{
                new Game { Id = 1, Name = "Game1"},
                new Game { Id = 2, Name = "Game2" }
            }.AsQueryable());

            GameController controller = new GameController(mock.Object);

            // Act
            ActionResult result = controller.GetImage(10);

            // Assert
            Assert.IsNull(result);
        } // end Cannot_Retrieve_Image_For_Invalid_ID()

    } // end class
} // end namespace
