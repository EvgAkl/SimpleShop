using System.Data.Entity;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public virtual DbSet<Game> Games { get; set; }

    } // end class

} // end namespase
