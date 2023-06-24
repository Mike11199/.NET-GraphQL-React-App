# .NET-GraphQL-React-App

- Set up the GraphQL API using C# classes, entities (Address, Customer, Orders) and dependency injection of OMAContext.
- Added GraphQL Voyager to allow one to visualize the GraphQL API as an interactive graph via the "/graphql-voyager" endopoint.
- Can view schema and run queries in Banana Cake Pop, which is a GraphQL IDE when running in local host with 'dotnet run' in backend folder

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/2cf96c89-271b-4677-84ae-06975092bdff)

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/ac719aef-3c07-4ed2-ba0d-c792cffd8c91)


```cs
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
```




