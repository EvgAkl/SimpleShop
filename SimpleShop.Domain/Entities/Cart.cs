using System.Collections.Generic;
using System.Linq;


namespace SimpleShop.Domain.Entities
{
    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    } // end class CartLine

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public void AddItem(Game game, int quantity)
        {
            CartLine line = lineCollection.Where(g => g.Game.Id == game.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        } // end  AddItem()

        public void RemoveLine(Game game)
        {
            lineCollection.RemoveAll(r => r.Game.Id == game.Id);
        } // end RemoveLine()

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(s => s.Game.Price * s.Quantity);
        } // end ComputeTotalValue()

        public void Clear()
        {
            lineCollection.Clear();
        } // end Clear()

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }

    } // end class Cart

} // end namespace
