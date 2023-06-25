using Core.Entities;

namespace Core.Interfaces
{
    internal interface ICustomerService
    {
        IQueryable<Customer> GetCustomersAndOrders();
    }
}
