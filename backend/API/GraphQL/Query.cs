using Core.Entities;
using Infrastructure.Data;

namespace API.GraphQL
{
    public class Query
    {

        [UseFiltering]  // can use one endpoint and filter the way we want with this!
        public IQueryable<Customer> GetCustomers([Service] OMAContext context )  //dependency injetion of service
        {
            context.Database.EnsureCreated();
            return context.Customers;
        }

        [UseFiltering]
        public IQueryable<Order> GetOrders([Service] OMAContext context)  //dependency injetion of service
        {
            context.Database.EnsureCreated();
            return context.Orders;
        }

    }
}
