using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.GraphQL
{
    public class Query
    {

        [UseFiltering]  // can use one endpoint and filter the way we want with this!
        public IQueryable<Customer> GetCustomers([Service] OMAContext context )  //dependency injection of service
        {
            context.Database.EnsureCreated();
            return context.Customers
                .Include(o => o.Orders)    //this so we can also query orders when querying customers in graphql
                .Include(a => a.Address);
        }

        [UseFiltering]
        public IQueryable<Order> GetOrders([Service] OMAContext context)  //dependency injection of service
        {
            context.Database.EnsureCreated();
            return context.Orders
                .Include(c => c.Customer);
        }

    }
}
