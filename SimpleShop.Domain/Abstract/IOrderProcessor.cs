using SimpleShop.Domain.Entities;

namespace SimpleShop.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShipingDetails shippingDetails);
    }

} // end namespace
