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
        public void AddItem(Game inGame, int inQuantity)
        {
            CartLine line = lineCollection.Where(g => g.Game.Id == inGame.Id).FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = inGame,
                    Quantity = inQuantity
                });
            }
            else
            {
                line.Quantity += inQuantity;
            }
        } // end  AddItem()

        public void RemoveLine(Game inGame)
        {
            lineCollection.RemoveAll(r => r.Game.Id == inGame.Id);
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
