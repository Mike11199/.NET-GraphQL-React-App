using Core.Entities;

namespace Core.Interfaces
{
    internal interface IOrderService
    {
        IQueryable<Order> GetOrders();
    }
}
