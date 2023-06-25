# Frontend - TypeScript

- Type "yarn start" to run.
- Using Vite as much faster than create-react-app (written in Go with hot module replacement as oppossed to webpack), and yarn (faster than npm).  Using typescript to mirror internship tech stack.

<img src="https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/02f1ba06-b6cf-4d8a-8a20-af80e9f7ce42" alt="image" style="width: 50%; height: auto;">

<br/>
<br/>
<br/>

- Added graphql codegen to generate code from graphql schema using "graphql-codegen init" to generate schema.ts file in frontend/graphql/generated folder.
  - https://the-guild.dev/graphql/codegen/plugins/typescript/typescript-react-apollo#usage-example

<br/>
 
```js

export function useGetCustomersQuery(baseOptions?: Apollo.QueryHookOptions<GetCustomersQuery, GetCustomersQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetCustomersQuery, GetCustomersQueryVariables>(GetCustomersDocument, options);
      }
export function useGetCustomersLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetCustomersQuery, GetCustomersQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetCustomersQuery, GetCustomersQueryVariables>(GetCustomersDocument, options);
        }
export type GetCustomersQueryHookResult = ReturnType<typeof useGetCustomersQuery>;
export type GetCustomersLazyQueryHookResult = ReturnType<typeof useGetCustomersLazyQuery>;
export type GetCustomersQueryResult = Apollo.QueryResult<GetCustomersQuery, GetCustomersQueryVariables>;

```

    


# Backend - .NET-GraphQL-React-App

- Set up the GraphQL API using C# classes, entities (Address, Customer, Orders) and dependency injection of OMAContext.
- Added a C# interface, which is an abstract class that can contain properties and methods, but not fields/variables.
- Added GraphQL Voyager to allow one to visualize the GraphQL API as an interactive graph via the "/graphql-voyager" endopoint.
- Can view schema and run queries in Banana Cake Pop, which is a GraphQL IDE when running in local host with 'dotnet run' in backend folder

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/2cf96c89-271b-4677-84ae-06975092bdff)

<br/>
<br/>

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/06c323e3-2c90-42f9-8d5f-79c1cb8c7b77)

<br/>
<br/>

![image](https://github.com/Mike11199/.NET-GraphQL-React-App/assets/91037796/ac719aef-3c07-4ed2-ba0d-c792cffd8c91)


# #Backend -C# Query and Service

-Query in C# that implements GraphQL 

```cs
using Core.Entities;
using Core.Interfaces;

namespace API.GraphQL
{
    public class Query
    {

        [UseFiltering]  // can use one endpoint and pass filters to it with this
        public IQueryable<Customer> GetCustomers([Service] ICustomerService customerService )  
        {
            return customerService.GetCustomersAndOrders(); 
        }

        [UseFiltering]
        public IQueryable<Order> GetOrders([Service] IOrderService orderService)  
        {
            return orderService.GetOrders();
        }

    }
}

```

<br/>
<br/>

-Service in C# that is used by GraphQL Query

```cs
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    { 

        private readonly IDbContextFactory<OMAContext> _contextFactory;

        public OrderService(IDbContextFactory<OMAContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IQueryable<Order> GetOrders()
        {
            var context = _contextFactory.CreateDbContext();
            context.Database.EnsureCreated();

            return context.Orders
                .Where(o => !o.IsDeleted)
                .Include(o => o.Customer);
        }
    }
}
```



