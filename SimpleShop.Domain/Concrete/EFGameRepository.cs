using System.Linq;
using System.Collections.Generic;
using SimpleShop.Domain.Entities;
using SimpleShop.Domain.Abstract;


namespace SimpleShop.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }

        public void SaveGame(Game game)
        {
            if (game.Id == 0)
            {
                context.Games.Add(game);
            }
            else
            {
                Game dbEntry = context.Games.Find(game.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.Description = game.Description;
                    dbEntry.Price = game.Price;
                    dbEntry.Category = game.Category;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }
            context.SaveChanges();
        } // end SaveGame()

        public Game DeleteGame(int Id)
        {
            Game dbEntry = context.Games.Find(Id);
            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        } // end DeleteGame()

    } // end class

} //end namespace
