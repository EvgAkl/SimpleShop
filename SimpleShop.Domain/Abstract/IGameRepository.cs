using System.Collections.Generic;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Domain.Abstract
{
    public interface IGameRepository
    {
        IEnumerable<Game> Games { get; }
        void SaveGame(Game game);
        Game DeleteGame(int Id);
    }

} // end namespace
