using System.Collections.Generic;
using SimpleShop.Domain.Entities;

namespace SimpleShop.WebUI.Models
{
    public class GamesListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PagingInfo PagingInfo { get; set; }
    } // end class

} // end namespace