
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Abstract;
using SimpleShop.WebUI.Controllers;
using SimpleShop.WebUI.Models;
using Moq;

namespace SimpleShop.UnitTests
{
    [TestClass]
    public class CartTests
    {
        // =============================================================
        // Cart Entity tests
        // =============================================================
        [TestMethod]
        public void Can_add_New_Lines()
        {
            // Arrange
            Game game1 = new Game { Id = 1, Name = "Game 1" };
            Game game2 = new Game { Id = 2, Name = "Game 2" };

            Cart cart = new Cart();
            // Act
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 2);
            List<CartLine> results = cart.Lines.ToList();
            // Assert
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Game, game1);
            Assert.AreEqual(results[1].Game, game2);
        } // end Can_add_New_Lines()

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange
            Game game1 = new Game { Id = 1, Name = "Game 1" };
            Game game2 = new Game { Id = 2, Name = "Game 2" };
            Game game3 = new Game { Id = 3, Name = "Game 3" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);
            // Act
            cart.RemoveLine(game2);
            // Assert
            Assert.AreEqual(cart.Lines.Where(c => c.Game == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        } // end Can_Remove_Line()

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrage
            Game game1 = new Game { Id = 1, Name = "Game 1", Price = 100 };
            Game game2 = new Game { Id = 2, Name = "Game 2", Price = 55 };

            Cart cart = new Cart();
            // Act
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            decimal result = cart.ComputeTotalValue();
            // Assert
            Assert.AreEqual(result, 655);
        } // end Calculate_Cart_Total()

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Assert
            Game game1 = new Game { Id = 1, Name = "Game 1", Price = 100 };
            Game game2 = new Game { Id = 2, Name = "Game 2", Price = 55 };

            Cart cart = new Cart();
            // Act
            cart.AddItem(game1, 1);
            cart.AddItem(game2, 2);
            cart.AddItem(game1, 5);
            cart.Clear();
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 0);
        } // end Can_Clear_Contents()

        // =============================================================
        // CartController tests
        // =============================================================

        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { Id = 1, Name = "Game 1", Category = "Cat1" }
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object);
            // Act
            controller.AddToCart(cart, 1, null);
            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Game.Id, 1);
        } // end Can_Add_To_Cart()

        [TestMethod]
        public void Adding_Game_To_Cart_Screed()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { Id = 1, Name = "Game 1", Category = "Kat1" }
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object);
            // Act
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");
            // Assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        } // end Adding_Game_To_Cart_Screed()

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // Arrange
            Cart cart = new Cart();
            CartController target = new CartController(null);
            // Act
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
            // Assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        } // end Can_View_Cart_Contents()

    } // end class

} // end namespace
