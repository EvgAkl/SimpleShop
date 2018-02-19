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
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;
            // Assert
            List<Game> games = result.Games.ToList();
            Assert.AreEqual(games[0].Name, "Game 4");
            Assert.AreEqual(games[1].Name, "Game 5");
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
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        } // end Can_Send_Paginate_View_Model()

        [TestMethod]
        public void Can_Filter_Games()
        {
            //Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Name = "Game 1", Category="Cat1" },
                new Game { Id = 2, Name = "Game 2", Category="Cat2" },
                new Game { Id = 3, Name = "Game 3", Category="Cat1" },
                new Game { Id = 4, Name = "Game 4", Category="Cat2" },
                new Game { Id = 5, Name = "Game 5", Category="Cat3" }
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;
            // Act
            List<Game> result = ((GamesListViewModel)controller.List("Cat2", 1).Model).Games.ToList();
            // Assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Game 2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Game 4" && result[1].Category == "Cat2");
        } // end Can_Filter_Games()

        public void Can_Create_Categories()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { Id = 1, Name = "Game 1", Category = "Симулятор" },
                new Game { Id = 2, Name = "Game 2", Category = "Симулятор" },
                new Game { Id = 3, Name = "Game 3", Category = "Шутер" },
                new Game { Id = 4, Name = "Game 4", Category = "GPG" }
            });

            NavController target = new NavController(mock.Object);
            // Act
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();
            // Assert
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "RPG");
            Assert.AreEqual(result[1], "Симулятор");
            Assert.AreEqual(result[2], "Шутер");
        } // end Can_Create_Categories()

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new Game[]{
                new Game { Id = 1, Name = "Game 1", Category = "Симулятор" },
                new Game { Id = 2, Name = "Game 2", Category = "Шутер" }
            });

            NavController target = new NavController(mock.Object);
            string categoryToSelect = "Шутер";
            // Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;
            // Assert
            Assert.AreEqual(categoryToSelect, result); 
        } // end Indicates_Selected_Category()

        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>{
                new Game { Id = 1, Name = "Game 1", Category = "Cat1" },
                new Game { Id = 2, Name = "Game 2", Category = "Cat2" },
                new Game { Id = 3, Name = "Game 3", Category = "Cat1" },
                new Game { Id = 4, Name = "Game 4", Category = "Cat2" },
                new Game { Id = 5, Name = "Game 5", Category = "Cat3" }
            });

            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;
            // Act
            int res1 = ((GamesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;
            // Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        } // end Generate_Category_Specific_Game_Count()











    } // end Test class

} // end namespace
