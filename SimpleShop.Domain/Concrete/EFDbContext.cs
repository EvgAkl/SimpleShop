using System.Data.Entity;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

    } // end class

} // end namespase
