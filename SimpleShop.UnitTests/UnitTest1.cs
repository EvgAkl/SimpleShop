using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;
using SimpleShop.WebUI.Controllers;
using SimpleShop.WebUI.Models;
using SimpleShop.WebUI.HtmlHelpers;

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
            GamesListViewModel result = (GamesListViewModel)controller.List(2).Model;
            // Assert
            List<Game> games = result.Games.ToList();
            Assert.AreEqual(games[0].Name, "Game 4");
            Assert.AreEqual(games[01].Name, "Game 5");
        } // end Can_Paginate()


        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // Arrange
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo { CurrentPage = 2, TotalItems = 28, ItemsPerPage = 10};

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        } // end Can_Generate_Page_Links()


        [TestMethod]
        public void Can_Send_Paginate_View_Model()
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
            GamesListViewModel result = (GamesListViewModel)controller.List(2).Model;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.totalPages, 2);
        } // end Can_Send_Paginate_View_Model()






    } // end Test class

} // end namespace
