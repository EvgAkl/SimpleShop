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


    } // end class

} //end namespace
